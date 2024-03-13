using FinanceCo.Library;
using System.Numerics;

namespace FinanceCo.Views;

public partial class AddInfoPage : ContentPage
{
	public AddInfoPage()
	{
		InitializeComponent();
	}

    private void Save_Button_Clicked(object sender, EventArgs e)
    {
        OperationUnitRepository.AddOperation(double.Parse(operationControl.Value), operationControl.Date,operationControl.Category, operationControl.Description);
        Shell.Current.GoToAsync($"//{nameof(MainOverseePage)}");
    }

    private void operationControl_OnError(object sender, string e)
    {
        DisplayAlert("Error", e, "OK");
    }
}