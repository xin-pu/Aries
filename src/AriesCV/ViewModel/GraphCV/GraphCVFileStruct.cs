using System.Collections.Generic;
using System.IO;
using System.Linq;
using GalaSoft.MvvmLight;
using GraphX.Common.Models;
using GraphX.Controls;
using YAXLib;

namespace AriesCV.ViewModel
{
    public class GraphCVFileStruct : ViewModelBase
    {

        public List<GraphSerializationData> GraphSerializationDatas { set; get; }

        public GraphCVConfig GraphCVConfig { set; get; }


        #region Serialize DeSerialize

        /// <summary>
        /// Serializes data classes list to file
        /// </summary>
        /// <param name="filename">File name</param>
        /// <param name="modelsList">Data classes list</param>
        public static void SerializeGraphDataToFile(string filename, GraphCVFileStruct modelsList)
        {
            using var stream = File.Open(filename, FileMode.Create, FileAccess.Write, FileShare.Read);
            SerializeDataToStream(stream, modelsList);
        }

        /// <summary>
        /// Deserializes data classes list from file
        /// </summary>
        /// <param name="filename">File name</param>
        public static GraphCVFileStruct DeserializeGraphDataFromFile(string filename)
        {
            using var stream = File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
            var res = DeserializeGraphDataFromStream(stream);
            foreach (var graphSerializationData in res.GraphSerializationDatas.Where(a =>
                a.Data.GetType() == typeof(ConnectionPointData)))
            {
                var a = graphSerializationData.Data as ConnectionPointData;
            }

            return res;
        }


        /// <summary>
        /// Serializes graph data list to a stream
        /// </summary>
        /// <param name="stream">The destination stream</param>
        /// <param name="modelsList">The graph data</param>
        public static void SerializeDataToStream(Stream stream, GraphCVFileStruct modelsList)
        {
            var serializer = new YAXSerializer(typeof(GraphCVFileStruct));
            using var textWriter = new StreamWriter(stream);
            serializer.Serialize(modelsList, textWriter);
            textWriter.Flush();
        }

        /// <summary>
        /// Deserializes graph data from a stream
        /// </summary>
        /// <param name="stream">The stream</param>
        /// <returns>The graph data</returns>
        public static GraphCVFileStruct DeserializeGraphDataFromStream(Stream stream)
        {
            var deserializer = new YAXSerializer(typeof(GraphCVFileStruct));
            using var textReader = new StreamReader(stream);
            return (GraphCVFileStruct) deserializer.Deserialize(textReader);
        }

        #endregion
    }
}
