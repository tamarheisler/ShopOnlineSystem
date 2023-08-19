using DalFacade.DO;
namespace Dal.DO;
/// <summary>
/// defining product struct
/// </summary>
public struct Product { 
    public int ID { get; set; }
    public string Name { get; set; }
    public eCategory Category { get; set; }
    public float Price { get; set; }
    public int InStock { get; set; }

    public override string ToString()
    {
        string product = $@"
        Product ID = {ID}: {Name}, 
        category: {Category}
    	Price: {Price}
    	Amount in stock: {InStock}";
        return product;
    }
}

