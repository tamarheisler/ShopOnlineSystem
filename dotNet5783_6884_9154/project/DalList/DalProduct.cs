using Dal.DO;
using DalApi;
using System.Runtime.CompilerServices;

namespace DalList;

/// <summary>
/// The class implements all functions of the Product entity.
/// </summary>
/// 
internal class DalProduct : IProduct
{
    /// <summary>
    /// The function gets a Dal Product entity, add an id and add it to the main product list
    /// </summary>
    /// <param name="obj"> the product that the function inserts to the product list</param>
    [MethodImpl(MethodImplOptions.Synchronized)]

    public int Add(Product obj)
    {
        Random rand = new Random();
        bool idExist = false;
        int proId;
        do
        {
            idExist = true;
            proId = (int)rand.NextInt64(100009, 999999);
            for (int j = 0; j < DalList.DataSource.products.Count(); j++)
                if (DalList.DataSource.products[j].ID == proId)
                    idExist = false;
        } while (!idExist);
        obj.ID = proId;
        DataSource.products.Add(obj);
       return  obj.ID;
    }
    /// <summary>
    /// The function gets an id of Product that have to be deleted from the product list
    /// and deletes it from the main product list.
    /// </summary>
    /// <param name="Id">the id of the object has to be deleted</param>
    /// <exception cref="ExceptionObjectNotFound">if the entered id dosen't belong to any 
    /// product, it can't be deleted and the user will get an exception</exception>
    [MethodImpl(MethodImplOptions.Synchronized)]

    public void Delete(int Id)
    {
        for (int i = 0; i < DataSource.products.Count(); i++)
        {
            if (DataSource.products[i].ID == Id)
            {
                DataSource.products.Remove(DataSource.products[i]);
                return;
            }
        }
        throw new ExceptionObjectNotFound();

    }

    /// <summary>
    /// the function return the whole list of the products
    /// </summary>
    /// <returns>the whole list of the products</returns>
    /// <exception cref="ExceptionFailedToRead">if an error was occured while reading the 
    /// product list</exception>
    [MethodImpl(MethodImplOptions.Synchronized)]

    public IEnumerable<Product> GetAll(Func<Product, bool> func = null)
    {
        if (DataSource.products.Count < 0)
            throw new ExceptionObjectNotFound();
        return (func == null ? DataSource.products : DataSource.products.Where(func).ToList());
    }

    /// <summary>
    /// the function gets and id and return the product with this id.
    /// </summary>
    /// <param name="Id">the id that was given in order to get that product</param>
    /// <returns>the product with the same given id</returns>
    /// <exception cref="ExceptionObjectNotFound">if the entered id dosen't belong to any 
    /// product, it can't be deleted and the user will get an exception</exception>
    [MethodImpl(MethodImplOptions.Synchronized)]

    public Product Get(Func<Product, bool> func)
    {
       
        return DataSource.products.Where(func).ToArray()[0];


    }
    /// <summary>
    /// the function gets a Product updated object and update the product with the 
    /// same id with the updated details.
    /// </summary>
    /// <param name="obj">the given updated product</param>
    /// <exception cref="ExceptionObjectNotFound">if the entered id dosen't belong to any 
    /// product, it can't be deleted and the user will get an exception</exception>
    [MethodImpl(MethodImplOptions.Synchronized)]

    public void Update(Product obj)
    {
        int i;
        Product p = obj;
        for (i = 0; i < DataSource.products.Count(); i++)
        {
            if (DataSource.products[i].ID == p.ID)
            {
                DataSource.products[i] = p;
                return;
            }
        }
        throw new ExceptionObjectNotFound();

    }
    /// <summary>
    /// update the amount for an obj.
    /// </summary>
    /// <param name="id">id of obj</param>
    /// <param name="am">the new amount</param>
    /// <exception cref="ExceptionObjectNotFound">if no obj with this id</exception>
[MethodImpl(MethodImplOptions.Synchronized)]

    public void updateAmount(int id, int am)
    {
        int i;
        Product p;
        for (i = 0; i < DataSource.products.Count(); i++)
        {
            if (DataSource.products[i].ID == id)
            {
                p = DataSource.products[i];
                p.InStock = p.InStock - am; DataSource.products[i] = p;
                return;
            }
        }
        throw new ExceptionObjectNotFound();

    }
}

