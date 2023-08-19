namespace BLApi
{
    public interface ICart
    {/// <summary>
/// the interface implements additional functions for the BL - Cart entity
/// </summary>
        public BO.Cart AddProductToCart(BO.Cart c, int id);
        public BO.Cart Update(BO.Cart c, int id, double newAmount);
        public int CartConfirmation(BO.Cart c, string customerName, string customerEmail, string customerAddress);
    }
}
