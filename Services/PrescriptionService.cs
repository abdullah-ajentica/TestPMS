using System;
using System.Collections.ObjectModel;
using TestPms.Models;

namespace TestPms.Services;

public class PrescriptionService
{
    public static PrescriptionService Instance { get; } = new PrescriptionService();

    public ObservableCollection<Prescription> Prescriptions { get; } = new();

    private int _nextRxNumber = 100001;

    private PrescriptionService()
    {
        Seed();
    }

    public string GenerateNextRxNumber() => (_nextRxNumber++).ToString();

    private void Seed()
    {
        Prescriptions.Add(new Prescription
        {
            RxNumber = GenerateNextRxNumber(),
            PatientName = "John Carter",
            DrugName = "Amoxicillin 500mg",
            Quantity = 30,
            Prescriber = "Dr. Susan Lee",
            DateWritten = DateTime.Today.AddDays(-2),
            Status = PrescriptionStatus.Pending
        });

        Prescriptions.Add(new Prescription
        {
            RxNumber = GenerateNextRxNumber(),
            PatientName = "Maria Gomez",
            DrugName = "Lisinopril 10mg",
            Quantity = 90,
            Prescriber = "Dr. Alan Brooks",
            DateWritten = DateTime.Today.AddDays(-1),
            Status = PrescriptionStatus.Pending
        });

        Prescriptions.Add(new Prescription
        {
            RxNumber = GenerateNextRxNumber(),
            PatientName = "David Kim",
            DrugName = "Metformin 850mg",
            Quantity = 60,
            Prescriber = "Dr. Susan Lee",
            DateWritten = DateTime.Today,
            Status = PrescriptionStatus.Pending
        });

        Prescriptions.Add(new Prescription
        {
            RxNumber = GenerateNextRxNumber(),
            PatientName = "Emily Chen",
            DrugName = "Atorvastatin 20mg",
            Quantity = 30,
            Prescriber = "Dr. Priya Patel",
            DateWritten = DateTime.Today,
            Status = PrescriptionStatus.Processed
        });
    }
}
