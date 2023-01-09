using Application.Entities.Customers.Commands.CreateCustomerCommand;
using Application.Entities.Customers.Commands.DeleteCustomerById;
using Application.Entities.Customers.Queries.GetCustomerById;
using Ductus.FluentDocker.Commands;
using FluentAssertions;
using TechTalk.SpecFlow.Assist;
using WebApi.Client.Exceptions;
using WebApi.Client.HttpClients;

namespace AcceptanceTests.Steps;

[Binding]
public class ManageCustomer
{
    readonly CustomerHttpClient _client;
    readonly ScenarioContext _scenarioContext;

    public ManageCustomer(CustomerHttpClient client, ScenarioContext scenarioContext)
    {
        _client = client;
        _scenarioContext = scenarioContext;
    }

    [When(@"I create customers with the following details")]
    public async Task WhenICreateCustomersWithTheFollowingDetails(Table table)
    {
        var commands = table.CreateSet<CreateCustomerCommand>();
        var createdCustomers = new List<(int, CreateCustomerCommand)>();

        foreach (var command in commands)
        {
            var res = await _client.CreateAsync(command);
            createdCustomers.Add((res.Data, command));
        }

        _scenarioContext.Add("CreatedCustomers", createdCustomers);
    }

    [Then(@"the customers are created successfully")]
    public async Task ThenTheCustomersAreCreatedSuccessfully()
    {
        var createdCustomers = _scenarioContext.Get<List<(int, CreateCustomerCommand)>>("CreatedCustomers");

        foreach (var item in createdCustomers)
        {
            var customer = await _client.GetCustomerByIdAsync(new GetCustomerById() { Id = item.Item1 });
            customer.Data.Should().BeEquivalentTo(item.Item2);
        }
    }



    [Given(@"Following customers created")]
    public async Task GivenFollowingCustomersCreated(Table table)
    {
        var commands = table.CreateSet<CreateCustomerCommand>();
        var createdCustomers = new List<int>();

        foreach (var command in commands)
        {
            var res = await _client.CreateAsync(command);
            createdCustomers.Add(res.Data);
        }

        _scenarioContext.Add("CreatedCustomersId", createdCustomers);
    }

    [When(@"Created customers in previous step get deleted")]
    public async Task WhenCreatedCustomersInPreviousStepGetDeleted()
    {
        var createdCustomers = _scenarioContext.Get<List<int>>("CreatedCustomersId");

        foreach (var item in createdCustomers)
        {
            var res = await _client.DeleteCustomerByIdAsync(new DeleteCustomerById() { Id = item });
            res.Data.Should().Be(true);
        }
    }

    [Then(@"Customers are deleted successfully")]
    public async Task ThenCustomersAreDeletedSuccessfully()
    {
        var createdCustomers = _scenarioContext.Get<List<int>>("CreatedCustomersId");

        foreach (var item in createdCustomers)
        {
            Func<Task> operation = () => _client.GetCustomerByIdAsync(new GetCustomerById() { Id = item });

            await operation.Should().ThrowAsync<ApiNotFoundException>();
        }
    }
}
