
using CoreGraphics;
using Foundation;
using lproject.MAUI.CustomControls.DatePickerControl;
using lproject.MAUI.CustomControls.DatePickerControl.Appearances;
using UIKit;

namespace lproject.MAUI.Platforms.iOS.CalendarView;

public class CalendarGridDataCellUI  : UICollectionViewCell
{
    public static readonly NSString Key =
        new("CalendarDayCell");
    UIView _circle;
    UILabel _label;
    private CalendarData _data;
    private CellAppearanceResolved  _appearance;
    [Export("initWithFrame:")]
    public CalendarGridDataCellUI(CGRect frame) : base(frame)
    {
        BackgroundColor = UIColor.Clear;
        _circle = new UIView
        {
            BackgroundColor = UIColor.Clear
        };
        _label = new UILabel
        {
            TextAlignment = UITextAlignment.Center,
            Font = UIFont.SystemFontOfSize(16, UIFontWeight.Medium),
            TextColor = UIColor.FromRGB(33, 33, 33)
        };

        _circle.AddSubview(_label);
        AddSubviews(_circle);
    }

    public override void LayoutSubviews()
    {
        base.LayoutSubviews();

        var w = ContentView.Bounds.Width;
        var h = ContentView.Bounds.Height;
        if (w <= 0 || h <= 0) return;

        var size = Math.Min(w, h);
        var circleSize = size - 10;
        if (circleSize <= 0) return;

        _circle.Frame = new CGRect((w - circleSize)/2, (h - circleSize)/2, circleSize, circleSize);
        _circle.Layer.CornerRadius = (nfloat)(circleSize / 2);
        _label.Frame = _circle.Bounds;
    }
    
    public void Bind(CalendarData data, CellAppearanceResolved cellAppearance)
    {
        _data = data;
        _appearance = cellAppearance;
        _label.Text = data.GetCellLabel();

        // Reset
        _circle.BackgroundColor = UIColor.Clear;
        _label.TextColor = UIColor.FromRGB(33, 33, 33);
        ApplyAppearance();

    }

    public void SetAppearance(CellAppearanceResolved cellAppearance)
    {
        _appearance = cellAppearance;
        ApplyAppearance();
    }

    public void ApplyAppearance()
    {
        Console.WriteLine(_circle.Frame);
    
        var day = _data as CalendarDay;
        var dayAppearance = _appearance as DayCellAppearanceResolved;
        if (day is null || dayAppearance is null)
        {
            return;
        }
        if (day.IsSelected)
        {
            _circle.BackgroundColor = dayAppearance.SelectedBackgroundColor;
            _label.TextColor = dayAppearance.SelectedTextColor;
            return;
        } 
        if (day.IsOutOfMonth)
        {
            _label.TextColor = dayAppearance.OutOfMonthTextColor;
            return;
        }
        if (day.Date == DateTime.Today)
        {
            _circle.BackgroundColor = dayAppearance.TodayBackgroundColor;
            _label.TextColor =  dayAppearance.TodayTextColor;    
        }
        SetNeedsLayout();
    }
}


