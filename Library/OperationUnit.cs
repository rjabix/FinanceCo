using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCo.Library
{
    public class OperationUnit
    {
        public int OperationId { get; set; }
        public double Value { get; set; }
        public string Date { get; set; }
        public string Category { get; set;}
        public string Description { get; set;}

        public string Title_Text;




        public OperationUnit(int id, double value, string date, string category, string description)
        {
            OperationId = id;
            Value = value;
            Date = date;
            Category = category;
            Description = description;
            Title_Text = $"{Category} - {Value} - {Date}";
        }
        public OperationUnit(int id, double value, string date, string category)
        {
            OperationId = id;
            Value = value;
            Date = date;
            Category = category;
            Description = "";
            Title_Text = $"{Category} - {Value} - {Date}";
        }
    }
}
