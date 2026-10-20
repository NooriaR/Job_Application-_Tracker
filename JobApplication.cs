using System;
using System.Collections.Generic;
using System.Text; 


namespace Job_Application_Tracker
{
    //enum för att representera statusen fär en jobbansökan
    public enum Status
    {
        Applied,
        Interview,
        Offer,
        Rejected
    }
    public class JobApplication
    {
        //attributes
       public string CompanyName { get; set; }
       public string PositionTitle { get; set; }
        public Status Status { get; set; } // Applied, Interview, Offer, Rejected 
        public int ApplicationDate { get; set; }
        public int ResponseDate { get; set; }
        public int SaleryExpectation { get; set; }


        //method 1
        //returnerar antak dagar sedan ansökan skickades
        public int GetDaysSinceApplied()
        {
            return DateTime.Now.Day - ApplicationDate;

        }

        //metod 2
        //Returnerar en kort sammanfattning av ansökan
        public void GetSummary()
        {
            Console.WriteLine("Company: " + CompanyName);
            Console.WriteLine("position; " + PositionTitle);
            Console.WriteLine("status: "+ Status);
            Console.WriteLine("Applied on: "+ ApplicationDate);
            Console.WriteLine("ResponseDate: "+ ResponseDate);
            Console.WriteLine("SaleryExpectations: " + SaleryExpectation);

        }
      
        //metod 2
      
      

    }
}
