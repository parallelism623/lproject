using CoreGraphics;
using lproject.MAUI.CustomControls.DatePickerControl;
using lproject.MAUI.CustomControls.DatePickerControl.Appearances;
using UIKit;
using Microsoft.Maui.Dispatching;

namespace lproject.MAUI.Platforms.iOS.CalendarView;

public class CalendarHeaderUI : UIView
{
    string _title = string.Empty;
    string _subTitle = string.Empty;

    UILabel _titleLabel;
    UILabel _subTitleLabel;
    UIButton _previousButton;
    UIButton _nextButton;
    private AppearanceBase _appearance;

    // Padding mặc định (bạn chỉnh sau cũng được)
    UIEdgeInsets _padding = new(8, 12, 8, 12);

    public event Action? PreviousTapped;
    public event Action? NextTapped;

    public CalendarHeaderUI(CGRect frame) : base(frame)
    {
        Initialize();
    }

    void Initialize()
    {
        BackgroundColor = UIColor.FromRGB(103, 80, 164);

        _titleLabel = new UILabel
        {
            TextAlignment = UITextAlignment.Center,
            Font = UIFont.BoldSystemFontOfSize(30),
            TextColor = UIColor.White,
            Lines = 1
        };

        _subTitleLabel = new UILabel
        {
            TextAlignment = UITextAlignment.Center,
            Font = UIFont.SystemFontOfSize(16),
            TextColor = UIColor.White,
            Lines = 1
        };

        

        var config = UIImageSymbolConfiguration.Create(
            pointSize: 11,
            weight: UIImageSymbolWeight.Semibold
        );
    
        var left  = UIImage.GetSystemImage("chevron.left", config);
        var right = UIImage.GetSystemImage("chevron.right", config);
        
        _previousButton = UIButton.FromType(UIButtonType.System);
        _nextButton     = UIButton.FromType(UIButtonType.System);
        _previousButton.SetImage(left, UIControlState.Normal);
        _nextButton.SetImage(right, UIControlState.Normal);

        _previousButton.TintColor = UIColor.White;
        _nextButton.TintColor = UIColor.White;


        _previousButton.TouchUpInside += (_, _) => PreviousTapped?.Invoke();
        _nextButton.TouchUpInside += (_, _) => NextTapped?.Invoke();

        AddSubviews(
            _subTitleLabel,
            _titleLabel,
            _previousButton,
            _nextButton
        );
    }

    // ===== Public API =====

    public void SetCalendarHeaderAppearance(AppearanceBase appearance)
    {
        _appearance = appearance;   
    }
    public void SetTitle(string title)
    {
        _title = title;
        RunOnUI(() => _titleLabel.Text = title);
    }

    public void SetSubTitle(string subTitle)
    {
        _subTitle = subTitle;
        RunOnUI(() => _subTitleLabel.Text = subTitle);
    }



    public override void LayoutSubviews()
    {
        base.LayoutSubviews();
    
        var contentBounds = new CGRect(
            _padding.Left,
            _padding.Top,
            Bounds.Width - _padding.Left - _padding.Right,
            Bounds.Height - _padding.Top - _padding.Bottom
        );

        const float buttonSize = 44;
        const float titleHeight = 22;
        const float subTitleHeight = 16;
        const float spacing = 2;

        float y = 0;

        
        _subTitleLabel.Frame = new CGRect(
            0,
            4,
            Bounds.Width,
            titleHeight + 2
        );

        y += titleHeight + 6;
        
        _titleLabel.Frame = new CGRect(
            0,
            y,
            Bounds.Width,
            Bounds.Height - y
        );
        _previousButton.Frame = new CGRect(
            18,
            y,
            buttonSize,
            Bounds.Height - y
        );


        _nextButton.Frame = new CGRect(
            Bounds.Width - buttonSize - 18,
            y,
            buttonSize,
            Bounds.Height - y
        );



    }

    // ===== Helper =====

    void RunOnUI(Action action)
    {
        if (MainThread.IsMainThread)
            action();
        else
            MainThread.BeginInvokeOnMainThread(action);
    }
}
