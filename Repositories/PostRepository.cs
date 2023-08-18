using Dapper;
using Microsoft.Data.SqlClient;
using projeto_blog.Repositories;

namespace projeto_blog.Models;

public class PostRepository : Repository<Post>
{
  private readonly SqlConnection _connection;

  public PostRepository(SqlConnection connection) : base(connection) => _connection = connection;

  public List<Post> GetPostsWithTags()
  {
    var query = @"
      SELECT 
        [Post].*,
        [Tag].*
      FROM [dbo].[Post] Post
      LEFT JOIN [dbo].[PostTag] PostTag ON PostTag.PostId = Post.Id
      LEFT JOIN [dbo].[Tag] Tag on Tag.Id = PostTag.TagId
    ";

    List<Post> posts = new List<Post>();

    var result = _connection.Query<Post, Tag, Post>(query, (post, tag) =>
    {
      Post pst = posts.FirstOrDefault(p => p.Id == post.Id);
      if (pst == null)
      {
        pst = post;
        if (post != null)        
          pst.Tags.Add(tag);
        
        posts.Add(pst);
      }
      else
        if (post != null)
          pst.Tags.Add(tag);
      
      return post;
    }, splitOn: "Id");

    return posts;
  }


}
