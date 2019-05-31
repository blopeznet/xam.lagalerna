using System.Threading.Tasks;
using Xamarin.Forms;

namespace Xam.LaGalerna.ViewModels.Base
{
    public class ViewModelBase : BindableObject
    {
        public virtual Task InitializeAsync(object navigationData) => Task.FromResult(false);
    }
}
