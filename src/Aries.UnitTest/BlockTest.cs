using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.ComponentModel;
using System.Linq;
using Aries.OpenCV.Blocks.Processing;
using GraphX.Common;

namespace Aries.UnitTest
{
    [TestClass]
    public class BlockTest
    {
        [TestMethod]
        public void TestR()
        {
            var type = typeof(Blur);
            var properties = TypeDescriptor.GetProperties(type)
                .OfType<PropertyDescriptor>()
                .ToList();
           
        }
    }
}
