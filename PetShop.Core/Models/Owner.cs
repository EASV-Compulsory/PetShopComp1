namespace PetShop.Core.Models
{
    public class Owner
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        //code smell primitive obsession. later may migrate phone no and email to classes??
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}