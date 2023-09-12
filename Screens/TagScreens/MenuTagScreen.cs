namespace projeto_blog.Screens.TagScreens;

public static class MenuTagScreen
{
  public static void Load()
  {
    Console.Clear();
    Console.WriteLine("Gestão de tags");
    Console.WriteLine("-----------------");
    Console.WriteLine("O que deseja fazer?");
    Console.WriteLine();
    Console.WriteLine("1 - Listar tags / 2 - Cadastrar tags / 3 - Atualizar tags / 4 - Deletar tags");
    Console.WriteLine();
    Console.WriteLine();
    var option = short.Parse(Console.ReadLine()!);

    switch (option)
    {
      case 1:
        ListTagsScreen.Load();
        break;
      case 2:
        CreateTagScreen.Load();
        break;
      case 3:
        UpdateTagScreen.Load();
        break;
      case 4:
        DeleteTagScreen.Load();
        break;
      default: 
        Load(); 
        break;
    }
  }
}
