using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timeticker
{
    public class EventHelper
    {
        private const int outDelay = 10;
        private static List<EventModel> eventModels = new List<EventModel>();
        public static List<EventModel> GetEventModels()
        {
            for (int i = eventModels.Count - 1; i >= 0; i--)
            {
                if (eventModels[i].EventTime.CompareTo(DateTime.Now) < 0)
                {
                    eventModels.RemoveAt(i);
                }
                else
                {
                    eventModels[i].EventTimeStr = eventModels[i].EventTime.ToString("HH:mm:ss");
                    eventModels[i].CountDownStr = (DateTime.Now.CompareTo(eventModels[i].EventTime) > 0 ? "-" : "") 
                        + DateTime.Now.Subtract(eventModels[i].EventTime).ToString("mm\\:ss");
                }
            }
            return eventModels;
        }



        public static void AddEvent(EventModel model)
        {
            if(model.EventTime.CompareTo(DateTime.MinValue) == 0)
            {
                return;
            }
            eventModels.Add(model);
        }
    }


    public class EventModel
    {
        public DateTime EventTime { get; set; }
        public string Remark { get; set; }

        public string EventTimeStr { get; set; }
        public string CountDownStr { get; set; }

        public bool IsNotify { get; set; }
    }
}
