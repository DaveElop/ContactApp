

class Program
{
    static void Main()
    {
        var menuService = new MenuService();
        var contactService = new ContactService(new JsonFileService());
        var consoleApplication = new ConsoleApplication(menuService, contactService);

        consoleApplication.Run();
    }
}