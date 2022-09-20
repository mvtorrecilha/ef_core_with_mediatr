using FluentAssertions;
using Library.Api;
using Library.Api.ViewModels;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace Library.IntegrationTests.Controllers;

public class StudentControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public StudentControllerTests(CustomWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }

    [Fact]
    public async Task Get_GetStudents_ReturnsStudents()
    {
        //Act
        var httpResponse = await _client.GetAsync("/api/students");
        httpResponse.EnsureSuccessStatusCode();

        var stringResponse = await httpResponse.Content.ReadAsStringAsync();
        var students = JsonConvert.DeserializeObject<IEnumerable<StudentViewModel>>(stringResponse);

        //Assert
        students.Should().NotBeNull();
        students.Should().HaveCount(4);
    }
}
