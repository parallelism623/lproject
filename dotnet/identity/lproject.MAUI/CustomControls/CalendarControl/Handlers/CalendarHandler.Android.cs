
using lproject.MAUI.Platforms.Android;
using Microsoft.Maui.Handlers;

namespace lproject.MAUI.CustomControls.DatePickerControl;

public partial class CalendarHandler : ViewHandler<Calendar, CalendarUI>
{
    protected override CalendarUI CreatePlatformView()
        => new CalendarUI(Context);
    protected override void ConnectHandler(CalendarUI platformView)
    {
        base.ConnectHandler(platformView);
        platformView.SetVirtualView(VirtualView);
    }

    protected override void DisconnectHandler(CalendarUI platformView)
    {
        base.DisconnectHandler(platformView);
    }
    

    static void MapConstantValues(CalendarHandler handler, Calendar view)
    {
        
    }
} 
