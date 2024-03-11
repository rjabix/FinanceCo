namespace FinanceCo.Views;

public partial class MainOverseePage : ContentPage
{
	public MainOverseePage()
	{
		InitializeComponent();
	}

    private void OnEditButtonClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync(nameof(EditInfoPage));
    }

    private void OnAddButtonClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync(nameof(AddInfoPage));
    }
}