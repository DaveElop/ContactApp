using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public interface IFileService
{
    /// <summary>
    /// Loads contacts from a file.
    /// </summary>
    /// <returns>A list of contacts loaded from the file.</returns>
    List<Contact> LoadContacts();

    /// <summary>
    /// Saves contacts to a file.
    /// </summary>
    /// <param name="contacts">The list of contacts to be saved to the file.</param>
    void SaveContacts(List<Contact> contacts);
}