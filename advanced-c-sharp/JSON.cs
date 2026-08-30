using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Newtonsoft.Json.Schema;

namespace advanced_c_sharp
{
    internal class Car
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
    }
    internal class JSON
    {
        internal static void CreateStudentJson()
        {
            JObject student = new JObject
            {
                ["name"] = "Balreet",
                ["age"] = 21,

                ["subjects"] = new JArray
            {
                "C#",
                "Java",
                "DSA"
            }
            };

            Console.WriteLine(student.ToString());
        }

        internal static void ConvertCarToJson()
        {
            Car car = new Car
            {
                Brand = "Toyota",
                Model = "Fortuner",
                Year = 2024
            };

            string json = JsonConvert.SerializeObject(car);

            Console.WriteLine(json);
        }

        internal static void ReadJsonFile()
        {
            string filePath = "users.json";

            string json = File.ReadAllText(filePath);

            JArray users = JArray.Parse(json);

            foreach (JObject user in users)
            {
                string name = (string)user["name"];
                string email = (string)user["email"];

                Console.WriteLine($"Name: {name}");
                Console.WriteLine($"Email: {email}");
                Console.WriteLine();
            }
        }

        internal static void MergeJsonObjects()
        {
            JObject obj1 = new JObject
            {
                ["name"] = "John",
                ["age"] = 25
            };

            JObject obj2 = new JObject
            {
                ["city"] = "Delhi",
                ["email"] = "john@gmail.com"
            };

            obj1.Merge(obj2);

            Console.WriteLine(obj1.ToString());
        }

        static void ValidateJson()
        {
            string json = @"
        {
            ""name"": ""John"",
            ""age"": 20,
            ""subjects"": [""C#"", ""Java""]
        }";

            string schemaJson = @"
        {
            ""type"": ""object"",
            ""properties"": {
                ""name"": {
                    ""type"": ""string""
                },
                ""age"": {
                    ""type"": ""integer""
                },
                ""subjects"": {
                    ""type"": ""array"",
                    ""items"": {
                        ""type"": ""string""
                    }
                }
            },
            ""required"": [
                ""name"",
                ""age"",
                ""subjects""
            ]
        }";

            JObject student = JObject.Parse(json);

            JSchema schema = JSchema.Parse(schemaJson);

            bool valid = student.IsValid(schema);

            Console.WriteLine($"Is JSON valid? {valid}");
        }

    }
}
