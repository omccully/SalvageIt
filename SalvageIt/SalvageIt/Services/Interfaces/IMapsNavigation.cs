using System;
using System.Collections.Generic;
using System.Text;

namespace SalvageIt.Services
{
    using Models;

    public interface IMapsNavigation
    {
        void NavigateToCoordinates(LocationCoordinates coords);
        void NavigateToAddress(string address);
    }
}
