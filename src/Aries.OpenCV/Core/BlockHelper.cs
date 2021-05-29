using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Aries.OpenCV.GraphModel;

namespace Aries.OpenCV.Core
{
    public class BlockHelper
    {
        public static List<Type> GetBlockClassType()
        {
            var executingAssembly = Assembly.GetExecutingAssembly();
            var allTypes = executingAssembly.GetTypes();
            var blockType = allTypes.Where(a => a.BaseType?.BaseType == typeof(BlockVertex));
            return blockType.ToList();
        }

        public static BlockType GetBlockType(Type blockTypeClass)
        {
            var baseType = blockTypeClass.BaseType;
            if (baseType == null)
                throw new ArgumentOutOfRangeException();
            if (baseType.Name.Contains("ExportBlock"))
                return BlockType.Export;
            if (baseType.Name.Contains("ImportBlock"))
                return BlockType.Import;
            if (baseType.Name.Contains("ProcessingBlock"))
                return BlockType.Processing;
            return GetBlockType(baseType);
        }

        public static string GetBlockICon(Type blockTypeCLass)
        {
            var typename = blockTypeCLass.Name;
            return iconDictionary.ContainsKey(typename)
                ? iconDictionary[typename]
                : iconDictionary["Default"];
        }

        private static readonly Dictionary<string, string> iconDictionary = new Dictionary<string, string>
        {
            ["Default"] = "\uef71",
            ["ImageRead"] = "\uef71"
        };


        public static BlockVertex CreateBlockVertex(Type type)
        {
            return (BlockVertex) Activator.CreateInstance(type, null);
        }

    }
}
