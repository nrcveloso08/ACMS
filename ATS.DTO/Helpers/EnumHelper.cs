using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.DTO.Helpers
{
    public class EnumHelper
    {
        public static IList<SelectionList> ToSelectionList<T>()
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerator type.");
            }

            return Enum.GetValues(
                typeof(T))
                .Cast<T>()
                .Select(x => new SelectionList
                {
                    Value = (int)Convert.ChangeType(x, x.GetType()),
                    Text = x.ToString().Replace("_", " ")

                }).ToList();
        }
    }

    public static class EnumExtensions
    {
        public static string GetDescription(this Enum GenericEnum)
        {
            Type genericEnumType = GenericEnum.GetType();
            System.Reflection.MemberInfo[] memberInfo =
                        genericEnumType.GetMember(GenericEnum.ToString());

            if ((memberInfo != null && memberInfo.Length > 0))
            {
                dynamic _Attribs = memberInfo[0].GetCustomAttributes
                      (typeof(System.ComponentModel.DescriptionAttribute), false);
                if ((_Attribs != null && _Attribs.Length > 0))
                {
                    return ((System.ComponentModel.DescriptionAttribute)_Attribs[0]).Description;
                }
            }

            return GenericEnum.ToString().Replace("_", " ");
        }
    }
}
