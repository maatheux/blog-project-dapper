using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;
using projeto_blog.Models;

namespace projeto_blog.Repositories;

public class UserRepository
{
  private readonly SqlConnection _connection;

  public UserRepository(SqlConnection connection)
  {
    _connection = connection;
  }

  public IEnumerable<User> GetAll()
    => _connection.GetAll<User>();

  public User Get(int id)
    => _connection.Get<User>(id);

  public void Create(User newUser)
    => _connection.Insert<User>(newUser); // expression body

}
