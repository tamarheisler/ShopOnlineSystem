using Dal.DO;
using DalApi;
using System.Runtime.CompilerServices;

namespace DalList;

/// <summary>
/// The class implements all functions of the Order entity.
/// </summary>
internal class DalOrder : IOrder
{
    /// <summary>
    /// The function gets a Dal Order entity, add an id and add it to the main order list
    /// </summary>
    /// <param name="obj"> the order that the function inserts to the order list</param>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Add(Order obj)
    {
        obj.OrderID = DataSource.Config.OrderId;
        DataSource.orders.Add(obj);
        return obj.OrderID;
    }
    /// <summary>
    /// The function gets an id of Order that have to be deleted from the product list
    /// and deletes it from the main product list.
    /// </summary>
    /// <param name="Id">the id of the object has to be deleted</param>
    /// <exception cref="ExceptionObjectNotFound">if the entered id dosen't belong to any 
    /// product, it can't be deleted and the user will get an exception</exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int Id)
    {

        int i;
        for (i = 0; i < DataSource.orders.Count(); i++)
        {
            if (DataSource.orders[i].OrderID == Id)
            {
                DataSource.orders.Remove(DataSource.orders[i]);
                return;
            }
        }
        throw new ExceptionObjectNotFound();
    }
    /// <summary>
    /// the function return the whole list of the orders
    /// </summary>
    /// <returns>the whole list of the orders</returns>
    /// <exception cref="ExceptionFailedToRead">if an error was occured while reading the 
    /// order list</exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<Order> GetAll(Func<Order, bool> func = null)
    {
        return (func == null ? DataSource.orders : DataSource.orders.Where(func).ToList());
    }
    /// <summary>
    /// the function gets and id and return the order with this id.
    /// </summary>
    /// <param name="Id">the id that was given in order to get that order</param>
    /// <returns>the order with the same as the given id</returns>
    /// <exception cref="ExceptionObjectNotFound">if the entered id dosen't belong to any 
    /// order, it can't be deleted and the user will get an exception</exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public Order Get(Func<Order, bool> func)
    {
        return DataSource.orders.Where(func).ToArray()[0];
        //throw new ExceptionObjectNotFound();
    }

    /// <summary>
    /// update the obj with the same id as the diven one
    /// </summary>
    /// <param name="obj">the given obj</param>
    /// <exception cref="ExceptionObjectNotFound">if no obj as the given one</exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(Order obj)
    {
        int i;
        Order o = obj;
        for (i = 0; i < DataSource.orders.Count(); i++)
        {
            if (DataSource.orders[i].OrderID == o.OrderID)
            {
                DataSource.orders[i] = o;
                break;
            }
        }
        if(i==DataSource.orders.Count())
        throw new ExceptionObjectNotFound();

    }
}

