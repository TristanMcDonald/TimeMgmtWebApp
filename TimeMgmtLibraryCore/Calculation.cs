using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeMgmtLibraryCore
{
    public class Calculation
    {
        public static double calcSelfStudyHrs(int NumOfCredits, double NumOfWeeks, double classHrsPerWeek)
        {
            double selfStudyHrsPerWeek = (NumOfCredits * 10 / NumOfWeeks) - classHrsPerWeek;
            return selfStudyHrsPerWeek;
        }

        public static DateTime endDateCalc(DateTime startDate, double numOfWeeks)
        {
            //Using the start date and number of weeks in the semester entered by the user to calculate the end date for the semester.
            //Converting number of weeks to number of days.
            double days = numOfWeeks * 7;
            //Calculating the end date of the semester with the given start date.
            DateTime endDate = startDate.AddDays(days);

            return endDate;
        }
    }
}
