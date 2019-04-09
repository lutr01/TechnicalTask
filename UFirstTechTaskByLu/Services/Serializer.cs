using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UFirstTechTaskByLu.Models;

namespace UFirstTechTaskByLu.Services
{
    public static class Serializer
    {
        public static Regex HostRegex { get; set; }
        public static Regex DayRegex { get; set; }
        public static Regex MethodRegex { get; set; }
        public static Regex UrlRegex { get; set; }
        public static Regex UrlRegex2 { get; set; }
        public static Regex ProtocolRegex { get; set; }
        public static Regex VersionRegex { get; set; }
        public static Regex CodeRegex { get; set; }
        public static Regex SizeRegex { get; set; }

        static Serializer()
        {
            HostRegex = new Regex(@"^([.\S]*)\s\[.*].*");
            DayRegex = new Regex(@"^.*\[(\d\d)\W(\d\d)\W(\d\d)\W(\d\d)]\s.*");
            MethodRegex = new Regex(@"""([A-Z\S]*)?.*"".*");
            UrlRegex = new Regex(@"""[A-Z]*\s?(.*)?\s*HTTPS?\/?\d*\W*\d*"".*");
            UrlRegex2 = new Regex(@"""[A-Z]*\s?(.*)?"".*");
            ProtocolRegex = new Regex(@".*\s""[A-Z]*\s.*?\s?(HTTP)*\/?\d*\W*\d*"".*");
            VersionRegex = new Regex(@".*\s""[A-Z]*\s.*?\s?HTTP?\/?(\d\W\d)?"".*");
            CodeRegex = new Regex(@".*\[.*]\s"".*""\s?([0-9\S]{3})\s?.*");
            SizeRegex = new Regex(@".*\[.*]\s"".*""\s?[0-9\S]{3}\s?([0-9\W]*)?");
        }

        public static ParsedLog Deserialize(string line)
        {
            line = Regex.Replace(line, @"[^\u0020-\u007F]", String.Empty);
            
            var hostMatch = HostRegex.Match(line);
            var dayMatch = DayRegex.Match(line);
            var methodMatch = MethodRegex.Match(line);
            var urlMatch = UrlRegex.Match(line);
            if (!urlMatch.Success)
            {
                urlMatch = UrlRegex2.Match(line);
            }

            var protocolMatch = ProtocolRegex.Match(line);
            var versionMatch = VersionRegex.Match(line);

            var codeMatch = CodeRegex.Match(line);
            var sizeMatch = SizeRegex.Match(line);

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
