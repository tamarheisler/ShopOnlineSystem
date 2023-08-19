namespace BO;
public class OrderTracking
{
    public int ID { get; set; }
    public OrderStatus Status { get; set; }
    public List<Tuple<DateTime?, OrderStatus?>>? dateAndTrack { get; set; } = new();

    public override string ToString()
    {
        string orderTracking = $@"
        Order id: {ID}
        Status : {Status}
        Order Tracking : {dateAndTrack}";
        return orderTracking;
    }
}

