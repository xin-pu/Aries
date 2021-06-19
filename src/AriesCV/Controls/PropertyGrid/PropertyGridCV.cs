using HandyControl.Controls;

namespace AriesCV.Controls.PropertyGrid
{
    
    public class PropertyGridCV : HandyControl.Controls.PropertyGrid
    {
        
        public override PropertyResolver PropertyResolver { get; } = new PropertyResolverCV();

     
    }
}
