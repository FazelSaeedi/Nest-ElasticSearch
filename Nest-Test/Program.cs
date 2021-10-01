using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using Nest;

namespace Nest_Test
{
    class Program
    {

        private const string IndexName = "person_real";

        static void Main(string[] args)
        {

            Console.WriteLine("Hello World!");

            var client = ElasticClientFactory.CreateElasticClient();
            var response = client.Ping();


            //var indexDefiner = new IndexDefiner();
            //indexDefiner.CreateIndex(IndexName);


            //var people = ReadPeople();
            //IndexPeople(people);

            // SampleQueries.BoolQuerySample2(IndexName);
             var persons = SampleQueries.BoolQuerySample1<Person>(IndexName);

        }


        private static List<Person> ReadPeople()
        {
            var json = File.ReadAllText(@"C:\Users\STRIX\source\repos\Nest-Test\Nest-Test\people.json");
            var people = JsonSerializer.Deserialize<List<Person>>(json);
            return people;
        }
        private static void IndexPeople(List<Person> people)
        {
            var importer = new Importer<Person>();
            importer.Import(people, IndexName);
        }
    }

    public class Person
    {
        [JsonPropertyName("age")]
        public int Age { get; set; }

        [JsonPropertyName("eyeColor")]
        public string EyeColor { get; set; }

        [JsonPropertyName("about")]
        public string About { get; set; }
    }

}
