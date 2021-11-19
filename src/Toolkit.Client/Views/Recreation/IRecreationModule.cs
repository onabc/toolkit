using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolkit.Client.Views
{
    public interface IRecreationModule : IScreen
    {
        /// <summary>
        /// 序号
        /// </summary>
        byte SortNumber { get; init; }
    }
}