using System;
using System.Collections.Generic;
using System.Text;

namespace SalvageIt.Models.Validators
{
    public interface IValidator<T>
    {
        bool IsValid(T entity);
        IEnumerable<string> BrokenRules(T entity);
    }
}
