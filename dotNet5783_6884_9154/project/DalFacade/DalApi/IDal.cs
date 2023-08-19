/// <summary>
/// the IDal interface implements the whole 3 interfaces of the 3 main entities.
/// </summary>
namespace DalApi;


public interface IDal
{
    public IProduct Product{ get; }
    public IOrder Order{ get;}
    public IOrderItem OrderItem{ get; }
}
