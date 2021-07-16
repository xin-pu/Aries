using System;
using System.IO;
using System.Linq;
using GraphX.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AriesTest
{
    [TestClass]
    public class UnitTest1
    {
        private string folder = @"\\wux-e81000276\CameraLog\LeftCameraImage\120_12_11";
        private string outDire = "D:\\Test";
        private string outDire2 = "D:\\Update";


        [TestMethod]
        public void SelectJPG()
        {
            var mainfolder = new DirectoryInfo(folder);
            var directories = mainfolder.GetDirectories();
            foreach (var dire in directories)
            {
                var files = dire.GetFiles();
                var filejpg = files.Where(a => a.Extension == ".bmp");
                filejpg.ForEach(f =>
                {
                    var dest = Path.Combine(outDire, f.Name);
                    File.Copy(f.FullName, dest, true);
                });

            }
        }


        [TestMethod]
        public void SelectJPG2()
        {
            var mainfolder = new DirectoryInfo(outDire);
            var files = mainfolder.GetFiles();
            var filejpg = files.Where(a => a.LastWriteTime > new DateTime(2021, 4, 1));
            filejpg.ForEach(f =>
            {
                try
                {
                    var dest = Path.Combine(outDire2, f.Name);
                    File.Copy(f.FullName, dest, true);
                }
                catch
                {
                    // ignored
                }
            });
        }
    }
}
