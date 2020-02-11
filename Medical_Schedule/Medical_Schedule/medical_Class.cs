using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace Medical_Schedule
{
    class medical_Class
    {
        public static string morn_begin_time;
        public static string morn_end_time;
        public static string aft_begin_time;
        public static string aft_end_time;
        public static string eve_begin_time;
        public static string eve_end_time;
        public static string session_Period;
        public static string morn_begin_name;
        public static string aft_begin_name;
        public static string eve_begin_name;
        public static string begin_name;
        public static string doctor_search;

        public static string pp;

       /** Module -6-
        * public static string last_evening_num;
        public static string last_afternoon_num;
        public static string last_morning_num;**/
        
        public void frequent_search()
        {

            //GET CURRENT TIME
            string strt = DateTime.Now.ToString("hh:mm:ss tt");
            DateTime strt2 = DateTime.ParseExact(strt, "hh:mm:ss tt", System.Globalization.DateTimeFormatInfo.InvariantInfo);


            DateTime begin_time_mrn_date = DateTime.ParseExact(morn_begin_time, "hh:mm:ss tt", System.Globalization.CultureInfo.CurrentCulture);
            DateTime end_time_mrn_date = DateTime.ParseExact(morn_end_time, "hh:mm:ss tt", System.Globalization.CultureInfo.CurrentCulture);

            if ((strt2 <= end_time_mrn_date) && (strt2 >= begin_time_mrn_date))
            {
                pp = "Morning";
                session_Period = "Morning";
                begin_name = morn_begin_name;
            }
            else
            {
                DateTime begin_time_aft_date = DateTime.ParseExact(aft_begin_time, "hh:mm:ss tt", System.Globalization.CultureInfo.CurrentCulture);
                DateTime end_time_aft_date = DateTime.ParseExact(aft_end_time, "hh:mm:ss tt", System.Globalization.CultureInfo.CurrentCulture);
                if ((strt2 <= end_time_aft_date) && (strt2 >= begin_time_aft_date))
                {
                     pp = "Morning";
                    session_Period = "Afternoon";
                    begin_name = aft_begin_name;
                }
                else
                {
                    DateTime begin_time_eve_date = DateTime.ParseExact(eve_begin_time, "hh:mm:ss tt", System.Globalization.CultureInfo.CurrentCulture);
                    DateTime end_time_eve_date = DateTime.ParseExact(eve_end_time, "hh:mm:ss tt", System.Globalization.CultureInfo.CurrentCulture);
                    if ((strt2 >= begin_time_eve_date) || (strt2 < end_time_eve_date))
                    {
                        session_Period = "Evening";
                        begin_name = eve_begin_name;
                    }
                }

            }

        }
    }
}
