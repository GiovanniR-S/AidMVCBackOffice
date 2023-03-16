using System.ComponentModel.DataAnnotations;

namespace AidBackOfficeCRUD.Models {
    public class MyUser {

        [Key]
        [Required]
        public string Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string NormalizedUserName { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        [EmailAddress]
        public string UserEmail { get; set; }
    }
}
