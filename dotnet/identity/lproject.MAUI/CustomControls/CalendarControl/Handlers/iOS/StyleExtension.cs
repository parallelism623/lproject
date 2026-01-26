
using UIKit;


namespace lproject.MAUI.CustomControls.DatePickerControl;

public static class StyleExtension
{

    public static UIColor ToUIColor(this Color color)
    {
        return UIColor.FromRGB(color.Red * 255, color.Green * 255, color.Blue * 255);
    }
    public static UIColor? ToUIColorOrNull(this Color? c)
        => c is null ? null : c.ToUIColor();

    public static nfloat ToNFloat(this double? v, double fallback = 0)
        => (nfloat)(v ?? fallback);

    public static UIEdgeInsets ToUIInsets(this Thickness? t)
        => t is null ? UIEdgeInsets.Zero : new UIEdgeInsets(
                (nfloat)t.Value.Top,
                (nfloat)t.Value.Left,
                (nfloat)t.Value.Bottom,
                (nfloat)t.Value.Right);

    public static UITextAlignment ToUITextAlignment(
        this TextAlignment? a,
        UITextAlignment fallback = UITextAlignment.Center)
        => a switch
        {
            TextAlignment.Start => UITextAlignment.Left,
            TextAlignment.Center => UITextAlignment.Center,
            TextAlignment.End => UITextAlignment.Right,
            _ => fallback
        };

    public static UIFont ToUIFont(
        this FontAttributes? attr,
        string fontFamily,
        double? fontSize,
        double defaultSize = 14)
    {
        var size = (nfloat)(fontSize ?? defaultSize);

        var weight = attr switch
        {
            FontAttributes.Bold => UIFontWeight.Bold,
            _ => UIFontWeight.Regular
        };

        if (!string.IsNullOrEmpty(fontFamily))
            return UIFont.FromName(fontFamily, size) ?? UIFont.SystemFontOfSize(size, weight);

        return UIFont.SystemFontOfSize(size, weight);
    }
}