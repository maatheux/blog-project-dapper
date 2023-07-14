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
}
