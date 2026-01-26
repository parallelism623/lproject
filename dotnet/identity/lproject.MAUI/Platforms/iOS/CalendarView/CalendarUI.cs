
    using CoreGraphics;
    using lproject.MAUI.CustomControls.DatePickerControl;
    using lproject.MAUI.CustomControls.DatePickerControl.Appearances;
    using Microsoft.Maui.Platform;
    using UIKit;

    namespace lproject.MAUI.Platforms.iOS.CalendarView;


    public class CalendarUI : MauiView
    {
        private CalendarGridDataUI _calendarGridDataUi;
        private CalendarHeaderUI _calendarHeader;
        private Calendar _virtualView;
        private CalendarCultureInfo _calendarCultureInfo;
        private CalendarDaysOfWeekUI _calendarDaysOfWeekUI;
        public UIEdgeInsets Padding { get; set; } = new(8, 24, 8, 24);
        public CalendarUI() : base()
        {
             Initializer();
        }
        
        void Initializer()
        {
            BackgroundColor = UIColor.FromRGB(245, 243, 247);
            _calendarHeader = new(CGRect.Empty);
            _calendarHeader.PreviousTapped += () => OnMoveDifferentMonth(-1);
            _calendarHeader.NextTapped += () => OnMoveDifferentMonth(1);

            _calendarGridDataUi = new CalendarGridDataUI(CGRect.Empty, new());
            _calendarGridDataUi.SetOwner(this);

            _calendarDaysOfWeekUI = new(CGRect.Empty, new());
            
            AddSubviews(_calendarHeader, _calendarDaysOfWeekUI, _calendarGridDataUi);
        }
        
        
        #region VIRTUAL_VIEW

        public void OnChooseYearActiveView()
        {
            _virtualView.OnChooseYearActiveView();
        }

        public void OnChooseDecadeActiveView()
        {
            _virtualView.OnChooseDecadeActiveView();
        }

        public void OnChooseMonthActiveView()
        {
            _virtualView.OnChooseMonthActiveView();
        }
        public void SetVirtualView(Calendar virtualView)
        {
            _virtualView = virtualView;
        }

        public void OnMoveDifferentMonth(int step)
        {
            _virtualView?.OnMonthChange(step);
        }
        
        public void OnCellTapped(string cellData)
        {
            _virtualView?.OnCellTapped(cellData);
        }
        
        public void SetCalendarCultureInfo(CalendarCultureInfo ci)
        {
            _calendarCultureInfo = ci;
            OnCalendarCultureInfoChange();
        }
        #region GRID
        
        public void SetCalendarDayCellAppearance(AppearanceBase  appearance)
        {
            _calendarGridDataUi.SetCalendarDayCellAppearance(appearance);
        }
        
        public void SetCalendarHeaderAppearance(AppearanceBase  appearance)
        {
            _calendarHeader.SetCalendarHeaderAppearance(appearance);
        }
        
        public void SetData(IReadOnlyList<CalendarDay> days, CalendarGridLayout layout = null)
        {
            if (layout is not null)
            {
                _calendarGridDataUi.UpdateLayout(layout);   
            }

            _calendarGridDataUi.UpdateData(days);
        }

        public void SetCalendarTitle(string title)
        {
            _calendarHeader.SetTitle(title);
        }


        public void SetCalendarSubtitle(string subtitle)
        {
            _calendarHeader.SetSubTitle(subtitle);
        }

        public void SetCellAppearance(CellAppearanceResolved appearance)
        {
            _calendarGridDataUi.SetAppearance(appearance);
        }
        
        #endregion GRID
        
        
        #endregion VIRTUAL_VIEW

        #region GRID_DATA

        public void OnActiveViewTypeChanged(CalendarGridLayout layout, CalendarCultureInfo data)
        {
            _calendarCultureInfo = data;
            SetData(_calendarCultureInfo.Days, layout);
            SetCalendarTitle(_calendarCultureInfo.GetCurrentMonthName());
            SetCalendarSubtitle(_calendarCultureInfo.CurrentYear.ToString());
            SetDaysOfWeekName(_calendarCultureInfo.DaysOfWeek.Select(it => it.AbbreviatedName).ToList());
        }

        #endregion
        public void SetDaysOfWeekName(List<string> names)
        {
            _calendarDaysOfWeekUI.SetDaysOfWeek(names);
        }


        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            var bw = Bounds.Width;
            var bh = Bounds.Height;
            if (bw <= 0 || bh <= 0) return;

            nfloat y = 0;

            _calendarHeader.Frame = new CGRect(0, y, bw, 80);
            y += 80;

            var daysW = Math.Max(0, bw - Padding.Left - Padding.Right);
            _calendarDaysOfWeekUI.Frame = new CGRect(Padding.Left, y + Padding.Top, daysW, 48);
            y += 48;

            var gridFrame = _calendarGridDataUi.CalculateFrameInternal(Bounds, Padding);
            gridFrame.Y += y;
            gridFrame.Width = gridFrame.Width;
            gridFrame.Height = gridFrame.Height;

            _calendarGridDataUi.Frame = gridFrame;
        }

        

        #region PRIVATE_METHOD
        private void OnCalendarCultureInfoChange()
        {
            SetData(_calendarCultureInfo.Days);
            SetCalendarTitle(_calendarCultureInfo.GetCurrentMonthName());
            SetCalendarSubtitle(_calendarCultureInfo.CurrentYear.ToString());
            SetDaysOfWeekName(_calendarCultureInfo.DayNames);
        }
        
        private void RunOnUI(Action action)
        {
            if (MainThread.IsMainThread)
                action();
            else
                MainThread.BeginInvokeOnMainThread(action);
        }
        #endregion PRIVATE_METHOD
    }