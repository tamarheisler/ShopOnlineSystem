namespace Dal.DO;
/// <summary>
/// defining order-item struct
/// </summary>
public struct OrderItem {
    public int ID { get; set; }
    public int OrderID { get; set; }
    public int ProductID { get; set; }
    public double Price { get; set; }
    public int Amount { get; set; }
    public override string ToString()
    {
        string orderItem = $@"
        Order #{ID}:
        Order ID: {OrderID}, 
        Product ID : {ProductID}
        Price : {Price}
    	Amount: {Amount}";
        return orderItem;
    }
}

