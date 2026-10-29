using System;
using System.Collections.Generic;
using System.Text; 


namespace Job_Application_Tracker
{
    //JobApplication är ritningen för en ansökan.Den har egenskaper
    // som företag, roll, status och datum, en konstruktor som fyller i
    // dem när objektet skapas, och metoder som räknar dagar och visar
    // en sammanfattning.
     public class JobApplication
     {
        //attributer 
        // { get; set; } betyder att andra klasser kan läsa (get) och ändra (set) värdet
        public string CompanyName { get; set; }
        public string PositionTitle { get; set; }
        public Status Status { get; set; } = Status.Applied;  //Ger standardvärde som den kan starta med 
        public DateTime ApplicationDate { get; set; } = DateTime.Today; //jag ger det ett standardvärde som den kan starta med. 
        public DateTime? ResponseDate { get; set; } = null; //frågetecken för att om man inte fått svar på respnsdate ska man kunna få null. Ger null som standarsvärde
        public int SalaryExpectation { get; set; }


        //konstruktorn körs automatiskt när man skapar ett nytt objekt, det bestämmer vad ett objekt ska innehålla
        //konstruktorn heter samma som klassen och har ingen returtyp (inte ens void).
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
        public string GetSummary()
        {
            {
                // ?. och ?? = om ResponseDate är tomt (null) skriver vi "Inget svar än" i stället
                string response = ResponseDate?.ToString("yyyy-MM-dd") ?? "Inget svar än";

                return $"Company: {CompanyName} | Position: {PositionTitle} | " +
                       $"Status: {Status} | Applied: {ApplicationDate:yyyy-MM-dd} | " +
                       $"Response: {response} | Salary: {SalaryExpectation} kr";
            }
        }

       //metod 3 (min egen)
       // Skriver ut sammanfattningen i RÄTT FÄRG beroende på status (VG-krav: färger i konsolen).
       // Tips att minnas: sätt färg -> skriv -> ResetColor (som en överstrykningspenna med lock).
        public void PrintSummary()
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

            Console.WriteLine(GetSummary());

            Console.ResetColor();   // sätt på locket, annars blir all text efteråt också färgad
        }
    }
}
