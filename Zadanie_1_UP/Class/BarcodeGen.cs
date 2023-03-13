using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aspose.BarCode.Generation;

namespace Zadanie_1_UP
{
    internal class BarcodeGen
    {
        public string Text { get; set; }

        public BaseEncodeType BarcodeType { get; set; }

        public BarCodeImageFormat ImageType { get; set; }
    }
}
