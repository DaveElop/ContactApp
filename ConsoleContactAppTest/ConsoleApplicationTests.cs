using System;
using System.IO;
using Xunit;
using Moq;

namespace ConsoleApp.Tests
{
    public class ConsoleApplicationTests
    {
        [Fact]
        public void Run_ShouldExit_WhenUserChoosesExitOption()
        {
            // Arrange
            var menuServiceMock = new Mock<IMenuService>();
            var contactServiceMock = new Mock<IContactService>();
            var consoleApplication = new ConsoleApplication(menuServiceMock.Object, contactServiceMock.Object);

            // Mocking the DisplayMenu method
            menuServiceMock.Setup(m => m.DisplayMenu());

            // Redirecting standard input for the test
            using (var stringReader = new StringReader("6\n"))
            {
                Console.SetIn(stringReader);

                // Act
                consoleApplication.Run();

                // Assert
                menuServiceMock.Verify(m => m.DisplayMenu(), Times.Once);
                menuServiceMock.Verify(m => m.GetSelectedOption(), Times.Once);
                // You may add more assertions based on the expected behavior when the user chooses the Exit option
            }
        }

        [Fact]
        public void Run_ShouldCallAddContact_WhenUserChoosesAddContactOption()
        {
            // Arrange
            var menuServiceMock = new Mock<IMenuService>();
            var contactServiceMock = new Mock<IContactService>();
            var consoleApplication = new ConsoleApplication(menuServiceMock.Object, contactServiceMock.Object);

            // Mocking the DisplayMenu method
            menuServiceMock.Setup(m => m.DisplayMenu());

            // Redirecting standard input for the test
            using (var stringReader = new StringReader("1\n"))
            {
                Console.SetIn(stringReader);

                // Act
                consoleApplication.Run();

                // Assert
                menuServiceMock.Verify(m => m.DisplayMenu(), Times.Once);
                menuServiceMock.Verify(m => m.GetSelectedOption(), Times.Once);
                contactServiceMock.Verify(c => c.AddContact(It.IsAny<Contact>()), Times.Once);                                                                                              
            }
        }
    }
}