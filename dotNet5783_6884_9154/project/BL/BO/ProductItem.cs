namespace BO
{
    public class ProductItem
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public Category Category { get; set; }
        public int Amount { get; set; }
        public bool inStock { get; set; }

        public override string ToString()
        {
            string productItem = $@"
        Order id: {ID}
        Name: {Name}, 
        Price : {Price}
        Category : {Category}
        Amount : {Amount}
        in Stock : {inStock}";
            return productItem;
        }
    }
}
