using OasiBank.ViewModel;

namespace OasiBank.View;

public partial class HomePage : ContentPage
{
	public HomePage(HomePageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}