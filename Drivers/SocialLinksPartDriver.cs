using Codesanook.OrganizationProfile.Models;
using Codesanook.OrganizationProfile.ViewModels;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;

namespace Codesanook.OrganizationProfile.Drivers {
    public class SocialLinksPartDriver : ContentPartDriver<SocialLinksPart> {
        private readonly IContentManager contentManager;
        protected override string Prefix => nameof(ContactInformationPart);

        public SocialLinksPartDriver(IContentManager contentManager) =>
            this.contentManager = contentManager;

        protected override DriverResult Display(
            SocialLinksPart part,
            string displayType,
            dynamic shapeHelper
        ) {

            return ContentShape(
                "Parts_SocialLinks",
                () => {
                    SocialLinksDisplayViewModel shape = shapeHelper.Parts_SocialLinks(typeof(SocialLinksDisplayViewModel));
                    shape.Facebook = part.Facebook;
                    return shape;
                }
            );
        }

        protected override DriverResult Editor(SocialLinksPart part, dynamic shapeHelper) {
            return ContentShape(
                "Parts_SocialLinks_Edit",
                () => shapeHelper.EditorTemplate(
                    // The shape is located in EditorTemplates folder and match exact file name
                    TemplateName: "Parts/SocialLinks",
                    Model: part,
                    Prefix: Prefix
                )
            );
        }

        protected override DriverResult Editor(SocialLinksPart part, IUpdateModel updater, dynamic shapeHelper) {
            // Fill form data to part
            updater.TryUpdateModel(part, Prefix, null, null);
            return Editor(part, shapeHelper);
        }
    }
}