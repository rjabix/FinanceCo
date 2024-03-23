using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceCo.Library;
using Microcharts;
using Microcharts.Maui;
using SkiaSharp;

namespace FinanceCo.Views.Controls
{
    public class DiagramsHandler
    {
        public static int reached_goal = 0;
        /* public static ChartEntry[] entries = new[]
         {
            new ChartEntry(212)
            {
                Label = "January",
                ValueLabel = "212",
                Color = SKColor.Parse("#266489")
            },
            new ChartEntry(248)
            {
                Label = "February",
                ValueLabel = "248",
                Color = SKColor.Parse("#68B9C0")
            },
            new ChartEntry(514)
            {
                Label = "March",
                ValueLabel = "514",
                Color = SKColor.Parse("#90D585")
            },
            new ChartEntry(100)
            {
                Label = "April",
                ValueLabel = "100",
                Color = SKColor.Parse("#e77e23")
            }
        };*/

        public static ChartEntry[] ThisWeekByCategoriesGraph(List<OperationUnit> operations)
        {
            Dictionary<OperationCategory, double> keyValuePairs = new Dictionary<OperationCategory, double>();

            foreach (var operation in operations)
            {
                if (!keyValuePairs.ContainsKey(operation.Category))
                {
                    keyValuePairs[operation.Category] = 0;
                }

                keyValuePairs[operation.Category] += operation.Value;
            }

            return new[]
            {
                   new ChartEntry((float)(keyValuePairs.ContainsKey(OperationCategory.Food) ? keyValuePairs[OperationCategory.Food] : 0)){
                       Label = "Food",
                       ValueLabelColor = SKColors.White,
                       ValueLabel = (keyValuePairs.ContainsKey(OperationCategory.Food) ? keyValuePairs[OperationCategory.Food] : 0).ToString(),
                       Color = SKColor.Parse("#90D585") // Green
                   },
                   new ChartEntry((float)(keyValuePairs.ContainsKey(OperationCategory.Transport) ? keyValuePairs[OperationCategory.Transport] : 0))
                   {
                       Label = "Transport",
                       ValueLabelColor = SKColors.White,
                       ValueLabel = (keyValuePairs.ContainsKey(OperationCategory.Transport) ? keyValuePairs[OperationCategory.Transport] : 0).ToString(),
                       Color = SKColor.Parse("#68B9C0") // Blue
                   },
                   new ChartEntry((float)(keyValuePairs.ContainsKey(OperationCategory.Alcohol) ? keyValuePairs[OperationCategory.Alcohol] : 0))
                   {
                        Label = "Alcohol",
                       ValueLabelColor = SKColors.White,
                        ValueLabel = (keyValuePairs.ContainsKey(OperationCategory.Alcohol) ? keyValuePairs[OperationCategory.Alcohol] : 0).ToString(),
                        Color = SKColor.Parse("#e77e23") // Orange
                   },
                   new ChartEntry((float)(keyValuePairs.ContainsKey(OperationCategory.Entertainment) ? keyValuePairs[OperationCategory.Entertainment] : 0))
                   {
                        Label = "Entertainment",
                       ValueLabelColor = SKColors.White,
                        ValueLabel = (keyValuePairs.ContainsKey(OperationCategory.Entertainment) ? keyValuePairs[OperationCategory.Entertainment] : 0).ToString(),
                        Color = SKColor.Parse("#FF0000") // Red
                   },
                   new ChartEntry((float)(keyValuePairs.ContainsKey(OperationCategory.Other) ? keyValuePairs[OperationCategory.Other] : 0))
                   {
                        Label = "Other",
                       ValueLabelColor = SKColors.White,
                        ValueLabel = (keyValuePairs.ContainsKey(OperationCategory.Other) ? keyValuePairs[OperationCategory.Other] : 0).ToString(),
                        Color = SKColor.Parse("#808080") // Grey
                   }
            };
        }

        public static ChartEntry[] ThisMonthByGoalGraph()
        {
            reached_goal = 0;
            List<OperationUnit> operations = OperationUnitRepository.GetOperationsOnTheLastFourWeeks();
            double sum = 0;
            Dictionary<int, double> keyValuePairs = new Dictionary<int, double>
            {
                { 0, 0 },
                { 1, 0 },
                { 2, 0 },
                { 3, 0 },
                { 4, 0 }
            };
            DateTime currentDate = DateTime.Now;
            DateTime startOfWeek = currentDate.AddDays(-(int)currentDate.DayOfWeek);
            DateTime endOfWeek = startOfWeek.AddDays(7);
            Dictionary<int, string> keyValuePairs1 = new Dictionary<int, string>
            {
                { 0, "" },
                { 1, "" },
                { 2, "" },
                { 3, "" },
                { 4, "" }
            };
            for(int i = 0; i<5; i++)
            {
                foreach (var operation in operations)
                {
                    if (operation.Date >= startOfWeek.AddDays(-7 * i) && operation.Date < endOfWeek.AddDays(-7 * i))
                    {
                        keyValuePairs[4-i] += operation.Value;
                        if (keyValuePairs1[4-i] == "")
                        {
                            keyValuePairs1[4 - i] = $"{startOfWeek.AddDays(-7 * i):dd/MM} - {endOfWeek.AddDays(-7 * i):dd/MM}";
                        }
                    }
                }
            }
           
            List<ChartEntry> entries = new List<ChartEntry>();
            int j = 0;
            foreach (var pair in keyValuePairs)
            {
                if(pair.Value/7 < OperationUnitRepository.CurrentGoal && pair.Value > 0) reached_goal++;
                if ((float)pair.Value > 0)
                {
                    sum += (float)pair.Value;

                    entries.Add(new ChartEntry((float)pair.Value)
                    {
                        Label = keyValuePairs1[j],
                        ValueLabel = pair.Value.ToString(),
                        ValueLabelColor = SKColors.White,
                        Color = (pair.Value / 7 > OperationUnitRepository.CurrentGoal) ? SKColor.Parse("#CC0000") : SKColor.Parse("#66CC00")
                    });
                }
                j++;
            }
            return [.. entries];
        }

    }
}
