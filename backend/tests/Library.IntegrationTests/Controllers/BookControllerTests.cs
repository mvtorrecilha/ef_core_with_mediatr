using FluentAssertions;
using Library.Api;
using Library.Api.ViewModels;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using Xunit;

namespace Library.IntegrationTests.Controllers;

public class BookControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public BookControllerTests(CustomWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }

    [Fact]
    public async Task Get_GetNotLentBooks_ReturnsNotLentBooks()
    {
        //Act
        var httpResponse = await _client.GetAsync("/api/books");
        httpResponse.EnsureSuccessStatusCode();

        var stringResponse = await httpResponse.Content.ReadAsStringAsync();
        var books = JsonConvert.DeserializeObject<IEnumerable<BookViewModel>>(stringResponse);

        //Assert
        books.Should().NotBeNull();
        books.Should().HaveCount(3, "Because are all not lent book.");
    }

    [Theory]
    [InlineData("3031d727-7fb5-47fa-a6d5-6e5cfeceff44", "student_ff@domain.com", HttpStatusCode.NotFound, "Because student was not found with the e-mail provided.")]
    [InlineData("3031d727-7fb5-47fa-a6d5-6e5cfeceff44", "student_two@domain.com", HttpStatusCode.Forbidden, "Because the book does not belong to the course category.")]
    [InlineData("5031d727-7fb5-47fa-a6d5-6e5cfeceff44", "student_one@domain.com", HttpStatusCode.Forbidden, "Because the book is already!")]
    [InlineData("4031d727-7fb5-47fa-a6d5-6e5cfeceff44", "student_four@domain.com", HttpStatusCode.NoContent, "Successfully borrowed book.")]
    public async Task Post_BorrowABook(string bookId, string studentEmail, HttpStatusCode expectedStatusCode, string message)
    {
        //Act
        var response = await _client.PostAsync("/api/books/" + bookId + "/student/" + studentEmail + "/borrow",
            null);

        //Assert
        response.StatusCode.Should().Be(expectedStatusCode, message);

    }
}
