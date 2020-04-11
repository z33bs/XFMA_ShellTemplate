using Xamarin.Forms;
using XfmaShellTemplate.Views;

namespace XfmaShellTemplate
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("newItem", typeof(NewItemPage));
        }
    }
}
