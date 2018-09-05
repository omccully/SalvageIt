using System;
using System.Collections.Generic;
using System.Text;

namespace SalvageIt.Services
{
    public interface IToaster
    {
        void DisplayMessage(string message);

        void DisplayError(string message);
    }
}
