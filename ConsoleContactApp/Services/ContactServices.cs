using System;
using System.Collections.Generic;
using System.Linq;

public class ContactService : IContactService
{
    private List<Contact> contacts;
    private readonly IFileService fileService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ContactService"/> class.
    /// </summary>
    /// <param name="fileService">The file service for loading and saving contacts.</param>
    public ContactService(IFileService fileService)
    {
        this.fileService = fileService;
        contacts = fileService.LoadContacts() ?? new List<Contact>();
    }

    public void AddContact(Contact contact)
    {
        contacts.Add(contact);
        fileService.SaveContacts(contacts);
    }

    public void RemoveContact(string emailAddress)
    {
        contacts.RemoveAll(c => c.Email == emailAddress);
        fileService.SaveContacts(contacts);
    }

    public Contact GetContactByEmail(string emailAddress)
    {
        return contacts.FirstOrDefault(c => c.Email == emailAddress);
    }

    public List<Contact> GetAllContacts()
    {
        return contacts;
    }

    public void SaveContacts(List<Contact> contacts)
    {
        fileService.SaveContacts(contacts);
    }

    public void EditContact(string email, Action<Contact> editAction)
    {
        var contact = contacts.FirstOrDefault(c => c.Email == email);

        if (contact != null)
        {
            editAction(contact);
            SaveContacts(contacts);
            Console.WriteLine("Contact edited successfully!");
        }
        else
        {
            Console.WriteLine($"Contact with email '{email}' not found.");
        }
    }
}