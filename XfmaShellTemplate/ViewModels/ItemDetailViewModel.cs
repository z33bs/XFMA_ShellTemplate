using System.Threading.Tasks;
using XamarinFormsMvvmAdaptor;
using XfmaShellTemplate.Models;

namespace XfmaShellTemplate.ViewModels
{
    public class ItemDetailViewModel : MvvmViewModelBase
    {
        Item item;
        public Item Item
        {
            get => item;
            set => SetProperty(ref item, value);
        }

        public override Task OnViewPushedAsync(object navigationData)
        {
            var item = navigationData as Item;
            Title = item?.Text;
            Item = item;

            return base.OnViewPushedAsync(navigationData);
        }
    }
}
