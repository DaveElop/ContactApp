using Xunit;
using Moq;

namespace MenuServices.Tests
{
    public class MenuServiceTests
    {
        [Fact]
        public void GetSelectedOption_ShouldReturnValidOption_WhenUserInputsValidOption()
        {
            // Arrange
            var mockConsoleInput = "3\n"; // Simulating user input "3"
            var menuService = new MenuService();

            using (var stringReader = new StringReader(mockConsoleInput))
            {
                Console.SetIn(stringReader);

                // Act
                var selectedOption = menuService.GetSelectedOption();

                // Assert
                Assert.Equal(MenuOption.GetContactDetails, selectedOption);
            }
        }

        [Fact]
        public void GetSelectedOption_ShouldRetry_WhenUserInputsInvalidOptionThenValidOption()
        {
            // Arrange
            var mockConsoleInput = "invalid\n3\n"; // Simulating user input "invalid" and then "3"
            var menuService = new MenuService();

            using (var stringReader = new StringReader(mockConsoleInput))
            {
                Console.SetIn(stringReader);

                // Act
                var selectedOption = menuService.GetSelectedOption();

                // Assert
                Assert.Equal(MenuOption.GetContactDetails, selectedOption);
            }
        }
    }
}