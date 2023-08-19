using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{    
    public enum Category
    {
        Drones,
        Cameras,
        Headphones,
        Computers,
        SmartWatches,
        All
    }

    public enum OrderStatus
    {
        Payed=1,
        Shiped,
        Delivered,
    }
}

