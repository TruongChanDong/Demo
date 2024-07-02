using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Demo.Models
{
    internal class Food
    {
        [JsonPropertyName("id")]
        public string? id {  get; set; }
        [JsonPropertyName("category")]
        public string? category { get; set; }
        [JsonPropertyName("price")]
        public double? price { get; set; }
        [JsonPropertyName("name")]
        public string? name { get; set; }
        [JsonPropertyName("description")]
        public string? description { get; set; }
        [JsonPropertyName("timestamp")]
        public DateTime? timestamp { get; set; }
        [JsonPropertyName("imgURL")]
        public string? imgURL { get; set; }


        public Food() { }

        public Food(string? category, double? price, string? name, string? description)
        {
            this.category = category;
            this.price = price;
            this.name = name;
            this.description = description;
        }

        public override string? ToString()
        {
            return "Id = " + id + "\n" +
                "Category = " + category + "\n" +
                "Price = " + price + "\n" +
                "Name = " + name + "\n" +
                "Timestamp = "+ timestamp;
        }
    }
}
