using FuzzyString;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DigitalUtil
{
    public static class StringExtentions
    {
        public static bool FuzzyEquals(this string source, string dest)
        {
            List<FuzzyStringComparisonOptions> options = new List<FuzzyStringComparisonOptions>();

            options.Add(FuzzyStringComparisonOptions.UseJaccardDistance);
            options.Add(FuzzyStringComparisonOptions.UseNormalizedLevenshteinDistance);
            options.Add(FuzzyStringComparisonOptions.UseOverlapCoefficient);
            options.Add(FuzzyStringComparisonOptions.UseLongestCommonSubsequence);
            options.Add(FuzzyStringComparisonOptions.CaseSensitive);

            return source.ApproximatelyEquals(dest, options, FuzzyStringComparisonTolerance.Strong);
        }


        public static bool FuzzyEqualNames(this string source, string dest)
        {
            var sourceLst = source.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            var destLst = dest.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            int counter = 0;
            foreach (string s in sourceLst)
            {
                counter += destLst.Any(p => p.FuzzyEquals(s)) ? 1 : 0;
            }
            if (counter >= 2)
            {
                return true;
            }
            return false;
            //var test = from ss in s join dd in d on ss equals dd into grp select new { equal = ss };
        }


        public static int IntParseDefaultOrValue(this string param)
        {
            int? intParam = null;

            if (!string.IsNullOrEmpty(param))
                intParam = int.Parse(param);

            return intParam.DefaultOrValue();
        }
        public static bool BoolParseDefaultOrValue(this string param)
        {
            bool boolParam = false;

            if (!string.IsNullOrEmpty(param))
            {
                if (param == "1")
                    boolParam = true;
                else if (param != "0")
                    boolParam = bool.Parse(param);
            }

            return boolParam;
        }

        public static string DefaultOrValue(this string param)
        {
            return string.IsNullOrEmpty(param) ? string.Empty : param;
        }
    }
}
