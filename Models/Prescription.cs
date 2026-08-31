using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TestPms.Models;

public enum PrescriptionStatus
{
    Pending,
    Processed
}

public class Prescription : INotifyPropertyChanged
{
    private string _rxNumber = string.Empty;
    private string _patientName = string.Empty;
    private string _drugName = string.Empty;
    private int _quantity;
    private string _prescriber = string.Empty;
    private DateTime _dateWritten = DateTime.Today;
    private PrescriptionStatus _status = PrescriptionStatus.Pending;
    private bool _isChecked;

    public string RxNumber
    {
        get => _rxNumber;
        set => SetField(ref _rxNumber, value);
    }

    public string PatientName
    {
        get => _patientName;
        set => SetField(ref _patientName, value);
    }

    public string DrugName
    {
        get => _drugName;
        set => SetField(ref _drugName, value);
    }

    public int Quantity
    {
        get => _quantity;
        set => SetField(ref _quantity, value);
    }

    public string Prescriber
    {
        get => _prescriber;
        set => SetField(ref _prescriber, value);
    }

    public DateTime DateWritten
    {
        get => _dateWritten;
        set => SetField(ref _dateWritten, value);
    }

    public PrescriptionStatus Status
    {
        get => _status;
        set => SetField(ref _status, value);
    }

    public bool IsChecked
    {
        get => _isChecked;
        set => SetField(ref _isChecked, value);
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (Equals(field, value)) return;
        field = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
