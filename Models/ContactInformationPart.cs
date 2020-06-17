using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Orchard.ContentManagement;

namespace Codesanook.OrganizationProfile.Models {
    public class ContactInformationPart : ContentPart {

        [DisplayName("Phone number")]
        [RegularExpression(@"^[\+]?[(]?[0-9]{1,3}[)]?[-\s\.]?[0-9]{3,4}[-\s\.]?[0-9]{4,6}$", ErrorMessage = "Format phone number is incorrect")]
        [Required]
        public string PhoneNumber {
            get => this.Retrieve(x => x.PhoneNumber);
            set => this.Store(x => x.PhoneNumber, value);
        }

        [DisplayName("Email address")]
        [RegularExpression(@"[^@ \t\r\n]+@[^@ \t\r\n]+\.[^@ \t\r\n]+", ErrorMessage = "Email code is incorrect")]
        [Required]
        public string EmailAddress {
            get => this.Retrieve(x => x.EmailAddress);
            set => this.Store(x => x.EmailAddress, value);
        }
    }
}