using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Aries.OpenCV.GraphModel;

namespace Aries.OpenCV.Core
{
    public class BlockHelper
    {

        private static readonly Dictionary<string, string> IconDictionary =
            new Dictionary<string, string>
            {
                ["Default"] = "\ued8d"
            };

        private static readonly Dictionary<string, string> PointIconDictionary =
            new Dictionary<string, string>
            {
                ["Point"] = "\ue6be",
                ["Scalar"] = "\uef53",
                ["Size"] = "\ue61b",
                ["CircleSegment[]"] = "\ued6f",
                ["CircleSegment"] = "\ued6f",
                ["LineSegmentPoint[]"] = "\ued74",
                ["LineSegmentPoint"] = "\ued74",
                ["MatType"] =  "\ued8d",
                ["Mat[]"] = "\ued8d",
                ["Mat"] = "\ued8d",
                ["Default"] = "\uef71",
            };

        public static List<Type> GetAllBlockClassType()
        {
            var executingAssembly = Assembly.GetExecutingAssembly();
            var allTypes = executingAssembly.GetTypes().Where(a=>!a.IsAbstract);
            var blockType = allTypes.Where(a =>a.IsSubclassOf(typeof(BlockVertex)));
            return blockType.ToList();
        }



        public static Dictionary<Type, string> GetAllCVCategory()
        {
            var types = GetAllBlockClassType().ToList();
            return types.ToDictionary(a => a, GetCvCategory);
        }

        public static string GetCvCategory(Type blockTypeClass)
        {
            try
            {
                //通过反射得到MyClass类的信息
                var info = blockTypeClass;

                //得到施加在MyClass类上的定制Attribute
                var attribute = Attribute.GetCustomAttribute(info, typeof(CategoryAttribute)) as CategoryAttribute;
                if (attribute == null)
                    throw new ArgumentNullException();
                return attribute.Category;
            }
            catch (Exception)
            {
                return "Unclassified";
            }
        }

        public static string GetBlockICon(string cvCategory)
        {
            return IconDictionary.ContainsKey(cvCategory)
                ? IconDictionary[cvCategory]
                : IconDictionary["Default"];
        }

        public static string GetPointICon(string typeName)
        {
            return PointIconDictionary.ContainsKey(typeName)
                ? PointIconDictionary[typeName]
                : PointIconDictionary["Default"];
        }


        public static BlockVertex CreateBlockVertex(Type type)
        {
            return (BlockVertex) Activator.CreateInstance(type, null);
        }

    }
}
