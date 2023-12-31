using System;

public interface IMenuService
{
    /// <summary>
    /// Gets the selected menu option from the user.
    /// </summary>
    /// <returns>The selected menu option.</returns>
    MenuOption GetSelectedOption();

    /// <summary>
    /// Displays the contact management menu to the user.
    /// </summary>
    void DisplayMenu();
}