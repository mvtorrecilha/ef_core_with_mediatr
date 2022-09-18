using FluentAssertions;
using Library.Api.Controllers;
using Library.Api.ViewModels;
using Library.Core.Models;
using Library.UnitTests.Mocks;
using Library.UnitTests.Mocks.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Library.UnitTests.Controllers
{
    public class StudentControllerTests
    {
        protected MockUnitOfWork _mockUnitOfWork;
        protected MockStudentRepository _mockStudentRepository;

        public StudentControllerTests()
        {
            _mockStudentRepository = new MockStudentRepository();
            _mockUnitOfWork = new MockUnitOfWork();
        }

        private StudentController GetController()
        {
            return new StudentController(
                _mockUnitOfWork.Object);
        }

        [Fact]
        public async Task GetStudents_ReturnAllStudents()
        {
            //Arrange
            var students = new List<Student>
            {
                new Student
                {
                    Id = Guid.NewGuid(),
                    Name = "Student One",
                    Email = "student_one@domain.com",
                    Course = new Course
                    {
                        Id = Guid.NewGuid(),
                        Name = "Systems Analysis"
                    }
                },
                new Student
                {
                    Id = new Guid(),
                    Name = "Student Two",
                    Email = "student_two@domain.com",
                    Course = new Course
                    {
                        Id = Guid.NewGuid(),
                        Name = "Civil Engineering"
                    }
                },
             };

            _mockStudentRepository.MockGetAllWithIncludes(students);
            _mockUnitOfWork.MockStudents(_mockStudentRepository);

            // Act
            var controller = GetController();
            var result = await controller.Get();

            // Assert
            var expectedStudents = new OkObjectResult(students.Select(s => new StudentViewModel
            {
                Name = s.Name,
                Email = s.Email,
                Course = s.Course.Name
            }));

            result.Should().BeEquivalentTo(expectedStudents);
        }
    }
}
