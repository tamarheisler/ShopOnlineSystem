using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BLApi;
namespace BlImplementation
{
    internal class BlOrderTracking : IOrderTracking
    {

        [MethodImpl(MethodImplOptions.Synchronized)]

        public static BO.OrderTracking GetOrderTracking(int id)
        {
            try
            {
                BO.Order order = new();
                lock (BLApi.Factory.get())
                {
                    order = BLApi.Factory.get().order.GetOrderDetails(id);
                }
                BO.OrderTracking returnOrder = new();
                returnOrder.Status = (BO.OrderStatus)order.Status;
                returnOrder.ID = order.ID;
                return returnOrder;
            }
            catch(BO.BlExceptionFailedToRead ex)
            {
                throw new BO.BlExceptionFailedToRead();
            }
            catch(BO.BlEntityNotFoundException ex)
            {
                throw new BO.BlEntityNotFoundException(ex.Message);
            }
        }
    }
}
