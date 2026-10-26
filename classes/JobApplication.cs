using System;
using System.Collections.Generic;
using System.Text; 


namespace Job_Application_Tracker
{

     public class JobApplication
     {
        private string? company;
        private string? position;
        private int salary;

        //attributer 
        public string CompanyName { get; set; }
        public string PositionTitle { get; set; }
        public Status Status { get; set; } = Status.Applied;  //Ger standardvärde som den kan starta med 
        public DateTime ApplicationDate { get; set; } = DateTime.Today; //jag ger det ett standardvärde som den kan starta med. 
        public DateTime? ResponseDate { get; set; } = null; //frågetecken för att om man inte fått svar på respnsdate ska man kunna få null. Ger null som standarsvärde
        public int SalaryExpectation { get; set; }


        //konstruktorn är den metod som körs automatiskt när man skapar ett nytt objekt.
        //Den tar emot värden utifrån de arguemnt och sparar i objektets egna egenskaper.
        //Attributer + de parametrar som finns under konstruktor 
        public JobApplication(string companyName, string positionTitle, DateTime applicationDate, int salaryExpectation, Status status, DateTime? responseDate)
        {
            CompanyName = companyName;
            PositionTitle = positionTitle;
            ApplicationDate = applicationDate;
            SalaryExpectation = salaryExpectation;
            Status = status;
            ResponseDate = responseDate;
        }

        //method 1
        //Räknar/returnerar antal dagar sedan ansökan skickades
        //Returnerar dagens datum - dagen som ansökan skickades. Days används för att inte få med tid utan endast dagar
        public int GetDaysSinceApplied()
        {
            return (DateTime.Now - ApplicationDate).Days;
        }

        //metod 2
        //Returnerar en kort sammanfattning av ansökan
        public void GetSummary()
        {
            // Välj färg beroende på status
            switch (Status)
            {
                case Status.Offer:
                    Console.ForegroundColor = ConsoleColor.Green;    // grön = erbjudande
                    break;
                case Status.Rejected:
                    Console.ForegroundColor = ConsoleColor.Red;      // röd = avslag
                    break;
                case Status.Interview:
                    Console.ForegroundColor = ConsoleColor.Yellow;   // gul = intervju
                    break;
                    // Applied behåller standardfärgen
            }

            Console.WriteLine($"Company: {CompanyName} | Position: {PositionTitle} | " +
                              $"Status: {Status} | Applied: {ApplicationDate:yyyy-MM-dd} | " +
                              $"Salary: {SalaryExpectation} kr");

            Console.ResetColor();   // sätt på locket
        }
    }
}
