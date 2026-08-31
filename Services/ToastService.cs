using System;

namespace TestPms.Services;

public static class ToastService
{
    public static event Action<string>? Requested;

    public static void Show(string message) => Requested?.Invoke(message);
}
