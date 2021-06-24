using Aries.OpenCV.Blocks;
using Aries.OpenCV.GraphModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace Aries.OpenCV.BlocksOCR
{
    public class GetBarCode : MatExport<TextSegment[]>
    {
        public override bool CanExecute()
        {
            throw new NotImplementedException();
        }

        public override void Execute()
        {
            DateTime now = DateTime.Now;
             

            using (Bar. scanner = new ZBar.ImageScanner())
            {
                scanner.SetConfiguration(ZBar.SymbolType.None, ZBar.Config.Enable, 0);
                scanner.SetConfiguration(ZBar.SymbolType.CODE39, ZBar.Config.Enable, 1);
                scanner.SetConfiguration(ZBar.SymbolType.CODE128, ZBar.Config.Enable, 1);

                List<ZBar.Symbol> symbols = new List<ZBar.Symbol>();
                symbols = scanner.Scan((Image)pImg);

                if (symbols != null && symbols.Count > 0)
                {
                    string result = string.Empty;
                    symbols.ForEach(s => result += "条码内容:" + s.Data + " 条码质量:" + s.Quality + Environment.NewLine);
                    MessageBox.Show(result);
                }
            }

        }
    }
}
