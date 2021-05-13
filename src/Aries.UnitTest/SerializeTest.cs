using System.IO;
using System.Xml.Serialization;
using Aries.OpenCV.Blocks.Processing;
using Aries.OpenCV.GraphModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aries.UnitTest
{
    [TestClass]
    public class SerializeTest
    {
        [TestMethod]
        public void SerializeBlur()
        {
            var a = new Blur();
            using (var fs = new FileStream("D:\\Blur.xml", FileMode.Create))
            {
                var formatter = new XmlSerializer(a.GetType());
                formatter.Serialize(fs, a);
            }
        }


        [TestMethod]
        public void SerializeEgde()
        {
            var a = new BlockEdge();
            using (var fs = new FileStream("D:\\BlockEdge.xml", FileMode.Create))
            {
                var formatter = new XmlSerializer(a.GetType());
                formatter.Serialize(fs, a);
            }
        }

        [TestMethod]
        public void SerializeLogicCore()
        {
           
        }
    }
}
