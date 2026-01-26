using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using lproject.MAUI.CustomControls.DatePickerControl;

namespace lproject.MAUI.Platforms.Android;

public class DatePickerAdapter : RecyclerView.Adapter
{
    IList<CalendarDay> _days = Array.Empty<CalendarDay>();

    public void SetDays(IList<CalendarDay> days)
    {
        _days = days ?? Array.Empty<CalendarDay>();
        NotifyDataSetChanged();
    }

    public override int ItemCount => _days.Count;

    public override RecyclerView.ViewHolder OnCreateViewHolder(
        ViewGroup parent, int viewType)
    {
        // giống iOS: cell rất nhẹ
        var textView = new TextView(parent.Context)
        {
            Gravity = GravityFlags.Center,
            TextSize = 14
        };

        var height = (int)(48 * parent.Context.Resources.DisplayMetrics.Density);
        textView.LayoutParameters = new ViewGroup.LayoutParams(
            ViewGroup.LayoutParams.MatchParent,
            height);

        return new DatePickerDayViewHolder(textView);
    }

    public override void OnBindViewHolder(
        RecyclerView.ViewHolder holder, int position)
    {
        ((DatePickerDayViewHolder)holder).Bind(_days[position]);
    }
}