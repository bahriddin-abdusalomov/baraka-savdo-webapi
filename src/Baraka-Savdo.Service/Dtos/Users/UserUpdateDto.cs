using Baraka_Savdo.Domain.Enums;

namespace Baraka_Savdo.Service.Dtos.Users
{
    public class UserUpdateDto
    {
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        public bool IsMale { get; set; }

        public DateTime BirthDate { get; set; }

        public string Country { get; set; } = string.Empty;

        public string Region { get; set; } = string.Empty;

        public IFormFile ImagePath { get; set; } 

        [MaxLength(13)]
        public string PhoneNumber { get; set; } = string.Empty;

        public bool PhoneNumberConfirmed { get; set; }

        [MaxLength(9)]
        public string PassportSeriaNumber { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public string Salt { get; set; } = string.Empty;

        public DateTime LastActivity { get; set; }

        public IdentityRole IdentityRole { get; set; }
    }
}
