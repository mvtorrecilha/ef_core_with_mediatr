using Dapper.Contrib.Extensions;

namespace Library.Domain.Entities;

public class BaseEntity
{
    [ExplicitKey]
    public Guid Id { get; set; } = Guid.NewGuid();
}
