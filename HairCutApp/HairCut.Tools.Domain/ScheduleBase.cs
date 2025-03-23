using HairCut.Generals;

namespace HairCut.Tools.Domain
{
    public class ScheduleBase : Create
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string ServiceDescription { get; set; }
        public int Duration { get; set; }
        public double Price { get; set; }
        public string Notes { get; set; }
        public DateTime Date { get; set; }
        public bool Done { get; set; }


        public ScheduleBase(int userId, string phone, DateTime date, string? notes) 
        {
            if (userId <= 0)
                throw new Exception("A key está vazia ou inválida");

            if (string.IsNullOrEmpty(phone) || phone == "string" || !StringFormat.IsValidPhoneNumber(phone))
                throw new Exception("O telefone está vazio ou inválido");

            if (date < DateTime.UtcNow || date > DateTime.UtcNow.AddMonths(1))
                throw new Exception("O data está vazia ou inválida");

            if (!string.IsNullOrEmpty(notes))
            {
                if (notes.Length > 200)
                    throw new Exception("A descrição está muito longa");
            }

            UserId = userId;
            Phone = phone;
            Date = date;
            Notes = notes;
        }
    }
}
