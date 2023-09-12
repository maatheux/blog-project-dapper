using projeto_blog.Models;
using projeto_blog.Repositories;

namespace projeto_blog.Screens.TagScreens;

public static class ListTagsScreen
{
  public static void Load()
  {
    Console.Clear();
    Console.WriteLine("Lista de tags");
    Console.WriteLine("");
    List();
  }

  private static void List()
  {
    var repository = new Repository<Tag>();
    IEnumerable<Tag> tags = repository.Get();

    foreach (Tag tag in tags)
    {
        Console.WriteLine($"Id: {tag.Id} / Nome Tag: {tag.Name} / Slug: {tag.Slug}");
        Console.WriteLine("---------------------------------------------------");
    }
  }
}
