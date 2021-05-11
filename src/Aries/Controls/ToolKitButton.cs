using System;
using System.Windows;
using System.Windows.Controls;
using Aries.OpenCV.GraphModel;

namespace Aries.Controls
{
    public class ToolKitButton : Button
    {
        static ToolKitButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ToolKitButton),
                new FrameworkPropertyMetadata(typeof(ToolKitButton)));
        }

        public Type BlockClassType
        {
            get { return (Type)GetValue(BlockClassTypeProperty); }
            set { SetValue(BlockClassTypeProperty, value); }
        }

        public static readonly DependencyProperty BlockClassTypeProperty =
            DependencyProperty.Register("BlockClassType", typeof(Type), typeof(ToolKitButton),
                new PropertyMetadata(null));

        public BlockType BlockType
        {
            get { return (BlockType)GetValue(BlockTypeProperty); }
            set { SetValue(BlockTypeProperty, value); }
        }

        public static readonly DependencyProperty BlockTypeProperty =
            DependencyProperty.Register("BlockType", typeof(BlockType), typeof(ToolKitButton),
                new PropertyMetadata(null));


    }
}
