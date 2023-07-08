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
  {
    newRole.Id = 0;
    _connection.Insert<Role>(newRole);
  }
    
  public void Update(Role role)
  {
    if (role.Id != 0)
      _connection.Update<Role>(role);
  }

  public void Delete(Role role)
  {
    if (role.Id != 0)
      _connection.Delete<Role>(role);
  }

  public void Delete(int id)
  {
    if (id != 0)
    {
      var role = _connection.Get<Role>(id);
      _connection.Delete<Role>(role);
    }
  }

}
