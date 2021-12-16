using System;
using System.ComponentModel;
using System.Reflection;
using System.Collections.Generic;
using System.Text;

namespace DigitalUtil
{
    public static class EnumExtentions
    {

        public static string Description<T>(this T source)
        {
            FieldInfo fi = source.GetType().GetField(source.ToString());
            DescriptionAttribute[] attributes = null;
            try
            {
                attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute), false);
            }
            catch
            {
                throw new Exception(string.Format("An error occurred in EnumExtentions.Description, fi: {0}", fi));
            }
            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return source.ToString();
        }
    }


    [AttributeUsage(AttributeTargets.Field)]
    public class IsInDigitalProcess : Attribute
    {
        private bool isInDigitalProcess;
        public IsInDigitalProcess(bool status)
        {
            this.isInDigitalProcess = status;
        }
        public static bool Get(Type tp, string name)
        {
            MemberInfo[] mi = tp.GetMember(name);
            if (mi != null && mi.Length > 0)
            {
                IsInDigitalProcess attr = Attribute.GetCustomAttribute(mi[0],
                    typeof(IsInDigitalProcess)) as IsInDigitalProcess;
                if (attr != null)
                {
                    return attr.isInDigitalProcess;
                }
            }
            return false;
        }
        public static bool Get(object enm)
        {
            if (enm != null)
            {
                MemberInfo[] mi = enm.GetType().GetMember(enm.ToString());
                if (mi != null && mi.Length > 0)
                {
                    IsInDigitalProcess attr = Attribute.GetCustomAttribute(mi[0],
                        typeof(IsInDigitalProcess)) as IsInDigitalProcess;
                    if (attr != null)
                    {
                        return attr.isInDigitalProcess;
                    }
                }
            }
            return false;
        }
    }
}
