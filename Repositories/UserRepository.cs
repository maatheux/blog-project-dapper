using Dapper;
using Microsoft.Data.SqlClient;
using projeto_blog.Models;
using projeto_blog.Repositories;

namespace projeto_blog.Repositories;

public class UserRepository : Repository<User>
{
  private readonly SqlConnection _connection;

  public UserRepository(SqlConnection connection) : base(connection) => _connection = connection; // base passa os params pra classe mae

  public List<User> GetWithRoles()
  {
    var query = @"
      SELECT
          [User].*,
          [Role].*
      FROM
          [User]
          LEFT JOIN [UserRole] ON [UserRole].[UserId] = [User].[Id]
          LEFT JOIN [Role] ON [UserRole].[RoleId] = [Role].[Id]
    ";

    List<User> users = new List<User>();

    var result = _connection.Query<User, Role, User>(query, (user, role) =>
    {
      var usr = users.FirstOrDefault(x => x.Id == user.Id);
      if (usr == null)
      {
        usr = user;
        usr.Roles.Add(role);
        users.Add(usr);
      }
      else
        usr.Roles.Add(role);

      return user;
    }, splitOn: "Id");

    return users;
  }
  
}
