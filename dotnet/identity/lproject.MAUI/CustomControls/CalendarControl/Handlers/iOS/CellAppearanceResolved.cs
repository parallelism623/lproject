using lproject.MAUI.CustomControls.DatePickerControl.Appearances;
using UIKit;

namespace lproject.MAUI.CustomControls.DatePickerControl;

public class CellAppearanceResolved
{
    public UIColor? TextColor { get; protected set; }
    public UIFont? Font { get; protected set; }
    public UITextAlignment TextAlignment { get; protected set; } = UITextAlignment.Center;
    public UIColor? BackgroundColor { get; protected set; }
    public UIColor? BorderColor { get; protected set; }
    public nfloat BorderWidth { get; protected set; }
    public nfloat CornerRadius { get; protected set; }
    public UIEdgeInsets Padding { get; protected set; }

    protected void ResolveBase(AppearanceBase a)
    {
        if (a == null) return;

        TextColor = a.TextColor.ToUIColorOrNull();
        Font = a.FontAttributes.ToUIFont(a.FontFamily, a.FontSize);
        TextAlignment = a.HorizontalTextAlignment.ToUITextAlignment();
        BackgroundColor = a.BackgroundColor.ToUIColorOrNull();
        BorderColor = a.BorderColor.ToUIColorOrNull();
        BorderWidth = a.BorderThickness.ToNFloat();
        CornerRadius = a.CornerRadius.ToNFloat();
        Padding = a.Padding.ToUIInsets();
    }
}
public class DayCellAppearanceResolved : CellAppearanceResolved
{
    public UIColor? TodayTextColor { get; private set; }
    public UIColor? DisabledTextColor { get; private set; }
    public UIColor? OutOfMonthTextColor { get; private set; }
    public UIColor? SelectedTextColor { get; private set; }
    public UIColor? StartedTextColor { get; private set; }
    public UIColor? EndTextColor { get; private set; }

    public UIColor? SelectedBackgroundColor { get; private set; }
    public UIColor? InRangeBackgroundColor { get; private set; }
    public UIColor? EndTextBackgroundColor { get; private set; }
    public UIColor? StartedTextBackgroundColor { get; private set; }
    public UIColor? TodayBackgroundColor { get; private set; }

    public static DayCellAppearanceResolved Resolve(
        CalendarDayCellAppearance a)
    {
        var result = new DayCellAppearanceResolved();
        
        result.ResolveBase(a);

        if (a == null)
            return result;

        result.TodayTextColor = a.TodayTextColor.ToUIColorOrNull();
        result.DisabledTextColor = a.DisabledTextColor.ToUIColorOrNull();
        result.OutOfMonthTextColor = a.OutOfMonthTextColor.ToUIColorOrNull();
        result.SelectedTextColor = a.SelectedTextColor.ToUIColorOrNull();
        result.StartedTextColor = a.StartedTextColor.ToUIColorOrNull();
        result.EndTextColor = a.EndTextColor.ToUIColorOrNull();

        result.SelectedBackgroundColor = a.SelectedBackgroundColor.ToUIColorOrNull();
        result.InRangeBackgroundColor = a.InRangeBackgroundColor.ToUIColorOrNull();
        result.EndTextBackgroundColor = a.EndTextBackgroundColor.ToUIColorOrNull();
        result.StartedTextBackgroundColor = a.StartedTextBackgroundColor.ToUIColorOrNull();
        result.TodayBackgroundColor = a.TodayBackgroundColor.ToUIColorOrNull();

        return result;
    }
}