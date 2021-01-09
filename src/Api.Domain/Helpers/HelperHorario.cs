using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Helpers
{
     public static  class HelperHorario
    {
        public static DateTime HoraDeBrasilia {
            
            get {
                var Standard_Time = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
             
                return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, Standard_Time);
             } 
        }
    }
}
