using System.Collections.Generic;
using System.IO;
using GalaSoft.MvvmLight;
using GraphX.Common.Models;
using YAXLib;

namespace Aries.OpenCV.GraphModel
{
    public class GraphCVFileStruct : ViewModelBase
    {

        public string Name { set; get; }

        public List<GraphSerializationData> GraphSerializationDatas { set; get; }

        public GraphCVLayoutConfig GraphCvLayoutConfig { set; get; }


        #region Serialize DeSerialize

        /// <summary>
        /// Serializes data classes list to file
        /// </summary>
        /// <param name="filename">File name</param>
        /// <param name="modelsList">Data classes list</param>
        public static void SerializeGraphDataToFile(string filename, GraphCVFileStruct modelsList)
        {
            using var stream = File.Open(filename, FileMode.Create, FileAccess.Write, FileShare.Read);
            var serializer = new YAXSerializer(typeof(GraphCVFileStruct));
            using var textWriter = new StreamWriter(stream);
            serializer.Serialize(modelsList, textWriter);
            textWriter.Flush();
        }

        /// <summary>
        /// Deserializes data classes list from file
        /// </summary>
        /// <param name="filename">File name</param>
        public static GraphCVFileStruct DeserializeGraphDataFromFile(string filename)
        {
            using var stream = File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
            var deserializer = new YAXSerializer(typeof(GraphCVFileStruct));
            using var textReader = new StreamReader(stream);
            return (GraphCVFileStruct)deserializer.Deserialize(textReader);
        }

        #endregion



    }
}
