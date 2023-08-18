using Dapper.Contrib.Extensions;

namespace projeto_blog.Models;

[Table("[PostTag]")]
public class PostTag
{
  public int PostId { get; set; }
  public int TagId { get; set; }
}
