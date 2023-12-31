using System;
using System.Collections.Generic;
using System.IO;
using Xunit;
using Moq;

namespace JsonFileServices.Tests
{
    public class JsonFileServiceTests
    {
        [Fact]
        public void LoadContacts_ShouldDeserializeContactsFromFile()
        {
            // Arrange
            var expectedContacts = new List<Contact>
            {
                new Contact { Email = "test@example.com" }
            };

            var json = System.Text.Json.JsonSerializer.Serialize(expectedContacts);

            var fileServiceMock = new Mock<IFileService>();
            fileServiceMock.Setup(f => f.LoadContacts()).Returns(expectedContacts);

            var jsonFileService = new JsonFileService();

            // Act
            var loadedContacts = jsonFileService.LoadContacts();

            // Assert
            Assert.Equal(expectedContacts.Count, loadedContacts.Count);

            for (int i = 0; i < expectedContacts.Count; i++)
            {
                Assert.Equal(expectedContacts[i].Email, loadedContacts[i].Email);
            }
        }

        [Fact]
        public void SaveContacts_ShouldSaveContactsToFile()
        {
            // Arrange
            var contactsToSave = new List<Contact>
    {
        new Contact { Email = "test@example.com" },
        new Contact { Email = "another@example.com" }
    };

            var fileServiceMock = new Mock<IFileService>();
            var contactService = new ContactService(fileServiceMock.Object);

            // Act
            contactService.SaveContacts(contactsToSave);

            // Assert
            fileServiceMock.Verify(fs => fs.SaveContacts(contactsToSave), Times.Once);
        }
    }
}
