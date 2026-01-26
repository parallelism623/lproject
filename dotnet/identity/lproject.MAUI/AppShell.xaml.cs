using lproject.MAUI.Features.HomeFeature;
using lproject.MAUI.Features.LoginFeature;

namespace lproject.MAUI;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        RegisterRouter();
    }

    private void RegisterRouter()
    {
        Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
    }
}

