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
        public DateTime Date { get; set; }
        public OperationCategory Category { get; set; }
        public string Description { get; set; }


        public OperationUnit(int id, double value, DateTime date, OperationCategory category, string description)
        {
            OperationId = id;
            Value = value;
            Date = date.Date; // Store only the date part without the time
            Category = category;
            Description = description;
        }
        public OperationUnit(int id, double value, DateTime date, OperationCategory category)
        {
            OperationId = id;
            Value = value;
            Date = date.Date; // Store only the date part without the time
            Category = category;
            Description = "";
        }
    }
}
