using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Orchard.DisplayManagement.Shapes;

namespace Codesanook.OrganizationProfile.ViewModels {
    public class ContactInformationDisplayViewModel: Shape {

        [DisplayName("Phone number")]
        public string PhoneNumber { get; set; }

        [DisplayName("Email address")]
        public string EmailAddress {get;set;}
    }
}