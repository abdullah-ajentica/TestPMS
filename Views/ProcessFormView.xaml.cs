using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using TestPms.Models;

namespace TestPms.Views;

public partial class ProcessFormView : UserControl
{
    private readonly List<Prescription> _prescriptions;

    public event EventHandler? ContinueRequested;

    public ProcessFormView(List<Prescription> prescriptions)
    {
        InitializeComponent();
        _prescriptions = prescriptions;
        ProcessGrid.ItemsSource = _prescriptions;
    }

    private void ContinueButton_Click(object sender, RoutedEventArgs e)
    {
        foreach (var prescription in _prescriptions)
        {
            prescription.Status = PrescriptionStatus.Processed;
            prescription.IsChecked = false;
        }

        ContinueRequested?.Invoke(this, EventArgs.Empty);
    }
}
