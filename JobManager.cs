using Job_Application_Tracker;
using System;
using System.Collections.Generic;

namespace JobbApplicaitionTracker
{
    public class JobbManager
    {
        // Lista som håller alla jobbansökningar
        public List<JobApplication> Applications { get; set; } = new List<JobApplication>();

        // Lägger till en ny ansökan
        public void AddJob()
        {
            Console.WriteLine("Company Name:");
            string company = Console.ReadLine();

            Console.WriteLine("Position Title:");
            string position = Console.ReadLine();

            Console.WriteLine("Salary Expectation:");
            int salary = int.Parse(Console.ReadLine());

            Console.WriteLine("Application Date (yyyy-MM-dd):");
            DateTime date = DateTime.Parse(Console.ReadLine());

            // Skapar ett nytt JobApplication‑objekt
            JobApplication job = new JobApplication
            {
                CompanyName = company,
                PositionTitle = position,
                SaleryExpectation = salary,
                ApplicationDate = date,
                Status = Status.Applied
            };

            Applications.Add(job);
            Console.WriteLine("Job added!");
        }

        // Visar alla ansökningar
        public void ShowAll()
        {
            if (Applications.Count == 0)
            {
                Console.WriteLine("No job applications found.");
                return;
            }

            foreach (var job in Applications)
            {
                job.GetSummary();
            }
        }

        // Uppdaterar status på en ansökan
        public void UpdateStatus()
        {
            // Visar alla ansökningar med index
            for (int i = 0; i < Applications.Count; i++)
            {
                Console.WriteLine($"{i}. {Applications[i].CompanyName} - {Applications[i].Status}");
            }

            Console.WriteLine("Choose index:");
            int index = int.Parse(Console.ReadLine());

            Console.WriteLine("New status (0=Applied, 1=Interview, 2=Offer, 3=Rejected):");
            int newStatus = int.Parse(Console.ReadLine());

            if (index >= 0 && index < Applications.Count)
            {
                Applications[index].Status = (Status)newStatus;
                Console.WriteLine("Status updated!");
            }
            else
            {
                Console.WriteLine("Invalid index.");
            }
        }

        // Tar bort en ansökan
        public void DeleteJob()
        {
            Console.WriteLine("Enter index to delete:");
            int index = int.Parse(Console.ReadLine());

            if (index >= 0 && index < Applications.Count)
            {
                Applications.RemoveAt(index);
                Console.WriteLine("Job deleted.");
            }
            else
            {
                Console.WriteLine("Invalid index.");
            }
        }
        // LINQ: Filtrera efter status
        public void ShowByStatus()
        {
            Console.WriteLine("Enter status (Applied, Interview, Offer, Rejected):");
            string input = Console.ReadLine();

            if (Enum.TryParse(input, out Status status))
            {
                var filtered = Applications.Where(a => a.Status == status);

                foreach (var job in filtered)
                {
                    job.GetSummary();
                }
            }
            else
            {
                Console.WriteLine("Invalid status.");
            }
        }

        // LINQ: Sortera efter datum
        public void SortByDate()
        {
            var sorted = Applications.OrderBy(a => a.ApplicationDate);

            foreach (var job in sorted)
            {
                job.GetSummary();
            }
        }

        // LINQ: Statistik
        public void ShowStatistics()
        {
            Console.WriteLine($"Total applications: {Applications.Count}");

            var countByStatus = Applications
                .GroupBy(a => a.Status)
                .Select(g => $"{g.Key}: {g.Count()}");

            foreach (var item in countByStatus)
            {
                Console.WriteLine(item);
            }

            var avgResponseTime = Applications
                .Where(a => a.ResponseDate != null)
                // Räknar ut hur många dagar det tog för företagen att svara i genomsnitt.
                .Average(a => (a.ResponseDate - a.ApplicationDate)?.TotalDays);

            Console.WriteLine($"Average response time: {avgResponseTime} days");
        }

    }
}