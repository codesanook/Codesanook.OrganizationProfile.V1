using Orchard.ContentManagement;

namespace Codesanook.OrganizationProfile.Models {
    public class ContactFormPart: ContentPart {
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string MobilePhoneNumber { get; set; }
        public string Message { get; set; }
    }
}