using System.Reflection;
using System.Windows.Controls;

namespace TestPms;

public partial class LumistryWebBrowserWindow : System.Windows.Window
{
    public LumistryWebBrowserWindow()
    {
        InitializeComponent();
        Browser.Loaded += (_, _) => SuppressScriptErrors(Browser);
    }

    private static void SuppressScriptErrors(WebBrowser browser)
    {
        var field = typeof(WebBrowser).GetField("_axIWebBrowser2", BindingFlags.Instance | BindingFlags.NonPublic);
        var comObject = field?.GetValue(browser);
        if (comObject is null) return;

        comObject.GetType().InvokeMember("Silent", BindingFlags.SetProperty, null, comObject, new object[] { true });
    }
}
