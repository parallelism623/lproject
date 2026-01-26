using Android.Content;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using lproject.MAUI.CustomControls.DatePickerControl;

namespace lproject.MAUI.Platforms.Android;

public class CalendarUI : FrameLayout
{
    RecyclerView _recyclerView;
    DatePickerAdapter _adapter;
    Calendar? _virtualView;

    public CalendarUI(Context context) : base(context)
    {
        Initialize(context);
    }

    // tương đương SetVirtualView bên iOS
    public void SetVirtualView(Calendar view)
    {
        _virtualView = view;
    }

    void Initialize(Context context)
    {
        _recyclerView = new RecyclerView(context)
        {
            OverScrollMode = OverScrollMode.Never
        };

        _recyclerView.SetLayoutManager(
            new GridLayoutManager(context, 7));

        _adapter = new DatePickerAdapter();
        _recyclerView.SetAdapter(_adapter);

        AddView(_recyclerView, new LayoutParams(
            LayoutParams.MatchParent,
            LayoutParams.MatchParent));
    }

    // tương đương LayoutSubviews()
    protected override void OnLayout(
        bool changed, int left, int top, int right, int bottom)
    {
        base.OnLayout(changed, left, top, right, bottom);

        _recyclerView.Layout(0, 0, Width, Height);
    }

    // giống hệt iOS SetDays
    public void SetDays(IList<CalendarDay> days)
    {
        _adapter.SetDays(days);
    }
}