using FinanceCo.Library;
using System.Collections.ObjectModel;

namespace FinanceCo.Views;

public partial class MainOverseePage : ContentPage
{
    public ObservableCollection<OperationUnit> Operations { get; set; }

    public MainOverseePage()
    {
        InitializeComponent();
        Operations = new ObservableCollection<OperationUnit>();
        BindingContext = this; // Встановлюємо BindingContext
        RefreshListOperations();
    }

    private void listOperations_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (listOperations.SelectedItem != null)
        {
            Shell.Current.GoToAsync($"{nameof(EditInfoPage)}?Id={((OperationUnit)listOperations.SelectedItem).OperationId}");
        }
    }

    private void listOperations_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        listOperations.SelectedItem = null;
    }

    private async void AddButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(AddInfoPage));
        RefreshListOperations();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        RefreshListOperations();
    }

    private void RefreshListOperations()
    {
        Operations.Clear();
        List<OperationUnit> operations = OperationUnitRepository.GetOperations();
        foreach (var operation in operations)
        {
            Operations.Add(operation);
        }
    }

    private async void OnDeleteButton_Clicked(object sender, EventArgs e)
    {
        var MenuItem = sender as MenuItem;
        if (MenuItem == null) return;

        var operation = MenuItem.CommandParameter as OperationUnit;

        bool shouldDelete = await DisplayAlert("Підтвердження", "Ви точно хочете це видалити?", "Так", "Ні");

        if (!shouldDelete) return;
        OperationUnitRepository.DeleteOperation(operation.OperationId);
        RefreshListOperations();
    }
}

