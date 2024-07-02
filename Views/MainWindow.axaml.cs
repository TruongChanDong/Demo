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
        }
        private void OnGoToSignUpClick(object sender, RoutedEventArgs e)
        {
            var signUpWindow = new SignUp();
            signUpWindow.Show();
            this.Close();
        }
    }
}
