using Library.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Core.Interfaces.Repositories;

public interface IStudentRepository : IBaseRepository<Student>
{
    Task<IEnumerable<Student>> GetAllStudentsWithCourseAsync();

    Task<Student> GetStudentRegisteredByEmailAsync(string studentEmail);
}
