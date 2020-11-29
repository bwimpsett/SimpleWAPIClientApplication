using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace ClientConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpClientHandler handler = new HttpClientHandler();
            HttpClient client = new HttpClient(handler);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", Convert.ToBase64String(Encoding.Default.GetBytes("AdminUser:123456")));
            var result = client.GetAsync(new Uri("https://localhost:44312/api/Employees")).Result;
            
            if (result.IsSuccessStatusCode)
            {
                Console.WriteLine("Done" + result.StatusCode);
                var JsonContent = result.Content.ReadAsStringAsync().Result;

                List<Employee> empList = JsonConvert.DeserializeObject<List<Employee>>(JsonContent);
                foreach(var emp in empList)
                {
                    Console.WriteLine("Name = " + emp.Name + "Gender = " + emp.Gender + " Dept = " + emp.Dept + " Salary = " + emp.Salary);
                }
            }
            else
            {
                Console.WriteLine("Error" + result.StatusCode);
            }
            Console.ReadLine();
        }
    }

    public class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Dept { get; set; }
        public int Salary { get; set; }
    }
}
