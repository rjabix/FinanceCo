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
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        double saved = OperationUnitRepository.CurrentGoal * 28;
        foreach (var operation in OperationUnitRepository.GetOperationsOnTheLastFourWeeks())
        {
            saved -= operation.Value;
        }
        ShowSavedMoneyThroughLastFourWeekends.Text = (saved > 0) ? $"За останній місяць ти досяг ціль {DiagramsHandler.reached_goal} рази, зберігши при цьому {saved} Zł!" : $"За останній місяць ти досяг ціль {DiagramsHandler.reached_goal} рази, проте вийшов у мінус на {saved} Zł";

        //charts:
        LastMonthByCategoriesGraph.Chart = new DonutChart()
        {
            Entries = DiagramsHandler.ThisWeekByCategoriesGraph(OperationUnitRepository.GetOperationsOnTheLastFourWeeks()),
            BackgroundColor = SKColors.Transparent,
            LabelColor = SKColors.White
        };
    }
}