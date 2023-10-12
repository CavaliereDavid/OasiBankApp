using OasiBank.ViewModel;

namespace OasiBank.View;

public partial class PhoneCreditRechargePage : ContentPage
{
	public PhoneCreditRechargePage(PhoneCreditRechargeViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}