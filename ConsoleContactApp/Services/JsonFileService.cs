using System.IO;
using System.Text.Json;
using System.Collections.Generic;

public class JsonFileService : IFileService
{
    private const string FileName = "contacts.json";

    public List<Contact> LoadContacts()
    {
        if (File.Exists(FileName))
        {
            var json = File.ReadAllText(FileName);
            return JsonSerializer.Deserialize<List<Contact>>(json);
        }
        return new List<Contact>();
    }

    public void SaveContacts(List<Contact> contacts)
    {
        var json = JsonSerializer.Serialize(contacts);
        File.WriteAllText(FileName, json);
    }
}