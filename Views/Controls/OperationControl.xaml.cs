using System.Globalization;

namespace FinanceCo.Views.Controls;

public partial class OperationControl : ContentView
{
    public event EventHandler<string> OnError;
    public event EventHandler<EventArgs> OnSave;
    public OperationControl()
    {
        InitializeComponent();
    }

    public string Value
    {
        get => ValueEntry.Text;
        set => ValueEntry.Text = value;
    }
    public string Date
    {
        get => DateEntry.Text;
        set => DateEntry.Text = value;
    }
    public string Category
    {
        get => CategoryEntry.Text;
        set => CategoryEntry.Text = value;
    }
    public string Description
    {
        get => DescriptionEntry.Text;
        set => DescriptionEntry.Text = value;
    }

    private void Save_Button_Clicked(object sender, EventArgs e)
    {
        if (ValueValidator.IsNotValid || DateValidator.IsNotValid)
        {
            OnError?.Invoke(sender, "Invalid input");
            return;
        }

        string userInput = DateEntry.Text; // Example input from the user

        string[] formats = { "dd/MM/yyyy", "d/M/yyyy" }; // Define possible date formats

        DateTime date;
        if (!DateTime.TryParseExact(userInput, formats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out date))
        {
            OnError?.Invoke(sender, "Invalid date format");
            return;
        }
        OnSave?.Invoke(sender, e);
    }
}