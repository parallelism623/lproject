
#if IOS
using CalendarUI = lproject.MAUI.Platforms.iOS.CalendarView.CalendarUI;
#elif ANDROID
using CalendarUI = lproject.MAUI.Platforms.Android.CalendarUI;
#endif
using Microsoft.Maui.Handlers;

namespace lproject.MAUI.CustomControls.DatePickerControl;

public partial class CalendarHandler 
{
    public static IPropertyMapper<Calendar, CalendarHandler> PropertyMapper = new PropertyMapper<Calendar, CalendarHandler>(ViewHandler.ViewMapper)
    {
        [nameof(Calendar.CalendarCultureInfo)] = MapCalendarCultureInfo,
        [nameof(Calendar.CalendarDayCellAppearance)] = MapCalendarDayCellAppearance,
        [nameof(Calendar.CalendarHeaderAppearance)] = MapBackground,
        [nameof(Calendar.GridLayout)] = MapGridLayout,
        ["CellAppearance"] = MapCellAppearance
    };


    
    public static CommandMapper<Calendar, CalendarHandler> CommandMapper = new(ViewCommandMapper)
    {

    };
    public CalendarHandler() : base(PropertyMapper, CommandMapper)
    {
    }
    
    
}