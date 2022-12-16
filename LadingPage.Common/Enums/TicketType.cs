using LadingPage.Common.Enums.EnumAttribute;
using System;
using System.Collections.Generic;
using System.Text;

namespace LadingPage.Common.Enums
{
    public enum TicketType
    {
        [EnumDisplayName(DisplayName = "Vé Gold")]
        Gold = 1,
        [EnumDisplayName(DisplayName = "Vé Diamond")]
        Diamond = 2,
        [EnumDisplayName(DisplayName = "Vé Platinum")]
        Platium = 3
    }
}
