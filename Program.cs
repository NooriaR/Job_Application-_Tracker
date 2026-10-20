namespace JobbApplicaitionTracker
    {
        internal class Program
        {
            // detta är huvudmetoden som körs när programmet startar
            static void Main()
            {
                JobbManager manager = new JobbManager();
                // Load existing jobs from file
                while (true)
                {
                    Console.WriteLine("Hej välkommen till min jobansökningsprogram");
                    Console.WriteLine("Välj ett av följande altenativ");
                    Console.WriteLine("1. Lägg till en ny ansökan");
                    Console.WriteLine("2. Visa alla ansökningar");
                    Console.WriteLine("3. Filtrera ansökningar efter status");
                    Console.WriteLine("4. Sortera ansökningar efter datum");
                    Console.WriteLine("5. Visa statistik på total antal ansökningar, antal per status, genomsnittlig svarstid");
                    Console.WriteLine("6. Visa status på en ansökan");
                    Console.WriteLine("7. Ta bort en ansökan");
                    Console.WriteLine("8. Avsluta programmet");

                    Console.Write("Choose option: ");
                    string choice = Console.ReadLine();
                    //Hantera användarens inmatning och anropa rätt metoder
                    switch (choice)
                    {
                        case "1":
                            manager.AddJob();
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
                            Console.WriteLine("Exiting program...");
                            return;

                        default:
                            Console.WriteLine("Invalid choice.");
                            break;
                    }
                }
            }
        }
    }

