using System;
using System.Windows;
using System.Windows.Controls;

namespace TestPms.Views;

public partial class LockScreenView : UserControl
{
    private const string ValidEmail = "test@testpms.com";
    private const string ValidPassword = "password123";

    public event EventHandler? LoginSucceeded;

    public LockScreenView()
    {
        InitializeComponent();
    }

    private void LoginButton_Click(object sender, RoutedEventArgs e)
    {
        if (EmailBox.Text.Trim().Equals(ValidEmail, StringComparison.OrdinalIgnoreCase)
            && PasswordBox.Password == ValidPassword)
        {
            ErrorText.Visibility = Visibility.Collapsed;
            PasswordBox.Clear();
            LoginSucceeded?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            ErrorText.Visibility = Visibility.Visible;
        }
    }
}
