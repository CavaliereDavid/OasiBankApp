using OasiBank.ViewModel;

namespace OasiBank.View;

public partial class RegistrationPage : ContentPage
{
	public RegistrationPage(RegistrationPageViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
	}
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }
}