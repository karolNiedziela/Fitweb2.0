using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Domain.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum @enum)
        {
            if (@enum is null)
            {
                return null;
            }

            if (@enum.GetType()
                .GetMember(@enum.ToString())
                .First().GetCustomAttribute<DisplayAttribute>() is null)
            {
                return @enum.ToString();
            }

            return @enum.GetType()
                        .GetMember(@enum.ToString())
                        .First()
                        .GetCustomAttribute<DisplayAttribute>()
                        .GetName();
        }
    }
}
