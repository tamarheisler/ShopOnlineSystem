
using DalApi;
using DalFacade.DO;
using System.Xml.Linq;

namespace Dal;

sealed public class DalXml : IDal
{
    static private Lazy<DalXml>? instance = null;
    public static IDal Instance { get => GetInstance(); }

    public IProduct Product { get; } = new Dal.DalProduct();
    public IOrder Order { get; } = new Dal.DalOrder();
    public IOrderItem OrderItem { get; } = new Dal.DalOrderItem();

    private DalXml() { }
    public static DalXml GetInstance()
    {
        lock (instance ??= new Lazy<DalXml>(() => new DalXml()))
        {


            return instance.Value;
        }

    }
}
