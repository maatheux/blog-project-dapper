using projeto_blog.Models;
using projeto_blog.Repositories;

namespace projeto_blog.Screens.TagScreens;

public class DeleteTagScreen
{
  public static void Load()
  {
    Console.Clear();
    Console.WriteLine("Excluir tag");
    Console.WriteLine("");

    ListTagsScreen.Load();

    Console.WriteLine("");
    Console.WriteLine("Escolha um id");

    Console.Write("Id: ");
    var id = Console.ReadLine();

    Delete(int.Parse(id));
    Console.ReadKey();
    MenuTagScreen.Load();
  }

  public static void Delete(int id)
  {
    try
    {
      var repository = new Repository<Tag>();
      repository.Delete(id);
      Console.WriteLine("");
      Console.WriteLine("Tag deletada com sucesso!");
    }
    catch (Exception ex)
    {
      Console.Clear();
      Console.WriteLine("Não foi possível deletar a tag");
      Console.WriteLine(ex.Message);
    }
  }
}
