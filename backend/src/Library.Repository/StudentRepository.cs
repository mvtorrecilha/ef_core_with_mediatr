using Library.Core.Interfaces.Repositories;
using Library.Core.Models;
using Library.Repository.Context;
using System.Threading.Tasks;

namespace Library.Repository;

public class StudentRepository : BaseRepository<Student>, IStudentRepository
{
    public StudentRepository(LibraryContext context) : base(context)
    {
    }

    public Task<Student> GetStudentRegisteredByEmailAsync(string studentEmail)
    {
        return FindByAsync(s => s.Email == studentEmail);
    }
}
