
using HairCut.Generals;

namespace HairCut.Tools.Domain
{
    public class ProductBase : Create
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BrandName { get; set; }
        public string Option { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public Guid ImageId { get; set; }
        public string? ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int CreateUserId { get; set; }
        public int Total { get; set; }

        public ProductBase(string name,string brandName, string option, string description, double price, int categoryId, int total)
        {

            if (string.IsNullOrEmpty(name) || name == "string" || name.Length > 200)
                throw new Exception("O nome está em um formato inválido");

            if (string.IsNullOrEmpty(brandName) || brandName == "string" || brandName.Length > 200)
                throw new Exception("O nome da marca está em um formato inválido");

            if (!string.IsNullOrEmpty(option))
            {
                if (option == "string" || option.Length > 100)
                    throw new Exception("A opção está em um formato inválido ou muito extenso");
            }

            if (!string.IsNullOrEmpty(description))
            {
                if (description == "string" || description.Length > 300)
                    throw new Exception("A descrição está em um formato inválido ou muito extenso");
            }

            if (price <= 0)
                throw new Exception("O preço está em um formato inválido");

            if (categoryId <= 0)
                throw new Exception("A categoria está em um formato inválido");

            if (total < 0)
                throw new Exception("O total está em um formato inválido");

            Name = HandleFormat.CleanName(name.ToUpper());
            BrandName = brandName.ToUpper();
            Option = HandleFormat.CleanName(option.ToUpper());
            Description = description;
            Price = price;
            CategoryId = categoryId;
            Total = total;
        }
    }
}
