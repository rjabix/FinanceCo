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
        public static Dictionary<OperationCategory, string> CategoryToColor = new Dictionary<OperationCategory, string>
        {
            { OperationCategory.Food, "#90D585" },
            { OperationCategory.Transport, "#68B9C0" },
            { OperationCategory.Alcohol, "#e77e23" },
            { OperationCategory.Entertainment, "#FF0000" },
            { OperationCategory.Other, "#808080" }
        };
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
            var keyValuePairs = operations
                .GroupBy(operation => operation.Category)
                .ToDictionary(group => group.Key, group => group.Sum(operation => operation.Value));

            var chartEntries = new List<ChartEntry>();

            foreach (var category in Enum.GetValues(typeof(OperationCategory)).Cast<OperationCategory>())
            {
                var value = keyValuePairs.ContainsKey(category) ? keyValuePairs[category] : 0;

                chartEntries.Add(new ChartEntry((float)value)
                {
                    Label = category.ToString(),
                    ValueLabelColor = SKColors.White,
                    ValueLabel = value.ToString(),
                    Color = SKColor.Parse(CategoryToColor[category])
                });
            }

            return chartEntries.ToArray();
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
            DateTime startOfWeek = currentDate.AddDays((-(int)currentDate.DayOfWeek + 1 - 7) % 7).Date;
            DateTime endOfWeek = startOfWeek.AddDays(6).Date;
            Dictionary<int, string> keyValuePairs1 = new Dictionary<int, string>
            {
                { 0, "" },
                { 1, "" },
                { 2, "" },
                { 3, "" },
                { 4, "" }
            };
            for (int i = 0; i < 5; i++)
            {
                foreach (var operation in operations)
                {
                    if (operation.Date >= startOfWeek.AddDays(-7 * i) && operation.Date <= endOfWeek.AddDays(-7 * i))
                    {
                        keyValuePairs[4 - i] += operation.Value;
                        if (keyValuePairs1[4 - i] == "")
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
                if (pair.Value / 7 < OperationUnitRepository.CurrentGoal && pair.Value > 0) reached_goal++;
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

        public static ChartEntry[] LastFourWeeksByCategoriesGraph(List<OperationUnit> operations, OperationCategory category)
        {
            DateTime currentDate = DateTime.Now;
            DateTime startOfCurrentWeek = currentDate.AddDays((-(int)currentDate.DayOfWeek + 1 - 7) % 7);

            var chartEntries = Enumerable.Range(0, 5)
                .Select(i =>
                {
                    DateTime startOfWeek = startOfCurrentWeek.AddDays(-7 * i).Date;
                    DateTime endOfWeek = startOfWeek.AddDays(6).Date;

                    var totalValue = operations
                        .Where(operation => operation.Date >= startOfWeek && operation.Date <= endOfWeek && operation.Category == category)
                        .Sum(operation => operation.Value);

                    return new ChartEntry((float)totalValue)
                    {
                        Label = $"{startOfWeek:dd/MM} - {endOfWeek:dd/MM}",
                        ValueLabel = totalValue.ToString(),
                        ValueLabelColor = SKColors.White,
                        Color = SKColor.Parse(CategoryToColor[category])
                    };
                })
                .Reverse()
                .ToArray();

            return chartEntries;
        }


        public static ChartEntry[] TheBiggestCategoryLastFourWeeksGraph(List<OperationUnit> operations)
        {
            var startOfCurrentWeek = DateTime.Now.AddDays((-(int)DateTime.Now.DayOfWeek + 1 - 7) % 7);

            var chartEntries = Enumerable.Range(0, 5)
                .Select(i =>
                {
                    var startOfWeek = startOfCurrentWeek.AddDays(-7 * i).Date;
                    var endOfWeek = startOfWeek.AddDays(6).Date;

                    var categoryTotalValues = operations
                        .Where(operation => operation.Date >= startOfWeek && operation.Date <= endOfWeek)
                        .GroupBy(operation => operation.Category)
                        .ToDictionary(group => group.Key, group => group.Sum(operation => operation.Value));

                    var maxCategory = categoryTotalValues.OrderByDescending(pair => pair.Value).FirstOrDefault();

                    return new ChartEntry((float)maxCategory.Value)
                    {
                        Label = $"{startOfWeek:dd/MM} - {endOfWeek:dd/MM}",
                        ValueLabel = $"{maxCategory.Key} {maxCategory.Value}",
                        ValueLabelColor = SKColors.White,
                        Color = SKColor.Parse(CategoryToColor[maxCategory.Key])
                    };
                })
                .Reverse() // Reverse the order of entries to match the chronological order
                .ToArray();

            return chartEntries;
        }


    }
}
