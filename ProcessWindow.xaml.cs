using System;
using System.Collections.Generic;
using TestPms.Models;
using TestPms.Views;

namespace TestPms;

public partial class ProcessWindow : System.Windows.Window
{
    public event EventHandler? Completed;

    public ProcessWindow(List<Prescription> selected)
    {
        InitializeComponent();

        var view = new ProcessFormView(selected);
        view.ContinueRequested += (_, _) =>
        {
            Completed?.Invoke(this, EventArgs.Empty);
            Close();
        };

        Host.Children.Add(view);
    }
}
