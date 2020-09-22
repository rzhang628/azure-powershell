﻿// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Microsoft.Azure.Commands.Common.Authentication.Abstractions;
using Microsoft.Identity.Client;
using Microsoft.Identity.Client.Extensions.Msal;

namespace Microsoft.Azure.Commands.Common.Authentication.Authentication.TokenCache
{
    public class AdalTokenMigrator
    {
        protected const string PowerShellClientId = "1950a258-227b-4e31-a9cf-717495945fc2";
        private byte[] AdalToken { get; set; }

        public AdalTokenMigrator(byte[] adalToken)
        {
            AdalToken = adalToken;
        }

        public void MigrateFromAdalToMsal()
        {
            var builder = PublicClientApplicationBuilder.Create(PowerShellClientId);

            var clientApplication = builder.Build();
            var cacheHelper = CreateCacheHelper(PowerShellClientId);
            cacheHelper.RegisterCache(clientApplication.UserTokenCache);
            clientApplication.UserTokenCache.SetBeforeAccess((TokenCacheNotificationArgs args) =>
            {
                if (AdalToken != null)
                {
                    try
                    {
                        args.TokenCache.DeserializeAdalV3(AdalToken);
                    }
                    catch (Exception)
                    {
                        //TODO: 
                    }
                    finally
                    {
                        AdalToken = null;
                    }
                }
            });


            var accounts = clientApplication.GetAccountsAsync().ConfigureAwait(false).GetAwaiter().GetResult();
            foreach (var account in accounts)
            {
                try
                {
                    var accountEnvironment = string.Format("https://{0}/", account.Environment);
                    var environment = AzureEnvironment.PublicEnvironments.Values.Where(e => e.ActiveDirectoryAuthority == accountEnvironment).FirstOrDefault();
                    if (environment == null)
                    {
                        // We cannot map the previous environment to one of the public environments
                        continue;
                    }

                    var scopes = new string[] { string.Format("{0}{1}", environment.ActiveDirectoryServiceEndpointResourceId, ".default") };
                    var token = clientApplication.AcquireTokenSilent(scopes, account).ExecuteAsync().ConfigureAwait(false).GetAwaiter().GetResult();
                    //TODO: Set HomeAccountId for migration
                }
                catch
                {
                    // Continue if we're unable to get the token for the current account
                    continue;
                }
            }
            cacheHelper.UnregisterCache(clientApplication.UserTokenCache);
        }

        private static MsalCacheHelper CreateCacheHelper(string clientId)
        {
            return MsalCacheHelperProvider.GetCacheHelper();
        }
    }
}
