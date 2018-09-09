using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Xamarin.Forms;
using SalvageIt.Models.Validators;

[assembly: Dependency(typeof(SubmitItemReportValidator))]
namespace SalvageIt.Models.Validators
{
    public class SubmitItemReportValidator : IValidator<ItemReport>
    {
        public const int MaxTitleLength = 20;
        public const int MaxDescriptionLength = 300;

        public IEnumerable<string> BrokenRules(ItemReport item_report)
        {
            if (String.IsNullOrWhiteSpace(item_report.Title))
                yield return "You must enter a title";

            if (item_report.Title != null && item_report.Title.Length > MaxTitleLength)
                yield return $"The title can't be more than {MaxTitleLength} characters long";

            if(item_report.Description != null && item_report.Description.Length > MaxDescriptionLength)
                yield return $"The description can't be more than {MaxDescriptionLength} characters long";

            if (item_report.ItemPhoto == null)
                yield return "Item reports require a photo";

            if (item_report.ItemLocation == null)
                yield return "You must select a location first";

            if (item_report.ReportTime == DateTime.MinValue)
                yield return "Item reports require a date and a time";

            yield break;
        }

        public bool IsValid(ItemReport item_report)
        {
            return BrokenRules(item_report).Count() == 0;
        }
    }
}
