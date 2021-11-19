using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolkit.Client.Views
{
    public class BattleCityViewModel : Screen, IRecreationModule
    {
        public byte SortNumber { get; init; } =1;

        public override string DisplayName { get; set; } = "Battle City";
    }
}
