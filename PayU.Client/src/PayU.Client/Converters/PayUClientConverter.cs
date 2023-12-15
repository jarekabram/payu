using System.Text.RegularExpressions;
using Newtonsoft.Json;
using PayU.Client.Models;

namespace PayU.Client.Converters;

public static class PayUClientConverter
{
    private const string OldValueLin = "\";\n";
    private const string OldValueWin = "\";\r\n";
    private const string NewValue = "\",\n";

    public static T DeserializeResponse<T>(string value)
        where T : class
    {
        if (typeof(T) == typeof(RetrivePayoutResponse))
        {
            return JsonConvert.DeserializeObject<T>(FixBadJsonResponse(value));
        }

        return JsonConvert.DeserializeObject<T>(value);
    }


    private static string FixBadJsonResponse(string value)
    {
        var regex = new Regex(OldValueLin);

        if (regex.IsMatch(value))
        {
            return regex.Replace(value, NewValue);
        }

        regex = new Regex(OldValueWin);
        if (regex.IsMatch(value))
        {
            return regex.Replace(value, NewValue);
        }

        return value;
    }
}