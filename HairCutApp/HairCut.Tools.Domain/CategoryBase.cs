
using HairCut.Generals;

namespace HairCut.Tools.Domain
{
    public class CategoryBase : Create
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CreateUserId { get; set; }

        public CategoryBase(string name)
        {

            if (string.IsNullOrEmpty(name) || name == "string")
                throw new Exception("O nome está em um formato inválido");

            Name = HandleFormat.CleanName(name.ToUpper());
        }
    }
}
