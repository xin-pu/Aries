using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Aries.OpenCV.Blocks.Processing;

namespace Aries.UnitTest
{
    [TestClass]
    public class BlockTest
    {
        [TestMethod]
        public void TestPropertyDescriptor()
        {
            var type = typeof(Blur);
            var properties = TypeDescriptor.GetProperties(type)
                .OfType<PropertyDescriptor>()
                .ToList();
        }


        [TestMethod]
        public void Test()
        {
            //通过反射得到MyClass类的信息
            MemberInfo info = typeof(Blur);

            //得到施加在MyClass类上的定制Attribute
            var att =  Attribute.GetCustomAttribute(info, typeof(CategoryAttribute)) as CategoryAttribute;
        }
    }
}
