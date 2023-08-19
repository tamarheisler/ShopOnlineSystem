namespace Dal;
using Dal.DO;
using DalApi;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Xml.Linq;
using System.Xml.Serialization;
internal class DalProduct :IProduct
{
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Add(DO.Product pro)
    {
        XElement? rootConfig = XDocument.Load(@"..\xml\config.xml").Root;
        XElement? id = rootConfig?.Element("productId");
        int pId = Convert.ToInt32(id?.Value);
        pro.ID = pId;
        pId++;
        id.Value = pId.ToString();
        rootConfig?.Save("../xml/config.xml");

        XmlRootAttribute xRoot = new XmlRootAttribute();
        xRoot.ElementName = "Products";
        xRoot.IsNullable = true;

        XmlSerializer ser = new XmlSerializer(typeof(List<Product>), xRoot);
        StreamReader reader = new StreamReader("..\\xml\\Product.xml");
        List<DO.Product> products = (List<DO.Product>)ser.Deserialize(reader);
        Product p = products.Where(p=> p.ID == pro.ID).FirstOrDefault();
        if (p.ID > 0)
            throw new xmlEntityAlreadyExistException("this product already exists");
        products?.Add(pro);
        reader.Close();
        StreamWriter writer = new StreamWriter("..\\xml\\Product.xml");
        ser.Serialize(writer, products);
        writer.Close();
        return pro.ID;
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int id)
    {
        XmlRootAttribute xRoot = new XmlRootAttribute();
        xRoot.ElementName = "Products";
        xRoot.IsNullable = true;
        XmlSerializer ser = new XmlSerializer(typeof(List<Product>), xRoot);
        StreamReader reader = new StreamReader("..\\xml\\Product.xml");
        List<DO.Product> products = (List<DO.Product>)ser.Deserialize(reader);
        reader.Close();
        StreamWriter writer = new StreamWriter("..\\xml\\Product.xml");
        Product pro = products.Where(p => p.ID==id).FirstOrDefault();
        if (pro.ID == 0)
            throw new xmlExceptionNoMatchingItems();
        products.Remove(pro);
        ser.Serialize(writer, products);
        writer.Close();
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public Product Get(Func<Product, bool> func)
    {
        XmlRootAttribute xRoot = new XmlRootAttribute();
        xRoot.ElementName = "Products";
        xRoot.IsNullable = true;
        XmlSerializer ser = new XmlSerializer(typeof(List<Product>), xRoot);
        StreamReader reader = new StreamReader("..\\xml\\Product.xml");
        List<DO.Product> products = (List<DO.Product>)ser.Deserialize(reader);
        reader.Close();
        var pro = products.Where(func).FirstOrDefault();
        if(pro.ID == 0)
            throw new xmlExceptionNoMatchingItems();
        return (func == null ? products.FirstOrDefault() : pro);
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<Product> GetAll(Func<Product, bool> func = null)
    {
        XmlRootAttribute xRoot = new XmlRootAttribute();
        xRoot.ElementName = "Products";
        xRoot.IsNullable = true;
        XmlSerializer ser = new XmlSerializer(typeof(List<Product>), xRoot);
        StreamReader reader = new StreamReader("..\\xml\\Product.xml");
        List<DO.Product> products = (List<DO.Product>)ser.Deserialize(reader);
        reader.Close();
        if (products.Count() == 0)
            throw new xmlNoEntitiesFound("there are no products");
        return products;
    }


    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(Product product)
    {

        XmlRootAttribute xRoot = new XmlRootAttribute();
        xRoot.ElementName = "Products";
        xRoot.IsNullable = true;
        XmlSerializer ser = new XmlSerializer(typeof(List<Product>), xRoot);
        StreamReader reader = new StreamReader("..\\xml\\Product.xml");
        List<DO.Product> products = (List<DO.Product>)ser.Deserialize(reader);
        reader.Close();
        StreamWriter writer = new StreamWriter("..\\xml\\Product.xml");
        Product pro = products.Where(p => p.ID==product.ID).FirstOrDefault();
        if (pro.ID == 0)
            throw new xmlExceptionNoMatchingItems();
        if (product.InStock < 0)
            throw new xmlInvalidAmountToken("invalid amount");

        products.Remove(pro);
        products.Add(product);
        ser.Serialize(writer, products);
        writer.Close();
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void updateAmount(int id, int amount)
    {
        if (amount < 0) throw new xmlInvalidAmountToken("invalid amount");
        XmlRootAttribute xRoot = new XmlRootAttribute();
        xRoot.ElementName = "Products";
        xRoot.IsNullable = true;
        XmlSerializer ser = new XmlSerializer(typeof(List<Product>), xRoot);
        StreamReader reader = new StreamReader("..\\xml\\Product.xml");
        List<DO.Product> products = (List<DO.Product>)ser.Deserialize(reader);
        reader.Close();
        StreamWriter writer = new StreamWriter("..\\xml\\Product.xml");
        Product pro = products.Where(p => p.ID==id).FirstOrDefault();
        if (pro.ID == 0) throw new xmlExceptionNoMatchingItems();
        Product prod = pro;
        prod.InStock = amount;
        products.Remove(pro);
        products.Add(prod);
        ser.Serialize(writer, products);
        writer.Close();
    }
}

