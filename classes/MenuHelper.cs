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
            JobManager manager = new JobManager(); //skapar ett nytt objekt Manager

            //kallar på Addjobexample från jobmanager och anger exempeldata
            // DateTime.Today.AddDays(-3) = "för 3 dagar sedan", så datumen stämmer alltid oavsett när programmet körs
            manager.AddJobExample("Ikea", "Säljare", DateTime.Today.AddDays(-3), 60000, Status.Applied, null);
            manager.AddJobExample("Ericsson", "Utvecklare", DateTime.Today.AddDays(-20), 25000, Status.Offer, DateTime.Today.AddDays(-10));
            manager.AddJobExample("Volvo", "Säljare", DateTime.Today.AddDays(-30), 60000, Status.Rejected, DateTime.Today.AddDays(-15));
            manager.AddJobExample("Spotify", "Utvecklare", DateTime.Today.AddDays(-40), 45000, Status.Applied, null);
            manager.AddJobExample("Klarna", "Testare", DateTime.Today.AddDays(-12), 38000, Status.Interview, DateTime.Today.AddDays(-6));


            bool running = true; //ber programmet köra loopen så länge det är true, så länge man inte ändrar värde på running
            while (running)
            {
                Console.WriteLine("Hej välkommen till mitt jobbansökningsprogram");
                Console.WriteLine("Välj ett av följande alternativ:");
                Console.WriteLine("1. Lägg till en ny ansökan");
                Console.WriteLine("2. Visa alla ansökningar");
                Console.WriteLine("3. Filtrera ansökningar efter status");
                Console.WriteLine("4. Sortera ansökningar efter datum");
                Console.WriteLine("5. Visa statistik (totalt antal, antal per status, genomsnittlig svarstid)");
                Console.WriteLine("6. Uppdatera status på en ansökan");
                Console.WriteLine("7. Ta bort en ansökan");
                Console.WriteLine("8. Visa ansökningar utan svar äldre än 14 dagar");
                Console.WriteLine("9. Avsluta programmet");
                Console.WriteLine("8. Avsluta programmet");
                Console.WriteLine("9. Visa ansökningar utan svar äldre än 14 dagar");
                Console.Write("Ditt val: ");

                // Tar emot val från användaren
                string choice = Console.ReadLine()!;
                Console.Clear(); // städar skärmen så att resultatet visas på en ren sida

                //switch choice används för att hantera användarens val
                // Tips att minnas: varje case är ett rum, break; stänger dörren ut ur rummet.
                switch (choice)
                {
                    case "1":
                        manager.AddJob(); //använder objektet manager som är kopplat med JobManager där AddJob metoden finns 
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
                        manager.UpdateStatus();
                        break;
                    case "7":
                        manager.DeleteJob();
                        break;
                    case "8":
                        manager.ShowOldWithoutResponse();
                        running = false; // loopen slutar när running blir false
                        Console.WriteLine("Programmet avslutat, lycka till!");
                        break;
                    case "9":
                        running = false; // loopen slutar när running blir false
                        Console.WriteLine("Programmet avslutat, lycka till!");
                        break;
                  
                    default:
                        Console.WriteLine("Ogiltigt val, välj mellan 1-9.");
                        break;
                }

                // Paus så att användaren hinner läsa resultatet innan menyn visas igen
                if (running)
                {
                    Console.WriteLine("\nTryck på valfri tangent för att fortsätta...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }
    }
}
