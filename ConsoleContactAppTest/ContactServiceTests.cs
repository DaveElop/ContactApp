using System;
using System.Collections.Generic;
using Moq;
using Xunit;

namespace ContactServices.Tests
{
    public class ContactServiceTests
    {
        [Fact]
        public void AddContact_ShouldAddContactToFile()
        {
            // Arrange
            var fileServiceMock = new Mock<IFileService>();
            var contactService = new ContactService(fileServiceMock.Object);

            var contactToAdd = new Contact { Email = "test@example.com" };

            // Act
            contactService.AddContact(contactToAdd);

            // Assert
            var addedContact = contactService.GetContactByEmail("test@example.com");
            Assert.NotNull(addedContact);

            fileServiceMock.Verify(fs => fs.SaveContacts(It.IsAny<List<Contact>>()), Times.Once);
        }

        [Fact]
        public void EditContact_ShouldEditContact()
        {
            // Arrange
            var initialContacts = new List<Contact>
            {
                new Contact { Email = "test@example.com", FirstName = "John", LastName = "Doe" }
            };

            var fileServiceMock = new Mock<IFileService>();
            fileServiceMock.Setup(f => f.LoadContacts()).Returns(initialContacts);

            var contactService = new ContactService(fileServiceMock.Object);

            // Act
            contactService.EditContact("test@example.com", contact =>
            {
                contact.FirstName = "Jane";
                contact.LastName = "Smith";
            });

            // Assert
            var editedContact = contactService.GetContactByEmail("test@example.com");
            Assert.NotNull(editedContact);
            Assert.Equal("Jane", editedContact.FirstName);
            Assert.Equal("Smith", editedContact.LastName);

            fileServiceMock.Verify(fs => fs.SaveContacts(It.IsAny<List<Contact>>()), Times.Once);
        }

        [Fact]
        public void RemoveContact_ShouldRemoveContactFromFile()
        {
            // Arrange
            var initialContacts = new List<Contact>
    {
        new Contact { Email = "test@example.com" },
        new Contact { Email = "another@example.com" }
    };

            var fileServiceMock = new Mock<IFileService>();
            fileServiceMock.Setup(f => f.LoadContacts()).Returns(initialContacts);

            var contactService = new ContactService(fileServiceMock.Object);

            // Act
            contactService.RemoveContact("test@example.com");

            // Assert
            var remainingContacts = contactService.GetAllContacts();
            Assert.Single(remainingContacts);
            Assert.Equal("another@example.com", remainingContacts[0].Email);

            fileServiceMock.Verify(fs => fs.SaveContacts(remainingContacts), Times.Once);
        }
    }
}
