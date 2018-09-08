using System;
using System.Collections.Generic;
using System.Text;

namespace SalvageIt.Services
{
    using Models.Validators;

    public static class Helpers
    {
        public static string ToUserFriendlyString(this TimeSpan ts)
        {
            if (ts < TimeSpan.FromHours(1))
                return PluralizeUnit((int)ts.TotalMinutes, "minute");
            if (ts < TimeSpan.FromDays(1))
                return PluralizeUnit((int)ts.TotalHours, "hour");
            return PluralizeUnit((int)ts.TotalDays, "day");
        }

        static string PluralizeUnit(int val, string word)
        {
            return $"{val} {word}" + (val == 1 ? "" : "s");
        }

        public static bool Validate<T>(this T obj, IValidator<T> validator, out IEnumerable<string> broken_rules)
        {
            broken_rules = validator.BrokenRules(obj);
            return validator.IsValid(obj);
        }
    }
}
