namespace Aries.Utility
{
    public interface IShallowCopy<T>
    {
        T ShallowCopy();
    }
    public interface IDeepCopy<T>
    {
        T DeepCopy();
    }
}
