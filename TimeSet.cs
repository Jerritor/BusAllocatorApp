using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusAllocatorApp
{
    //Used for intermediate deserialization since TimeSpan class cant be deserialized
    public class TimeSetDTO
    {
        public bool IsOutgoing { get; set; }
        public string Time { get; set; }
        public bool IsFirstDay { get; set; }
        public bool IsOutModel { get; set; }
    }
    public class TimeSet
    {
        public bool IsOutgoing { get; set; }
        public TimeSpan Time {  get; set; }
        public bool IsFirstDay { get; set; }
        public bool IsOutModel { get; set; }

        //Constructors

        public TimeSet(bool isOutgoing, string time, bool isfirstday)
        {
            IsOutgoing = isOutgoing;
            Time = ConvertTimeFromAMPM(time);
            IsFirstDay = isfirstday;
            IsOutModel = isOutgoing;
        }

        public TimeSet(string time, bool isOutgoing, bool isfirstday)
        {
            IsOutgoing = isOutgoing;
            Time = ConvertTimeFromAMPM(time);
            IsFirstDay = isfirstday;
            IsOutModel = isOutgoing;
        }

        public TimeSet(bool isOutgoing, string time, bool isfirstday, bool isOutModel)
        {
            IsOutgoing = isOutgoing;
            Time = ConvertTimeFromAMPM(time);
            IsFirstDay = isfirstday;
            IsOutModel = isOutModel;
        }

        public TimeSet(string time, bool isOutgoing, bool isfirstday, bool isOutModel)
        {
            IsOutgoing = isOutgoing;
            Time = ConvertTimeFromAMPM(time);
            IsFirstDay = isfirstday;
            IsOutModel = isOutModel;
        }

        //DO NOT USE THIS IN CODE -- ONLY USED FOR SERIALIZATION
        public TimeSet() { }

        //To string
        public override string ToString()
        {
            return $"[IsOutgoing={IsOutgoing}, Time={ConvertTimeSpanToAMPM()}, IsFirstDay={IsFirstDay}, IsOutModel={IsOutModel}]";
        }

        //Time converter methods
        public static TimeSpan ParseTimeFromAMPM(string time)
        {
            return DateTime.ParseExact(time, "h:mmtt", CultureInfo.InvariantCulture).TimeOfDay;
        }

        private TimeSpan ConvertTimeFromAMPM(string timestring)
        {
            DateTime dt = DateTime.Parse(timestring);
            return dt.TimeOfDay;
        }

        public string ConvertTimeSpanToAMPM(TimeSpan timeSpan)
        {
            DateTime dateTime = DateTime.Today.Add(timeSpan);
            return dateTime.ToString("h:mmtt").ToUpperInvariant();
        }
        public string ConvertTimeSpanToAMPM()
        {
            DateTime dateTime = DateTime.Today.Add(this.Time);
            return dateTime.ToString("h:mmtt").ToUpperInvariant();
        }

        public string GetFormattedTime()
        {
            return ConvertTimeSpanToAMPM(Time);
        }
    }
}
