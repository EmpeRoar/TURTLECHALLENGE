using System.Text.RegularExpressions;

namespace TURTLECHALLENGE.extensions
{
    public static class CommandExt
    {
        public static bool IsValidPlaceCommand(this string input)
        {
            var regex = @"PLACE\s\d,\d,(NORTH|SOUTH|EAST|WEST)";
            var match = Regex.Match(input, regex, RegexOptions.IgnoreCase);
            return match.Success;
        }
    }
}
