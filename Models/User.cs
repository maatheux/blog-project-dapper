using Dapper.Contrib.Extensions;

namespace projeto_blog.Models;

[Table("[User]")] // por padrao o dapper tenta buscar pela tabela com nome no plural da classe, para evitarmos isso usamos o [Table()]
public class User
{
  public User() => this.Roles = new List<Role>();

  public int Id { get; set; }
  public string Name { get; set; }
  public string Email { get; set; }
  public string PasswordHash { get; set; }
  public string Bio { get; set; }
  public string Image { get; set; }
  public string Slug { get; set; }
  
  [Write(false)] // usamos o Write(false) para nao incluir os Roles no insert
  public List<Role> Roles { get; set; }
}