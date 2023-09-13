using System.ComponentModel;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Service.Biz.UiRestrictions.GraphQL.Dto;

public enum TypeOfClient
{
    [Description("Separate organization")]
    Organization,
    
    [Description("Network of organizations")]
    Network
}
