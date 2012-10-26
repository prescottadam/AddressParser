using System.Text.RegularExpressions;

namespace adamprescott.net.AddressParser
{
    public static class Parser
    {
        public static Address Parse(string address)
        {
            if (string.IsNullOrEmpty(address))
                return new Address();

            Address result;
            var input = address.ToUpper();

            var re = new Regex(BuildPattern());
            if (re.IsMatch(input))
            {
                var m = re.Match(input);
                result = new Address
                {
                    HouseNumber = m.Groups["HouseNumber"].Value,
                    StreetPrefix = m.Groups["StreetPrefix"].Value,
                    StreetName = m.Groups["StreetName"].Value,
                    StreetType = m.Groups["StreetType"].Value,
                    StreetSuffix = m.Groups["StreetSuffix"].Value,
                    Apt = m.Groups["Apt"].Value,
                };
            }
            else
            {
                result = new Address
                {
                    StreetName = input,
                };
            }
            return result;
        }

        private static string BuildPattern()
        {
            var pattern = "^" +                                           // beginning of string
                "(?<HouseNumber>\\d+)" +                                  // 1 or more digits
                "(?:\\s+(?<StreetPrefix>" + GetStreetPrefixes() + "))?" + // whitespace + valid prefix (optional)
                "(?:\\s+(?<StreetName>.*?))" +                            // whitespace + anything
                "(?:" +                                                   // group (optional) {
                "(?:\\s+(?<StreetType>" + GetStreetTypes() + "))" +       //   whitespace + valid street type
                "(?:\\s+(?<StreetSuffix>" + GetStreetSuffixes() + "))?" + //   whitespace + valid street suffix (optional)
                "(?:\\s+(?<Apt>.*))?" +                                   //   whitespace + anything (optional)
                ")?" +                                                    // }
                "$";                                                      // end of string

            return pattern;
        }

        private static string GetStreetPrefixes()
        {
            return "TE|NW|HW|RD|E|MA|EI|NO|AU|SE|GR|OL|W|MM|OM|SW|ME|HA|JO|OV|S|OH|NE|K|N";
        }

        private static string GetStreetTypes()
        {
            return "TE|STCT|DR|SPGS|PARK|GRV|CRK|XING|BR|PINE|CTS|TRL|VI|RD|PIKE|MA|LO|TER|UN|CIR|WALK|CO|RUN|FRD|LDG|ML|AVE|NO|PA|SQ|BLVD|VLGS|VLY|GR|LN|HOUSE|VLG|OL|STA|CH|ROW|EXT|JC|BLDG|FLD|CT|HTS|MOTEL|PKWY|COOP|ACRES|ESTS|SCH|HL|CORD|ST|CLB|FLDS|PT|STPL|MDWS|APTS|ME|LOOP|SMT|RDG|UNIV|PLZ|MDW|EXPY|WALL|TR|FLS|HBR|TRFY|BCH|CRST|CI|PKY|OV|RNCH|CV|DIV|WA|S|WAY|I|CTR|VIS|PL|ANX|BL|ST TER|DM|STHY|RR|MNR";
        }

        private static string GetStreetSuffixes()
        {
            return "NW|E|SE|W|SW|S|NE|N";
        }
    }
}
