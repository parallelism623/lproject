using CoreGraphics;
using Foundation;
using lproject.MAUI.CustomControls.DatePickerControl;
using UIKit;

namespace lproject.MAUI.Platforms.iOS.CalendarView;

public class CalendarGridDataDelegate :  UICollectionViewDelegate
{
    readonly CalendarGridDataUI _owner;

    public CalendarGridDataDelegate(CalendarGridDataUI owner)
    {
        _owner = owner;
    }

    public override void ItemSelected(
        UICollectionView collectionView,
        NSIndexPath indexPath)
    {
        _owner.OnDayTapped(indexPath.Row);
    }
 
    
}


public class CalendarDaysLayout : UICollectionViewFlowLayout
{
    public CalendarGridLayout CalendarGridLayout { get; set; }

    public CalendarDaysLayout()
    {
        CalendarGridLayout = new();
    }
    
    

    public override void PrepareLayout()
    {
        base.PrepareLayout();
        var width = CollectionView.Bounds.Width / CalendarGridLayout.Column; 
        ItemSize = new CGSize(width, CalendarGridLayout.CellHeight);

        MinimumInteritemSpacing = 0;
        MinimumLineSpacing = 0;
        SectionInset = UIEdgeInsets.Zero;
    }

    
    public override bool ShouldInvalidateLayoutForBoundsChange(CGRect newBounds)
        => true;

}

