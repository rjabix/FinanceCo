using FinanceCo.Library;

namespace FinanceCo.Views;
[QueryProperty(nameof(OperationUnitId), "Id")]
public partial class EditInfoPage : ContentPage
{
    private OperationUnit Current_operation;
	public EditInfoPage()
	{
		InitializeComponent();
	}

	public string OperationUnitId
	{
        set
		{
            Current_operation = OperationUnitRepository.GetOperationByID(int.Parse(value));
            if(Current_operation != null)
            {
                BindingContext = Current_operation;
                operationControl.Value = Current_operation.Value.ToString();
                operationControl.Date = Current_operation.Date.Date;
                operationControl.Category = Current_operation.Category;
                if (Current_operation.Description != null) operationControl.Description = Current_operation.Description;
            }
           
			//lblTxt.Text = Current_operation.Title_Text;
        }
    }

    private void Save_Button_Clicked(object sender, EventArgs e)
    {
        Current_operation.Value = double.Parse(operationControl.Value);
        Current_operation.Date = operationControl.Date;
        Current_operation.Category = operationControl.Category;
        Current_operation.Description = operationControl.Description;
        OperationUnitRepository.EditOperation(Current_operation.OperationId, Current_operation);
        Shell.Current.GoToAsync("..");
    }

    private void operationControl_OnError(object sender, string e)
    {
        DisplayAlert("Error", e, "OK");
    }
}