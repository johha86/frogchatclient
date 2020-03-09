using PusherClient;
using System;
using System.Net.Http;
using Newtonsoft.Json;

namespace ChatClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Ingrese el nombre del usuario.");
            var usernameInput = Console.ReadLine();

            using (HttpClient client = new HttpClient())
            {
                string url = "http://localhost:64952/Auth/Login";
                var dataModel = new LoginRequest();
                dataModel.username = usernameInput;

                var data = JsonConvert.SerializeObject(dataModel);

                StringContent requestData = new StringContent(data);
                var response = client.PostAsync(url, requestData).Result;

                var userDataJson = response.Content.ReadAsStringAsync().Result;
                var userData =  JsonConvert.DeserializeObject<User>(userDataJson);

                //  Obtener la lista de rooms

                var _pusher = new Pusher("04016b8df0172af7d6fd");
                _pusher.ConnectionStateChanged += _pusher_ConnectionStateChanged;
                _pusher.Error += _pusher_Error;
                _pusher.ConnectAsync();
            }
        }

        private static void _pusher_Error(object sender, PusherException error)
        {
            
        }

        private static void _pusher_ConnectionStateChanged(object sender, ConnectionState state)
        {
            
        }
    }

    public class LoginRequest
    {
        public string username { get; set; }
    }

    public class User
    {
        public User()
        {
        }

        public int id { get; set; }
        public string name { get; set; }
        public DateTime created_at { get; set; }
    }
}
