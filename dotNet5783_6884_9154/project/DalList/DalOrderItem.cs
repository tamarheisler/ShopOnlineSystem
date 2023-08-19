using Dal.DO;
using DalApi;
using System.Runtime.CompilerServices;

namespace DalList;

/// <summary>
/// The class implements all functions of the OrderItem entity.
/// </summary>
internal class DalOrderItem : IOrderItem
{
    /// <summary>
    /// The function gets a Dal OrderItem entity, add an id and add it to the main orderItems list
    /// </summary>
    /// <param name="obj"> the oderItem that the function inserts to the orderItem list</param>
    [MethodImpl(MethodImplOptions.Synchronized)]

    public int Add(OrderItem obj)
    {
        obj.OrderID = DataSource.Config.OrderItemId;
        DataSource.orderItems.Add(obj);
        return obj.OrderID;
    }

    /// <summary>
    /// The function gets an id of Product that have to be deleted from the orderItem list
    /// and deletes it from the main orderItem list.
    /// </summary>
    /// <param name="Id">the id of the object has to be deleted</param>
    /// <exception cref="ExceptionObjectNotFound">if the entered id dosen't belong to any 
    /// orderItem, it can't be deleted and the user will get an exception</exception>
    [MethodImpl(MethodImplOptions.Synchronized)]

    public void Delete(int Id)
    {
        int i;
        for (i = 0; i < DataSource.orderItems.Count(); i++)
        {
            if (DataSource.orderItems[i].ID == Id)
            {
                DataSource.orderItems.Remove(DataSource.orderItems[i]);
                return;
            }
        }
        throw new ExceptionObjectNotFound();
    }

    /// <summary>
    /// the function return the whole list of the orderItem
    /// </summary>
    /// <returns>the whole list of the orderItems</returns>
    /// <exception cref="ExceptionFailedToRead">if an error was occured while reading the 
    /// orderItems list</exception>
    [MethodImpl(MethodImplOptions.Synchronized)]

    public IEnumerable<OrderItem> GetAll(Func<OrderItem, bool> func = null)
    {
        return (func == null ? DataSource.orderItems : DataSource.orderItems.Where(func).ToList());

    }

    /// <summary>
    /// the function gets and id and return the orderItem with this id.
    /// </summary>
    /// <param name="Id">the id that was given in order to get that orderItem</param>
    /// <returns>the orderItem with the same given id</returns>
    /// <exception cref="ExceptionObjectNotFound">if the entered id dosen't belong to any 
    /// orderItem, it can't be deleted and the user will get an exception</exception>
    [MethodImpl(MethodImplOptions.Synchronized)]

    public IEnumerable<OrderItem> getByOrderId(int orderId)
    {
        List<OrderItem> OrderItemList = new List<OrderItem>();
        for (int i = 0; i < DataSource.orderItems.Count(); i++)
        {
            if (DataSource.orderItems[i].OrderID == orderId)
            {
                OrderItemList.Add(DataSource.orderItems[i]);
            }
        } return OrderItemList;
    }

    /// <summary>
    /// get a obj with this id
    /// </summary>
    /// <param name="Id">the given id</param>
    /// <returns>the obj</returns>
    /// <exception cref="ExceptionObjectNotFound">if not found</exception>
[MethodImpl(MethodImplOptions.Synchronized)]

    public OrderItem Get(Func<OrderItem, bool> func)
    {
        return DataSource.orderItems.Where(func).ToArray()[0];

    }


    /// <summary>
    /// update the obj with the same id as the diven one
    /// </summary>
    /// <param name="obj">the given obj</param>
    /// <exception cref="ExceptionObjectNotFound">if no obj as the given one</exception>
    [MethodImpl(MethodImplOptions.Synchronized)]

    public void Update(OrderItem obj)
    {
        int i;
        OrderItem oi = obj;
        for (i = 0; i < DataSource.orderItems.Count(); i++)
        {
            if (DataSource.orderItems[i].ID == oi.ID)
            {
                DataSource.orderItems[i] = oi;
                return;
            }
        }
        throw new ExceptionObjectNotFound();

    }
}

