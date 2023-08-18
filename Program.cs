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
                    RelateTable(connection);
                }

                if (key == ConsoleKey.D3)
                {
                    Console.WriteLine("Listando...");
                    Thread.Sleep(2000);
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
        
        if (registerType == ConsoleKey.D3)
            CreateCategory(connection);
        
        if (registerType == ConsoleKey.D4)
            CreateTag(connection);
        
        if (registerType == ConsoleKey.D5)
            CreatePost(connection);

            
    }

    public static void ListData(SqlConnection connection)
    {
        Console.Clear();
        Console.WriteLine("O que gostaria de listar?");
        Console.WriteLine("1 - Usuários / 2 - Categorias com Qtd de Posts / 3 - Tags com Qtd de Posts / 4 - Posts com sua categorias / 5 - Posts com suas tags");
        Console.WriteLine("-------------------------------------------------------");
        ConsoleKey typeList = Console.ReadKey().Key;

        Console.Clear();

        if (typeList == ConsoleKey.D1)
            ReadUsersWithRoles(connection);
        
        if (typeList == ConsoleKey.D2)
            ListCategoryWithPostsCount(connection);
        
        if (typeList == ConsoleKey.D3)
            ReadTagsWithPostsCount(connection);
        
        if (typeList == ConsoleKey.D4)
            ListPostsWithCategory(connection);
        
        if (typeList == ConsoleKey.D5)
            ListPostsWithTags(connection);
        
        Console.ReadKey();
        Console.Clear();

    }

    public static void RelateTable(SqlConnection connection)
    {
        Console.Clear();
        Console.WriteLine("Qual vínculo deseja realizar");
        Console.WriteLine("1 - Usuário-Perfil / 2 - Post-Tag");
        Console.WriteLine("-------------------------------------------------------");
        ConsoleKey typeLink = Console.ReadKey().Key;

        Console.Clear();

        if (typeLink == ConsoleKey.D1)
        {
            LinkUserRole(connection);
        }

        if (typeLink == ConsoleKey.D2)
            LinkPostTag(connection);

    }

    public static void LinkUserRole(SqlConnection connection)
    {
        User? user;
        Role? role;
        
        do
        {
            Console.WriteLine($"Escolha o usuário abaixo pelo Id");
            ReadUsersWithRoles(connection);

            string selectedUser = Console.ReadLine() ?? "";
            Console.Clear();

            user = new Repository<User>(connection).Get(int.Parse(selectedUser));

            if (user == null)
            {
                Console.WriteLine($"O Id {selectedUser} não foi encontrado! Digite novamente um Id válido...");
                Thread.Sleep(4000);
                Console.Clear();
            }       
        }
        while (user == null);

        do
        {
            Console.WriteLine($"Escolha o perfil abaixo pelo Id");
            ReadRoles(connection);
            string selectedRole = Console.ReadLine() ?? "";

            role = new Repository<Role>(connection).Get(int.Parse(selectedRole));

            if (role == null)
            {
                Console.WriteLine($"O Id {selectedRole} não foi encontrado! Digite novamente um Id válido...");
                Thread.Sleep(4000);
                Console.Clear();
            }
        }
        while (role == null);

        Console.Clear();
        Console.WriteLine("Vinculando...");
        Thread.Sleep(3000);

        UserRole linkedObject = new UserRole() 
        {
            UserId = user.Id,
            RoleId = role.Id,
        };
        
        new Repository<UserRole>(connection).Create(linkedObject);

        Console.Clear();
        Console.WriteLine("Usuário e Perfil vinculados!");
        Thread.Sleep(3000);

    }

    public static void LinkPostTag(SqlConnection connection)
    {
        Post? post;
        Tag? tag;

        do
        {
            Console.WriteLine("Escolha o post abaixo pelo Id");
            ListPosts(connection);

            string postIdSelected = Console.ReadLine() ?? "";

            post = new Repository<Post>(connection).Get(int.Parse(postIdSelected));

            if (post == null)
            {
                Console.WriteLine($"O Id {postIdSelected} não foi encontrado! Digite novamente um Id válido...");
                Thread.Sleep(4000);
                Console.Clear();
            }

        }
        while (post == null);

        do
        {
            Console.WriteLine("Escolha a tag abaixo pelo Id");
            ReadTags(connection);

            string tagIdSelected = Console.ReadLine() ?? "";

            tag = new Repository<Tag>(connection).Get(int.Parse(tagIdSelected));

            if (tag == null)
            {
                Console.WriteLine($"O Id {tagIdSelected} não foi encontrado! Digite novamente um Id válido...");
                Thread.Sleep(4000);
                Console.Clear();
            }

        }
        while (tag == null);

        var postTagRepo = new Repository<PostTag>(connection);

        PostTag newPostTag = new PostTag()
        {
            PostId = post.Id,
            TagId = tag.Id,
        };
        postTagRepo.Create(newPostTag);

        Console.Clear();
        Console.WriteLine("Post e Tag vinculados!");
        Thread.Sleep(3000);
    }

    
    
    // USER
    public static void ReadUsersWithRoles(SqlConnection connection)
    {
        var repository = new UserRepository(connection);
        IEnumerable<User> users = repository.GetWithRoles();

        Console.WriteLine("");
        foreach (User user in users)
        {
            Console.WriteLine($"Id do Usuário: {user.Id} /Nome: {user.Name} / Email: {user.Email}");
            
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

        Console.WriteLine("Perfis disponíveis");
        foreach (Role role in roles)
        {
            Console.WriteLine($"Id: {role.Id} / Nome: {role.Name}");
            Console.WriteLine("---------------------------------------------------");
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

        Console.WriteLine("Lista de tags");
        Console.WriteLine("");
        foreach (Tag tag in tags)
        {
            Console.WriteLine($"Id: {tag.Id} / Nome Tag: {tag.Name}");
            Console.WriteLine("---------------------------------------------------");
        }
    }

    public static void ReadTagsWithPostsCount(SqlConnection connection)
    {
        var repository = new TagRepository(connection);
        IEnumerable<Tag> tags = repository.GetTagsWithPosts();

        Console.WriteLine("Lista de Tags com quantidade de Posts vinculados");
        Console.WriteLine("");

        foreach(Tag tag in tags)
        {
            Console.WriteLine($"Nome: {tag.Name} / Quantidade de Posts: {tag.Posts.Count}");
            Console.WriteLine("---------------------------------------------------");
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

    public static void ReadCategories(SqlConnection connection)
    {
        IEnumerable<Category> categories = new Repository<Category>(connection).Get();

        foreach(Category category in categories)
        {
            Console.WriteLine($"Id: {category.Id} / Nome: {category.Name} / Slug: {category.Slug}");
            Console.WriteLine("---------------------------------------------------");
        }
    }

    public static void ListCategoryWithPostsCount(SqlConnection connection)
    {
        var repository = new CategoryRepository(connection);
        IEnumerable<Category> categories = repository.ListCategoryWithPosts();

        Console.WriteLine("Lista de categorias com quantidade de Posts");
        Console.WriteLine("");
        foreach(Category category in categories)
        {
            Console.WriteLine($"Id: {category.Id} / Nome: {category.Name} / Quantidade de Posts: {category.Posts.Count}");
            Console.WriteLine("---------------------------------------------------");
        }

        Category? categorySelected;
        
        do
        {
            Console.WriteLine("");
            Console.WriteLine("Escolha pelo Id para listar os posts de alguma categoria");
            string idCategorySelected = Console.ReadLine() ?? "";
            
            categorySelected = categories.FirstOrDefault(x => x.Id == int.Parse(idCategorySelected));

            if (categorySelected == null)
            {
                Console.WriteLine($"O Id {idCategorySelected} não foi encontrado! Digite novamente um Id válido...");
            }

        }
        while (categorySelected == null);

        Console.WriteLine("");
        Console.WriteLine($"Posts da categoria {categorySelected.Name}");
        foreach (Post post in categorySelected.Posts)
        {
            Console.WriteLine($"Id: {post.Id} / Nome: {post.Title} / Resumo: {post.Summary}");
            Console.WriteLine("---------------------------------------------------");
        }

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

        User? user;
        Category? category;

        Console.WriteLine("");
        Console.WriteLine("Escolha uma categoria para esse post");
        ReadCategories(connection);
        do
        {
            string selectedCategory = Console.ReadLine();

            category = new Repository<Category>(connection).Get(int.Parse(selectedCategory));

            if (category == null)
            {
                Console.SetCursorPosition(0, Console.CursorTop - 1);
            }
        }
        while (category == null);

        Console.WriteLine("");
        Console.WriteLine("Escolha um Autor para esse post");
        ReadUsersWithRoles(connection);
        do
        {
            string selectedUser = Console.ReadLine();

            user = new Repository<User>(connection).Get(int.Parse(selectedUser));

            if (user == null)
            {
                Console.SetCursorPosition(0, Console.CursorTop - 1);
            }
        }
        while (user == null);

        post.CategoryId = category.Id;
        post.AuthorId = user.Id;

        post.CreateDate = DateTime.Now;
        post.LastUpdateDate = DateTime.Now;

        new Repository<Post>(connection).Create(post);

    }

    public static void ListPosts(SqlConnection connection)
    {
        var repository = new Repository<Post>(connection);
        IEnumerable<Post> posts = repository.Get();

        Console.WriteLine("Lista de posts");

        foreach (Post post in posts)
        {
            Console.WriteLine($"Id: {post.Id} / Nome: {post.Title} / Resumo: {post.Summary}");
            Console.WriteLine("---------------------------------------------------");
        }
    }

    public static void ListPostsWithCategory(SqlConnection connection)
    {
        var repository = new Repository<Post>(connection);
        IEnumerable<Post> posts = repository.Get();

        var categoryRepository = new Repository<Category>(connection);
        IEnumerable<Category> categories = categoryRepository.Get();

        Console.WriteLine("Lista de posts");

        foreach (Post post in posts)
        {
            Console.WriteLine($"Id: {post.Id} / Nome: {post.Title} / Resumo: {post.Summary} / Categoria: {categories.FirstOrDefault(x => x.Id == post.CategoryId).Name}");
            Console.WriteLine("---------------------------------------------------");
        }
    }


    public static void ListPostsWithTags(SqlConnection connection)
    {
        var repository = new PostRepository(connection);

        IEnumerable<Post> posts = repository.GetPostsWithTags();
        foreach (Post post in posts)
        {
            Console.WriteLine($"Id: {post.Id} / Nome: {post.Title} / Resumo: {post.Summary}");
            
            Console.Write("Tags: ");
            int index = 0;
            foreach(Tag tag in post.Tags)
            {
                if (index + 1 < post.Tags.Count)
                    Console.Write($"{tag.Name}, ");
                else
                    Console.Write($"{tag.Name}");

                index++;
            }
            Console.WriteLine("");
            Console.WriteLine("---------------------------------------------------");
        }
    }

    
}
