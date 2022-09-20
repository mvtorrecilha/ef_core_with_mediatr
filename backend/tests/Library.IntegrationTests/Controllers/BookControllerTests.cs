using FluentAssertions;
using Library.Api;
using Library.Api.ViewModels;
using Newtonsoft.Json;
using System.Net;
using Xunit;

namespace Library.IntegrationTests.Controllers;

public class BookControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    public CustomWebApplicationFactory<Program> _factory;

    public BookControllerTests(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
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

    [Fact]
    public async Task Post_BorrowABookWithEmptyEmail_ReturnsStatusCodeBadRequest()
    {
        //Arrange
        var studentEmail = " ";
        var bookId = Guid.NewGuid();

        //Act
        var response = await _client.PostAsync("/api/books/"+bookId+ "/student/" + studentEmail + "/borrow",
            null);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest, "because the e-mail provided was empty.");
    }

    [Fact]
    public async Task Post_BorrowABookWithNotFoundStudent_ReturnsStatusCodeNotFound()
    {
        //Arrange
        var studentEmail = "student_ff@domain.com";
        var bookId = Guid.NewGuid();

        //Act
        var response = await _client.PostAsync("/api/books/" + bookId + "/student/" + studentEmail + "/borrow",
            null);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound, "Because student was not found with the e-mail provided.");
    }

    [Fact]
    public async Task Post_BorrowABookWithABookThatNotBelongToTheCourseCategory_ReturnsStatusCodeForbidden()
    {
        //Arrange
        var studentEmail = "student_two@domain.com";
        var bookId = Guid.Parse("3031d727-7fb5-47fa-a6d5-6e5cfeceff44");

        //Act
        var response = await _client.PostAsync("/api/books/" + bookId + "/student/" + studentEmail + "/borrow",
            null);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden, "Because the book does not belong to the course category.");
    }

    [Fact]
    public async Task Post_BorrowABookWithABookIsAlreadyLent_ReturnsStatusCodeForbidden()
    {
        //Arrange
        var studentEmail = "student_one@domain.com";
        var bookId = Guid.Parse("5031d727-7fb5-47fa-a6d5-6e5cfeceff44");

        //Act
        var response = await _client.PostAsync("/api/books/" + bookId + "/student/" + studentEmail + "/borrow",
            null);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden, "Because the book is already!");
    }

    [Fact]
    public async Task Post_BorrowABookWithValidRequest_ReturnsStatusCodeNoContent()
    {
        //Arrange
        var studentEmail = "student_four@domain.com";
        var bookId = Guid.Parse("4031d727-7fb5-47fa-a6d5-6e5cfeceff44");

        //Act
        var response = await _client.PostAsync("/api/books/" + bookId + "/student/" + studentEmail + "/borrow",
            null);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent, "Successfully borrowed book.");
    }
}
