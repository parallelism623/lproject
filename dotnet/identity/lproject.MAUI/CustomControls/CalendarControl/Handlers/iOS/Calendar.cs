
namespace lproject.MAUI.CustomControls.DatePickerControl;
public partial class Calendar
{
    public CellAppearanceResolved CellAppearanceResolved
        => ActiveViewType switch
        {
            ActiveViewType.Month => DayCellAppearanceResolved.Resolve(CalendarDayCellAppearance),
            ActiveViewType.Year => DayCellAppearanceResolved.Resolve(CalendarDayCellAppearance),
            ActiveViewType.Decade => DayCellAppearanceResolved.Resolve(CalendarDayCellAppearance),
            _ => throw new ArgumentOutOfRangeException(nameof(ActiveViewType))
        };

}