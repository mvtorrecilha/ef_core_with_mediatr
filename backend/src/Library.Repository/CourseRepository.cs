using Library.Core.Interfaces.Repositories;
using Library.Core.Models;
using Library.Repository.Context;

namespace Library.Repository;

public class CourseRepository : BaseRepository<Course>, ICourseRepository
{
    public CourseRepository(LibraryContext context) : base(context)
    {
    }
}
