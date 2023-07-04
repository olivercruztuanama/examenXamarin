namespace Examen.ViewModels
{
    public class MainViewModel
    {
        private static MainViewModel instance;
        public MainViewModel()
        {
            instance = this;
            this.GetApiServices = new GetApiServicesViewModel();
        }
        public GetApiServicesViewModel GetApiServices { get; set; }
        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }

            return instance;
        }
    }
}
