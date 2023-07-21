using Microsoft.Data.SqlClient;
using projeto_blog.Models;
using projeto_blog.Repositories;

namespace projeto_blog.Repositories;

public class UserRepository : Repository<User>
{
  private readonly SqlConnection _connection;

  public UserRepository(SqlConnection connection) : base(connection) => _connection = connection; // base passa os params pra classe mae
}
