namespace BLApi;
static public class Factory
{
    static public IBL get() { return new BlImplementation.Bl(); }
}

