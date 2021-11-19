using Caliburn.Micro;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Toolkit.Client.Views
{
    public class RecreationViewModel : Conductor<IRecreationModule>.Collection.OneActive, IDisplayModule
    {
        public byte SortNumber { get; init; } = 1;

        public override string DisplayName { get; set; } = "Recreation";

        public RecreationViewModel(IEnumerable<IRecreationModule> items)
        {
            Items.AddRange(items.OrderBy(item => item.SortNumber));
        }

        protected override async Task OnActivateAsync(CancellationToken cancellationToken)
        {
            await this.ActivateFirstItemAsync();
            await base.OnActivateAsync(cancellationToken);
        }
    }
}