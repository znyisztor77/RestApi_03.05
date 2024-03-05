using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using RestApi_03._05;

namespace RestApi_03._05
{
    class Program
    {
        static string endPointUrl = "https://retoolapi.dev/KqpqJ9/data";
        /* "id": 1,
           "name": "Hershel Shields",
           "salary": 30903
        */

        static List<Adat> adatok = new List<Adat>();
        static void Main(string[] args)
        {
            //https://merlinvizsga.hu/index.php?menu=syncAsync
            restapiAdatok().Wait();
            foreach(Adat item in adatok)
            {
                Console.WriteLine($"{item.Id}, {item.Name}, {item.Salary}");
            }

            legjobbanKereso();
            Console.WriteLine("Program vége!");
            Console.ReadLine();
        }

        
        static async Task restapiAdatok()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, endPointUrl);
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            // Console.WriteLine(await response.Content.ReadAsStringAsync());
            string jsonString = await response.Content.ReadAsStringAsync();
            adatok = Adat.FromJson(jsonString).ToList();

        }
        private  static void legjobbanKereso()
        {
            long maxSalary = adatok.Max(a => a.Salary);
            Adat legmagasabb = adatok.Find(a => a.Salary == maxSalary);

            Console.WriteLine("1. feladat");
            Console.WriteLine($"\t A legjobban kereső dolgozo {legmagasabb.Name}, a fizetése: {legmagasabb.Salary}");
        }
    }
}
