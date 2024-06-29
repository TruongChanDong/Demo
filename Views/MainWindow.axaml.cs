using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using System;

namespace Demo.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var username = this.FindControl<TextBox>("Username").Text;
                var password = this.FindControl<TextBox>("Password").Text;

                Console.WriteLine($"Username: {username}, Password: {password}");

                if (username == "admin" && password == "admin")
                {
                    this.FindControl<TextBlock>("Message").Text = "Login Successful!";
                    Console.WriteLine("Login Successful!");
                }
                else
                {
                    this.FindControl<TextBlock>("Message").Text = "Invalid username or password.";
                    Console.WriteLine("Invalid username or password.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
