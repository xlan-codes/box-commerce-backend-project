using System.Text.RegularExpressions;
using Application.Exceptions;
using Newtonsoft.Json;

namespace Application.Utils
{
    public static class PhoneFormatter
    {
        public static string Format(string phone)
        {
            if (phone == null)
                return null;

            phone = Regex.Replace(phone, @"^\+", "00");
            phone = Regex.Replace(phone, @"[^\d]", "");
            phone = Regex.Replace(phone, @"^(7|07)", "00407");
            return phone;
        }
    }
}