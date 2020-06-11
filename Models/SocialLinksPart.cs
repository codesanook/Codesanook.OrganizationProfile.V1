using Orchard.ContentManagement;

namespace Codesanook.OrganizationProfile.Models {
    public class SocialLinksPart : ContentPart {

        public string Facebook {
            get => this.Retrieve(x => x.Facebook);
            set => this.Store(x => x.Facebook, value);
        }
    }
}