using Foundation;
using lproject.MAUI.CustomControls.DatePickerControl;
using lproject.MAUI.CustomControls.DatePickerControl.Appearances;
using UIKit;

namespace lproject.MAUI.Platforms.iOS.CalendarView;

public class CalendarGridDataSource  : UICollectionViewDataSource
{
    IReadOnlyList<CalendarData> _days;
    CellAppearanceResolved? _cellAppearance;
    public CalendarGridDataSource()
    {
        _days = Array.Empty<CalendarData>();
    }
    
    public void UpdateDays(IReadOnlyList<CalendarData> days)
    {
        _days = days ?? Array.Empty<CalendarData>();
    }

    public void UpdateAppearanceBase(CellAppearanceResolved cellAppearance)
    {
        _cellAppearance = cellAppearance;
        
    }

    public override nint GetItemsCount(UICollectionView collectionView, nint section)
        => _days.Count;

    public string GetCellDataWhenTapped(int index)
    {
        
        if ((uint)index < (uint)_days.Count)
        {
            return _days[index].GetCellData();
        }
        
        return string.Empty;
    }

    public override UICollectionViewCell GetCell(
        UICollectionView collectionView,
        NSIndexPath indexPath)
    {
        var cell = (CalendarGridDataCellUI)
            collectionView.DequeueReusableCell(
                CalendarGridDataCellUI.Key, indexPath);

        cell.Bind(_days[indexPath.Row], _cellAppearance);
        return cell;
    }
}