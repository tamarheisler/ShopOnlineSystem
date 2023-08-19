using BO;
using Dal.DO;
using System.Text.RegularExpressions;
using System.Runtime.CompilerServices;

namespace BlImplementation;
internal class BlOrder : BLApi.IOrder
{
    DalApi.IDal? Dal = DalApi.Factory.Get();
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int AddNewOrder(BO.Order order)
    {
        try
        {

            Dal.DO.Order o = new()
            {
                OrderID = order.ID,
                OrderDate = order.OrderDate,
                ShipDate = order.ShipDate,
                DeliveryDate = order.DeiveryDate,
                CustomerAddress = order.CustomerAddress,
                CustomerEmail = order.CustomerEmail,
                CustomerName = order.CustomerName,
            };
            int orderId;
            lock (Dal)
            {
                orderId = Dal.Order.Add(o);

                order.Items.ForEach(oi => { oi.ID = orderId; Dal.OrderItem.Add(convertToDal(oi)); });
            }
            return orderId;

        }

        catch (DalApi.ExceptionFailedToRead)
        {
            throw new BO.BlExceptionFailedToRead();
        }
        catch (DalApi.ExceptionObjectNotFound)
        {
            throw new BO.BlEntityNotFoundException("");
        }
        catch (Dal.xmlFailedAccessToRoot)
        {
            throw new BO.BlDefaultException("Failed to load the root");
        }
        catch (Exception)
        {

            throw new BO.BlDefaultException("unexpected error");
        }
    }

    public Dal.DO.OrderItem convertToDal(BO.OrderItem bo)
    {
        Dal.DO.OrderItem o = new();
        o.OrderID = bo.ID;
        o.ProductID = bo.ProductID;
        o.Amount = bo.Amount;
        o.Price = bo.Price;
        return o;
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<BO.OrderForList?> GetOrderList()
    {
        try
        {
            IEnumerable<Dal.DO.Order> existingOrdersList;
            lock (Dal)
            {
                existingOrdersList = Dal.Order.GetAll();
            }
            List<BO.OrderForList> ordersList = new List<BO.OrderForList>();
            existingOrdersList.Select(item =>
            {
                BO.OrderForList o = new BO.OrderForList();
                o.ID = item.OrderID;
                o.CustomerName = item.CustomerName;
                lock (Dal)
                {
                    var orderItems = Dal.OrderItem.getByOrderId(item.OrderID);
                    orderItems.Select(orderItem =>
                    {
                        o.AmountOfItems++;
                        o.TotalPrice += orderItem.Price * orderItem.Amount;
                        return orderItem;
                    }).ToList();
                }
                if (item.DeliveryDate != null)
                    o.Status = (BO.OrderStatus)3;
                else if (item.ShipDate != null)
                    o.Status = (BO.OrderStatus)2;
                else
                    o.Status = (BO.OrderStatus)1;
                ordersList.Add(o);
                return item;
            }).ToList();
            if (ordersList.Count() == 0)
                throw new BO.BlNoEntitiesFound("");
            return ordersList;
        }
        catch (DalApi.ExceptionFailedToRead)
        {
            throw new BO.BlExceptionFailedToRead();
        }
        catch (DalApi.ExceptionNoMatchingItems)
        {
            throw new BO.BlExceptionNoMatchingItems();
        }
        catch (Dal.xmlFailedAccessToRoot)
        {
            throw new BO.BlExceptionFailedToRead();
        }
        catch (Exception)
        {
            throw new BO.BlDefaultException("Unknown exception occured");
        }
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.Order GetOrderDetails(int id)
    {
        try
        {
            if (id < 0)
                throw new BO.BlInvalidIntegerException();
            Dal.DO.Order o;
            lock (Dal)
            {
                o = Dal.Order.Get(o => o.OrderID == id);
            }
            BO.Order oi = new BO.Order();
            oi.ID = o.OrderID;
            oi.OrderDate = o.OrderDate;
            oi.ShipDate = o.ShipDate;
            oi.CustomerAddress = o.CustomerAddress;
            oi.CustomerEmail = o.CustomerEmail;
            oi.CustomerName = o.CustomerName;
            oi.DeiveryDate = o.DeliveryDate;
            if (o.DeliveryDate != null)
                oi.Status = (BO.OrderStatus)3;
            else if (o.ShipDate != null)
                oi.Status = (BO.OrderStatus)2;
            else
                oi.Status = (BO.OrderStatus)1;
            IEnumerable<Dal.DO.OrderItem> orderItems;
            lock (Dal)
            {
                orderItems = Dal.OrderItem.getByOrderId(id);
            }
            List<BO.OrderItem> items = new();
            orderItems.Select(item =>
            {
                BO.OrderItem orderItem = new();
                orderItem.ID = item.ID;
                lock (Dal)
                {
                    orderItem.ProductName = Dal.Product.Get(p => p.ID == item.ProductID).Name;
                }
                orderItem.ProductID = item.ProductID;
                orderItem.Price = item.Price;
                orderItem.Amount = item.Amount;
                orderItem.TotalPrice = orderItem.Amount * orderItem.Price;
                oi.TotalPrice += orderItem.TotalPrice;
                items.Add(orderItem);
                return item;
            }).ToList();

            oi.Items = items.ToList();
            if (oi.CustomerName != null)
                return oi;
            return null;
        }
        catch (DalApi.ExceptionFailedToRead)
        {
            throw new BO.BlExceptionFailedToRead();
        }
        catch (Dal.xmlFailedAccessToRoot)
        {
            throw new BlEntityNotFoundException("failed to accsess resource");
        }
        catch (Dal.xmlExceptionNoMatchingItems)
        {
            throw new BlEntityNotFoundException("no matchin items");
        }
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.Order UpdateOrderDelivery(int id)
    {
        try
        {
            Dal.DO.Order oDO;
            List<Dal.DO.OrderItem> orderItems = new List<Dal.DO.OrderItem>();

            lock (Dal)
            {
                oDO = Dal.Order.Get(o => o.OrderID == id);
                orderItems = Dal.OrderItem.getByOrderId(id).ToList();
            }
            if (oDO.ShipDate == null)
            {
                throw new BlExceptionCantUpdateDelivery();
            }
            if (oDO.DeliveryDate == null)
            {
                oDO.DeliveryDate = DateTime.Now;
                BO.Order order = new BO.Order();
                order.ID = oDO.OrderID;
                order.OrderDate = oDO.OrderDate;
                order.DeiveryDate = oDO.DeliveryDate;
                order.ShipDate = oDO.ShipDate;
                order.CustomerAddress = oDO.CustomerAddress;
                order.CustomerEmail = oDO.CustomerEmail;
                order.CustomerName = oDO.CustomerName;
                order.Status = (BO.OrderStatus)3;
                double total = 0;
                orderItems.ForEach(o => { total += o.Price * o.Amount; });
                order.TotalPrice = total;
                lock (Dal)
                {
                    Dal.Order.Update(oDO);
                }
                return order;
            }
            throw new BlEntityNotFoundException("");
        }
        catch (Dal.xmlEntityNotFoundException)
        {
            throw new BlEntityNotFoundException("could not find order");
        }
        catch (Dal.xmlFailedAccessToRoot)
        {
            throw new BlEntityNotFoundException("failed to accsess resource");
        }
        catch (DllNotFoundException e)
        {
            throw new BlEntityNotFoundException("could not find order");
        }
        catch (BlExceptionCantUpdateDelivery e)
        {
            throw new BlExceptionCantUpdateDelivery();
        }
    }






    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.Order UpdateOrderShipping(int orderId)
    {
        try
        {
            Dal.DO.Order oDO;
            List<Dal.DO.OrderItem> orderItems = new List<Dal.DO.OrderItem>();
            lock (Dal)
            {
                oDO = Dal.Order.Get(o => o.OrderID == orderId);
                orderItems = Dal.OrderItem.getByOrderId(orderId).ToList();
            }

            if (oDO.ShipDate == null)
            {
                oDO.ShipDate = DateTime.Now;
                BO.Order order = new BO.Order();
                order.ID = oDO.OrderID;
                order.OrderDate = oDO.OrderDate;
                order.DeiveryDate = oDO.DeliveryDate;
                order.ShipDate = oDO.ShipDate;
                order.CustomerAddress = oDO.CustomerAddress;
                order.CustomerEmail = oDO.CustomerEmail;
                order.CustomerName = oDO.CustomerName;
                order.Status = (BO.OrderStatus)2;
                double total=0;
                orderItems.ForEach(o => { total += o.Price * o.Amount; }) ;
                order.TotalPrice = total;
                lock (Dal)
                {
                    Dal.Order.Update(oDO);
                }
                return order;
            }
            throw new BlEntityNotFoundException("could not find order");
        }
        catch (DllNotFoundException e)
        {
            throw new BlEntityNotFoundException("could not find order");
        }
        catch (Dal.xmlEntityNotFoundException)
        {
            throw new BlEntityNotFoundException("could not find order");
        }
        catch (Dal.xmlFailedAccessToRoot)
        {
            throw new BlEntityNotFoundException("failed to accsess resource");
        }
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.Order UpdateOrderForManager(BO.Order or)
    {
        try
        {

            Dal.DO.Order o = new Dal.DO.Order();
            o.OrderID = or.ID;
            o.CustomerName = or.CustomerName;
            o.OrderDate = or.OrderDate;
            o.ShipDate = or.ShipDate;
            o.DeliveryDate = or.DeiveryDate;
            o.CustomerEmail = or.CustomerEmail;
            o.CustomerAddress = or.CustomerAddress;
            o.OrderID = or.ID;
            lock (Dal)
            {
                Dal.Order.Update(o);
            }
        }
        catch (DalApi.ExceptionFailedToRead)
        {
            throw new BO.BlExceptionFailedToRead();
        }
        catch (Dal.xmlFailedAccessToRoot)
        {
            throw new BlEntityNotFoundException("failed to accsess resource");
        }
        return or;
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.OrderTracking OrderTrack(int id)
    {
        try
        {
            Dal.DO.Order currOrder;
            lock (Dal)
            {
                currOrder = Dal.Order.Get(x => x.OrderID == id);
            }
            BO.OrderTracking orderTracking = new BO.OrderTracking();
            orderTracking.ID = currOrder.OrderID;
            orderTracking?.dateAndTrack.Add(new Tuple<DateTime?, OrderStatus?>(currOrder.OrderDate, BO.OrderStatus.Payed));
            orderTracking.Status = BO.OrderStatus.Payed;
            if (currOrder.ShipDate <= DateTime.Now)
            {
                orderTracking.dateAndTrack.Add(new Tuple<DateTime?, OrderStatus?>(currOrder.ShipDate, BO.OrderStatus.Shiped));
                orderTracking.Status = BO.OrderStatus.Shiped;
            }
            if (currOrder.DeliveryDate <= DateTime.Now)
            {
                orderTracking.dateAndTrack.Add(new Tuple<DateTime?, OrderStatus?>(currOrder.DeliveryDate, BO.OrderStatus.Delivered));
                orderTracking.Status = BO.OrderStatus.Delivered;
            }
            return orderTracking;
        }
        catch (DalApi.ExceptionObjectNotFound)
        {
            throw new BlEntityNotFoundException("could not find the order");
        }
    }
    [MethodImpl(MethodImplOptions.Synchronized)]


    public int? ChooseOrder()
    {
        try {
            DateTime minDate = DateTime.Now;
            int? orderId = null;
            List<OrderForList>? orderList = GetOrderList().ToList();
            orderList?.ForEach(o =>

            {
                switch (o.Status)
                {
                    case BO.OrderStatus.Payed:
                        if (GetOrderDetails(o.ID).OrderDate < minDate)
                        {
                            orderId = o.ID;
                            minDate = (DateTime)GetOrderDetails(o.ID).OrderDate;
                        }
                        break;
                    case BO.OrderStatus.Shiped:
                        if (GetOrderDetails(o.ID).ShipDate < minDate)
                        {
                            orderId = o.ID;
                            minDate = (DateTime)GetOrderDetails(o.ID).ShipDate;
                        }
                        break;
                    default:
                        break;
                }
            });
            return orderId;
        }
        catch(BlExceptionFailedToRead ex)
        {
            throw new BlExceptionFailedToRead();
        }
        catch(BlNoEntitiesFound ex)
        {
            throw new BlNoEntitiesFound(ex.Message);
        }
        catch(BlEntityNotFoundException ex)
        {
            throw new BlEntityNotFoundException(ex.Message);
        }
        catch(BlExceptionNoMatchingItems ex)
        {
            throw new BlExceptionNoMatchingItems();
        }
        catch (Exception ex)
        {
            throw new BlDefaultException(ex.Message);
        }
        }
}