using CoreGraphics;
using lproject.MAUI.CustomControls.DatePickerControl.Appearances;
using UIKit;

namespace lproject.MAUI.Platforms.iOS.CalendarView;

public class CalendarDaysOfWeekUI : UICollectionView
{

    readonly CalendarDaysOfWeekDataSource _dataSource;

    public CalendarDaysOfWeekUI(CGRect frame, CalendarDaysOfWeekLayout layout) : base(frame, layout)
    {

        BackgroundColor =  UIColor.Clear;
        RegisterClassForCell(
            typeof(CalendarDaysOfWeekCellUI),
            CalendarDaysOfWeekCellUI.Key);

        _dataSource = new CalendarDaysOfWeekDataSource();
        DataSource = _dataSource;
        
    }


    public void SetDaysOfWeek(List<string> daysOfWeek)
    {
        _dataSource.UpdateDays(daysOfWeek);
        RunOnUI(ReloadData);
    }
    
    private void RunOnUI(Action action)
    {
        if (MainThread.IsMainThread)
            action();
        else
            MainThread.BeginInvokeOnMainThread(action);
    }
}