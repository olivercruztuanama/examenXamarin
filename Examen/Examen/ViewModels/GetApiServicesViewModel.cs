using Examen.Models;
using Examen.Services;
using System.Collections.ObjectModel;

namespace Examen.ViewModels
{
    public class GetApiServicesViewModel : BaseViewModel
    {
        private ObservableCollection<ApiClass> listaprincipal;
        public GetApiServicesViewModel()
        {
            GetApi();
        }

        public ObservableCollection<ApiClass> Listaprincipal
        {
            get { return this.listaprincipal; }
            set { SetValue(ref this.listaprincipal, value); }
        }
        private async void GetApi()
        {
            var dato = await new ApiService().CallApi<ApiClass>("https://random-data-api.com/api/v2/users?size=20", string.Empty);
            if (dato.IsSuccess)
            {
                this.Listaprincipal = new ObservableCollection<ApiClass>((List<ApiClass>)dato.Result);
            }
        }
    }
}
