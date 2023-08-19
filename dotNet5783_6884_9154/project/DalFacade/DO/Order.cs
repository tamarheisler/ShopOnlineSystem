namespace Dal.DO;

/// <summary>
/// defining order struct
/// </summary>
public struct Order { 
    public int OrderID { get; set; }
    public string? CustomerName { get; set; }
    public string? CustomerEmail { get; set; }
    public string? CustomerAddress { get; set; }
    public DateTime? OrderDate { get; set; }
    public DateTime? ShipDate { get; set; }
    public DateTime? DeliveryDate { get; set; }

    public override string ToString()
    {
        string order = $@"
        Order #{OrderID}:
        Customer Name: {CustomerName}, 
        Customer Adress : {CustomerAddress}
        Customer E-mail : {CustomerEmail}
    	Order Date: {OrderDate}
        Ship Date: {ShipDate}
        Delivery Date: {DeliveryDate}";
        return order;
    }
}

