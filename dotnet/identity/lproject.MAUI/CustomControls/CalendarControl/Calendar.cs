using lproject.MAUI.CustomControls.DatePickerControl.Appearances;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Platform;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace lproject.MAUI.CustomControls.DatePickerControl;

public interface IDatePicker;

public partial class Calendar : View
{
    bool _isNormalizing;
    SelectionState _selectionState = SelectionState.None;
    public CalendarGridLayout GridLayout { get; set; }
    public CalendarCultureInfo CalendarCultureInfo { get; private set; }
    
    public Calendar()
    {
        Initializer();
    }
    private void Initializer()
    {
        CalendarCultureInfo = CalendarCultureInfo.CreateCalendarCultureInfo(DisplayDate, CultureInfo.CurrentCulture);
        CalculateCalendarDaysInMonth();
        GridLayout = new();
        ActiveViewType = ActiveViewType.Month;
    }
    

    
    static void OnCalendarBackgroundColorChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        var calendar = (Calendar)bindable;
        calendar.OnBackgroundChanged();
    }

    void OnBackgroundChanged()
    {
        
        Handler?.UpdateValue(nameof(CalendarHeaderAppearance));
    }
    
    public static readonly BindableProperty CalendarHeaderAppearanceProperty =
        BindableProperty.Create(
            nameof(CalendarHeaderAppearance),
            typeof(CalendarHeaderAppearance),
            typeof(Calendar),
            CalendarHeaderAppearance.CreateDefaultValue(),
            BindingMode.TwoWay,
            propertyChanged: OnDateChanged);
    
    
    public CalendarHeaderAppearance CalendarHeaderAppearance
    {
        get => (CalendarHeaderAppearance)GetValue(CalendarHeaderAppearanceProperty);
        set => SetValue(CalendarHeaderAppearanceProperty, value);
    }
    public static readonly BindableProperty CalendarDayCellAppearanceProperty =
        BindableProperty.Create(
            nameof(CalendarDayCellAppearance),
            typeof(CalendarDayCellAppearance),
            typeof(Calendar),
            CalendarDayCellAppearance.CreateDefaultValue(),
            BindingMode.TwoWay,
            propertyChanged: OnDateChanged);

    public CalendarDayCellAppearance CalendarDayCellAppearance
    {
        get => (CalendarDayCellAppearance)GetValue(CalendarDayCellAppearanceProperty);
        set => SetValue(CalendarDayCellAppearanceProperty, value);
    }
    public static readonly BindableProperty StartDateProperty =
        BindableProperty.Create(
            nameof(StartDate),
            typeof(DateTime?),
            typeof(Calendar),
            null,
            BindingMode.TwoWay,
            propertyChanged: OnDateChanged);

    public static readonly BindableProperty EndDateProperty =
        BindableProperty.Create(
            nameof(EndDate),
            typeof(DateTime?),
            typeof(Calendar),
            null,
            BindingMode.TwoWay,
            propertyChanged: OnDateChanged);

    public static readonly BindableProperty DisplayDateProperty =
        BindableProperty.Create(
            nameof(DisplayDate),
            typeof(DateTime),
            typeof(Calendar),
            DateTime.Now,
            BindingMode.TwoWay,
            propertyChanged: OnDisplayDateChanged);

    public static readonly BindableProperty ActiveViewTypeProperty =
        BindableProperty.Create(
            nameof(ActiveViewType),
            typeof(ActiveViewType),
            typeof(Calendar),
            ActiveViewType.Month,
            BindingMode.TwoWay, propertyChanged: OnActiveViewTypeChanged);

    public DateTime? StartDate
    {
        get => (DateTime?)GetValue(StartDateProperty);
        set => SetValue(StartDateProperty, value);
    }

    private static void OnActiveViewTypeChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var calendar =  (Calendar)bindable;
        calendar.RecalculateGridLayout();
        calendar.CalculateCalendarDaysInMonth();
    }

    public void OnChooseMonthActiveView()
    {
        ActiveViewType = ActiveViewType.Month;
    }
    public void OnChooseYearActiveView()
    {
        ActiveViewType = ActiveViewType.Year;
    }
    public void OnChooseDecadeActiveView()
    {
        ActiveViewType = ActiveViewType.Decade;
    }
    public void RecalculateGridLayout()
    {
        GridLayout = CalendarGridLayout.GetGridLayoutByViewType(ActiveViewType);
     
    }

    public DateTime? EndDate
    {
        get => (DateTime?)GetValue(EndDateProperty);
        set => SetValue(EndDateProperty, value);
    }

    public DateTime DisplayDate
    {
        get => (DateTime)GetValue(DisplayDateProperty);
        set => SetValue(DisplayDateProperty, value);
    }

    public ActiveViewType ActiveViewType
    {
        get => (ActiveViewType)GetValue(ActiveViewTypeProperty);
        set => SetValue(ActiveViewTypeProperty, value);
    }

    static void OnDisplayDateChanged(BindableObject bindable, object oldValue, object newValue)
    {
        ((Calendar)bindable).OnDisplayDateChanged();
    }

    static void OnDateChanged(BindableObject bindable, object oldValue, object newValue)
    {
        ((Calendar)bindable).NormalizeDates();
    }

    void OnDisplayDateChanged()
    {
        CalendarCultureInfo.CurrentMonth = DisplayDate.Month;
        CalendarCultureInfo.CurrentYear = DisplayDate.Year;
        OnRecalculateCalendarDaysInMonth();
    }

    void NormalizeDates()
    {
        if (_isNormalizing) return;

        try
        {
            _isNormalizing = true;

            if (StartDate.HasValue && EndDate.HasValue && StartDate > EndDate)
                EndDate = StartDate;
        }
        finally
        {
            _isNormalizing = false;
        }
    }

    public void OnCellTapped(string cellData)
    {
        if (ActiveViewType is ActiveViewType.Month)
        {
            CalendarHelper.TryParseDate(cellData, CalendarCultureInfo.CurrentCulture, out DateTime date);
            SelectDate(date);
        }
    }

    public void SelectDate(DateTime date)
    {
        date = date.Date;

        if (_selectionState == SelectionState.None)
        {
            StartDate = date;
            EndDate = date;
            _selectionState = SelectionState.Both;
        }
        else
        {
            if (StartDate.HasValue && EndDate.HasValue &&
                (date == StartDate.Value.Date || date == EndDate.Value.Date))
            {
                StartDate = EndDate = date;
            }
            else if (StartDate.HasValue && date > StartDate.Value.Date)
            {
                EndDate = date;
            }
            else
            {
                StartDate = date;
            }
        }

        OnRecalculateCalendarDaysInMonth();
    }

    public void OnResetRange()
    {
        StartDate = null;
        EndDate = null;
        _selectionState = SelectionState.None;
        OnRecalculateCalendarDaysInMonth();
    }

    public void OnMonthChange(int step)
    {
        CalendarCultureInfo.ChangeMonth(step);
        OnRecalculateCalendarDaysInMonth();
        Handler?.UpdateValue(nameof(CalendarCultureInfo));
    }

    void OnRecalculateCalendarDaysInMonth()
    {
        CalculateCalendarDaysInMonth();
        Handler?.UpdateValue(nameof(Calendar.CalendarCultureInfo));
    }

    void CalculateCalendarDaysInMonth(ActiveViewType  activeViewType = ActiveViewType.Month)
    {
        CalendarCultureInfo.GetMonthViewDays(); 

        if (_selectionState == SelectionState.Both &&
            StartDate.HasValue &&
            EndDate.HasValue)
        {
            foreach (var d in CalendarCultureInfo.Days)
                d.IsSelected = d.Date >= StartDate && d.Date <= EndDate;
        }
    }
}


public enum ActiveViewType
{
    Month,
    Decade,
    Year
}

public enum SelectionState
{
    None,
    StartDate,
    EndDate,
    Both
}