using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SalvageIt.Models.Validators
{
    public class DataInvalidException : Exception
    {
        public IEnumerable<string> BrokenRules { get; private set; }

        public DataInvalidException(IEnumerable<string> broken_rules)
        {
            this.BrokenRules = broken_rules;
        }

        public override string Message
        {
            get
            {
                if (BrokenRules.Count() == 0)
                {
                    return "The data is invalid";
                }

                return BrokenRules.ElementAt(0);
            }
        }

    }
}
