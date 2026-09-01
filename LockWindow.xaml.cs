using System;
using System.Windows;

namespace TestPms;

public partial class LockWindow : Window
{
    public LockWindow()
    {
        InitializeComponent();

        // Automation tooling can call ShowWindow(SW_RESTORE) to bring this window to the
        // foreground, which un-maximizes an already-maximized window as a side effect —
        // snap straight back so the layout stays consistent with what a workflow expects.
        StateChanged += (_, _) =>
        {
            if (WindowState != WindowState.Maximized)
                WindowState = WindowState.Maximized;
        };
    }

    private void LockScreen_LoginSucceeded(object? sender, EventArgs e)
    {
        var mainFormWindow = new MainFormWindow();
        mainFormWindow.Show();
        Close();
    }
}
