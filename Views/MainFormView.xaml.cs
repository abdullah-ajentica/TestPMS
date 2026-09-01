using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using TestPms.Models;
using TestPms.Services;
using TestPms;

namespace TestPms.Views;

public partial class MainFormView : UserControl
{
    public event EventHandler? LockRequested;
    public event EventHandler<List<Prescription>>? ProcessRequested;

    public MainFormView()
    {
        InitializeComponent();
        DataContext = PrescriptionService.Instance.Prescriptions;
    }

    public void SetProcessButtonEnabled(bool enabled)
    {
        ProcessButton.IsEnabled = enabled;
    }

    private void AddButton_Click(object sender, RoutedEventArgs e)
    {
        var dialog = new PrescriptionDialog { Owner = Window.GetWindow(this) };
        if (dialog.ShowDialog() == true && dialog.Result is not null)
        {
            PrescriptionService.Instance.Prescriptions.Add(dialog.Result);
            ToastService.Show("Successfully saved.");
        }
    }

    private void EditButton_Click(object sender, RoutedEventArgs e)
    {
        if (PrescriptionGrid.SelectedItem is not Prescription selected)
        {
            MessageBox.Show("Please select a prescription to edit.", "Edit Prescription",
                MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }

        var dialog = new PrescriptionDialog(selected) { Owner = Window.GetWindow(this) };
        if (dialog.ShowDialog() == true)
        {
            ToastService.Show("Successfully saved.");
        }
    }

    private void RemoveButton_Click(object sender, RoutedEventArgs e)
    {
        if (PrescriptionGrid.SelectedItem is not Prescription selected)
        {
            MessageBox.Show("Please select a prescription to remove.", "Remove Prescription",
                MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }

        var result = MessageBox.Show($"Remove prescription {selected.RxNumber} for {selected.PatientName}?",
            "Confirm Remove", MessageBoxButton.YesNo, MessageBoxImage.Question);

        if (result == MessageBoxResult.Yes)
        {
            PrescriptionService.Instance.Prescriptions.Remove(selected);
        }
    }

    private void RefreshButton_Click(object sender, RoutedEventArgs e)
    {
        PrescriptionGrid.Items.Refresh();
    }

    private void ChecklistButton_Click(object sender, RoutedEventArgs e)
    {
        var prescriptions = PrescriptionService.Instance.Prescriptions;
        bool allChecked = prescriptions.Count > 0 && prescriptions.All(p => p.IsChecked);

        foreach (var prescription in prescriptions)
        {
            prescription.IsChecked = !allChecked;
        }
    }

    private void ProcessButton_Click(object sender, RoutedEventArgs e)
    {
        var selected = PrescriptionService.Instance.Prescriptions.Where(p => p.IsChecked).ToList();
        if (selected.Count == 0)
        {
            MessageBox.Show("Please check at least one prescription to process.", "Nothing To Process",
                MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }

        ProcessRequested?.Invoke(this, selected);
    }

    private void LockButton_Click(object sender, RoutedEventArgs e)
    {
        LockRequested?.Invoke(this, EventArgs.Empty);
    }

    private void LumistryWebpageButton_Click(object sender, RoutedEventArgs e)
    {
        var window = new LumistryWebpageWindow { Owner = Window.GetWindow(this) };
        window.Show();
    }

    private void LumistryWebBrowserButton_Click(object sender, RoutedEventArgs e)
    {
        var window = new LumistryWebBrowserWindow { Owner = Window.GetWindow(this) };
        window.Show();
    }

    private void PrescriptionGrid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        var cell = FindAncestor<DataGridCell>(e.OriginalSource as DependencyObject);
        if (cell?.DataContext is not Prescription prescription) return;

        prescription.IsChecked = !prescription.IsChecked;

        if (cell.Column is DataGridCheckBoxColumn)
        {
            // We just toggled it manually — stop the click from also reaching the native
            // CheckBox, which would otherwise toggle it a second time (net no-op).
            e.Handled = true;
        }
    }

    private static T? FindAncestor<T>(DependencyObject? current) where T : DependencyObject
    {
        while (current is not null)
        {
            if (current is T match) return match;
            current = VisualTreeHelper.GetParent(current);
        }
        return null;
    }
}
