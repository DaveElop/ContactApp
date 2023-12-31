using System;
using System.Xml.Linq;

public class ConsoleApplication
{
    private readonly IContactService contactService;
    private readonly IMenuService menuService;

    public ConsoleApplication(IMenuService menuService, IContactService contactService)
    {
        this.menuService = menuService;
        this.contactService = contactService;
    }

    public void Run()
    {
        Console.WriteLine("Welcome to the contact manager!");

        while (true)
        {
            menuService.DisplayMenu();

            var choice = menuService.GetSelectedOption();

            switch (choice)
            {
                case MenuOption.AddContact:
                    AddContact();
                    break;
                case MenuOption.ListAllContacts:
                    ListAllContacts();
                    break;
                case MenuOption.GetContactDetails:
                    GetContactDetails();
                    break;
                case MenuOption.EditContact:
                    EditContact();
                    break;
                case MenuOption.RemoveContact:
                    RemoveContact();
                    break;
                case MenuOption.Exit:
                    Console.WriteLine("Exiting the application. Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid option. Please enter a number between 1 and 6.");
                    break;
            }
        }
    }

    private void AddContact()
    {
        Console.WriteLine();
        var contact = new Contact
        {
            FirstName = ReadValidatedInput("First Name: ", ValidationHelpers.IsValidName, "First name must not contain numbers.", "You have to write your first name."),
            LastName = ReadValidatedInput("Last Name: ", ValidationHelpers.IsValidName, "Last name must not contain numbers.", "You have to write your last name."),
            PhoneNumber = ReadValidatedInput("Phone Number: ", ValidationHelpers.IsValidPhoneNumber, "Phone number must not contain letters.", "You have to write your phone number."),
            Email = ReadValidatedInput("Email: ", ValidationHelpers.IsValidEmail, "Check your email again.", "You have to write your email."),
            Address = ReadInput("Address: ")
        };

        contactService.AddContact(contact);
        Console.Clear();
        Console.WriteLine("Contact added successfully!");
    }

    private void ListAllContacts()
    {
        Console.Clear();

        var contacts = contactService.GetAllContacts();

        if (contacts.Count > 0)
        {
            Console.WriteLine("\nAll Contacts:\n");

            foreach (var contact in contacts)
            {
                Console.WriteLine($"First Name: {contact.FirstName}");
                Console.WriteLine($"Last Name: {contact.LastName}");
                Console.WriteLine($"Phone Number: {contact.PhoneNumber}");
                Console.WriteLine($"Email: {contact.Email}");
                Console.WriteLine($"Address: {contact.Address}");
                Console.WriteLine(new string('-', 20)); // Separator line
            }
        }
        else
        {
            Console.WriteLine("No contacts available.");
        }
    }


    private void GetContactDetails()
    {
        var email = ReadInput("Enter Email to get details: ");
        var contact = contactService.GetContactByEmail(email);
        Console.WriteLine("Welcome to the contact manager!");

        if (contact != null)
        {
            Console.Clear();
            Console.WriteLine($"\nContact Details:\n\n{contact.FirstName} {contact.LastName}\n");
            Console.WriteLine($"Phone: {contact.PhoneNumber}");
            Console.WriteLine($"Email: {contact.Email}");
            Console.WriteLine($"Address: {contact.Address}");
        }
        else
        {
            Console.WriteLine($"Contact with email '{email}' not found.");
        }
    }

    private void RemoveContact()
    {
        var email = ReadInput("Enter Email to remove contact: ");
        contactService.RemoveContact(email);
        Console.Clear();
        Console.WriteLine($"Contact with email '{email}' removed successfully.");

    }

    private void EditContact()
    {
        var email = ReadInput("Enter Email to edit contact: ");

        contactService.EditContact(email, contact =>
        {
            Console.WriteLine("\nEditing Contact:");
            Console.WriteLine($"1. First Name: {contact.FirstName}");
            Console.WriteLine($"2. Last Name: {contact.LastName}");
            Console.WriteLine($"3. Phone Number: {contact.PhoneNumber}");
            Console.WriteLine($"4. Email: {contact.Email}");
            Console.WriteLine($"5. Address: {contact.Address}");

            Console.Write("Select a field to edit (1-5), or press Enter to cancel: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    contact.FirstName = ReadValidatedInput("Enter new First Name: ", ValidationHelpers.IsValidName, "First name must not contain numbers.", "You have to write your first name.");
                    break;
                case "2":
                    contact.LastName = ReadValidatedInput("Enter new Last Name: ", ValidationHelpers.IsValidName, "Last name must not contain numbers.", "You have to write your last name.");
                    break;
                case "3":
                    contact.PhoneNumber = ReadValidatedInput("Enter new Phone Number: ", ValidationHelpers.IsValidPhoneNumber, "Phone number must not contain letters.", "You have to write your phone number.");
                    break;
                case "4":
                    contact.Email = ReadValidatedInput("Enter new Email: ", ValidationHelpers.IsValidEmail, "Check your email again.", "You have to write your email.");
                    break;
                case "5":
                    contact.Address = ReadInput("Enter new Address: ");
                    break;
                default:
                    Console.WriteLine("Editing canceled.");
                    return;
            }
        });
    }


    private string ReadValidatedInput(string prompt, Func<string, bool> validation, string validationRule, string emptyInputError)
    {
        string userInput;
        do
        {
            userInput = ReadInput(prompt);

            if (string.IsNullOrWhiteSpace(userInput))
            {
                Console.WriteLine($"Invalid input. {emptyInputError}");
                continue;
            }

            if (!validation(userInput))
            {
                Console.WriteLine($"Invalid input. {validationRule}");
            }

        } while (string.IsNullOrWhiteSpace(userInput) || !validation(userInput));

        return userInput;
    }

    private string ReadInput(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine();
    }
}