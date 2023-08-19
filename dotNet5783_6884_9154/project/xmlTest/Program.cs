using Dal.DO;
using Dal;
using DalApi;
using System.Xml.Linq;
using System.Text;

Dal.DalXml d = (DalXml)DalApi.Factory.Get();

var a = d.Order.Get(a => a.OrderID == 500002);
var b = d.Order.GetAll();

Console.WriteLine(a.ToString());
Console.WriteLine("success");
Console.WriteLine("finish");
Console.WriteLine("success");
Console.WriteLine("end");