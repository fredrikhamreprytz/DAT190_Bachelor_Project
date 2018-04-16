using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using PCLCrypto;

namespace DAT190_Bachelor_Project
{
    public class OBPUtil
    {
        string oauth_callback;
        string oauth_consumer_key;
        string oauth_nonce;
        string oauth_signature_method;
        string oauth_timestamp;
        string oauth_version;
        string oauth_consumer_secret;
        string method;
        string uri;
        string authorizationstring;
        public HttpWebRequest request;
        public string requestToken;

        public OBPUtil(string callbackURI, string key, string secret)
        {
            this.oauth_callback = callbackURI;
            this.oauth_consumer_key = key;
            this.oauth_nonce = Guid.NewGuid().ToString("N");
            this.oauth_signature_method = "HMAC-SHA256";
            this.oauth_timestamp = ConvertToUnixTimestamp(DateTime.Now);
            this.oauth_version = "1.0";
            this.oauth_consumer_secret = secret;
            this.method = "POST";
            this.uri = "https://apisandbox.openbankproject.com/oauth/initiate";

            List<KeyValuePair<string, string>> parameter_list = MakeParamList();
            string baseline = MakeBaseline(parameter_list);
            string signature = MakeSignature(baseline);
            this.authorizationstring = MakeAuthorizationString(parameter_list, signature);

        }

        private string ConvertToUnixTimestamp(DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan diff = date.ToUniversalTime() - origin;
            return Math.Floor(diff.TotalSeconds).ToString();
        }

        private List<KeyValuePair<string, string>> MakeParamList()
        {

            List<KeyValuePair<string, string>> oauthparameters = new List<KeyValuePair<string, string>>();

            oauthparameters.Add(new KeyValuePair<string, string>("oauth_callback", oauth_callback));
            oauthparameters.Add(new KeyValuePair<string, string>("oauth_consumer_key", oauth_consumer_key));
            oauthparameters.Add(new KeyValuePair<string, string>("oauth_nonce", oauth_nonce));
            oauthparameters.Add(new KeyValuePair<string, string>("oauth_signature_method", oauth_signature_method));
            oauthparameters.Add(new KeyValuePair<string, string>("oauth_timestamp", oauth_timestamp));
            oauthparameters.Add(new KeyValuePair<string, string>("oauth_version", oauth_version));
            oauthparameters.Sort(Compare);
            return oauthparameters;
        }

        static int Compare(KeyValuePair<string, string> a, KeyValuePair<string, string> b)
        {
            return a.Key.CompareTo(b.Key);
        }

        private string MakeBaseline(List<KeyValuePair<string, string>> oauthparameters)
        {

            string basestring = method.ToUpper() + "&" + WebUtility.UrlEncode(uri) + "&";
            foreach (KeyValuePair<string, string> pair in oauthparameters)
            {
                basestring += pair.Key + "%3D" + WebUtility.UrlEncode(pair.Value) + "%26";
            }
            basestring = basestring.Substring(0, basestring.Length - 3);
            //Gets rid of the last "%26" added by the foreach loop

            // Makes sure all the Url encoding gives capital letter hexadecimal 
            // i.e. "=" is encoded to "%3D", not "%3d"
            char[] basestringchararray = basestring.ToCharArray();
            for (int i = 0; i < basestringchararray.Length - 2; i++)
            {
                if (basestringchararray[i] == '%')
                {
                    basestringchararray[i + 1] = char.ToUpper(basestringchararray[i + 1]);
                    basestringchararray[i + 2] = char.ToUpper(basestringchararray[i + 2]);
                }
            }

            basestring = new string(basestringchararray);
            return basestring;
        }

        private string MakeSignature(string basestring)
        {

            // Encrypt with either SHA1 or SHA256, creating the Signature
            byte[] keyBytes = Encoding.UTF8.GetBytes(this.oauth_consumer_secret + "&");
            byte[] dataBytes = Encoding.UTF8.GetBytes(basestring);

            MacAlgorithm a = oauth_signature_method == "HMAC-SHA1" ? MacAlgorithm.HmacSha1 : MacAlgorithm.HmacSha256;
            var algorithm = WinRTCrypto.MacAlgorithmProvider.OpenAlgorithm(a);
            CryptographicHash hasher = algorithm.CreateHash(keyBytes);
            hasher.Append(dataBytes);
            byte[] mac = hasher.GetValueAndReset();
            string hmacsha1 = BitConverter.ToString(mac).Replace("-", "").ToLower();
            byte[] resultantArray = new byte[hmacsha1.Length / 2];

            for (int i = 0; i < resultantArray.Length; i++)
            {
                resultantArray[i] = Convert.ToByte(hmacsha1.Substring(i * 2, 2), 16);
            }

            string base64 = Convert.ToBase64String(resultantArray);
            string result = WebUtility.UrlEncode(base64);

            return result;
        }

        private string MakeAuthorizationString(List<KeyValuePair<string, string>> oauthparameters, string signature)
        {
            string result = "";
            foreach (KeyValuePair<string, string> pair in oauthparameters)
            {
                result += pair.Key;
                result += "=";
                result += pair.Value;
                result += ",";
            }
            result += "oauth_signature=" + signature;

            System.Diagnostics.Debug.WriteLine(result);
            return result;
        }

        public void getRequestToken(Action<IAsyncResult> callback)
        {
            this.request = WebRequest.Create(uri) as HttpWebRequest;
            this.request.Method = method;
            this.request.Headers["Authorization"] = "OAuth " + authorizationstring;
            this.request.BeginGetResponse(new AsyncCallback(callback), null);
        }
    }
}
//h
