using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Timers;
using System.Configuration;
using System.Collections.Specialized;

namespace TimerTriggerWebJobCustomSchedule
{
    public class Functions
    {
        // This function will get triggered/executed on a custom schedule on daily basis.        
        public static void CustomTimerJobFunctionDaily([TimerTrigger(typeof(CustomScheduleDaily))] TimerInfo timerInfo)
        {
            Console.WriteLine("CustomTimerJobFunctionDaily ran at : " + DateTime.UtcNow);
        }

        //Class for custom schedule daily.
        public class CustomScheduleDaily : DailySchedule
        {
            public CustomScheduleDaily() : base("18:06:00", "18:07:00")// calling the base class constructor to supply the schedule for every day. This can be a colleaction of strings or timespan itself.
            {               
            }
        }

        // This function will get triggered/executed on a custom schedule on weekly basis. 
        public static void CustomTimerJobFunctionWeekly([TimerTrigger(typeof(CustomScheduleWeekly))] TimerInfo timerInfo)
        {
            Console.WriteLine("CustomTimerJobFunctionWeekly ran at : " + DateTime.UtcNow);
        }


        public class CustomScheduleWeekly : WeeklySchedule
        {
            public CustomScheduleWeekly()
            {
                TimeSpan ts = new TimeSpan();
                string[] values = null;
                //Iterating through complete appsettings section to get the schedule for all the days and then adding the schedule to the timer trigger for each day.
                foreach (String key in ConfigurationManager.AppSettings.Keys)
                {
                    if (ConfigurationManager.AppSettings[key] !=null)
                    {
                        string val = ConfigurationManager.AppSettings[key];
                        values = val.Split('|');
                    }

                    switch (key)
                    {
                        //Calling the Add(day, time) method of WeeklySchedule class to add the weekday and time on that weekday to run the webjob.
                        case "Mon":
                            foreach(string val in values)
                            {
                                ts = DateTime.Parse(val).TimeOfDay;
                                Add(DayOfWeek.Monday, ts);
                            }                            
                            break;
                        case "Tue":
                            foreach (string val in values)
                            {
                                ts = DateTime.Parse(val).TimeOfDay;
                                Add(DayOfWeek.Tuesday, ts);
                            }
                            break;
                        case "Wed":
                            foreach (string val in values)
                            {
                                ts = DateTime.Parse(val).TimeOfDay;
                                Add(DayOfWeek.Wednesday, ts);
                            }
                            break;
                        case "Thu":
                            foreach (string val in values)
                            {
                                ts = DateTime.Parse(val).TimeOfDay;
                                Add(DayOfWeek.Thursday, ts);
                            }
                            break;
                        case "Fri":
                            foreach (string val in values)
                            {
                                ts = DateTime.Parse(val).TimeOfDay;
                                Add(DayOfWeek.Friday, ts);
                            }
                            break;
                        case "Sat":
                            foreach (string val in values)
                            {
                                ts = DateTime.Parse(val).TimeOfDay;
                                Add(DayOfWeek.Saturday, ts);
                            }
                            break;
                        case "Sun":
                            foreach (string val in values)
                            {
                                ts = DateTime.Parse(val).TimeOfDay;
                                Add(DayOfWeek.Sunday, ts);
                            }
                            break;
                    }

                }                
               
            }
        }
    }
}
