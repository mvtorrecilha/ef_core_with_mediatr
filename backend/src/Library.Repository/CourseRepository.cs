using Library.Domain.Entities;
using Library.Domain.Repositories;
using Library.Repository.Context;

namespace Library.Repository;

public class CourseRepository : BaseRepository<Course>, ICourseRepository
{
    public CourseRepository(LibraryContext context) : base(context)
    {
    }
}
