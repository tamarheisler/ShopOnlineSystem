namespace BO;
public class Product
{
    public int ID { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public Category Category { get; set; }
    public int inStock { get; set; }

    public override string ToString()
    {
        string product = $@"
        Product id: {ID}
        Name: {Name}, 
        Price : {Price}
        Category : {Category}
        in Stock : {inStock}";
        return product;
    }
}

