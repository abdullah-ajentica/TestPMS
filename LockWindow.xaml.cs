using System;
using System.Windows;

namespace TestPms;

public partial class LockWindow : Window
{
    public LockWindow()
    {
        InitializeComponent();
    }

    private void LockScreen_LoginSucceeded(object? sender, EventArgs e)
    {
        var mainFormWindow = new MainFormWindow();
        mainFormWindow.Show();
        Close();
    }
}
