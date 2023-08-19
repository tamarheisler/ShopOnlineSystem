using BLApi;
using System.Diagnostics;
namespace Simulator;
public static class Simulator
{
    private static string? previousState;
    private static string? afterState;
    static bool finishFlag = false;
    public static event EventHandler StopSimulator;
    public static event EventHandler ProgressChange;
    public static void DoStop()
    {
        finishFlag = true;
        if (StopSimulator != null)
            StopSimulator("", EventArgs.Empty);
    }
    /// <summary>
    /// the function runs the program using maun thread
    /// </summary>
    public static void run()
    {
        finishFlag = false;
        Thread mainThreads = new Thread(new ThreadStart(chooseOrder));
        mainThreads.Start();
        return;
    }
    /// <summary>
    /// the function choose the order that has to be cared now.
    /// </summary>
    public static void chooseOrder()
    {
        try
        {
            IBL bl = new BlImplementation.Bl();
            int? id;
            while (!finishFlag)
            {
                id = bl.order.ChooseOrder();
                if (id == null)
                    DoStop();
                else
                {
                    BO.Order o = bl.order.GetOrderDetails((int)id);
                    previousState = o.Status.ToString();
                    Random rand = new Random();
                    int num = rand.Next(1000, 5000);
                    Details details = new Details(o, num);
                    if (ProgressChange != null)
                    {
                        ProgressChange(null, details);
                    }
                    Thread.Sleep(num);
                    afterState = (previousState == "Payed" ? bl.order.UpdateOrderShipping((int)id) : bl.order.UpdateOrderDelivery((int)id)).Status.ToString();
                }
            }
            return;
        }catch(BO.BlExceptionFailedToRead ex)
        {
            throw new BO.BlExceptionFailedToRead();
        }
        catch(BO.BlEntityNotFoundException ex)
        {
            throw new BO.BlEntityNotFoundException(ex.Message);
        }
        catch(BO.BlExceptionCantUpdateDelivery ex)
        {
            throw new BO.BlExceptionCantUpdateDelivery();
        }
        catch (BO.BlNoEntitiesFound ex)
        {
            throw new BO.BlNoEntitiesFound(ex.Message);
        }
        catch (BO.BlExceptionNoMatchingItems ex)
        {
            throw new BO.BlExceptionNoMatchingItems();
        }
        catch (Exception ex)
        {
            throw new BO.BlDefaultException(ex.Message);
        }
    }
}

/// <summary>
/// class to define the things that are sended from the Simulator.cs to the window.
/// </summary>
public class Details : EventArgs
{
    public BO.Order order;
    public int seconds;
    public Details(BO.Order ord, int sec)
    {
        order = ord;
        seconds = sec;
    }
}