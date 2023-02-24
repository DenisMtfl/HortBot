#nullable disable

using HortBot.Model;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace HortBot.Service
{
    public class HortApi
    {
        private string SidHepCookie;
        private string cookiePath;
        public readonly Config _config;

        public HortApi(Config config)
        {
            _config = config;

            cookiePath = Path.Combine(Path.GetDirectoryName(Assembly.GetCallingAssembly().Location), "cookie.txt");
            SaveLoginCookie();
        }

        public void SaveLoginCookie(bool forceNewCookie = false)
        {
            if ((File.Exists(cookiePath) && File.ReadAllText(cookiePath).Length > 0) && !forceNewCookie)
            {
                SidHepCookie = File.ReadAllText(cookiePath);
            }
            else
            {
                CookieContainer cookies = new();
                HttpClientHandler handler = new()
                {
                    CookieContainer = cookies
                };

                HttpClient loginClient = new(handler)
                {
                    BaseAddress = new Uri("https://elternportal.hortpro.de/api/user/login")
                };

                var login = new Login(_config.HortProLogin.Email, _config.HortProLogin.Password);

                var json = JsonSerializer.Serialize(login);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                Task post = loginClient.PostAsync(loginClient.BaseAddress, data);
                post.Wait();

                IEnumerable<Cookie> loginResponseCookies = cookies.GetCookies(loginClient.BaseAddress).Cast<Cookie>();
                var cookie = loginResponseCookies.Single(x => x.Name == "sid-hep").Value;

                //Safe Cookie to file
                StreamWriter tw = File.CreateText(cookiePath);
                tw.Write(cookie);
                tw.Close();

                SidHepCookie = cookie;
            }
        }

        public async Task<T> GetObjectAsync<T>(string uri, bool forceNewCookie = false) where T : class
        {
            if (forceNewCookie)
                SaveLoginCookie(true);

            if (SidHepCookie.Length == 0)
                throw new ArgumentNullException(nameof(SidHepCookie));

            var baseAddress = new Uri(uri);

            var cookieContainer = new CookieContainer();
            cookieContainer.Add(baseAddress, new Cookie("sid-hep", SidHepCookie));

            using var handler = new HttpClientHandler() { CookieContainer = cookieContainer };
            using var client = new HttpClient(handler) { BaseAddress = baseAddress };
            var getResult = await client.GetAsync(baseAddress);

            JsonSerializerOptions options = new();
            options.Converters.Add(new DateTimeConverterUsingDateTimeParse());
            options.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;


            if(getResult.StatusCode != HttpStatusCode.OK)
            {
                return await GetObjectAsync<T>(uri, true);
            }

            var str = await getResult.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<T>(str, options);

            return result;
        }
    }
}
