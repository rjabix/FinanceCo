﻿using FinanceCo.Library;
using FinanceCo.Views.Controls;
using Microcharts;
using Microcharts.Maui;
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

        CategoryPicker.SelectedItem = OperationCategory.Food;

        //charts:
        LastMonthByCategoriesGraph.Chart = new DonutChart()
        {
            Entries = DiagramsHandler.ThisWeekByCategoriesGraph(OperationUnitRepository.GetOperationsOnTheLastFourWeeks()),
            BackgroundColor = SKColors.Transparent,
            LabelColor = SKColors.White
        };

        // --->>>This is the second displayed chart, but it is displayed via OnCategoryPickerIndexChanged event down below<<<---
        //
        //LastFourWeeksByCategoriesGraph.Chart = new PointChart()
        //{
        //    Entries = DiagramsHandler.LastFourWeeksByCategoriesGraph(OperationUnitRepository.GetOperationsOnTheLastFourWeeks(), OperationCategory.Food),
        //    BackgroundColor = SKColors.Transparent,
        //    LabelColor = SKColors.White
        //};

        TheBiggestCategoryLastFourWeeksGraph.Chart = new LineChart()
        {
            Entries = DiagramsHandler.TheBiggestCategoryLastFourWeeksGraph(OperationUnitRepository.GetOperationsOnTheLastFourWeeks()),
            BackgroundColor = SKColors.Transparent,
            LabelColor = SKColors.White,
            LabelOrientation = Orientation.Horizontal
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

        // Створити новий ChartView
        var newChartView = new ChartView
        {
            HeightRequest = 250,
            Margin = new Thickness(20, 0, 0, 0)
        };

        newChartView.Chart = new BarChart() // Change to PointChart() if BarCHart() looks bad
        {
            Entries = DiagramsHandler.LastFourWeeksByCategoriesGraph(OperationUnitRepository.GetOperationsOnTheLastFourWeeks(), (OperationCategory)CategoryPicker.SelectedItem),
            BackgroundColor = SKColors.Transparent,
            LabelColor = SKColors.White,
            LabelOrientation = Orientation.Horizontal
        };
// -------------->>>TMP: index = 3 !!!<<<----------------
        //To find the index of the old chart view, uncomment the following code, and look through debbuger to (int) index:


        //var oldChartView = LeftVerticalStackLayout.FindByName<ChartView>("LastFourWeeksByCategoriesGraph");
        //if(oldChartView == null)
        //    oldChartView = LeftVerticalStackLayout.FindByName<ChartView>("newChartView");
        
        //if (oldChartView != null)
        //{
            //var index = LeftVerticalStackLayout.Children.IndexOf(oldChartView);
            LeftVerticalStackLayout.Children[3] = newChartView;
        //}
    }

    private void OnBackButtonClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainOverseePage");
    }
}