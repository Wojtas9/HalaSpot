using Microsoft.AspNetCore.Identity;

namespace HalaSystem.Models
{
    public class Rezerwacja
    {
        public int Id { get; set; }
        public int HalaId { get; set; }
        public string UserId { get; set; }
        public DateTime Od { get; set; }
        public DateTime Do { get; set; }
        
        public Hala? Hala { get; set; }
        public IdentityUser? identityUser { get; set; }
    }
}
