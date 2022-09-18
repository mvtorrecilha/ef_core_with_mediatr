using Library.Core.Interfaces.Repositories;
using Library.Core.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Library.UnitTests.Mocks.Repositories;

public class MockStudentRepository : Mock<IStudentRepository>
{
    public MockStudentRepository() : base(MockBehavior.Strict) { }

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

    public MockStudentRepository MockGetAllWithIncludes1(Expression<Func<Student, object>>[] includes, IEnumerable<Student> output)
    {
        Setup(s => s.GetAllWithIncludes(includes)).ReturnsAsync(output);

        return this;
    }

    public MockStudentRepository MockGetAllWithIncludes(IEnumerable<Student> output)
    {
        Setup(s => s.GetAllWithIncludes(It.IsAny<Expression<Func<Student, object>>>())).ReturnsAsync(output);

        return this;
    }
}
