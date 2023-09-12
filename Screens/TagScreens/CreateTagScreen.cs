using projeto_blog.Models;
using projeto_blog.Repositories;

namespace projeto_blog.Screens.TagScreens;

public static class CreateTagScreen
{
  public static void Load()
  {
    Console.Clear();
    Console.WriteLine("Nova tag");
    Console.WriteLine("");
    Tag tag = new Tag();

    Console.Write("Nome: ");
    tag.Name = Console.ReadLine();
    Console.Write("Url Slug: ");
    tag.Slug = Console.ReadLine();

    Create(tag);
    Console.ReadKey();
    MenuTagScreen.Load();
  }

  public static void Create(Tag tag)
  {
    try
    {
      var repository = new Repository<Tag>();
      repository.Create(tag);
      Console.WriteLine("");
      Console.WriteLine("Tag cadastrada com sucesso!");
    }
    catch (Exception ex)
    {
      Console.Clear();
      Console.WriteLine("Não foi possível salvar a tag");
      Console.WriteLine(ex.Message);
    }
  }
}
