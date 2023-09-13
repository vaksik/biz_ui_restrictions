using System.Reflection;
using Service.Biz.UiRestrictions.BLL.Infrastructure.Attributes;

namespace Service.Biz.UiRestrictions.BLL.Infrastructure.Extensions;

public static class EnumExtensions
{
    public static string GetDataViewStringValue(this Enum value)
    {
        var type = value.GetType();
        var field = type.GetField(value.ToString());
        var attribute = field!.GetCustomAttributes(typeof(DataViewAttribute), false) as DataViewAttribute[];
        return attribute!.Length > 0 ? attribute[0].Value : value.ToString();
    }
    
    
}