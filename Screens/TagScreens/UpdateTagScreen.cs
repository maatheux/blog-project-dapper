using projeto_blog.Models;
using projeto_blog.Repositories;

namespace projeto_blog.Screens.TagScreens;

public static class UpdateTagScreen
{
  public static void Load()
  {
    Console.Clear();
    Console.WriteLine("Atualizando tag");
    Console.WriteLine("");

    ListTagsScreen.Load();

    Console.WriteLine("");
    Console.WriteLine("Escolha um id");

    Console.Write("Id: ");
    var id = Console.ReadLine();
    
    Tag tag = new Tag();

    Console.Write("Nome: ");
    tag.Name = Console.ReadLine();
    Console.Write("Url Slug: ");
    tag.Slug = Console.ReadLine();
    tag.Id = int.Parse(id);

    Update(tag);
    Console.ReadKey();
    MenuTagScreen.Load();
  }

  public static void Update(Tag tag)
  {
    try
    {
      var repository = new Repository<Tag>();
      repository.Update(tag);
      Console.WriteLine("");
      Console.WriteLine("Tag atualizada com sucesso!");
    }
    catch (Exception ex)
    {
      Console.Clear();
      Console.WriteLine("Não foi possível atualizar a tag");
      Console.WriteLine(ex.Message);
    }
  }
}
