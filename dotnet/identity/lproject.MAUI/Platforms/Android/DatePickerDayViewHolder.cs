using Android.Graphics;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using lproject.MAUI.CustomControls.DatePickerControl;
using Color = Android.Graphics.Color;
using View = Android.Views.View;

namespace lproject.MAUI.Platforms.Android;

class DatePickerDayViewHolder : RecyclerView.ViewHolder
{
    readonly TextView _label;

    public DatePickerDayViewHolder(View itemView) : base(itemView)
    {
        // itemView được tạo là TextView (giống UILabel bên iOS)
        _label = (TextView)itemView;

        _label.Gravity = GravityFlags.Center;
        _label.TextSize = 14;

        // padding cho đẹp giống calendar native
        int padding = (int)(8 * itemView.Context.Resources.DisplayMetrics.Density);
        _label.SetPadding(padding, padding, padding, padding);
    }

    public void Bind(CalendarDay day)
    {
        _label.Text = day.Date.Day.ToString();

        // ----- text color -----
        if (day.IsOutOfMonth)
            _label.SetTextColor(Color.Gray);
        else
            _label.SetTextColor(Color.Black);

        // ----- background / today highlight -----
        if (day.IsToday)
        {
            _label.SetBackgroundColor(Color.ParseColor("#2196F3")); // giống iOS SystemBlue
            _label.SetTextColor(Color.White);
        }
        else
        {
            _label.SetBackgroundColor(Color.Transparent);
        }
    }
}