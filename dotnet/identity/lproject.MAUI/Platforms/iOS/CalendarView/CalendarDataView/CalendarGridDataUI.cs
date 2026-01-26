using CoreGraphics;
using Foundation;
using lproject.MAUI.CustomControls.DatePickerControl;
using lproject.MAUI.CustomControls.DatePickerControl.Appearances;
using ObjCRuntime;
using UIKit;

namespace lproject.MAUI.Platforms.iOS.CalendarView;
public class CalendarGridDataUI : UICollectionView
{
    CalendarGridDataSource? _dataSource;
    CalendarGridDataDelegate? _delegate;
    CalendarDaysLayout? _layout;
    AppearanceBase? _cellAppearance;
    CalendarUI? _owner;
    public CalendarGridDataUI(CGRect frame, CalendarDaysLayout layout)
        : base(frame, layout)
    {
        _layout = layout;
        Initialize();
    }

    public void SetCalendarDayCellAppearance(AppearanceBase cellAppearance)
    {
        _cellAppearance = cellAppearance;

    }
    public void SetOwner(CalendarUI owner)
    {
        _owner = owner;
    }
    

    void Initialize()
    {
        BackgroundColor = UIColor.Clear;
        ScrollEnabled = false;
        RegisterClassForCell(typeof(CalendarGridDataCellUI), CalendarGridDataCellUI.Key);
        _dataSource = new();
        _delegate = new(this);
        DataSource =  _dataSource;
        Delegate =  _delegate;
    }

    public void SetAppearance(CellAppearanceResolved appearance)
    {
        _dataSource?.UpdateAppearanceBase(appearance);

        foreach (CalendarGridDataCellUI cell in VisibleCells)
            cell.SetAppearance(appearance);
    }
    public CGRect CalculateFrameInternal(CGRect parentBound, UIEdgeInsets padding)
    {
        var width = parentBound.Width - padding.Right - padding.Left;
        

        var h = _layout!.CalendarGridLayout.Row * _layout!.CalendarGridLayout.CellHeight;
        h = Math.Max(0, h);

        return new CGRect(padding.Left, padding.Top, width, h);
    }
    
    
    public void UpdateLayout(CalendarGridLayout layout)
    {
        _layout?.CalendarGridLayout = layout;
        CollectionViewLayout.InvalidateLayout();
    }

    public void UpdateData(IReadOnlyList<CalendarData> data)
    {
        _dataSource?.UpdateDays(data);
        ReloadData();
    }
    public void OnDayTapped(int index)
    {
        if (_dataSource == null)
            return;

        var day = _dataSource.GetCellDataWhenTapped(index);
        _owner?.OnCellTapped(day);
    }
    
    public string GetCellDataWhenTapped(int index)
        => _dataSource?.GetCellDataWhenTapped(index);

    private void RunOnUI(Action action)
    {
        if (MainThread.IsMainThread)
            action();
        else
            MainThread.BeginInvokeOnMainThread(action);
    }
    
    

}

