using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace QuanLyTiemDaQuy.Maui.Converters;

/// <summary>
/// Converts product name/type to appropriate gem SVG icon
/// MAUI converts SVG to PNG at build time, reference without extension
/// </summary>
public class ProductImageConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        // Get product name or image URL
        var input = value?.ToString()?.ToLowerInvariant() ?? "";
        
        // If empty or null, return default diamond icon
        if (string.IsNullOrWhiteSpace(input))
        {
            return "gem_diamond";
        }

        // If it's already a valid URL or file path, use it
        if (input.StartsWith("http") || input.EndsWith(".png") || input.EndsWith(".jpg"))
        {
            return input;
        }

        // Match based on gemstone type in the name (Vietnamese first!)
        // Ruby - Hồng ngọc
        if (input.Contains("ruby") || input.Contains("hồng ngọc"))
        {
            return "❤️";
        }
        
        // Emerald - Ngọc lục bảo
        if (input.Contains("emerald") || input.Contains("ngọc lục bảo") || input.Contains("lục bảo"))
        {
            return "💚";
        }
        
        // Sapphire - Sapphire (bích ngọc)
        if (input.Contains("sapphire") || input.Contains("bích ngọc"))
        {
            return "💙";
        }
        
        // Pearl - Ngọc trai
        if (input.Contains("pearl") || input.Contains("ngọc trai"))
        {
            return "🤍";
        }
        
        // Quartz - Thạch anh  
        if (input.Contains("quartz") || input.Contains("thạch anh"))
        {
            return "💜";
        }
        
        // Diamond - Kim cương (default)
        return "💎";
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
