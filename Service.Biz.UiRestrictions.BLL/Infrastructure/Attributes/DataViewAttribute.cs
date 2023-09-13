namespace Service.Biz.UiRestrictions.BLL.Infrastructure.Attributes;

[AttributeUsage(AttributeTargets.Field)]
public class DataViewAttribute : Attribute
{
    public string Value { get; }

    public DataViewAttribute(string value) => Value = value;
}

