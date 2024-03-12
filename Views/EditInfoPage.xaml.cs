using FinanceCo.Library;

namespace FinanceCo.Views;
[QueryProperty(nameof(OperationUnitId), "Id")]
public partial class EditInfoPage : ContentPage
{
	public EditInfoPage()
	{
		InitializeComponent();
	}

	public string OperationUnitId
	{
        set
		{
            OperationUnit Current_operation = OperationUnitRepository.GetOperationByID(int.Parse(value));
            BindingContext = Current_operation;
			lblTxt.Text = Current_operation.Title_Text;
        }
    }
}