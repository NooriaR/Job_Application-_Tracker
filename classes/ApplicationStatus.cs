using System;
using System.Collections.Generic;
using System.Text;

namespace Job_Application_Tracker
{
      // enum = en lista med FASTA val. Man kan bara välja ett av dessa fyra värden,
      // så stavfel som "Aplied" är omöjliga (C# stoppar dig direkt).
    public enum Status
    {
        Applied,    // 0 = ansökan är skickad
        Interview,  // 1 = jag har blivit kallad till intervju
        Offer,      // 2 = jag har fått ett erbjudande
        Rejected    // 3 = jag har fått avslag
    }
}
