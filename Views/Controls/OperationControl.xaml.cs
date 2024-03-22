using FinanceCo.Library;
using System.Globalization;

namespace FinanceCo.Views.Controls;

public partial class OperationControl : ContentView
{
    public event EventHandler<string> OnError;
    public event EventHandler<EventArgs> OnSave;
    public OperationControl()
    {

        InitializeComponent();
        CategoryPicker.ItemsSource = Enum.GetValues(typeof(OperationCategory));
        DateEntry.Date = DateTime.Now;
        DateEntry.MaximumDate = DateTime.Now;

    }

    public string Value
    {
        get => ValueEntry.Text;
        set => ValueEntry.Text = value;
    }
    public DateTime Date
    {
        get => DateEntry.Date;
        set => DateEntry.Date = value;
    }
    public OperationCategory Category
    {
        get => (CategoryPicker.SelectedItem != null) ? OperationUnitRepository.ToOperationCategory(CategoryPicker.SelectedItem.ToString()) : OperationCategory.Other;
        set => CategoryPicker.SelectedItem = OperationUnitRepository.ToOperationCategory(value.ToString());
    }
    public string Description
    {
        get => DescriptionEntry.Text;
        set => DescriptionEntry.Text = value;
    }

    private void Save_Button_Clicked(object sender, EventArgs e)
    {
        if (ValueValidator.IsNotValid)
        {
            OnError?.Invoke(sender, "Invalid input");
            return;
        }

        //string userInput = DateEntry.Text; // Example input from the user

        //string[] formats = { "dd.MM.yyyy", "d.M.yyyy", "d.MM.yyyy", "dd.M.yyyy" }; // Define possible date formats

        //DateTime date;
        //if (!DateTime.TryParseExact(userInput, formats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out date))
        //{
        //    OnError?.Invoke(sender, "Invalid date format");
        //    return;
        //}
        try
        {
            if (Convert.ToDouble(ValueEntry.Text) <= 0)
            {
                OnError?.Invoke(sender, "Value must be positive");
                return;
            }
        }
        catch
        {
            OnError?.Invoke(sender, "Invalid value format");
            return;
        }

        //if (date > DateTime.Now)
        //{
        //    OnError?.Invoke(sender, "Date cannot be in the future");
        //    return;
        //}
        OnSave?.Invoke(sender, e);
    }
}