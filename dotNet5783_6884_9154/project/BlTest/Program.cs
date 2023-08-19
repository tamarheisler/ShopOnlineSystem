using BlImplementation;
using DalApi;


BLApi.IBL? bl = BLApi.Factory.get();
BO.Cart cart = new BO.Cart();
DalApi.IDal? DalList = DalApi.Factory.Get();
//=============orders==================
void getOrders()
{
    try
    {
        IEnumerable<BO.OrderForList> orderList = bl.order.GetOrderList();
        orderList.FirstOrDefault(i => { Console.WriteLine(i); return false; });

    }
    catch (BO.BlNoEntitiesFound ex)
    {
        Console.WriteLine(ex.Message);
    }
    catch (BO.BlExceptionFailedToRead ex)
    {
        Console.WriteLine(ex.Message);
    }
    catch (BO.BlExceptionNoMatchingItems ex)
    {
        Console.WriteLine(ex.Message);
    }
    catch (BO.BlDefaultException ex)
    {
        Console.WriteLine(ex.Message);
    }
}

void getOrderItems()
{
    try
    {
        Console.WriteLine("enter order id");
        int id = int.Parse(Console.ReadLine());
        BO.Order orderItems = bl.order.GetOrderDetails(id);
        //List<Dal.DO.OrderItem> items = Dal.OrderItem.getByOrderId(id);
        Console.WriteLine(orderItems);
    }
    catch (BO.BlInvalidIntegerException ex)
    {
        Console.WriteLine(ex.Message);
    }
    catch (BO.BlExceptionFailedToRead ex) { Console.WriteLine(ex.Message); }
    catch (BO.BlDefaultException ex) { Console.WriteLine(ex.Message); }

}

void updateOrderShipping()
{
    try
    {
        Console.WriteLine("enter order id");
        int id = int.Parse(Console.ReadLine());
        bl.order.UpdateOrderShipping(id);
    }
    catch (BO.BlInvalidIdToken ex) { Console.WriteLine(ex.Message); }
    catch (BO.BlInvalidNameToken ex) { Console.WriteLine(ex.Message); }
    catch (BO.BlExceptionFailedToRead ex) { Console.WriteLine(ex.Message); }
    catch (BO.BlEntityNotFoundException ex) { Console.WriteLine(ex.Message); }
    catch (BO.BlDefaultException ex) { Console.WriteLine(ex.Message); }
}

void updateOrderDelivery()
{
    try
    {
        Console.WriteLine("enter order id");
        int id = int.Parse(Console.ReadLine());
        bl.order.UpdateOrderDelivery(id);
    }
    catch (BO.BlExceptionFailedToRead ex) { Console.WriteLine(ex.Message); }
    catch (BO.BlEntityNotFoundException ex) { Console.WriteLine(ex.Message); }
    catch (BO.BlDefaultException ex) { Console.WriteLine(ex.Message); }
}

void orders()
{
    int choice;
    Console.WriteLine("enter the choice: 1.get orders list 2. get order items. 3.update shipping 4.update delivery");
    choice = int.Parse(Console.ReadLine());
    switch (choice)
    {
        case 1:
            getOrders();
            break;
        case 2:
            getOrderItems();
            break;
        case 3:
            updateOrderShipping();
            break;
        case 4:
            updateOrderDelivery();
            break;
        default:

            break;
    }
}


//=============products=================

void getProducts()
{
    try
    {
        IEnumerable<BO.ProductForList> products = bl.product.GetProductList();
        products.FirstOrDefault(i => { Console.WriteLine(i); return false; });

    }
    catch (BO.BlExceptionFailedToRead ex)
    {
        Console.WriteLine(ex.Message);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}

void getCatalog()
{
    try
    {
        IEnumerable<BO.ProductItem> catalog = bl.product.GetCatalog();
        catalog.FirstOrDefault(i => { Console.WriteLine(i); return false; });

    }
    catch (BO.BlExceptionFailedToRead ex) { Console.WriteLine(ex.Message); }
    catch (Exception ex) { Console.WriteLine(ex.Message); }
}

void getProManager()
{
    try
    {
        Console.WriteLine("enter product id");
        int id = int.Parse(Console.ReadLine());
        BO.Product pro = bl.product.GetProductManager(id);
        Console.WriteLine(pro);
    }
    catch (BO.BlEntityNotFoundException ex) { Console.WriteLine(ex.Message); }
    catch (Exception ex) { Console.WriteLine(ex.Message); }
}
void getProCustomer()
{
    try
    {
        Console.WriteLine("enter product id");
        int id = int.Parse(Console.ReadLine());
        BO.Product pro = bl.product.GetProductCustomer(id);
        Console.WriteLine(pro);
    }
    catch (BO.BlEntityNotFoundException ex) { Console.WriteLine(ex.Message); }
    catch (Exception ex) { Console.WriteLine(ex.Message); }
}

void addPro()
{

}

void deletePro()
{
    try
    {
        Console.WriteLine("enter product id");
        int id = int.Parse(Console.ReadLine());
        bl.product.DeleteProduct(id);
    }
    catch (BO.BlEntityNotFoundException ex) { Console.WriteLine(ex.Message); }
    catch (BO.BlDefaultException ex) { Console.WriteLine(ex.Message); }
}

void updatePro()
{
    try
    {
        BO.Product pro = new BO.Product();
        Console.WriteLine("enter id, name, price, amount in stock");
        pro.ID = int.Parse(Console.ReadLine());
        pro.Name = Console.ReadLine();
        pro.Price = int.Parse(Console.ReadLine());
        pro.inStock = int.Parse(Console.ReadLine());
        Console.WriteLine("enter the Product's category: 1.Drones,2.Cameras, 3.Headphones, 4.Computers, 5.SmartWatches");
        int choice = Convert.ToInt32(Console.ReadLine());
        pro.Category = (BO.Category)choice;
        bl.product.Update(pro);
    }
    catch (BO.BlEntityNotFoundException ex) { Console.WriteLine(ex.Message); }
    catch (BO.BlDefaultException ex) { Console.WriteLine(ex.Message); }
}


void products()
{
    int choice;
    Console.WriteLine("enter the choice: 1.get products list 2. get catalog. 3.get product for manager 7.get product for customer 4.add product 5.delete product 6.update product");
    choice = int.Parse(Console.ReadLine());
    switch (choice)
    {
        case 1:
            getProducts();
            break;
        case 2:
            getCatalog();
            break;
        case 3:
            getProManager();
            break;
        case 7:
            getProCustomer();
            break;
        case 4:
            addPro();
            break;
        case 5:
            deletePro();
            break;
        case 6:
            updatePro();
            break;
        default:
            Console.WriteLine("wrong choice");
            break;
    }
}

//=============carts===================

void addProduct()
{
    try
    {
        Console.WriteLine("enter product id");
        int productId;
        if (!(int.TryParse(Console.ReadLine(), out productId)))
            throw new BO.BlInvalidIntegerException();
        cart = bl.cart.AddProductToCart(cart, productId);
    }

    catch (BO.BlDefaultException ex) { Console.WriteLine(ex.Message); }
    catch (BO.BlEntityNotFoundException ex) { Console.WriteLine(ex.Message); }
}


void updateProductAmount()
{
    try
    {
        int productId, newAmount;
        Console.WriteLine("enter product id");
        if (!(int.TryParse(Console.ReadLine(), out productId)))
            throw new BO.BlInvalidIntegerException();
        Console.WriteLine("enter new amount for the product");
        if (!(int.TryParse(Console.ReadLine(), out newAmount)))
            throw new BO.BlInvalidIntegerException();
        cart = bl.cart.Update(cart, productId, newAmount);
    }
    catch (BO.BlDefaultException ex) { Console.WriteLine(ex.Message); }
    catch (BO.BlEntityNotFoundException ex) { Console.WriteLine(ex.Message); }
}


void confirmCart()
{
    try
    {
        Console.WriteLine("enter the customer's name");
        string CustomerName = Console.ReadLine();
        Console.WriteLine("enter the customer's email");
        string CustomerEmail = Console.ReadLine();
        Console.WriteLine("enter the customer's address");
        string CustomerAddress = Console.ReadLine();
        bl.cart.CartConfirmation(cart, CustomerName, CustomerEmail, CustomerAddress);
    }
    catch (BO.BlDefaultException ex) { Console.WriteLine(ex.Message); }
    catch (BO.BlEntityNotFoundException ex) { Console.WriteLine(ex.Message); }
}


void carts()
{
    int choice;
    Console.WriteLine("enter the choice: 1.add product 2.update product amount. 3.confirm cart ");
    choice = int.Parse(Console.ReadLine());
    switch (choice)
    {
        case 1:
            addProduct();
            break;
        case 2:
            updateProductAmount();
            break;
        case 3:
            confirmCart();
            break;
        default:
            Console.WriteLine("wrong choice");
            break;
    }
}

// ============MAIN PROGRAM============

void main()
{
    int choice;


    try
    {
        do
        {
            Console.WriteLine("enter the entity number: 1. order 2. product 3. cart  0. to exit");
            choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case 0:
                    return;
                case 1:
                    orders();
                    break;
                case 2:
                    products();
                    break;
                case 3:
                    carts();
                    break;
                default:
                    Console.WriteLine("wrong choice");
                    break;

            }
        } while (choice != 0);
    }
    catch (Exception err)
    {
        Console.WriteLine(err);
    }
}

main();