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
                       ValueLabel = (keyValuePairs.ContainsKey(OperationCategory.Food) ? keyValuePairs[OperationCategory.Food] : 0).ToString(),
                       Color = SKColor.Parse("#90D585") // Green
                   },
                   new ChartEntry((float)(keyValuePairs.ContainsKey(OperationCategory.Transport) ? keyValuePairs[OperationCategory.Transport] : 0))
                   {
                       Label = "Transport",
                       ValueLabel = (keyValuePairs.ContainsKey(OperationCategory.Transport) ? keyValuePairs[OperationCategory.Transport] : 0).ToString(),
                       Color = SKColor.Parse("#68B9C0") // Blue
                   },
                   new ChartEntry((float)(keyValuePairs.ContainsKey(OperationCategory.Alcohol) ? keyValuePairs[OperationCategory.Alcohol] : 0))
                   {
                        Label = "Alcohol",
                        ValueLabel = (keyValuePairs.ContainsKey(OperationCategory.Alcohol) ? keyValuePairs[OperationCategory.Alcohol] : 0).ToString(),
                        Color = SKColor.Parse("#e77e23") // Orange
                   },
                   new ChartEntry((float)(keyValuePairs.ContainsKey(OperationCategory.Entertainment) ? keyValuePairs[OperationCategory.Entertainment] : 0))
                   {
                        Label = "Entertainment",
                        ValueLabel = (keyValuePairs.ContainsKey(OperationCategory.Entertainment) ? keyValuePairs[OperationCategory.Entertainment] : 0).ToString(),
                        Color = SKColor.Parse("#FF0000") // Red
                   },
                   new ChartEntry((float)(keyValuePairs.ContainsKey(OperationCategory.Other) ? keyValuePairs[OperationCategory.Other] : 0))
                   {
                        Label = "Other",
                        ValueLabel = (keyValuePairs.ContainsKey(OperationCategory.Other) ? keyValuePairs[OperationCategory.Other] : 0).ToString(),
                        Color = SKColor.Parse("#808080") // Grey
                   }
            };
        }
    }
}
