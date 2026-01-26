using lproject.MAUI.Abstraction;
using lproject.MAUI.CustomControls.DatePickerControl;


using Microsoft.Extensions.Logging;
using Scrutor;
using System.Reflection;


namespace lproject.MAUI;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            .ConfigureMauiHandlers(handlers =>
            {
                handlers.AddHandler<Calendar, CalendarHandler>();
            })
            .RegisterServices();
        


        AppDomain.CurrentDomain.UnhandledException += (s, e) =>
        {
            var ex = e.ExceptionObject as Exception;
        
        };

        TaskScheduler.UnobservedTaskException += (s, e) =>
        {
            var ex = e.Exception;
            e.SetObserved();
            // log
        };
        return builder.Build();
    }

    private static MauiAppBuilder RegisterServices(this MauiAppBuilder builder)
    {
        builder.Services.Scan(b =>
        {
            b.FromAssemblies(LoadAllAssembliesFromBaseDirectory())
                .AddClasses(it => it.AssignableTo<ITransientService>())
                .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                .AsSelfWithInterfaces()
                .WithTransientLifetime()
                .AddClasses(it => it.AssignableTo<IScopedService>())
                .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                .AsSelfWithInterfaces()
                .WithScopedLifetime()
                .AddClasses(it => it.AssignableTo<ISingletonService>())
                .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                .AsSelfWithInterfaces()
                .WithSingletonLifetime();
        });
        return builder;
    }

    public static List<Assembly> LoadAllAssembliesFromBaseDirectory()
    {
        var assemblies = new List<Assembly>();

        var basePath = AppContext.BaseDirectory;
        string projectName =
            Assembly.GetEntryAssembly()!.GetName().Name!;
        var dllFiles = Directory.GetFiles(basePath, $"{projectName}*.dll", SearchOption.AllDirectories);

        foreach (var dll in dllFiles)
        {
            try
            {
                var assemblyName = AssemblyName.GetAssemblyName(dll);

                if (IsAlreadyLoaded(assemblyName))
                    continue;

                var asm = Assembly.LoadFrom(dll);
                assemblies.Add(asm);
            }
            catch
            {
            }
        }

        return assemblies;
    }

    private static bool IsAlreadyLoaded(AssemblyName assemblyName)
    {
        foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
        {
            var name = asm.GetName();
            if (AssemblyName.ReferenceMatchesDefinition(name, assemblyName))
                return true;
        }

        return false;
    }
}