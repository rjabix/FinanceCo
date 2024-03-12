using FinanceCo.Library;

namespace FinanceCo.Views;

public partial class MainOverseePage : ContentPage
{
	public MainOverseePage()
	{
		InitializeComponent();

        List<OperationUnit> operations = OperationUnitRepository.GetOperations();

        listOperations.ItemsSource = operations;
	}

    //private void OnEditButtonClicked(object sender, EventArgs e)
    //{
    //    Shell.Current.GoToAsync(nameof(EditInfoPage));
    //}

    //private void OnAddButtonClicked(object sender, EventArgs e)
    //{
    //    Shell.Current.GoToAsync(nameof(AddInfoPage));
    //}

    private void listOperations_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if(listOperations.SelectedItem != null)
        {
            Shell.Current.GoToAsync($"{nameof(EditInfoPage)}?Id={((OperationUnit)listOperations.SelectedItem).OperationId}");
        }
    }

    private void listOperations_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        listOperations.SelectedItem = null;
    }
}