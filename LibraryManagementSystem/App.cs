using Microsoft.Maui.Controls;
using LibraryManagementSystem.Pages;

namespace LibraryManagementSystem;

public class App : Application
{
   protected override Window CreateWindow(IActivationState? activationState)
   {
       return new Window(new RegisterPage());
   }
}
