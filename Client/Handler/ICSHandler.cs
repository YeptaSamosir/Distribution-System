using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Handler
{
    public class ICSHandler
    {
        public string GetFileContent(string summary,string description, DateTime dtStart, DateTime dtEnd, string url)
        {

            var icalStringbuilder = new StringBuilder();

            icalStringbuilder.AppendLine("BEGIN:VCALENDAR");
            icalStringbuilder.AppendLine("PRODID:-//MyTestProject//EN");
            icalStringbuilder.AppendLine("VERSION:2.0");

            icalStringbuilder.AppendLine("BEGIN:VEVENT");
            icalStringbuilder.AppendLine("SUMMARY;LANGUAGE=en-us:" + summary);
            icalStringbuilder.AppendLine("CLASS:PUBLIC");
            icalStringbuilder.AppendLine(string.Format("CREATED:{0:yyyyMMddTHHmmssZ}", DateTime.UtcNow));
            icalStringbuilder.AppendLine("DESCRIPTION:" + string.Format(description).Trim());
            icalStringbuilder.AppendLine(string.Format("DTSTART:{0:yyyyMMddTHHmmssZ}", dtStart.ToString("yyyyMMddTHHmm00")));
            icalStringbuilder.AppendLine(string.Format("DTEND:{0:yyyyMMddTHHmmssZ}", dtEnd.ToString("yyyyMMddTHHmm00")));
            icalStringbuilder.AppendLine("SEQUENCE:0");
            icalStringbuilder.AppendLine("UID:" + Guid.NewGuid());
            /*icalStringbuilder.AppendLine(
                string.Format(
                    "LOCATION:{0}\\, {1}\\, {2}\\, {3} {4}",
                   "ini lokasi online",
                    "alamat",
                    "kota",
                    "indonesia",
                    "123333").Trim());*/
            icalStringbuilder.AppendLine(
                string.Format(
                    "LOCATION:{0}", url
                    ).Trim());
            icalStringbuilder.AppendLine("URL:" + url);
            icalStringbuilder.AppendLine("END:VEVENT");
            icalStringbuilder.AppendLine("END:VCALENDAR");

            return icalStringbuilder.ToString();
        }
    }
}
