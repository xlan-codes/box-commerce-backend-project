using System.Security.Cryptography;
using System.Text;

namespace Application.Utils
{
    public static class HMACUtils
    {
        public static string getHMAC_MD5(string key, string data)
        {
            byte[] bKey, bData, bRet;
            string ret = "";
            UTF8Encoding encoder = new UTF8Encoding();
            bKey = encoder.GetBytes(key);
            bData = encoder.GetBytes(data);

            System.Security.Cryptography.HMACMD5 c = new HMACMD5(bKey);
            bRet = c.ComputeHash(bData);
            foreach (byte b in bRet)
            {
                ret += String.Format("{0:x2}", b);
            }

            return ret;
        }
    }
}