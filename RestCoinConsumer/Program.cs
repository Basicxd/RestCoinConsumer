using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RestCoinConsumer
{
    class Program
    {
        private static async Task<IList<Coin>> GetAllCoins()
        {
            string Uri = "https://localhost:44347/api/coin";
            using (HttpClient client = new HttpClient())
            {
                string content = await client.GetStringAsync(Uri);
                IList<Coin> coinList = JsonConvert.DeserializeObject<IList<Coin>>(content);
                return coinList;
            }
        }

        private static async Task<Coin> GetOneCoin(int id)
        {
            string Uri = "https://localhost:44347/api/coin/" + id;
            using (HttpClient client = new HttpClient())
            {
                string content = await client.GetStringAsync(Uri);
                Coin oneCoin = JsonConvert.DeserializeObject<Coin>(content);
                return oneCoin;
            }
        }

        public static async Task<HttpResponseMessage> GetCustomersAsyncAdd(int id, string genstand, int bud, string navn)
        {
            string CustomerUri = "https://localhost:44336/api/customer/";
            using (HttpClient client = new HttpClient())
            {
                

                var jsonString = JsonConvert.SerializeObject(new Coin(id, genstand, bud, navn));

                StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(CustomerUri, content);
                return response;
            }
        }



        static void Main(string[] args)
        {
            IList<Coin> result = GetAllCoins().Result;
            Console.WriteLine(result.Count);
            foreach (Coin C in result)
            {
                Console.WriteLine(C.ToString());
            }

            Console.WriteLine();

            Console.WriteLine(GetOneCoin(1).Result);

            Console.ReadLine();
        }
    }
}
