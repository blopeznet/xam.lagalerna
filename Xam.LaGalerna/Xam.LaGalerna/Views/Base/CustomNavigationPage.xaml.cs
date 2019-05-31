using Plugin.SharedTransitions;
using Xamarin.Forms;

namespace Xam.LaGalerna.Views
{
    public partial class CustomNavigationPage : SharedTransitionNavigationPage
    {
        public CustomNavigationPage(Page root) : base(root)
        {
            InitializeComponent();
        }
    }
}