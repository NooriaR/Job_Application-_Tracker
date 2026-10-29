using System;
using System.Collections.Generic;
using System.Linq; // behövs för LINQ (Where, OrderBy, GroupBy, Average, Any)

namespace Job_Application_Tracker
{
    //jobmanager hanterar hela listan med ansökningar
    public class JobManager
    {
        // Lista som håller alla jobbansökningar
        public List<JobApplication> Applications = new List<JobApplication>();

        // Frågar efter ett heltal tills användaren skriver ett riktigt tal
        private int ReadInt(string question)
        {
            while (true)
            {
                Console.Write(question); // visa frågan (Write = ingen radbrytning)
               // TryParse returnerar true om texten kan bli ett tal.
               // Talet hamnar i variabeln "result" (out = skicka ut ett värde).
                if (int.TryParse(Console.ReadLine(), out int result))
                {
                    return result; // giltigt tal, lämna tillbaka det och avsluta metoden
                }
                Console.WriteLine("Ogiltigt tal, försök igen.");
            }
        }

        // Frågar efter ett datum tills användaren skriver ett riktigt datum
        private DateTime ReadDate(string question)
        {
            while (true)
            {
                Console.Write(question);
                if (DateTime.TryParse(Console.ReadLine(), out DateTime result))
                {
                    return result;
                }
                Console.WriteLine("Ogiltigt datum, skriv så här: YYYY-MM-DD.");
            }
        }
        // Frågar efter en status tills användaren skriver ett giltigt namn
        private Status ReadStatus(string question)
        {
            while (true)
            {
                Console.Write(question);
                // ?? "" betyder: om ReadLine ger null (inget), använd en tom text i stället
                string input = Console.ReadLine() ?? "";

                //if sats måste användas för att omvandla input/string från användaren till enum
                //det som kommer in måste gå att göra om till enum, om vi lyckas med det så kommer vår status få ett värde.
                // true = strunta i stora/små bokstäver. IsDefined stoppar siffror som inte finns, t.ex. "99".
                if (Enum.TryParse<Status>(input, true, out Status status) && Enum.IsDefined(typeof(Status), status))
                {
                    return status;
                }
                Console.WriteLine("Ogiltig status, välj Applied, Interview, Offer eller Rejected.");
            }
        }

        //Metoder
        // Lägger till en ny ansökan
        public void AddJob()
        {
            Console.Write("CompanyName: ");
            string company = Console.ReadLine() ?? "";

            Console.WriteLine("PositionTitle:");
            string position = Console.ReadLine() ?? "";

            DateTime datum = ReadDate("AnsökningsDatum (YYYY-MM-DD): ");

            int lon = ReadInt("Lönekrav (kr): ");

            Status status = ReadStatus("Status (Applied, Interview, Offer, Rejected): ");

            DateTime? responseDate = null; //ger responsetime ett null värde, ingen har svarat än

            //jag använder konsturktorn för att skapa ett jobbansökan. 
            //Jag skapar ett nytt objekt här och kallar det job. Job tar emot de variabler som jag har skapat ovan.
            JobApplication job = new JobApplication(company, position, datum, lon, status, responseDate);
            Applications.Add(job); //jag lägger in mitt objekt job i min lista applications
            Console.WriteLine("Ansökan tillagd!");
        }

        //Denna metod används för att lägga in testdata. Min metod heter AddJobExample som anropas sedan i MenuHelper
        // Lägger in en färdig ansökan utan att fråga användaren.
        // Används i MenuHelper så att listan inte är tom när jag testar.
        // Skillnaden mot AddJob: värdena skickas IN som parametrar.
        public void AddJobExample(string companyName, string title, DateTime applicationDate, int salaryExpectation, Status status, DateTime? responseDate)
        {
            JobApplication job = new JobApplication(companyName, title, applicationDate, salaryExpectation, status, responseDate);
            Applications.Add(job);
        }

        // Visar alla ansökningar
        //Går igenom listan och skriver ut varje ansökan med ett nummer.
        //count räknar antal objekt i listan
        public void ShowAll()
        {
            int number = 1; //skapar en räknare som börjar på ett (människor räknar från 1, listan från 0)
            if (Applications.Count == 0) //om listan inte innehåller objekt så ska det skrivas... return går tillbaka till menyn
            {
                Console.WriteLine("Inga jobbansökningar hittades.");
                return;
            }

            //för varje job i applications skriv ut numret och räkna sedan upp det med 1, så att nästa ansökan får nästa nummer.
            //hämta PrintSummary metoden (skriver ut informationen av varje jobb)
            foreach (var job in Applications)
            {
                Console.WriteLine($"Ansökan nummer: {number++}");
                job.PrintSummary();
            }
        }

        // Uppdaterar status på en ansökan
        //Först kollar metoden om listan är tom.
        //Om den är det skriver den ett meddelande och avslutar med return, så att programmet inte försöker uppdatera något som inte finns.
        public void UpdateStatus()
        {
            if (Applications.Count == 0) //Säkerhetskontroll: finns det något att uppdatera?
            {
                Console.WriteLine("Det finns inga ansökningar att uppdatera.");

                // return avslutar metoden direkt, så att koden aldrig körs på en tom lista.
                // Annars skulle programmet fråga efter ett nummer fast det inte finns något att välja.
                return;
            }

            // Visar alla ansökningar med nummer
            ShowAll();

            //Ber användaren välja ett jobb att uppdatera. Numret användaren ser börjar på 1, 
            //men listan börjar på 0, därför räknar vi ut index = nummer - 1.
            int number = ReadInt("Välj nummer på ansökan du vill uppdatera: ");
            int index = number - 1;

            //Kontrollera att indexet finns: inte negativt OCH mindre än antalet ansökningar
            if (index >= 0 && index < Applications.Count)
            {
                //Fråga efter ny status (frågar om tills den är giltig)
                Status newStatus = ReadStatus("Ny status (Applied, Interview, Offer, Rejected): ");

                //Applications[index] = ansökan på den platsen. Byt dess status.
                Applications[index].Status = newStatus;

                //Går man tillbaka till Applied tas svarsdatumet bort igen.
                //Ifall new status inte är applied så uppdateras svarsdatumet till dagens datum
                if (newStatus != Status.Applied)
                {
                    Applications[index].ResponseDate = DateTime.Today;  //ResponseDate uppdateras
                }
                else
                {
                    Applications[index].ResponseDate = null;
                }

                Console.WriteLine("Status uppdaterad!");
            }
            else
            {
                Console.WriteLine("Ogiltigt nummer.");
            }
        }

        //Skapar en metod som heter DeletJob, Kontrollerar om listan är tom. Sedan visas listan som frågar användaren om vilket jobb ska raderas. 
        public void DeleteJob()
        {
            if (Applications.Count == 0) //ifall listan är tom 
            {
                Console.WriteLine("Det finns inga ansökningar att ta bort.");
                return;
            }

            ShowAll();
            int number = ReadInt("Ange numret på ansökan du vill radera: ");
            int index = number - 1; // användaren räknar från 1, listan från 0

            //ifall index är större eller lika med 0 och Applications.Count är större än index
            //Då ska det tas bort med hjälp av inbyggda metoden RemoveAt, indexet som angavs tas då bort 
            if (index >= 0 && index < Applications.Count) // Samma kontroll som i UpdateStatus: finns platsen?
            {
                Applications.RemoveAt(index);
                Console.WriteLine("Ansökan borttagen.");
            }
            else
            {
                Console.WriteLine("Ogiltigt nummer.");
            }
        }

        // LINQ 1: Filtrera efter status
        // Where = ett såll som bara släpper igenom det som stämmer.
        public void ShowByStatus()
        {
            Status status = ReadStatus("Ange status (Applied, Interview, Offer, Rejected): ");

            var filtered = Applications.Where(job => job.Status == status); //var = vet inte vad det ska vara för datatyp, låter c# själv bestämma.
                                                                            //visa alla jobb som finns i listan där status stämmer med användarens val
                                                                            //status-rubriken filtreras till så många jobb som har tex applied.
           //ifall filtered listan inte gav resultat (mha any) så kommer det skrivas inga ansökningar med den statusen. 
           // Any() svarar ja/nej på "finns det något alls i resultatet?"
           // ! betyder NOT, alltså "om det INTE finns något"
            if (!filtered.Any()) 
            {
                Console.WriteLine("Inga ansökningar med den statusen.");
                return;
            }

            foreach (var job in filtered) //för varje job i filtered så ska det skrivas sammanfattning av jobben.
            {
                job.PrintSummary();
            }
        }

        // LINQ 2: Sortera efter datum
        //sortera jobb efter jobbets ansökningsdatum med hjälp av funktionen OrderBy (sortera efter).
        // OrderBy = äldst först
        public void SortByDate()
        {
            var sorted = Applications.OrderBy(job => job.ApplicationDate);

            foreach (var job in sorted)
            {
                job.PrintSummary();
            }
        }

        // LINQ 3: Visa statistik
        public void ShowStatistics()
        {
            Console.WriteLine($"Totala ansökningar: {Applications.Count}");
            //grupperar jobben efter status
            //gruppera jobb efter jobbstatus (GroupBy), välj det nyckelvärde som i detta fallet är status och antal jobb
            //med select bestämmer jag hur resultatet ska visas, i detta fall räkna antal jobb per status/key
            //select utförs efter jag har grupperat för att få det resultat jag önskar, om det hade varit tvärtom hade jag fått annat resultat
            var countByStatus = Applications.GroupBy(job => job.Status).Select(group => $"{group.Key}: {group.Count()}");

            //skriver ut resultatet av uträkningen
            foreach (var item in countByStatus)
            {
                Console.WriteLine(item);
            }

            //Kollar på responsetime i hela listan. 
            //mha where bestämmer jag vad jag vill behålla i listan

            var avgResponseTime = Applications
                .Where(job => job.ResponseDate != null) //om responsedate inte är tomt(null) ska jag behålla jobben i listan.

                // Räknar ut hur många dagar det tog för företagen att svara i genomsnitt. Responsedate-applicationdate=tiden som gått
                //frågetecken betyder att om det inte finns responsedate så ska programmet inte krascha utan endast ge null
                .Average(job => (job.ResponseDate - job.ApplicationDate)?.Days);

            if (avgResponseTime != null) //om responstiden inte är null så ska den skriva följande 
            {
                Console.WriteLine($"Genomsnittlig svarstid: {avgResponseTime:F1} dagar");
            }
            else
            {
                Console.WriteLine("Inga svar ännu.");
            }
        }

        // LINQ 4: Extra filter - ansökningar utan svar som är äldre än 14 dagar
        // om svarstiden är tom och getdayssinceapplied är 14 så skrivs följande.. 
        public void ShowOldWithoutResponse()
        {
            var oldWithoutResponse = Applications  // ResponseDate == null  -> inget svar än
                                                   // GetDaysSinceApplied() > 14 -> använder min egen metod från JobApplication
                .Where(job => job.ResponseDate == null && job.GetDaysSinceApplied() > 14);

            if (!oldWithoutResponse.Any())   // Any() = finns det något alls?
            {
                Console.WriteLine("Inga ansökningar utan svar som är äldre än 14 dagar.");
                return;
            }

            foreach (var job in oldWithoutResponse)
            {
                job.PrintSummary();
            }
        }
    }
}