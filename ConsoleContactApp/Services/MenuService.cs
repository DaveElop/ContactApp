using System;

public class MenuService : IMenuService
{

    public void DisplayMenu()
    {
        Console.WriteLine("\nMenu:");
        Console.WriteLine("1. Add Contact");
        Console.WriteLine("2. List All Contacts");
        Console.WriteLine("3. Get Contact Details");
        Console.WriteLine("4. Edit Contact");
        Console.WriteLine("5. Remove Contact");
        Console.WriteLine("6. Exit");
    }

    public MenuOption GetSelectedOption()
    {
        Console.Write("Select an option (1-6): ");
        if (Enum.TryParse(Console.ReadLine(), out MenuOption choice) && Enum.IsDefined(typeof(MenuOption), choice))
        {
            return choice;
        }

        Console.WriteLine("Invalid option. Please enter a number between 1 and 6.");
        return GetSelectedOption();
    }
}