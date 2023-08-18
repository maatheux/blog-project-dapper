using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;

namespace projeto_blog.Repositories;

public class Repository<T> where T : class // usamos generics pra quando instanciarmos a classe possamos definir de que tipo ele é (User, Role, Tag, ...) / e clausula where é para dizer que o T tem que ser uma classe
{
  private readonly SqlConnection _connection;

  public Repository(SqlConnection connection)
    => _connection = connection;

  public IEnumerable<T> Get()
    => _connection.GetAll<T>();

  public T Get(int id)
  => _connection.Get<T>(id);

  public void Create(T model)
  {
    _connection.Insert<T>(model);
  }
    
  public void Update(T model)
    => _connection.Update<T>(model);

  public void Delete(T model)
  {
    _connection.Delete<T>(model);
  }

  public void Delete(int id)
  {
    if (id != 0)
    {
      var model = _connection.Get<T>(id);
      _connection.Delete<T>(model);
    }
  }
}
