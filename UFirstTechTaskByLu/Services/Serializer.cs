using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UFirstTechTaskByLu.Models;

namespace UFirstTechTaskByLu.Services
{
    public static class Serializer
    {
        public static ParsedLog Deserialize(string line)
        {
            line = Regex.Replace(line, @"[^\u0020-\u007F]", String.Empty);
            
            var hostRegex = new Regex(@"^([.\S]*)\s\[\d\d\W\d\d\W\d\d\W\d\d].*");
            var hostMatch = hostRegex.Match(line);

            var dayRegex = new Regex(@"^[.\S]*\s\[(\d\d)\W(\d\d)\W(\d\d)\W(\d\d)]\s.*");
            var dayMatch = dayRegex.Match(line);

            var methodRegex = new Regex(@"""([A-Z\S]*).*");
            var methodMatch = methodRegex.Match(line);

            var urlRegex = new Regex(@".*\s""[A-Z]*\s(.*)?\s?HTTP?\/?\d\W\d"".*");
            var urlMatch = urlRegex.Match(line);

            var protocolRegex = new Regex(@".*\s""[A-Z]*\s.*?\s?(HTTP)?\/?\d?\W?\d?"".*");
            var protocolMatch = protocolRegex.Match(line);

            var versionRegex = new Regex(@".*\s""[A-Z]*\s.*?\s?HTTP?\/?(\d\W\d)?"".*");
            var versionMatch = versionRegex.Match(line);

            var codeRegex = new Regex(@".*\s""[A-Z]*\s.*?\s?HTTP?\/?\d?\W?\d?""\s?([0-9\S]{3})\s?.*");
            var codeMatch = codeRegex.Match(line);

            var sizeRegex = new Regex(@".*\s""[A-Z]*\s.*?\s?HTTP?\/?\d?\W?\d?""\s?[0-9\S]{3}\s?([0-9\W]*)?");
            var sizeMatch = sizeRegex.Match(line);

            ParsedLog call = new ParsedLog()
            {
                Host = hostMatch.Groups[1].Value,
                DateTime = new Date
                {
                    Day = dayMatch.Groups[1].Value,
                    Hours = dayMatch.Groups[2].Value,
                    Minutes = dayMatch.Groups[3].Value,
                    Seconds = dayMatch.Groups[4].Value
                },
                Request = new Request
                {
                    Method = methodMatch.Groups[1].Value,
                    Url = urlMatch.Groups[1].Value,
                    Protocol = protocolMatch.Groups[1].Value,
                    ProtocolVersion = versionMatch.Groups[1].Value
                },
                ResponseCode = codeMatch.Groups[1].Value,
                Size = sizeMatch.Groups[1].Value
            };

            return call;
        }
    }
}
