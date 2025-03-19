
namespace HairCut.Tools.Domain
{
    public class ProductResult
    {
        public string Name { get; set; }
        public string BrandName { get; set; }
        public string Option { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }
        public string CategoryName { get; set; }
        public int Total { get; set; }
    }

    public class ProductTotalResults
    {
        public List<ProductResult> Products { get; set; }
        public int TotalPages { get; set; }

        public ProductTotalResults() 
        {
            Products = new List<ProductResult>();
        }
    }
}
