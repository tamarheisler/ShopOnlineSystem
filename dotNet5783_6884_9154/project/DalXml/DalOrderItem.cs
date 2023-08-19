using DalApi;
using Dal.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Runtime.CompilerServices;

namespace Dal;

internal class DalOrderItem : IOrderItem
{
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Add(OrderItem oi)
    {
        XElement? rootConfig = XDocument.Load(@"..\xml\config.xml").Root;
        XElement? id = rootConfig?.Element("orderItemId");
        int oiId = Convert.ToInt32(id?.Value);
        oi.ID = oiId;
        oiId++;
        id.Value = oiId.ToString();
        rootConfig?.Save("../xml/config.xml");
        XmlRootAttribute xRoot = new XmlRootAttribute();
        xRoot.ElementName = "orderItems";
        xRoot.IsNullable = true;
        XmlSerializer ser = new XmlSerializer(typeof(List<OrderItem>), xRoot);
        StreamReader reader = new StreamReader("..\\xml\\OrderItem.xml");
        List<DO.OrderItem> orderItems = (List<DO.OrderItem>)ser.Deserialize(reader);
        reader.Close();
        orderItems?.Add(oi);
        StreamWriter writer = new StreamWriter("..\\xml\\OrderItem.xml");
        ser.Serialize(writer, orderItems);
        writer.Close();
        return oi.ID;
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int id)
    {
        XmlRootAttribute xRoot = new XmlRootAttribute();
        xRoot.ElementName = "orderItems";
        xRoot.IsNullable = true;
        XmlSerializer ser = new XmlSerializer(typeof(List<OrderItem>), xRoot);
        StreamReader reader = new StreamReader("..\\xml\\OrderItem.xml");
        List<DO.OrderItem> orderItems = (List<DO.OrderItem>)ser.Deserialize(reader);
        reader.Close();
        StreamWriter writer = new StreamWriter("..\\xml\\OrderItem.xml");
        OrderItem oi = orderItems.Where(p => p.ID == id).FirstOrDefault();
        orderItems.Remove(oi);
        ser.Serialize(writer, orderItems);
        writer.Close();
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public OrderItem Get(Func<OrderItem, bool> func)
    {
        XmlRootAttribute xRoot = new XmlRootAttribute();
        xRoot.ElementName = "orderItems";
        xRoot.IsNullable = true;
        XmlSerializer ser = new XmlSerializer(typeof(List<OrderItem>), xRoot);
        StreamReader reader = new StreamReader("..\\xml\\OrderItem.xml");
        List<DO.OrderItem> ois = (List<DO.OrderItem>)ser.Deserialize(reader);
        reader.Close();
        return (func == null ? ois : ois.Where(func).ToList()).FirstOrDefault();
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<OrderItem> GetAll(Func<OrderItem, bool> func = null)
    {
        XmlRootAttribute xRoot = new XmlRootAttribute();
        xRoot.ElementName = "orderItems";
        xRoot.IsNullable = true;
        XmlSerializer ser = new XmlSerializer(typeof(List<OrderItem>), xRoot);
        StreamReader reader = new StreamReader("..\\xml\\OrderItem.xml");
        List<DO.OrderItem> ois = (List<DO.OrderItem>)ser.Deserialize(reader);
        reader.Close();
        return ois;
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<OrderItem> getByOrderId(int orderId)
    {
        XmlRootAttribute xRoot = new XmlRootAttribute();
        xRoot.ElementName = "orderItems";
        xRoot.IsNullable = true;
        XmlSerializer ser = new XmlSerializer(typeof(List<OrderItem>), xRoot);
        StreamReader reader = new StreamReader("..\\xml\\OrderItem.xml");
        List<DO.OrderItem> ois = (List<DO.OrderItem>)ser.Deserialize(reader);
        reader.Close();
        return ois.Where(oit => oit.OrderID==orderId).ToList();
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(OrderItem oi)
    {
        XmlRootAttribute xRoot = new XmlRootAttribute();
        xRoot.ElementName = "orderItems";
        xRoot.IsNullable = true;
        XmlSerializer ser = new XmlSerializer(typeof(List<OrderItem>), xRoot);
        StreamReader reader = new StreamReader("..\\xml\\OrderItem.xml");
        List<DO.OrderItem> ois = (List<DO.OrderItem>)ser.Deserialize(reader);
        reader.Close();
        StreamWriter writer = new StreamWriter("..\\xml\\OrderItem.xml");
        OrderItem orderItem = ois.Where(p => p.ID == oi.ID).FirstOrDefault();
        ois.Remove(orderItem);
        ois.Add(oi);
        ser.Serialize(writer, ois);
        writer.Close();
    }
}

