using System.ComponentModel.DataAnnotations.Schema;

namespace projeto_blog.Models;

[Table("[Role]")]
public class Role
{
  public int Id { get; set; }
  public string Name { get; set; }
  public string Slug { get; set; }
}
