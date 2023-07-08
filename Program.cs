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

        ReadUsers(connection);
        // ReadUser();
        // CreateUser();
        // UpdateUser();
        // DeleteUser();

        connection.Close();
    }

    public static void ReadUsers(SqlConnection connection)
    {
        var repository = new UserRepository(connection);
        IEnumerable<User> users = repository.GetAll();

        foreach (User user in users)
            Console.WriteLine(user.Name);
    }

    public static void ReadUser()
    {
        using (var connection = new SqlConnection(CONNECTION_STRING))
        {
            var user = connection.Get<User>(1);

            Console.WriteLine(user.Name);
        }
    }

    public static void CreateUser()
    {
        using (var connection = new SqlConnection(CONNECTION_STRING))
        {
            User newUser = new User() {
                Name = "Matheus",
                Bio = "Hello World",
                Email = "matheus@balta.io",
                Image = "matheus-png",
                PasswordHash = "HASH",
                Slug = "/matheus-lima"
            };

            connection.Insert<User>(newUser);

            Console.WriteLine($"User cadastrado!");
        }
    }

    public static void UpdateUser()
    {
        using (var connection = new SqlConnection(CONNECTION_STRING))
        {
            User newUser = new User() {
                Id = 2,
                Name = "Matheus Lima",
                Bio = "Hello World",
                Email = "matheus@balta.io",
                Image = "matheus-png",
                PasswordHash = "HASH",
                Slug = "/matheus-lima"
            };

            connection.Update<User>(newUser);

            Console.WriteLine($"User atualizado!");
        }
    }

    public static void DeleteUser()
    {
        using (var connection = new SqlConnection(CONNECTION_STRING))
        {
            var user = connection.Get<User>(2);

            connection.Delete<User>(user);

            Console.WriteLine($"User deletado!");
        }
    }

    
}
