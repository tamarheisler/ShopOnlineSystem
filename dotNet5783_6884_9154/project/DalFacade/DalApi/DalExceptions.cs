namespace DalApi;

/// <summary>
/// The exception will be scheduled when the required object is not found
/// </summary>
public class ExceptionObjectNotFound : Exception
{
    public override string Message => "object not found";
}
/// <summary>
/// The exception will be scheduled when error occurd while rading an entity list
/// </summary>
public class ExceptionFailedToRead : Exception
{
    public override string Message => "Faild to read properties";

}
/// <summary>
/// The exception will be scheduled when the given detail is not 
/// matching to any entity
/// </summary>
public class ExceptionNoMatchingItems : Exception
{
    public override string Message => "no matching items";

}

