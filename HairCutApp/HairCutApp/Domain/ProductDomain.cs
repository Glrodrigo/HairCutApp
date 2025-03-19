namespace HairCutApp.Domain
{
    public class ProductDomain
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string BrandName { get; set; }
        public string Option { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int CategoryId { get; set; }
        public int Total { get; set; }
    }
}
