using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL;


/// <summary>
/// The exception will be scheduled when the name field is empty
/// </summary>

public class PLEmptyNameField : Exception
{
    public PLEmptyNameField(DalApi.ExceptionObjectNotFound? inner = null) : base("please enter name", inner) { }
    public override string Message =>
                    "please enter name";
}/// <summary>
 /// The exception will be scheduled when the id field is empty
 /// </summary>

public class PLEmptyIdField : Exception
{
    public PLEmptyIdField(DalApi.ExceptionObjectNotFound? inner = null) : base("please enter id", inner) { }
    public override string Message =>
                    "please enter id";
}
/// <summary>
/// The exception will be scheduled when the price field is empty
/// </summary>

public class PLEmptyPriceField : Exception
{
    public PLEmptyPriceField(DalApi.ExceptionObjectNotFound? inner = null) : base("please enter price", inner) { }
    public override string Message =>
                    "please enter price";
}
/// <summary>
/// The exception will be scheduled when the amount field is empty
/// </summary>

public class PLEmptyAmountField : Exception
{
    public PLEmptyAmountField(DalApi.ExceptionObjectNotFound? inner = null) : base("please enter amount", inner) { }
    public override string Message =>
                    "please enter amount";
}/// <summary>
 /// The exception will be scheduled when the category field is empty
 /// </summary>

public class PLEmptyCategoryField : Exception
{
    public PLEmptyCategoryField(DalApi.ExceptionObjectNotFound? inner = null) : base("please choose category", inner) { }
    public override string Message =>
                    "please choose category";
}
/// <summary>
/// The exception will be scheduled when one of the values is invalid
/// </summary>

public class PlInvalidValueExeption : Exception
{
    public string invalidVal { get; set; }
    public PlInvalidValueExeption(string val) { invalidVal = val; }
    public override string Message => $@"{invalidVal} is invalid";
}


/// <summary>
/// The exception will be scheduled when unknown error was occured
/// </summary>
public class PlDefaultException : Exception
{
    public readonly string msg;
    public PlDefaultException(string m) { msg = m; }
    public override string Message => msg;
}
