using FinanceCo.Library;
using FinanceCo.Views.Controls;
using Microcharts;
using SkiaSharp;

namespace FinanceCo.Views;

public partial class ShowStatsPage : ContentPage
{
    public ShowStatsPage()
    {
        InitializeComponent();
        CategoryPicker.ItemsSource = Enum.GetValues(typeof(OperationCategory));
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        ShowSavedMoneyThroughLastFourWeekends.Text = ShowSavedMoneyFunction();

        TheBiggestOperationThroughLastFourWeekends.Text = TheBiggestOperationFunction();

        //charts:
        LastMonthByCategoriesGraph.Chart = new DonutChart()
        {
            Entries = DiagramsHandler.ThisWeekByCategoriesGraph(OperationUnitRepository.GetOperationsOnTheLastFourWeeks()),
            BackgroundColor = SKColors.Transparent,
            LabelColor = SKColors.White
        };

        LastFourWeeksByCategoriesGraph.Chart = new BarChart()
        {
            Entries = DiagramsHandler.LastFourWeeksByCategoriesGraph(OperationUnitRepository.GetOperationsOnTheLastFourWeeks(), OperationCategory.Food),
            BackgroundColor = SKColors.Transparent,
            LabelColor = SKColors.White
        };
    }

    private static string ShowSavedMoneyFunction()
    {
        double saved = OperationUnitRepository.CurrentGoal * 28;
        foreach (var operation in OperationUnitRepository.GetOperationsOnTheLastFourWeeks())
        {
            saved -= operation.Value;
        }
        return (saved > 0) ? $"За останній місяць ти досяг ціль {DiagramsHandler.reached_goal} рази, зберігши при цьому {saved} Zł!" : $"За останній місяць ти досяг ціль {DiagramsHandler.reached_goal} рази, проте вийшов у мінус на {saved} Zł";
    }

    private static string TheBiggestOperationFunction()
    {
        var BiggestOperation = OperationUnitRepository.GetOperationsOnTheLastFourWeeks().OrderByDescending(o => o.Value).FirstOrDefault();
        if (BiggestOperation == null)
        {
            return "За останній місяць не було жодної транзакції";
        }
        return $"Найбільша транзакція за останній місяць: {BiggestOperation.Value} Zł на {BiggestOperation.Category} у {BiggestOperation.Date.ToShortDateString()}";
    }

    private void OnCategoryPickerIndexChanged(object sender, EventArgs e)
    {
        CategoryPicker.SelectedItem ??= OperationCategory.Food;
        LastFourWeeksByCategoriesGraph.Chart = new BarChart()
        {
            Entries = DiagramsHandler.LastFourWeeksByCategoriesGraph(OperationUnitRepository.GetOperationsOnTheLastFourWeeks(), (OperationCategory)CategoryPicker.SelectedItem),
            BackgroundColor = SKColors.Transparent,
            LabelColor = SKColors.White
        };
    }
}