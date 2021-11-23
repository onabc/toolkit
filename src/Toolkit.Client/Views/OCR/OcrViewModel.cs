using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Toolkit.Client
{
    public class OcrViewModel : Screen, IDisplayModule
    {
        public byte SortNumber => 1;
        public Geometry Icon => Application.Current.TryFindResource("ocr") as Geometry;

        public override string DisplayName { get; set; } = "文字识别";
    }
}