using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCo.Library
{
    public static class OperationUnitRepository
    {
        private static int _nextId = 3;
        public static List<OperationUnit> _operations = new List<OperationUnit>()
        {
            new OperationUnit(1, 50, "01/01/2021", "Food", "Bought some food"),
            new OperationUnit(2, 100, "02/01/2021", "Transport", "Bus"),
        };

        public static List<OperationUnit> GetOperations() => _operations;

        public static OperationUnit GetOperationByID(int OperationId)
        {
            return _operations.FirstOrDefault(x => x.OperationId == OperationId);
        }

        public static int GetNextId()
        {
            int nextId = _nextId;
            _nextId++;
            return nextId;
        }
        public static void AddOperation(double value, string date, string category, string description)
        {
            OperationUnit operation = new OperationUnit(GetNextId(), value, date, category, description);
            _operations.Add(operation);
        }
    }
}
