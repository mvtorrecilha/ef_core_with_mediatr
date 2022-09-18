using Library.Core.Interfaces.Repositories;
using Library.Core.Models;
using Moq;
using System.Collections.Generic;

namespace Library.UnitTests.Mocks.Repositories;

public class MockStudentRepository : Mock<IStudentRepository>
{
    public MockStudentRepository() : base(MockBehavior.Strict) { }

    public MockStudentRepository MockGetAllStudentsWithCourseAsync(IEnumerable<Student> output)
    {
        Setup(s => s.GetAllStudentsWithCourseAsync()).ReturnsAsync(output);

        return this;
    }

    public MockStudentRepository MockGetStudentRegisteredByEmailAsync(string studentEmail, Student output)
    {
        Setup(s => s.GetStudentRegisteredByEmailAsync(studentEmail)).ReturnsAsync(output);

        return this;
    }

    public MockStudentRepository VerifyGetStudentRegisteredByEmailAsync(string studentEmail, Times times)
    {
        Verify(s => s.GetStudentRegisteredByEmailAsync(studentEmail), times);

        return this;
    }
}
