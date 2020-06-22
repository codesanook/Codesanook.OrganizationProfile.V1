using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Orchard.ContentManagement;

namespace Codesanook.OrganizationProfile.Models {
    public class ContactFormPart: ContentPart {

        [Required]
        public string Name { get; set; }

        [DisplayName("Email address")]
        [Required]
        public string EmailAddress { get; set; }

        [DisplayName("Mobile phone number")]
        [Required]
        public string MobilePhoneNumber { get; set; }

        [Required]
        public string Message { get; set; }
    }
}