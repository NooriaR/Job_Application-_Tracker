using JobbApplicaitionTracker;
using System;
using System.Collections.Generic;
using System.Text;

namespace Job_Application_Tracker
{
    public class MenuHelper
    {
        public static void MainMenu()
        {
            JobbManager manager = new JobbManager(); //skapar ett nytt objekt Manager

            //kallar på Addjobexample från jobmanager och anger exempeldata
            manager.AddJobExample("Ikea","Säljare",new DateTime (2026,10,23),60000,Status.Applied,null);
            manager.AddJobExample("Ericsson", "utvecklare", new DateTime(2026, 10, 30), 25000, Status.Offer, new DateTime(2026, 11, 20));
            manager.AddJobExample("Volvo", "Säljare", new DateTime(2026, 09, 30), 60000, Status.Rejected, new DateTime(2026, 12, 30));
            manager.AddJobExample("Spotify", "Utvecklare", new DateTime(2026, 9, 1), 45000, Status.Applied, null);


            bool running = true; //ber programmet köra loopen så länge det är true, så länge man inte ändrar värde på running
            while (running)
            {
                Console.WriteLine("Hej välkommen till min jobansökningsprogram");
                Console.WriteLine("Välj ett av följande altenativ");
                Console.WriteLine("1. Lägg till en ny ansökan");
                Console.WriteLine("2. Visa alla ansökningar");
                Console.WriteLine("3. Filtrera ansökningar efter status");
                Console.WriteLine("4. Sortera ansökningar efter datum");
                Console.WriteLine("5. Visa statistik på total antal ansökningar, antal per status, genomsnittlig svarstid");
                Console.WriteLine("6. Uppdatera status på en ansökan");
                Console.WriteLine("7. Ta bort en ansökan");
                Console.WriteLine("8. Avsluta programmet");
                Console.WriteLine("9. Visa ansökningar utan svar äldre än 14 dagar");
                // Tar emot val från användaren
                string choice = Console.ReadLine()!;
                Console.Clear();
                //switch choice används för att hantera användarens val
                switch (choice)
                {
                    case "1":
                        manager.AddJob(); //använder objekten manager som är kopplad med jobmanager där addjob metoden finns 
                        Console.Clear();
                        break;

                    case "2":
                        manager.ShowAll();
                        break;
                    case "3":
                        manager.ShowByStatus(); 
                        break;
                    case "4":
                        manager.SortByDate();
                        break;
                    case "5":
                        manager.ShowStatistics();
                        break;
                    case "6":
                        UpdateStatusMenu(manager); //här skickar jag manager som ett argument
                        break;
                    case "7":
                        manager.DeleteJob();
                        break;
                    case "8":
                        running = false;
                        Console.WriteLine("Program avslutat, lycka till! ");
                        break;
                    case "9":
                        manager.ShowOldWithoutResponse();
                        break;
                    default:
                        Console.WriteLine("Ogiltigt val, välj mellan 1-8");
                        break;
                }
            }

            static void UpdateStatusMenu(JobbManager manager)
            {
                manager.ShowAll(); //visar alla ansökningar 
                Console.Write("Ange numret på den ansökan du vill uppdatera: ");
                if (int.TryParse(Console.ReadLine(),out int index)) //tar emot input från användaren och omvandlar till int
                {
                    manager.UpdateStatus(); //vi uppdaterar status på det valda jobbet mha manager objekt
                }   
                else
                {
                    Console.WriteLine("ogiltigt val");
                }
            }
        }
    }
}
