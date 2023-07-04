using Examen.Views;

namespace Examen;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new GetApiServices();
	}
}
