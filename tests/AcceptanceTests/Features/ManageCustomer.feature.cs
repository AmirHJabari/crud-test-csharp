﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (https://www.specflow.org/).
//      SpecFlow Version:3.9.0.0
//      SpecFlow Generator Version:3.9.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace AcceptanceTests.Features
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public partial class ManageCustomerInTheSystemFeature : object, Xunit.IClassFixture<ManageCustomerInTheSystemFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private static string[] featureTags = ((string[])(null));
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "ManageCustomer.feature"
#line hidden
        
        public ManageCustomerInTheSystemFeature(ManageCustomerInTheSystemFeature.FixtureData fixtureData, AcceptanceTests_XUnitAssemblyFixture assemblyFixture, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Features", "Manage customer in the system", null, ProgrammingLanguage.CSharp, featureTags);
            testRunner.OnFeatureStart(featureInfo);
        }
        
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public void TestInitialize()
        {
        }
        
        public void TestTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<Xunit.Abstractions.ITestOutputHelper>(_testOutputHelper);
        }
        
        public void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        void System.IDisposable.Dispose()
        {
            this.TestTearDown();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Customers get created successfully")]
        [Xunit.TraitAttribute("FeatureTitle", "Manage customer in the system")]
        [Xunit.TraitAttribute("Description", "Customers get created successfully")]
        public void CustomersGetCreatedSuccessfully()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Customers get created successfully", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 3
 this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
                TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                            "FirstName",
                            "LastName",
                            "Email",
                            "DateOfBirth",
                            "PhoneCountryCode",
                            "PhoneNumber",
                            "BankAccountNumber"});
                table1.AddRow(new string[] {
                            "Amir H.",
                            "Jabari",
                            "test1@gmail.com",
                            "2002-12-02",
                            "98",
                            "9051877561",
                            "5422570172410822"});
                table1.AddRow(new string[] {
                            "Amir H.",
                            "Jabari",
                            "test2@gmail.com",
                            "2002-12-03",
                            "98",
                            "9051877561",
                            "5422570172410822"});
#line 4
  testRunner.When("I create customers with the following details", ((string)(null)), table1, "When ");
#line hidden
#line 8
  testRunner.Then("the customers are created successfully", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Customers get deleted successfully")]
        [Xunit.TraitAttribute("FeatureTitle", "Manage customer in the system")]
        [Xunit.TraitAttribute("Description", "Customers get deleted successfully")]
        public void CustomersGetDeletedSuccessfully()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Customers get deleted successfully", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 10
 this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
                TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                            "FirstName",
                            "LastName",
                            "Email",
                            "DateOfBirth",
                            "PhoneCountryCode",
                            "PhoneNumber",
                            "BankAccountNumber"});
                table2.AddRow(new string[] {
                            "Amir H.",
                            "Jabari",
                            "test21@gmail.com",
                            "2002-12-21",
                            "98",
                            "9051877561",
                            "5422570172410822"});
                table2.AddRow(new string[] {
                            "Amir H.",
                            "Jabari",
                            "test22@gmail.com",
                            "2002-12-23",
                            "98",
                            "9051877561",
                            "5422570172410822"});
#line 11
  testRunner.Given("Following customers created", ((string)(null)), table2, "Given ");
#line hidden
#line 15
  testRunner.When("Created customers in previous step get deleted", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 16
  testRunner.Then("Customers are deleted successfully", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Customers pagination returns correct data get")]
        [Xunit.TraitAttribute("FeatureTitle", "Manage customer in the system")]
        [Xunit.TraitAttribute("Description", "Customers pagination returns correct data get")]
        public void CustomersPaginationReturnsCorrectDataGet()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Customers pagination returns correct data get", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 18
 this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
                TechTalk.SpecFlow.Table table3 = new TechTalk.SpecFlow.Table(new string[] {
                            "FirstName",
                            "LastName",
                            "Email",
                            "DateOfBirth",
                            "PhoneCountryCode",
                            "PhoneNumber",
                            "BankAccountNumber"});
                table3.AddRow(new string[] {
                            "Amir H.",
                            "Jabari",
                            "test21@gmail.com",
                            "2002-12-21",
                            "98",
                            "9051877561",
                            "5422570172410822"});
                table3.AddRow(new string[] {
                            "Amir H.",
                            "Jabari",
                            "test22@gmail.com",
                            "2002-12-23",
                            "98",
                            "9051877561",
                            "5422570172410822"});
                table3.AddRow(new string[] {
                            "Amir H.",
                            "Jabari",
                            "test23@gmail.com",
                            "2002-12-24",
                            "98",
                            "9051877561",
                            "5422570172410822"});
                table3.AddRow(new string[] {
                            "Amir H.",
                            "Jabari",
                            "test24@gmail.com",
                            "2002-12-25",
                            "98",
                            "9051877561",
                            "5422570172410822"});
                table3.AddRow(new string[] {
                            "Amir H.",
                            "Jabari",
                            "test25@gmail.com",
                            "2002-12-26",
                            "98",
                            "9051877561",
                            "5422570172410822"});
                table3.AddRow(new string[] {
                            "Amir H.",
                            "Jabari",
                            "test26@gmail.com",
                            "2002-12-27",
                            "98",
                            "9051877561",
                            "5422570172410822"});
                table3.AddRow(new string[] {
                            "Amir H.",
                            "Jabari",
                            "test27@gmail.com",
                            "2002-12-28",
                            "98",
                            "9051877561",
                            "5422570172410822"});
                table3.AddRow(new string[] {
                            "Amir H.",
                            "Jabari",
                            "test28@gmail.com",
                            "2002-12-29",
                            "98",
                            "9051877561",
                            "5422570172410822"});
                table3.AddRow(new string[] {
                            "Amir H.",
                            "Jabari",
                            "test29@gmail.com",
                            "2002-12-30",
                            "98",
                            "9051877561",
                            "5422570172410822"});
#line 19
  testRunner.When("All following customers created", ((string)(null)), table3, "When ");
#line hidden
#line 30
  testRunner.Then("Customers are returned successfully with pagination", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
        public class FixtureData : System.IDisposable
        {
            
            public FixtureData()
            {
                ManageCustomerInTheSystemFeature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                ManageCustomerInTheSystemFeature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion
