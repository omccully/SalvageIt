using System;
using System.Collections.Generic;
using System.Text;

namespace SalvageIt.Services
{
    using Models;
    using System.Threading.Tasks;

    public interface IGeolocator
    {
        Task<LocationCoordinates> GetPositionAsync();
    }
}
