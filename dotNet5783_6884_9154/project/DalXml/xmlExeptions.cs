using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;
/// <summary>
/// The exception will be scheduled when the required object is not found
/// </summary>

public class xmlEntityNotFoundException : Exception
{
    public xmlEntityNotFoundException(DalApi.ExceptionObjectNotFound? inner = null) : base("entity not found", inner) { }
    public override string Message =>
                    "entity not found";
}
/// <summary>
/// The exception will be scheduled when the object is already exist
/// </summary>

public class xmlEntityAlreadyExistException : Exception
{
    public readonly string msg;
    public xmlEntityAlreadyExistException(string m) { msg = m; }
    public override string Message => msg;
}
/// <summary>
/// The exception will be scheduled when no entities found
/// </summary>

public class xmlNoEntitiesFound : Exception
{
    public readonly string msg;
    public xmlNoEntitiesFound(string m) { msg = m; }
    public override string Message => msg;
}
public class xmlOutOfStockException : Exception
{
    public xmlOutOfStockException() : base("product is out of stock") { }
    public override string Message => ("product is out of stock");
}

public class xmlFailedAccessToRoot : Exception
{
    public xmlFailedAccessToRoot() : base("fail to access the xml root") { }
    public override string Message => ("fail to access the xml root");
}

/// <summary>
/// The exception will be scheduled when invalid integer was taken
/// </summary>
public class xmlInvalidIntegerException : Exception
{
    public xmlInvalidIntegerException() : base("invalid input: not integer") { }
    public override string Message => ("invalid input: not integer");
}
/// <summary>
/// The exception will be scheduled when customer details are invalid
/// </summary>
public class CustomerDetailsAreInValid : Exception
{
    public CustomerDetailsAreInValid(Exception inner) : base("the details are invalid", inner) { }
    public override string Message => ("the details are invalid");
}
/// <summary>
/// The exception will be scheduled when error occured while reading an
/// entity list
/// </summary>
public class xmlExceptionFailedToRead : Exception
{
    public xmlExceptionFailedToRead(DalApi.ExceptionFailedToRead? inner = null) : base("entity not found", inner) { }
    public override string Message =>
                    "entity not found";
}
/// <summary>
/// The exception will be scheduled when no matching items as the given one.
/// </summary>
public class xmlExceptionNoMatchingItems : Exception
{
    public xmlExceptionNoMatchingItems(DalApi.ExceptionNoMatchingItems? inner = null) : base("no matching items", inner) { }
    public override string Message =>
                    "no matching items";
}
/// <summary>
/// The exception will be scheduled when invalid id was taken
/// </summary>
public class xmlInvalidIdToken : Exception
{
    public readonly string msg;
    public xmlInvalidIdToken(string m) { msg = m; }
    public override string Message => msg;
}/// <summary>
/// The exception will be scheduled when invalid name was taken
/// </summary>
public class xmlInvalidNameToken : Exception
{
    public readonly string msg;
    public xmlInvalidNameToken(string m) { msg = m; }
    public override string Message => msg;
}/// <summary>
/// The exception will be scheduled when order already delivered
/// </summary>
public class xmlOrderAlreadyDelivered : Exception
{
    public readonly string msg;
    public xmlOrderAlreadyDelivered(string m) { msg = m; }
    public override string Message => msg;
}
/// <summary>
/// The exception will be scheduled when unknown error was occured
/// </summary>
public class xmlDefaultException : Exception
{
    public readonly string msg;
    public xmlDefaultException(string m) { msg = m; }
    public override string Message => msg;
}
/// <summary>
/// The exception will be scheduled when invalid price was taken
/// </summary>
public class xmlInvalidPriceToken : Exception
{
    public readonly string msg;
    public xmlInvalidPriceToken(string m) { msg = m; }
    public override string Message => msg;
}

/// <summary>
/// The exception will be scheduled when invalid amount was taken
/// </summary>
public class xmlInvalidAmountToken : Exception
{
    public readonly string msg;
    public xmlInvalidAmountToken(string m) { msg = m; }
    public override string Message => msg;
}


