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

        // ReadUsers(connection);

        ReadRoles(connection);

        connection.Close();
    }

    public static void ReadUsers(SqlConnection connection)
    {
        var repository = new UserRepository(connection);
        IEnumerable<User> users = repository.GetAll();

        foreach (User user in users)
            Console.WriteLine(user.Name);
    }

    public static void ReadRoles(SqlConnection connection)
    {
        var repository = new RoleRepository(connection);
        IEnumerable<Role> roles = repository.GetAll();

        foreach (Role role in roles)
        {
            Console.WriteLine(role.Name);
        }
    }

    
}
