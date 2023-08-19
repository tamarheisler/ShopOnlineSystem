using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLApi;
/// <summary>
/// The interface implements all BL - entities 
/// </summary>
public interface IBL
{
    public IProduct product { get; }
    public ICart cart { get; }
    public IOrder order { get; }
    public IOrderTracking orderTracking { get; }
}

