using Dal.DO;
namespace DalApi;

/// <summary>
/// used to implement the additional required functions that are not 
/// implemented in the base template CRUD interface
/// </summary>
public interface IProduct : ICrud<Product> {
    void updateAmount(int a, int b);
}
