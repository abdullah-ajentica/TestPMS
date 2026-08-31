using System;
using System.Windows;
using TestPms.Models;
using TestPms.Services;

namespace TestPms.Views;

public partial class PrescriptionDialog : Window
{
    private readonly Prescription? _editing;

    public Prescription? Result { get; private set; }

    public PrescriptionDialog()
    {
        InitializeComponent();
        _editing = null;
        RxNumberBox.Text = PrescriptionService.Instance.GenerateNextRxNumber();
        DateWrittenPicker.SelectedDate = DateTime.Today;
    }

    public PrescriptionDialog(Prescription editing)
    {
        InitializeComponent();
        _editing = editing;
        TitleText.Text = "Edit Prescription";

        RxNumberBox.Text = editing.RxNumber;
        PatientNameBox.Text = editing.PatientName;
        DrugNameBox.Text = editing.DrugName;
        QuantityBox.Text = editing.Quantity.ToString();
        PrescriberBox.Text = editing.Prescriber;
        DateWrittenPicker.SelectedDate = editing.DateWritten;
    }

    private void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        var rxNumber = RxNumberBox.Text.Trim();
        var patientName = PatientNameBox.Text.Trim();
        var drugName = DrugNameBox.Text.Trim();

        if (string.IsNullOrEmpty(rxNumber) || string.IsNullOrEmpty(patientName) || string.IsNullOrEmpty(drugName))
        {
            ValidationError.Visibility = Visibility.Visible;
            return;
        }

        if (!int.TryParse(QuantityBox.Text.Trim(), out var quantity))
        {
            quantity = 0;
        }

        if (_editing is not null)
        {
            _editing.RxNumber = rxNumber;
            _editing.PatientName = patientName;
            _editing.DrugName = drugName;
            _editing.Quantity = quantity;
            _editing.Prescriber = PrescriberBox.Text.Trim();
            _editing.DateWritten = DateWrittenPicker.SelectedDate ?? DateTime.Today;
        }
        else
        {
            Result = new Prescription
            {
                RxNumber = rxNumber,
                PatientName = patientName,
                DrugName = drugName,
                Quantity = quantity,
                Prescriber = PrescriberBox.Text.Trim(),
                DateWritten = DateWrittenPicker.SelectedDate ?? DateTime.Today,
                Status = PrescriptionStatus.Pending
            };
        }

        DialogResult = true;
        Close();
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
        Close();
    }
}
