namespace foodsale_api.Models
{
    public class Food
    {
        public Guid Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string Category { get; set; }
        public string Product { get; set; }
        public int? Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
