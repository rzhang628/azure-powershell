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

using Microsoft.Azure.Commands.ScenarioTest;
using Microsoft.Azure.ServiceManagemenet.Common.Models;
using Microsoft.WindowsAzure.Commands.ScenarioTest;
using Xunit;

namespace Microsoft.Azure.Commands.PolicyInsights.Test.ScenarioTests
{
    public class PolicyEventTests
    {
        private readonly XunitTracingInterceptor _logger;

        public PolicyEventTests(Xunit.Abstractions.ITestOutputHelper output)
        {
            _logger = new XunitTracingInterceptor(output);
            XunitTracingInterceptor.AddToContext(_logger);
            TestExecutionHelpers.SetUpSessionAndProfile();
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void Get_AzureRmPolicyEvent_ManagementGroupScope()
        {
            TestController.NewInstance.RunPowerShellTest(_logger, "Get-AzureRmPolicyEvent-ManagementGroupScope");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void Get_AzureRmPolicyEvent_SubscriptionScope()
        {
            TestController.NewInstance.RunPowerShellTest(_logger, "Get-AzureRmPolicyEvent-SubscriptionScope");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void Get_AzureRmPolicyEvent_ResourceGroupScope()
        {
            TestController.NewInstance.RunPowerShellTest(_logger, "Get-AzureRmPolicyEvent-ResourceGroupScope");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void Get_AzureRmPolicyEvent_ResourceScope()
        {
            TestController.NewInstance.RunPowerShellTest(_logger, "Get-AzureRmPolicyEvent-ResourceScope");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void Get_AzureRmPolicyEvent_PolicySetDefinitionScope()
        {
            TestController.NewInstance.RunPowerShellTest(_logger, "Get-AzureRmPolicyEvent-PolicySetDefinitionScope");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void Get_AzureRmPolicyEvent_PolicyDefinitionScope()
        {
            TestController.NewInstance.RunPowerShellTest(_logger, "Get-AzureRmPolicyEvent-PolicyDefinitionScope");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void Get_AzureRmPolicyEvent_SubscriptionLevelPolicyAssignmentScope()
        {
            TestController.NewInstance.RunPowerShellTest(_logger, "Get-AzureRmPolicyEvent-SubscriptionLevelPolicyAssignmentScope");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void Get_AzureRmPolicyEvent_ResourceGroupLevelPolicyAssignmentScope()
        {
            TestController.NewInstance.RunPowerShellTest(_logger, "Get-AzureRmPolicyEvent-ResourceGroupLevelPolicyAssignmentScope");
        }
    }
}
