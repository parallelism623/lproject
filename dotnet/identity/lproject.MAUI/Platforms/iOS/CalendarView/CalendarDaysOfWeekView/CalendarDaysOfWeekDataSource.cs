using Foundation;
using lproject.MAUI.CustomControls.DatePickerControl.Appearances;
using UIKit;

namespace lproject.MAUI.Platforms.iOS.CalendarView;


public class CalendarDaysOfWeekDataSource : UICollectionViewDataSource
{
    List<string> _daysOfWeek;

    public CalendarDaysOfWeekDataSource()
    {
        _daysOfWeek = new();
    }

    public void UpdateDays(List<string> daysOfWeek)
    {
        _daysOfWeek = daysOfWeek;
    }

    public override nint GetItemsCount(UICollectionView collectionView, nint section)
        => _daysOfWeek.Count;

    public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
    {
        var cell = (CalendarDaysOfWeekCellUI)
            collectionView.DequeueReusableCell(CalendarDaysOfWeekCellUI.Key, indexPath);

        cell.Bind(_daysOfWeek[indexPath.Row]);
        return cell;
    }
}