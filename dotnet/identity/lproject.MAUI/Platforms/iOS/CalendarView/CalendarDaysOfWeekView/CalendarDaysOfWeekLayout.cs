using CoreGraphics;
using UIKit;

namespace lproject.MAUI.Platforms.iOS.CalendarView;

public class CalendarDaysOfWeekLayout : UICollectionViewFlowLayout
{
    const int Columns = 7;

    public override void PrepareLayout()
    {
        base.PrepareLayout();
        var width = CollectionView.Bounds.Width / Columns;
        ItemSize = new CGSize(width, CollectionView.Bounds.Height);

        MinimumInteritemSpacing = 0;
        MinimumLineSpacing = 0;
        SectionInset = UIEdgeInsets.Zero;
    }

    public override bool ShouldInvalidateLayoutForBoundsChange(CGRect newBounds)
        => true;
}