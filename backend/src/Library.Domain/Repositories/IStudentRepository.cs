using Library.Domain.Entities;

namespace Library.Domain.Repositories;

public interface IStudentRepository : IBaseRepository<Student>
{
    Task<IEnumerable<Student>> GetAllStudentsWithCourseAsync();

    Task<Student> GetStudentRegisteredByEmailAsync(string studentEmail);
}
