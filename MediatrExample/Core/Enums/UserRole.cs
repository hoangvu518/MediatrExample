using System.ComponentModel;

namespace MediatrExample.Core.Enums
{
    public enum UserRole
    {
        [Description("Website admin")]
        Admin = 1,

        [Description("Standard user")]
        User = 2,

        [Description("Super user")]
        SuperUser = 3
    }
}
