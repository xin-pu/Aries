using System;
using System.Collections.Generic;
using System.Windows.Media;
using Aries.Control.CvControl.Editor;
using HandyControl.Controls;
using OpenCvSharp;

namespace Aries.Control.CvControl
{
    public class PropertyResolverCV : PropertyResolver
    {

        public override PropertyEditorBase CreateDefaultEditor(Type type)
        {
            var res = CVTypeCodeDic.TryGetValue(type, out var editorType);
            var editor = (res)
                ? CreateCVEditor(editorType)
                : base.CreateDefaultEditor(type);
            return editor;
        }


        public PropertyEditorBase CreateCVEditor(CVEditorType type)
        {
            switch (type)
            {
                case CVEditorType.Brush:
                    return new ColorPropertyEditor();
                default:
                    return new PlainTextPropertyEditor();
            }
        }


        private static readonly Dictionary<Type, CVEditorType> CVTypeCodeDic =
            new Dictionary<Type, CVEditorType>
            {
                [typeof(Brush)] = CVEditorType.Brush,
                [typeof(Point)] = CVEditorType.Point
            };

        public enum CVEditorType
        {
            Brush,
            Point
        }

    }
}
