using System.ComponentModel;
using Orchard.ContentManagement;

namespace Codesanook.OrganizationProfile.Models {
    public class OrganizationProfileSettingPart : ContentPart {
        [DisplayName("Show contact us form")]
        public bool ShowContactUsForm {
            get => this.Retrieve(x => x.ShowContactUsForm);
            set => this.Store(x => x.ShowContactUsForm, value);
        }
    }
}