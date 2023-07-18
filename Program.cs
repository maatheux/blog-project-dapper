using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;
using projeto_blog.Models;
using projeto_blog.Repositories;

namespace projeto_blog;

class Program
{
    private const string CONNECTION_STRING = "Server=localhost,1433;Initial Catalog=Blog;User ID=sa;Password=1q2w3e4r@#$;TrustServerCertificate=true;";

    static void Main(string[] args)
    {
        var connection = new SqlConnection(CONNECTION_STRING);

        connection.Open();

        User myNewUser = new User() {
            Id = 3,
            Name = "Testando update",
            Bio = "Testando bio",
            Email = "teste@email",
            Image = "utlimage.com",
            PasswordHash = "hash",
            Slug = "teste-user",
        };
        // CreateUser(connection, myNewUser);
        // UpdateUser(connection, myNewUser);
        // DeleteUser(connection, myNewUser);

        ReadUsers(connection);

        // ReadRoles(connection);

        connection.Close();
    }

    public static void ReadUsers(SqlConnection connection)
    {
        var repository = new Repository<User>(connection);
        IEnumerable<User> users = repository.Get();

        foreach (User user in users)
        {
            Console.WriteLine(user.Name);
            
            foreach (Role role in user.Roles)
            {
                Console.WriteLine($" - {role.Name}");
            }
        }
            
    }

    public static void ReadRoles(SqlConnection connection)
    {
        var repository = new Repository<Role>(connection);
        IEnumerable<Role> roles = repository.Get();

        foreach (Role role in roles)
        {
            Console.WriteLine(role.Name);
        }
    }

    public static void ReadTags(SqlConnection connection)
    {
        var repository = new Repository<Tag>(connection);
        IEnumerable<Tag> tags = repository.Get();

        foreach (Tag tag in tags)
        {
            Console.WriteLine(tag.Name);
        }
    }

    // Fazer o delete, update e create

    public static void CreateUser(SqlConnection connection, User user)
    {
        var repository = new Repository<User>(connection);

        repository.Create(user);
    }

    public static void UpdateUser(SqlConnection connection, User user)
    {
        var repository = new Repository<User>(connection);

        repository.Update(user);
    }

    public static void DeleteUser(SqlConnection connection, User user)
    {
        var repository = new Repository<User>(connection);

        repository.Delete(user);
    }

    
}
