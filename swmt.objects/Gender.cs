using System.ComponentModel;
using System.Runtime.Serialization;

namespace Swmt.Objects
{
    public enum Gender {
        [EnumMember(Value = "X")]
        Unknown = 0,

        [EnumMember(Value = "F")]
        Female = 1,

        [EnumMember(Value = "M")]
        Male = 2,
    }
}