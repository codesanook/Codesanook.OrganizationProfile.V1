using Orchard.ContentManagement;

namespace Codesanook.OrganizationProfile.Models {
    public class ContactInformationPart : ContentPart {
        public string PhoneNumber {
            get => this.Retrieve(x => x.PhoneNumber);
            set => this.Store(x => x.PhoneNumber, value);
        }
        public string EmailAddress {
            get => this.Retrieve(x => x.EmailAddress);
            set => this.Store(x => x.EmailAddress, value);
        }
    }
}