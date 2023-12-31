using System;
using System.Collections.Generic;

public interface IContactService
{
    /// <summary>
    /// Adds a new contact to the contact list.
    /// </summary>
    /// <param name="contact">The contact to be added.</param>
    void AddContact(Contact contact);

    /// <summary>
    /// Removes a contact with the specified email address from the contact list.
    /// </summary>
    /// <param name="emailAddress">The email address of the contact to be removed.</param>
    void RemoveContact(string emailAddress);

    /// <summary>
    /// Retrieves a contact by the specified email address.
    /// </summary>
    /// <param name="emailAddress">The email address of the contact to retrieve.</param>
    /// <returns>The contact with the specified email address, or null if not found.</returns>
    Contact GetContactByEmail(string emailAddress);

    /// <summary>
    /// Retrieves all contacts in the contact list.
    /// </summary>
    /// <returns>A list of all contacts.</returns>
    List<Contact> GetAllContacts();

    /// <summary>
    /// Edits an existing contact identified by email address using the provided edit action.
    /// </summary>
    /// <param name="email">The email address of the contact to be edited.</param>
    /// <param name="editAction">The action to perform on the contact for editing.</param>
    void EditContact(string email, Action<Contact> editAction);
}
