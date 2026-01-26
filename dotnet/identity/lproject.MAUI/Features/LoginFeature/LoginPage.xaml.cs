using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lproject.MAUI.Features.LoginFeature;

public partial class LoginPage : ContentPage
{
 
    public LoginPage()
    {
        InitializeComponent();
        
    }

 
}


public record Test
{
    public bool IsDarkMode { get; set; } = false;
    public Color BackgroundColor { get; set; } = Color.FromRgb(195, 232, 234);
    
}