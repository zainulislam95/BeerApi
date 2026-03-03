using System.Text.Json.Serialization;

namespace BeerApi.Models
{
    public class Item
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("brandName")]
        public string BrandName { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("descriptionText")]
        public string DescriptionText { get; set; }

        [JsonPropertyName("articles")]
        public List<ItemArticle> Articles { get; set; }
    }

    public class ItemArticle
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        // Foreign key to Item when stored in the database
        public int ItemId { get; set; }

        [JsonPropertyName("shortDescription")]
        public string ShortDescription { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("unit")]
        public string Unit { get; set; }

        public decimal? PricePerUnit { get; set; }

        [JsonPropertyName("pricePerUnitText")]
        public string PricePerUnitText
        {
            get => _pricePerUnitText;
            set
            {
                _pricePerUnitText = value;
                PricePerUnit = null;
                if (!string.IsNullOrEmpty(value))
                {
                    var cleanedValue = value.Replace("€/Liter", "").Replace("(", "").Replace(")", "").Replace(",", ".").Trim();
                    if (decimal.TryParse(cleanedValue, out var parsedPrice))
                    {
                        PricePerUnit = parsedPrice;
                    }
                }
            }
        }

        private string _pricePerUnitText;

        [JsonPropertyName("image")]
        public string Image { get; set; }
    }
}
