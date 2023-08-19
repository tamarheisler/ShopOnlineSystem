namespace BO;
/// <summary>
/// The exception will be scheduled when the required object is not found
/// </summary>

public class BlEntityNotFoundException : Exception
{
    public readonly string msg;
    public BlEntityNotFoundException(string m) { msg = m; }
    public override string Message => msg;
}
/// <summary>
/// The exception will be scheduled when the object is already exist
/// </summary>

public class BlEntityAlreadyExistException : Exception
{
    public BlEntityAlreadyExistException(Exception inner) : base("entity already exists", inner) { }
    public override string Message => "entity already exists";
}
/// <summary>
/// The exception will be scheduled when no entities found
/// </summary>

public class BlNoEntitiesFound : Exception
{
    public readonly string msg;
    public BlNoEntitiesFound(string m) { msg = m; }
    public override string Message => msg;
}
public class BlOutOfStockException : Exception
{

    public BlOutOfStockException() : base("product is out of stock") { }
    public override string Message => ("product is out of stock");
}

/// <summary>
/// The exception will be scheduled when invalid integer was taken
/// </summary>
public class BlInvalidIntegerException : Exception
{
    public BlInvalidIntegerException() : base("invalid input: not integer") { }
    public override string Message => ("invalid input: not integer");
}
/// <summary>
/// The exception will be scheduled when customer details are invalid
/// </summary>
public class CustomerDetailsAreInValid : Exception
{
    public CustomerDetailsAreInValid() : base("the details are invalid") { }
    public override string Message => ("the details are invalid");
}

/// <summary>
/// The exception will be scheduled when customer details are invalid
/// </summary>
public class BlExceptionCantUpdateDelivery : Exception
{
    public BlExceptionCantUpdateDelivery() : base("cannot update delivery date for unshipped order") { }
    public override string Message => ("cannot update delivery date for unshipped order");
}

/// <summary>
/// The exception will be scheduled when error occured while reading an
/// entity list
/// </summary>
public class BlExceptionFailedToRead : Exception
{
    public BlExceptionFailedToRead(DalApi.ExceptionFailedToRead? inner = null) : base("entity not found", inner) { }
    public override string Message =>
                    "entity not found";
}
/// <summary>
/// The exception will be scheduled when no matching items as the given one.
/// </summary>
public class BlExceptionNoMatchingItems : Exception
{
    public BlExceptionNoMatchingItems(DalApi.ExceptionNoMatchingItems? inner = null) : base("no matching items", inner) { }
    public override string Message =>
                    "no matching items";
}


/// <summary>
/// The exception will be scheduled when invalid id was taken
/// </summary>
public class BlInvalidIdToken : Exception
{
    public readonly string msg;
    public BlInvalidIdToken(string m) { msg = m; }
    public override string Message => msg;
}/// <summary>
/// The exception will be scheduled when invalid name was taken
/// </summary>
public class BlInvalidNameToken : Exception
{
    public readonly string msg;
    public BlInvalidNameToken(string m) { msg = m; }
    public override string Message => msg;
}/// <summary>
/// The exception will be scheduled when order already delivered
/// </summary>
public class BlOrderAlreadyDelivered : Exception
{
    public readonly string msg;
    public BlOrderAlreadyDelivered(string m) { msg = m; }
    public override string Message => msg;
}
/// <summary>
/// The exception will be scheduled when unknown error was occured
/// </summary>
public class BlDefaultException : Exception
{
    public readonly string msg;
    public BlDefaultException(string m) { msg = m; }
    public override string Message => msg;
}
/// <summary>
/// The exception will be scheduled when invalid price was taken
/// </summary>
public class BlInvalidPriceToken : Exception
{
    public readonly string msg;
    public BlInvalidPriceToken(string m) { msg = m; }
    public override string Message => msg;
}
/// <summary>
/// The exception will be scheduled when product already exists in an order
/// </summary>
public class BlProductExistsInAnOrder : Exception
{
    public readonly string msg;
    public BlProductExistsInAnOrder(string m) { msg = m; }
    public override string Message => msg;
}
/// <summary>
/// The exception will be scheduled when invalid amount was taken
/// </summary>
public class blInvalidAmountToken : Exception
{
    public readonly string msg;
    public blInvalidAmountToken(string m) { msg = m; }
    public override string Message => msg;
}

