using System.Collections.Generic;
using System.Text.RegularExpressions;
using UFirstTechTaskByLu.Models;

namespace UFirstTechTaskByLu.Services
{
    public static class Serializer
    {
        public static ParsedLog Deserialize(string line)
        {
            var pattern = "^(.*)\\s+\\[(.*):(.*):(.*):(.*)\\]\\s+\"(.*)\\s+(.*)\\s+(....)/?(.*?)\"\\s+(...)\\s*(.*)$";

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
