using OasiBank.ViewModel;

namespace OasiBank.View;

public partial class BankTransferPage : ContentPage
{
	public BankTransferPage(BankTransferPageViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}