using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;
using projeto_blog.Models;

namespace projeto_blog.Repositories;

public class RoleRepository
{
  private readonly SqlConnection _connection;

  public RoleRepository(SqlConnection connection)
  {
    _connection = connection;
  }

  public IEnumerable<Role> GetAll()
    => _connection.GetAll<Role>();
  
  public Role Get(int id)
    => _connection.Get<Role>(id);

  public void Create(Role newRole)
    => _connection.Insert<Role>(newRole);

}
