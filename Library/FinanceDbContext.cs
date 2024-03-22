using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FinanceCo.Library
{
    public class FinanceDbContext : DbContext
    {
        public DbSet<OperationUnit> Operations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=finance.db");
        }

        public static void SeedData()
        {
            using (var context = new FinanceDbContext())
            {
                context.Database.EnsureCreated();


                foreach (var operation in OperationUnitRepository.GetOperations())
                {
                    var existingOperation = context.Operations.FirstOrDefault(o => o.OperationId == operation.OperationId);

                    if (existingOperation == null) 
                        context.Operations.Add(operation);

                }
                context.SaveChanges();

            }
        }

        public static List<OperationUnit> GetOperationsFromDatabase()
        {
            using (var context = new FinanceDbContext())
            {
                var operations = context.Operations.ToList();
                return operations;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OperationUnit>()
                .HasKey(o => o.OperationId);

            // Other model configurations...
        }
        public static double GetGoalFromDatabase()
        {
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "goal.txt");
            try
            {
                if (File.Exists(filePath))
                {
                    string goalText = File.ReadAllText(filePath);
                    double goal;

                    if (double.TryParse(goalText, out goal))
                    {
                        return goal;
                    }
                }
                else 
                    return 16;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка зчитування з файлу: {ex.Message}");
            }

            return 0;

        }
        public static void SeedGoaltoDatabase()
        {
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "goal.txt");
            // Шлях до файлу, де буде збережено значення

            try
            {
                // Записуємо значення у файл
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.Write(OperationUnitRepository.CurrentGoal.ToString());
                }
            }
            catch (Exception ex)
            {
                // Обробка помилок запису у файл
                Console.WriteLine($"Помилка запису до файлу: {ex.Message}");
            }
        }
        public static void DeleteOperationById(int operationId)
        {
            using (var context = new FinanceDbContext())
            {
                var operation = context.Operations.FirstOrDefault(o => o.OperationId == operationId);

                if (operation != null)
                {
                    context.Operations.Remove(operation);
                    context.SaveChanges();
                }
            }
        }
    }
}

