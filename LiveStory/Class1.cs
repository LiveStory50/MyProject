using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveStory
{
    public class DoExcel
    {
        public void ToExcel()
        {
            FileStream fs = new FileStream(, FileMode.Open, FileAccess.Read);
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook(fs);
            int sheetCount = book.NumberOfSheets;.
        }
    }
}
