using CoreGraphics;
using Foundation;
using UIKit;

namespace lproject.MAUI.Platforms.iOS.CalendarView;
public class CalendarDaysOfWeekCellUI : UICollectionViewCell
{
    public static readonly NSString Key = new(nameof(CalendarDaysOfWeekCellUI));

    readonly UILabel _label;
    [Export("initWithFrame:")]
    public CalendarDaysOfWeekCellUI(CGRect frame) : base(frame)
    {
        _label = new UILabel
        {
            TextAlignment = UITextAlignment.Center,
            Font = UIFont.SystemFontOfSize(15, UIFontWeight.Semibold),
            TextColor = UIColor.FromRGB(33, 33, 33)
        };

        AddSubviews(_label);
    }

    public override void LayoutSubviews()
    {
        base.LayoutSubviews();

        var height = _label.Font.LineHeight;
        _label.Frame = Bounds;
    }


    public void Bind(string text)
    {
        _label.Text = text;
    }
}
