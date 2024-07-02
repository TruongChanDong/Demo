using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using System;

namespace Demo.Views
{
    public partial class SignUp: Window
    {
        public SignUp()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void OnSignUpClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var username = this.FindControl<TextBox>("Username").Text;
                var email = this.FindControl<TextBox>("Email").Text;
                var password = this.FindControl<TextBox>("Password").Text;

                // Simple validation for demo purposes
                if (!string.IsNullOrWhiteSpace(username) &&
                    !string.IsNullOrWhiteSpace(email) &&
                    !string.IsNullOrWhiteSpace(password))
                {
                    this.FindControl<TextBlock>("Message").Text = "Sign Up Successful!";
                    Console.WriteLine("Sign Up Successful!");
                }
                else
                {
                    this.FindControl<TextBlock>("Message").Text = "All fields are required.";
                    Console.WriteLine("All fields are required.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        private void OnGoToLoginClick(object sender, RoutedEventArgs e)
        {
            var loginWindow = new MainWindow();
            loginWindow.Show();
            this.Close();
        }
    }
}
