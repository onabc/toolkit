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
    public class TranslatorViewModel : Screen, IDisplayModule
    {
        public byte SortNumber => 0;
        public Geometry Icon => Application.Current.TryFindResource("translator") as Geometry;

        public override string DisplayName { get; set; } = "翻译";
    }
}