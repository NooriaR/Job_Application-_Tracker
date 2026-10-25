using Job_Application_Tracker;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace JobbApplicaitionTracker
{
    //jobbmanager hanterar hela listan med ansökningar
    public class JobbManager
    {
        // Lista som håller alla jobbansökningar
        public List<JobApplication> Applications = new List<JobApplication>();

        //Metoder
        // Lägger till en ny ansökan
        public void AddJob()
        {
            Status status; //deklarerar variabel eftersom programmet ska veta att jag ska plocka mitt värde från enum.

            Console.WriteLine("CompanyName:");
            string company = Console.ReadLine();

            Console.WriteLine("PositionTitle:");
            string position = Console.ReadLine();

            Console.WriteLine("AnsökningsDatum YYYY-MM-DD");
            DateTime datum = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Lönekrav: ");
            int lon = int.Parse(Console.ReadLine());

            DateTime? responseDate = null; //ger responsetime ett null värde

            while (true)
            {
                Console.Write("Status (Applied, Interview, Offer, Rejected): ");
                string input = Console.ReadLine()!;
                //if sats måste användas för att få ut omvandla input/string från användaren till enum
                //det som kommer in måste gå att göra om till enum, om vi lyckas med det så kommer vi vårt status få ett värde och koden kommer att köras.
                if (Enum.TryParse<Status>(input, true, out status))
                {
                    //jag använder konsturktorn för att skapa ett jobbansökan. 
                    //Jag skapar en ny objekt här och kallar den job. Job tar emot de variabler som jag har skapar ovan, de blåa är mina variabler 
                    JobApplication job = new JobApplication(company, position, datum, lon, status, responseDate);
                    Applications.Add(job); //jag lägger in min objekt job i min lista applications
                    break;
                }
                else
                {
                    Console.WriteLine("ogiltig staus: ");
                }
            }
        }

        //Denna metod används för att lägga in testdata. Min metod heter AddJobExample som anropas sedan i manuhelper
        public void AddJobExample(string companyName, string title, DateTime applicationDate, int SalaryExpectation, Status status, DateTime? responseDate)
        {
            JobApplication job = new JobApplication(companyName, title, applicationDate, SalaryExpectation, status, responseDate);
            Applications.Add(job); 

        }

        // Visar alla ansökningar
        //count räknar antal objekt i listan
        public void ShowAll()
        {
            int number = 0; //skapar en räknare som börjar på noll
            if (Applications.Count == 0) //om listan inte innehåller objekt så ska det skrivas... return går tillbaka till menyn
            {
                Console.WriteLine("Inga jobsökningar hittades: ");
                return;
            }

            //för varje job i applications skriv ut numret och räkna sedan upp det med 1, så att nästa ansökan får nästa nummer.
            //hämta getsummary metoden
            foreach (var job in Applications)
            {
                Console.WriteLine($"ApplicationsNumber: {number++}");
                job.GetSummary();
            }
        }

        // Uppdaterar status på en ansökan
        public void UpdateStatus()
        {
            // Visar alla ansökningar med index
            //varje gång loopen körs och loopens värde är mindre än totala antalet sökningar i listan så kommer den köra koden i kroppen. 
            // samt lägger till/ökar värdet på loop med en (++).           
            for (int loop = 0; loop < Applications.Count; loop++)
            {
                Console.WriteLine($"{loop}. {Applications[loop].CompanyName} - {Applications[loop].PositionTitle} - {Applications[loop].Status}");
            }

            //Ber användaren välja ett jobb att uppdatera. Deklarerar variabeln index som int. 
            Console.WriteLine("Välj ett av jobben: ");
            int index = int.Parse(Console.ReadLine());

            //Ber användaren välja ett enum. Deklarerar variabeln newStatus som int. 
            Console.WriteLine("New status (0=Applied, 1=Interview, 2=Offer, 3=Rejected):");
            int newStatus = int.Parse(Console.ReadLine());

            //Ifall index(det som användaren skrev på välja jobb att uppdatera) är större eller lika med 0 och appliccations.count är större än index
            if (index >= 0 && index < Applications.Count)
            {
                Applications[index].Status = (Status)newStatus; //omvandlar newStatus från int till enum
                Applications[index].ResponseDate = DateTime.Today;  //ResponsDate uppdateras
                Console.WriteLine("Status updated!");
            }
            else
            {
                Console.WriteLine("Invalid index.");
            }
        }

        //Först visa alla jobb/objekt
        //Sedan ta bort jobb
        public void DeleteJob()
        {
            ShowAll();
            Console.WriteLine("Ange jobb du vill radera: ");
            int index = int.Parse(Console.ReadLine());

            //ifall index är större eller lika med 0 och applications.count är större än index
            //Då ska det tas bort med hjälp av inbyggda metoden removeat, indexet som angavs tas då bort 
            if (index >= 0 && index < Applications.Count)
            {
                Applications.RemoveAt(index);
                Console.WriteLine("Job deleted.");
            }
            else
            {
                Console.WriteLine("Ogiltig val: ");
            }
        }
        // LINQ: Filtrera efter status-rubriken
        public void ShowByStatus()
        {
            Console.WriteLine("Ange status (Applied, Interview, Offer, Rejected):");
            string input = Console.ReadLine();

            //Tar emot en input som den försöker göra till enum och specefikt till den enum i status
            if (Enum.TryParse(input, out Status status))
            {
                var filtered = Applications.Where(job => job.Status == status); //var = vet inte vad det ska vara för datatyp, låter c# själv bestämma.
                //visa alla jobb som finns i listan där status stämmer med användarens val - status-rubriken filteras till så många jobb som har tex applied.

                foreach (var job in filtered) //för varje job i filtered så ska det skrivas sammanfattning av jobben.
                {
                    job.GetSummary();
                }
            }
            else
            {
                Console.WriteLine("ogiltig status.");
            }
        }

        // LINQ: Sortera efter datum
        //sortera jobb efter jobbets ansökningsdatum med hjälp av funktionen orderby(sortera efter).
        public void SortByDate()
        {
            var sorted = Applications.OrderBy(job => job.ApplicationDate);

            foreach (var job in sorted)
            {
                job.GetSummary();
            }
        }

        // LINQ: Statistik
        public void ShowStatistics()
        {
            Console.WriteLine($"Totala ansökningar : {Applications.Count}");
            //gruppera jobb efter jobstatus, välj job med det nyckelvärde som i detta fallet är status och antal job
            //med select bestämmer jag hur resultatet ska visas i detta fall räkna antal jobb per status/key
            //select utförs efter jag har sorterat för att få det resultat jag önskar, om det hade varit tvärttom hade jag fått annat resultat
            var countByStatus = Applications.GroupBy(job => job.Status).Select(job => $"{job.Key}: {job.Count()}");

            //skriver ut resultatet av uträkningen
            foreach (var item in countByStatus)
            {
                Console.WriteLine(item);
            }

            //Kollar på responsetime i hela listan. 
            //mha where bestämmer jag vad jag vill behålla i listan

            var avgResponseTime = Applications
                .Where(job => job.ResponseDate != null) //om responsdate inte är tomt(null) ska jag behålla jobben i listan.

                // Räknar ut hur många dagar det tog för företagen att svara i genomsnitt. Responsdate-applicationdate=tiden som gått
                //frågetecken betyder att om det inte finns responsdate så ska programmet inte krascha utan endast ge null
                .Average(job => (job.ResponseDate - job.ApplicationDate)?.Days);

            if (avgResponseTime != null) //om responstiden inte är null så ska den skriva följande 
            {
                Console.WriteLine($"Genomsnittlig svarstid i dagar: {avgResponseTime}");
            }
            else
            {
                Console.WriteLine("inga svar ännu");
            }
        }
        // LINQ: Extra filter - ansökningar utan svar som är äldre än 14 dagar
        public void ShowOldWithoutResponse()
        {
            var oldWithoutResponse = Applications
                .Where(job => job.ResponseDate == null && job.GetDaysSinceApplied() > 14);

            if (!oldWithoutResponse.Any())   // Any() = finns det något alls?
            {
                Console.WriteLine("Inga ansökningar utan svar som är äldre än 14 dagar.");
                return;
            }

            foreach (var job in oldWithoutResponse)
            {
                job.GetSummary();
            }
        }
    }
}