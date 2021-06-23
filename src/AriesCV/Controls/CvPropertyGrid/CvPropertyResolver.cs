using System;
using System.Collections.Generic;
using System.Windows.Media;
using GalaSoft.MvvmLight.Command;
using HandyControl.Controls;
using OpenCvSharp;

namespace AriesCV.Controls
{
    public class CVPropertyResolver : PropertyResolver
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
                case CVEditorType.Point:
                    return new PointPropertyEditor();
                case CVEditorType.Size:
                    return new SizePropertyEditor();
                case CVEditorType.Scalar:
                    return new ScalarPropertyEditor();
                case CVEditorType.Mat:
                    return new MatPropertyEditor();
                case CVEditorType.Command:
                    return new CommandPropertyEditor();
                default:
                    return new PlainTextPropertyEditor();
            }
        }


        private static readonly Dictionary<Type, CVEditorType> CVTypeCodeDic =
            new Dictionary<Type, CVEditorType>
            {
                [typeof(Brush)] = CVEditorType.Brush,
                [typeof(Point)] = CVEditorType.Point,
                [typeof(Size)] = CVEditorType.Size,
                [typeof(Rect)] = CVEditorType.Rect,
                [typeof(Scalar)] = CVEditorType.Scalar,
                [typeof(Mat)] = CVEditorType.Mat,
                [typeof(RelayCommand)] = CVEditorType.Command
            };
    }
}
