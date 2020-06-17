using System.ComponentModel.DataAnnotations;
using Orchard.ContentManagement;

namespace Codesanook.OrganizationProfile.Models {
    public class SocialLinksPart : ContentPart {

        [RegularExpression(@"https?:\/\/(www\.)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()!@:%_\+.~#?&\/\/=]*)",
            ErrorMessage = "Url is incorrect")]
        [Required]
        public string Facebook {
            get => this.Retrieve(x => x.Facebook);
            set => this.Store(x => x.Facebook, value);
        }
    }
}