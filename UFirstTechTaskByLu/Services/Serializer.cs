using System.Collections.Generic;
using System.Text.RegularExpressions;
using UFirstTechTaskByLu.Models;

namespace UFirstTechTaskByLu.Services
{
    public static class Serializer
    {
        public static ParsedLog Deserialize(string line)
        {
            var template = "{0} [{1}:{2}:{3}:{4}] \"{5} {6} {7}/{8}\" {9} {10}";
            //141.243.1.172 [29:23:53:25] "GET /Software.html HTTP/1.0" 200 1497

            //Handels regex special characters.
            template = Regex.Replace(template, @"[\\\^\$\.\|\?\*\+\(\)\[\]]", v => "\\"
                 + v.Value);

            var pattern = "^" + Regex.Replace(template, @"\{[0-9]+\}", "(.*?)") + "$";

            var regex = new Regex(pattern);
            var match = regex.Match(line);

            var values = new List<string>();

            for (var i = 1; i < match.Groups.Count; i++)
            {
                values.Add(match.Groups[i].Value);
            }

            ParsedLog call = new ParsedLog()
            {
                Host = values[0],
                DateTime = new Date
                {
                    Day = values[1],
                    Hours = values[2],
                    Minutes = values[3],
                    Seconds = values[4]
                },
                Request = new Request
                {
                    Method = values[5],
                    Url = values[6],
                    Protocol = values[7],
                    ProtocolVersion = values[8]
                },
                ResponseCode = values[9],
                Size = values[10]
            };

            return call;
        }
    }
}
