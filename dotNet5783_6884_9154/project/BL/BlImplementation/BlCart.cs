using BLApi;
using BO;
using Dal;
using System.Linq;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace BlImplementation;
internal class BlCart : ICart
{
    DalApi.IDal? Dal = DalApi.Factory.Get();
    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.Cart AddProductToCart(BO.Cart cart, int productId)
    {
        try
        {
            int productInStock;
            double productPrice;
            string productName;
            lock (Dal)
            {
                productInStock = Dal.Product.Get(p => p.ID == productId).InStock;
                productPrice = Dal.Product.Get(p => p.ID == productId).Price;
                productName = Dal.Product.Get(p => p.ID == productId).Name;
            }
            BO.OrderItem oi = cart.items.Find(item => item.ProductID == productId);
            if (productInStock > 0)
            {
                if (oi != null)
                {
                    oi.Amount++;
                    oi.TotalPrice += productPrice;
                    cart.TotalPrice += productPrice;
                    return cart;
                }
                else
                {
                    oi = new BO.OrderItem();
                    oi.Price = productPrice;
                    oi.TotalPrice = productPrice;
                    oi.ProductID = productId;
                    oi.ProductName = productName;
                    oi.Amount = 1;
                    cart.items.Add(oi);
                    cart.TotalPrice += productPrice;
                }
                return cart;
            }
            else
                throw new BO.BlOutOfStockException();
        }
        catch (DalApi.ExceptionObjectNotFound)
        {
            throw new BO.BlEntityNotFoundException("");
        }
        catch (BO.BlOutOfStockException)
        {
            throw new BO.BlOutOfStockException();
        }
        catch (Dal.xmlFailedAccessToRoot)
        {
            throw new BlEntityNotFoundException("failed to accsess resource");
        }
        catch (Exception)
        {
            throw new BO.BlDefaultException("unexpected error occured");
        }
    }

    public static bool IsValidEmail(string email)
    {
        try
        {
            MailAddress mailAddress = new MailAddress(email);
            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int CartConfirmation(BO.Cart c, string customerName, string customerEmail, string customerAddress)
    {
        int id = 0;
        try
        {
            if (customerAddress == "" || !IsValidEmail(customerEmail) || customerEmail == "" || customerName == "")
                throw new BO.CustomerDetailsAreInValid();
            Dal.DO.Order o = new Dal.DO.Order();
            c.items.Select(item =>
            {
                lock (Dal)
                {
                    if (item.Amount < 0 || (Dal.Product.Get(p => p.ID == item.ProductID).InStock - item.Amount) < 0)
                        throw new BlOutOfStockException();
                }
                return item;

            });

            o.OrderID = 0;
            o.OrderDate = DateTime.Now;
            o.DeliveryDate = null;
            o.ShipDate = null;
            o.CustomerAddress = customerAddress;
            o.CustomerName = customerName;
            o.CustomerEmail = customerEmail;
            lock (Dal)
            {
                id = Dal.Order.Add(o);
                List<Dal.DO.OrderItem> allItems = Dal.OrderItem.GetAll().ToList();
                c.items.ForEach(oi => { oi.ID = id; Dal.OrderItem.Add(convertToDal(oi)); });
            }
            var cartItems = from BO.OrderItem item1 in c.items
                            select new BO.OrderItem
                            {
                                ID = item1.ID,
                                Amount = item1.Amount,
                                Price = item1.Price,
                                ProductID = item1.ProductID,
                                ProductName = item1.ProductName,
                                TotalPrice = item1.TotalPrice
                            };

            var items1 = from BO.OrderItem item1 in c.items
                         select new BO.OrderItem
                         {
                             ID = item1.ID,
                             Amount = item1.Amount,
                             Price = item1.Price,
                             ProductID = item1.ProductID,
                             ProductName = item1.ProductName,
                             TotalPrice = item1.TotalPrice
                         };
            return id;
        }


        catch (DalApi.ExceptionObjectNotFound)
        {
            throw new BO.BlEntityNotFoundException("");
        }
        catch (Dal.CustomerDetailsAreInValid)
        {
            throw new BO.CustomerDetailsAreInValid();
        }
        catch (Dal.xmlFailedAccessToRoot)
        {
            throw new BO.BlDefaultException("Failed to load the root");
        }
        catch (Exception)
        {
            throw new Exception();
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
    public BO.Cart Update(BO.Cart c, int id, double newAmount)
    {
        try
        {
            int productInStock;
            lock (Dal) { productInStock = Dal.Product.Get(p => p.ID == id).InStock; }
            if (productInStock < newAmount) throw new BlOutOfStockException();
            BO.OrderItem oi = c.items.Find(item => item.ProductID == id);
            if (oi != null)
            {
                if (newAmount == 0)
                {
                    c.items.Remove(oi);
                    c.TotalPrice -= oi.TotalPrice;
                }
                else if (newAmount > oi.Amount)
                {
                    AddProductToCart(c, id);
                }
                else if (newAmount < oi.Amount)
                {
                    oi.Amount--;
                    oi.TotalPrice -= oi.Price;
                    c.TotalPrice -= oi.Price;
                }


                return c;
            }
            else

                throw new BO.BlOutOfStockException();

        }
        catch (DalApi.ExceptionObjectNotFound)
        {
            throw new BO.BlEntityNotFoundException("");
        }
        catch (BlOutOfStockException ex)
        {
            throw new BO.BlOutOfStockException();
        }
        catch (Dal.xmlFailedAccessToRoot)
        {
            throw new BlEntityNotFoundException("failed to accsess resource");
        }
        catch(BlEntityNotFoundException ex)
        {
            throw new BlEntityNotFoundException(ex.Message);
        }
        catch (Exception)
        {
            throw new BO.BlDefaultException("unexpected error occured");
        }
    }
}
