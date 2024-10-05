using System.ComponentModel;

namespace StackLab.Survey.Domain.Common.Enums;
public enum Status
{
    [Description("Inactive")]
    Inactive = 0,

    [Description("Active")]
    Active = 1,
}
