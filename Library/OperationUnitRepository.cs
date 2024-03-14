using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCo.Library
{
    public enum OperationCategory
    {
        Food,
        Transport,
        Alcohol,
        Entertainment,
        Other
    }
    public static class OperationUnitRepository
    {
        private static int _nextId = 4;
        public static double CurrentGoal = 16;
        private static OperationCategory _category;
        public static List<OperationUnit> _operations = new List<OperationUnit>()
        {
            new OperationUnit(1, 50, DateTime.Parse("10.03.2024"), ToOperationCategory("Food"), "Bought some food"),
            new OperationUnit(2, 100, DateTime.Parse("11.03.2024"), ToOperationCategory("Transport"), "Bus"),
            new OperationUnit(3, 200, DateTime.Parse("12.03.2024"), ToOperationCategory("Food"), "Bought more food")
        };

        public static List<OperationUnit> GetOperations() => _operations;

        public static OperationUnit GetOperationByID(int OperationId)
        {
            var operation = _operations.FirstOrDefault(x => x.OperationId == OperationId);
            if(operation != null)
            {
                return new OperationUnit(operation.OperationId, operation.Value, operation.Date, operation.Category, operation.Description);
            }
            throw new Exception("Operation not found");
        }

        public static int GetNextId()
        {
            int nextId = _nextId;
            _nextId++;
            return nextId;
        }
        public static void AddOperation(double value, DateTime date, OperationCategory category, string description)
        {
            OperationUnit operation = new OperationUnit(GetNextId(), value, date, category, description);
            _operations.Add(operation);
        }

        public static void EditOperation(int OperationId, OperationUnit operation)
        {
            if(OperationId != operation.OperationId)
            {
                throw new Exception("OperationId does not match");
            }
            var OperationToUpdate = _operations.FirstOrDefault(x => x.OperationId == OperationId);
            if(OperationToUpdate != null)
            {
                OperationToUpdate.Value = operation.Value;
                OperationToUpdate.Date = operation.Date;
                OperationToUpdate.Category = operation.Category;
                OperationToUpdate.Description = operation.Description;
            }
        }

        public static void DeleteOperation(int OperationId)
        {
            var OperationToDelete = _operations.FirstOrDefault(x => x.OperationId == OperationId);
            if(OperationToDelete != null)
            {
                _operations.Remove(OperationToDelete);
            }
        }
        public static List<OperationUnit> GetOperationsFilteredByCategory(OperationCategory category)
        {
            return _operations.Where(operation => operation.Category == category).ToList();
        }
        public static OperationCategory ToOperationCategory(string categoryString)
        {
            if (Enum.TryParse(categoryString, out OperationCategory category))
            {
                return category;
            }
            else
            {
                throw new ArgumentException("Invalid operation category string.");
            }
        }

        public static List<OperationUnit> GetOperationsOnTheCurrentWeek()
        {
            DateTime currentDate = DateTime.Now;
            DateTime startOfWeek = currentDate.AddDays(-(int)currentDate.DayOfWeek);
            DateTime endOfWeek = startOfWeek.AddDays(7);

            return _operations.Where(operation => operation.Date >= startOfWeek && operation.Date < endOfWeek).ToList();

        }

        public static double GetWeekTotalValueOfOperations()
        {
            double totalValue = 0;
            foreach (var operation in GetOperationsOnTheCurrentWeek())
            {
                totalValue += operation.Value;
            }
            return totalValue;
        }
    }
}
