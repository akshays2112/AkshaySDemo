using System.ComponentModel.DataAnnotations;
using Google.Cloud.Firestore;

namespace AkshaySDemoGoogleFireStoreDB.Models
{
    [FirestoreData]
    public class Chef
    {
        public string ID { get; set; }
        [FirestoreProperty]
        [Required]
        [StringLength(10, ErrorMessage = "Your first name can only be 10 characters long.")]
        public string FirstName { get; set; }
        [FirestoreProperty]
        [Required]
        [StringLength(10, ErrorMessage = "Your first name can only be 10 characters long.")]
        public string LastName { get; set; }
        [FirestoreProperty]
        [Required]
        [StringLength(10, ErrorMessage = "Your first name can only be 10 characters long.")]
        public string CountryName { get; set; }
    }
}
