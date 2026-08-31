using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Threading;
using TestPms.Models;
using TestPms.Services;

namespace TestPms;

public partial class MainFormWindow : Window
{
    private readonly DispatcherTimer _toastTimer;

    public MainFormWindow()
    {
        InitializeComponent();

        _toastTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(3) };
        _toastTimer.Tick += (_, _) =>
        {
            ToastBorder.Visibility = Visibility.Collapsed;
            _toastTimer.Stop();
        };

        ToastService.Requested += ShowToast;
        Closed += (_, _) => ToastService.Requested -= ShowToast;
    }

    private void ShowToast(string message)
    {
        ToastText.Text = message;
        ToastBorder.Visibility = Visibility.Visible;
        _toastTimer.Stop();
        _toastTimer.Start();
    }

    private void MainForm_LockRequested(object? sender, EventArgs e)
    {
        var lockWindow = new LockWindow();
        lockWindow.Show();
        Close();
    }

    private void MainForm_ProcessRequested(object? sender, List<Prescription> selected)
    {
        MainForm.SetProcessButtonEnabled(false);

        var processWindow = new ProcessWindow(selected) { Owner = this };
        processWindow.Completed += (_, _) => ToastService.Show("Successfully processed.");
        processWindow.Closed += (_, _) => MainForm.SetProcessButtonEnabled(true);
        processWindow.Show();
    }
}
