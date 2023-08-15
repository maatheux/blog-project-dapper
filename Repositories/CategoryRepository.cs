using Dapper;
using Microsoft.Data.SqlClient;
using projeto_blog.Models;

namespace projeto_blog.Repositories;

public class CategoryRepository : Repository<Category>
{
  private readonly SqlConnection _connection;

  public CategoryRepository(SqlConnection connection) : base(connection) => _connection = connection;

  public List<Category> ListCategoryWithPosts()
  {
    var query = @"
      SELECT
        C.*,
        P.*
      FROM dbo.Category C
      LEFT JOIN dbo.Post P ON P.CategoryId = C.Id

    ";

    List<Category> categories = new List<Category>();

    var result = _connection.Query<Category, Post, Category>(query, (category, post) => 
    {
      var ctgr = categories.FirstOrDefault(c => c.Id == category.Id);
      if (ctgr == null)
      {
        ctgr = category;
        if (post != null)
          ctgr.Posts.Add(post);
        categories.Add(ctgr);
      }
      else
        if (post != null)
          ctgr.Posts.Add(post);

      return category;
    }, splitOn: "Id");

    return categories;
  }


}
