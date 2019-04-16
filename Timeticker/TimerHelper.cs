using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timeticker
{
    public class TimerHelper
    {
        /// <summary>
        /// 处理截止时间
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static DateTime ConvertSpeTime(string timestr)
        {
            if(string.IsNullOrWhiteSpace(timestr))
            {
                return DateTime.MinValue;
            }
            timestr = Strings.StrConv(timestr.Trim(), VbStrConv.Narrow);
            var times = timestr.Split(':');
            if (times.Length != 2)
            {
                return DateTime.MinValue;
            }
            try
            {
                var dt = DateTime.Now;
                if (Convert.ToInt32(times[0]) * 60 + Convert.ToInt32(times[1]) <= dt.Hour * 60 + dt.Minute)
                {
                    return dt.Date.AddDays(1).AddHours(Convert.ToInt32(times[0])).AddMinutes(Convert.ToInt32(times[1]));
                }
                else
                {
                    return dt.Date.AddHours(Convert.ToInt32(times[0])).AddMinutes(Convert.ToInt32(times[1]));
                }
            }
            catch
            {

            }
            finally
            {

            }
            return DateTime.MinValue;
        }

        /// <summary>
        /// 处理累加时间
        /// </summary>
        /// <param name="timestr"></param>
        /// <returns></returns>
        public static DateTime ConvertEventTime(string timestr)
        {
            if (string.IsNullOrWhiteSpace(timestr))
            {
                return DateTime.MinValue;
            }
            timestr = Strings.StrConv(timestr.Trim(), VbStrConv.Narrow);
            var times = timestr.Split(':');
            if (times.Length < 2 || times.Length > 3)
            {
                return DateTime.MinValue;
            }
            try
            {
                if (times.Length == 2)
                {
                    return DateTime.Now.AddMinutes(Convert.ToInt32(times[0])).AddSeconds(Convert.ToInt32(times[1]));
                }
                else if (times.Length == 3)
                {
                    return DateTime.Now.AddHours(Convert.ToInt32(times[0])).AddMinutes(Convert.ToInt32(times[1])).AddSeconds(Convert.ToInt32(times[2]));
                }
            }
            catch
            {

            }
            finally
            {

            }
            return DateTime.MinValue;
        }
    }
}
