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
        ConsoleKey key = Greetings();
        while (key != ConsoleKey.Escape)
        {
            if ((new List<ConsoleKey>() {ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.D3}).Contains(key))
            {
                var connection = new SqlConnection(CONNECTION_STRING);
                connection.Open();

                if (key == ConsoleKey.D1)
                {
                    Console.WriteLine("Cadastro...");
                    Thread.Sleep(2000);
                    Register(connection);
                    Console.WriteLine("Registro Inserido!");
                    Thread.Sleep(4000);
                }
                
                if (key == ConsoleKey.D2)
                {
                    Console.WriteLine("Vinculo");
                }

                if (key == ConsoleKey.D3)
                {
                    ListData(connection);
                }

                connection.Close();
            }

            key = Greetings();
        }
        Console.WriteLine("Saindo...");
        Thread.Sleep(3000);
        Console.Clear();
    }

    public static ConsoleKey Greetings()
    {
        Console.Clear();
        Console.WriteLine("Bem vindo ao sistema do Blog!");
        Console.WriteLine("Qual operação deseja realizar?");
        Console.WriteLine("1 - Cadastrar / 2 - Vincular / 3 - Listar / Esc - Sair");
        Console.WriteLine("-------------------------------------------------------");
        
        ConsoleKey choosenOption = Console.ReadKey().Key;
        Console.Clear();

        return choosenOption;
    }

    public static void Register(SqlConnection connection){
        Console.Clear();
        Console.WriteLine("Qual seria o tipo de registro?");
        Console.WriteLine("1 - Novo Usuário / 2 - Novo Perfil / 3 - Nova Categoria / 4 - Nova Tag / 5 - Novo Post");
        Console.WriteLine("-------------------------------------------------------");
        ConsoleKey registerType = Console.ReadKey().Key;

        Console.Clear();
        Console.WriteLine("Dados de Registro");
        
        if (registerType == ConsoleKey.D1)
            CreateUser(connection);
        
        if (registerType == ConsoleKey.D2)
            CreateRole(connection);
        
        if (registerType != ConsoleKey.D3)
            CreateCategory(connection);
        
        if (registerType != ConsoleKey.D4)
            CreateTag(connection);

            
    }

    public static void ListData(SqlConnection connection)
    {
        Console.Clear();
        Console.WriteLine("O que gostaria de listar?");
        Console.WriteLine("1 - Usuários / 2 - Perfis / 3 - Categorias / 4 - Tags / 5 - Posts");
        Console.WriteLine("-------------------------------------------------------");
        ConsoleKey typeList = Console.ReadKey().Key;

        Console.Clear();

        if (typeList == ConsoleKey.D1)
            ReadUsersWithRoles(connection);
        
        Console.ReadKey();
        Console.Clear();

    }



    // USER
    public static void ReadUsersWithRoles(SqlConnection connection)
    {
        var repository = new UserRepository(connection);
        IEnumerable<User> users = repository.GetWithRoles();

        Console.WriteLine("");
        foreach (User user in users)
        {
            Console.WriteLine($"Nome: {user.Name} / Email: {user.Email}");
            
            int index = 0;
            Console.Write("Perfil(s): ");
            foreach (Role role in user.Roles)
            {
                if (index + 1 < user.Roles.Count)
                    Console.Write($"{role.Name}, ");
                else
                    Console.Write($"{role.Name}");

                index++;
            }

            Console.WriteLine("");
            Console.WriteLine("-----------------------------------------------------");

        }
            
    }

    public static void CreateUser(SqlConnection connection)
    {
        Console.WriteLine("");
        User user = new User();
        
        Console.Write("Nome: ");
        user.Name = Console.ReadLine();
        Console.Write("Email: ");
        user.Email = Console.ReadLine();
        Console.Write("Senha Hash: ");
        user.PasswordHash = Console.ReadLine();
        Console.Write("Bio: ");
        user.Bio = Console.ReadLine();
        Console.Write("Url da Imagem: ");
        user.Image = Console.ReadLine();
        Console.Write("Url Slug: ");
        user.Slug = Console.ReadLine();
        
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


    // ROLE
    public static void ReadRoles(SqlConnection connection)
    {
        var repository = new Repository<Role>(connection);
        IEnumerable<Role> roles = repository.Get();

        foreach (Role role in roles)
        {
            Console.WriteLine(role.Name);
        }
    }

    public static void CreateRole(SqlConnection connection)
    {
        Console.WriteLine("");
        Role role = new Role();

        Console.Write("Nome: ");
        role.Name = Console.ReadLine();
        Console.Write("Url Slug: ");
        role.Slug = Console.ReadLine();
        
        var repository = new Repository<Role>(connection);
        repository.Create(role);    
    }


    // TAGS
    public static void ReadTags(SqlConnection connection)
    {
        var repository = new Repository<Tag>(connection);
        IEnumerable<Tag> tags = repository.Get();

        foreach (Tag tag in tags)
        {
            Console.WriteLine(tag.Name);
        }
    }

    public static void CreateTag(SqlConnection connection)
    {
        Console.WriteLine("");
        Tag tag = new Tag();

        Console.Write("Nome: ");
        tag.Name = Console.ReadLine();
        Console.Write("Url Slug: ");
        tag.Slug = Console.ReadLine();
        
        var repository = new Repository<Tag>(connection);
        repository.Create(tag);    
    }


    // CATEGORY
    public static void CreateCategory(SqlConnection connection)
    {
        Console.WriteLine("");
        Category category = new Category();

        Console.Write("Nome: ");
        category.Name = Console.ReadLine();
        Console.Write("Url Slug: ");
        category.Slug = Console.ReadLine();
        
        var repository = new Repository<Category>(connection);
        repository.Create(category);
    }


    // POST
    public static void CreatePost(SqlConnection connection)
    {
        Console.WriteLine("");
        Post post = new Post();

        Console.Write("Title: ");
        post.Title = Console.ReadLine();
        Console.Write("Resumo: ");
        post.Summary = Console.ReadLine();
        Console.Write("Corpo do post: ");
        post.Body = Console.ReadLine();
        Console.Write("Url Slug: ");
        post.Slug = Console.ReadLine();



    }

    
}
