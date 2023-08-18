using Dapper;
using Microsoft.Data.SqlClient;
using projeto_blog.Models;

namespace projeto_blog.Repositories;

public class TagRepository : Repository<Tag>
{
  private readonly SqlConnection _connection;

  public TagRepository(SqlConnection connection) : base(connection) => _connection = connection;

  public List<Tag> GetTagsWithPosts()
  {
    var query = @"
      SELECT 
        [Tag].*,
        [Post].*
      FROM [dbo].[Tag] Tag
      LEFT JOIN [dbo].[PostTag] PostTag ON PostTag.TagId = Tag.Id
      LEFT JOIN [dbo].[Post] Post on Post.Id = PostTag.PostId
    ";

    List<Tag> tags = new List<Tag>();

    var result = _connection.Query<Tag, Post, Tag>(query, (tag, post) =>
    {
      Tag tg = tags.FirstOrDefault(t => t.Id == tag.Id);
      if (tg == null)
      {
        tg = tag;
        if (post != null)        
          tg.Posts.Add(post);
        
        tags.Add(tg);
      }
      else
        if (post != null)
          tg.Posts.Add(post);
      
      return tag;
    }, splitOn: "Id");

    return tags;
  }

}
