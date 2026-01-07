using System.Globalization;

namespace QuanLyTiemDaQuy.Maui.Converters;

/// <summary>
/// Inverts a boolean value
/// </summary>
public class InvertedBoolConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool boolValue)
            return !boolValue;
        return value;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool boolValue)
            return !boolValue;
        return value;
    }
}

/// <summary>
/// Converts decimal to Vietnamese currency format
/// </summary>
public class CurrencyConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is decimal decimalValue)
            return $"{decimalValue:N0} ₫";
        return value?.ToString();
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

/// <summary>
/// Converts tier to background color
/// </summary>
public class TierToColorConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string tier)
        {
            return tier switch
            {
                "VVIP" => Color.FromArgb("#FFD700"), // Gold
                "VIP" => Color.FromArgb("#C0C0C0"),  // Silver
                _ => Color.FromArgb("#6750A4")       // Primary
            };
        }
        return Color.FromArgb("#6750A4");
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

/// <summary>
/// Converts stock quantity to color indicator
/// </summary>
public class StockToColorConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is int stock)
        {
            return stock switch
            {
                <= 0 => Color.FromArgb("#F44336"), // Error/Red
                <= 5 => Color.FromArgb("#FF9800"), // Warning/Orange
                _ => Color.FromArgb("#4CAF50")     // Success/Green
            };
        }
        return Color.FromArgb("#4CAF50");
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

/// <summary>
/// Converts stone type name to image source for gemstone icons
/// </summary>
public class StoneTypeToIconConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        string imageName = "diamond.png"; // Default
        
        if (value is string typeName && !string.IsNullOrEmpty(typeName))
        {
            string lower = typeName.ToLower();
            if (lower.Contains("ruby") || lower.Contains("hồng ngọc"))
                imageName = "ruby.png";
            else if (lower.Contains("emerald") || lower.Contains("lục bảo"))
                imageName = "emerald.png";
            else if (lower.Contains("sapphire") || lower.Contains("sa phia") || lower.Contains("bích ngọc"))
                imageName = "sapphire.png";
            else if (lower.Contains("kim cương") || lower.Contains("diamond"))
                imageName = "diamond.png";
        }
        
        return imageName;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
