namespace Aries.OpenCV.Core
{
    public class BlockTypeConvert<T>
    {
        public object ConvertToObject(T value)
        {
            return value;
        }


        public T ConvertToT(object value)
        {
            return (T)value;
        }


    }

}
