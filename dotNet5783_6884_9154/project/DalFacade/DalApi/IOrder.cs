using Dal.DO;
namespace DalApi;

/// <summary>
/// used to implement the additional required functions that are not 
/// implemented in the base template CRUD interface
/// </summary>
public interface IOrder : ICrud<Order> { }