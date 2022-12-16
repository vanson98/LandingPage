using LadingPage.Common.Enums.EnumAttribute;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace LadingPage.Common.Enums.Extension
{
    public static class EnumConverter
    {
        public static SelectList ToSelectList<TEnum>(this TEnum obj, object selectedValue)
        {
            var selectListItems = Enum.GetValues(typeof(TEnum)).OfType<Enum>().Select(x => new SelectListItem
            {
                Text = x.DisplayName(),
                Value = (Convert.ToInt32(x)).ToString(),
                Selected = (int)selectedValue == Convert.ToInt32(x)
            });
            return new SelectList(selectListItems, "Value", "Text", selectedValue);
        }

        public static string DisplayName(this Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());
            EnumDisplayNameAttribute attribute = Attribute.GetCustomAttribute(field, typeof(EnumDisplayNameAttribute)) as EnumDisplayNameAttribute;
            return attribute == null ? value.ToString() : attribute.DisplayName;
        }
    }
}
