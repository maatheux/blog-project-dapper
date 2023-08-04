using Dapper.Contrib.Extensions;

namespace projeto_blog.Models;

[Table("UserRole")]
public class UserRole
{
  public int Id { get; set;}
  public int UserId { get; set;}
  public int RoleId { get; set;}
}
