
using CoreGraphics;
using lproject.MAUI.Platforms.iOS.CalendarView;
using Microsoft.Maui.Handlers;

namespace lproject.MAUI.CustomControls.DatePickerControl;

public partial class CalendarHandler : ViewHandler<Calendar, CalendarUI>
{
    private CalendarGridLayout _calendarGridLayout;
    protected override CalendarUI CreatePlatformView()
        => new CalendarUI();
    protected override void ConnectHandler(CalendarUI platformView)
    {
        base.ConnectHandler(platformView);
        _calendarGridLayout = VirtualView.GridLayout;
        platformView.SetVirtualView(VirtualView);
        platformView.OnActiveViewTypeChanged(VirtualView.GridLayout, VirtualView.CalendarCultureInfo);

        platformView.SetCellAppearance(VirtualView.CellAppearanceResolved);
        
    }

    
    protected override void DisconnectHandler(CalendarUI platformView)
    {
        base.DisconnectHandler(platformView);
    }

    public override Size GetDesiredSize(double widthConstraint, double heightConstraint)
    {
        var result =  base.GetDesiredSize(widthConstraint, heightConstraint);
        return result;
    }
    
    static void MapCalendarCultureInfo(CalendarHandler handler, Calendar view)
    {
        handler.PlatformView.SetCalendarCultureInfo(view.CalendarCultureInfo);
    }

    static void MapCalendarDayCellAppearance(CalendarHandler handler, Calendar view)
    {
        handler.PlatformView.SetCalendarDayCellAppearance(view.CalendarDayCellAppearance);
    }

    static void MapCalendarHeaderAppearance(CalendarHandler handler, Calendar view)
    {
        handler.PlatformView.SetCalendarHeaderAppearance(view.CalendarHeaderAppearance);
    }

    static void MapGridLayout(CalendarHandler handler, Calendar view)
    {
        handler.PlatformView.OnActiveViewTypeChanged(view.GridLayout, view.CalendarCultureInfo);
    }

    static void MapCellAppearance(CalendarHandler handler, Calendar view)
    {
        handler.PlatformView.SetCellAppearance(view.CellAppearanceResolved);
    }
} 

