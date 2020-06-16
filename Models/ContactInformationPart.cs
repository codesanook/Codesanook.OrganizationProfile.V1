using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Orchard.ContentManagement;

namespace Codesanook.OrganizationProfile.Models {
    public class ContactInformationPart : ContentPart {

        [DisplayName("Phone number")]
        [Required]
        public string PhoneNumber {
            get => this.Retrieve(x => x.PhoneNumber);
            set => this.Store(x => x.PhoneNumber, value);
        }

        [DisplayName("Email address")]
        [Required]
        public string EmailAddress {
            get => this.Retrieve(x => x.EmailAddress);
            set => this.Store(x => x.EmailAddress, value);
        }
    }
}