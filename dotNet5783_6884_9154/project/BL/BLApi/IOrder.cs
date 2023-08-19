namespace BLApi
{/// <summary>
/// the interface implements additional functions for the BL - Order entity
/// </summary>
    public interface IOrder
    {
        public int AddNewOrder(BO.Order order);
        public IEnumerable<BO.OrderForList> GetOrderList();
        public BO.Order GetOrderDetails(int id);
        public BO.Order UpdateOrderShipping(int id);
        public BO.Order UpdateOrderDelivery(int id);
        public BO.OrderTracking OrderTrack(int id);
        public int? ChooseOrder();
        //BONUS.
        public BO.Order UpdateOrderForManager(BO.Order o);

    }
}
