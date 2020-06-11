using Codesanook.OrganizationProfile.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;

namespace Codesanook.OrganizationProfile.Drivers {
    public class ContactInformationPartDriver : ContentPartDriver<ContactInformationPart> {
        private readonly IContentManager contentManager;
        protected override string Prefix => nameof(ContactInformationPart);

        public ContactInformationPartDriver(IContentManager contentManager) => this.contentManager = contentManager;

        protected override DriverResult Display(
            ContactInformationPart part,
            string displayType,
            dynamic shapeHelper
        ) {

            return ContentShape(
                "Parts_ContactInformation",
                () =>  shapeHelper.Parts_ContactInformation(
                    PhoneNumber: part.PhoneNumber,
                    EmailAddress: part.EmailAddress 
                )
            );
        }

        protected override DriverResult Editor(ContactInformationPart part, dynamic shapeHelper) {
            return ContentShape(
                "Parts_ContactInformation_Edit",
                () => shapeHelper.EditorTemplate(
                    // The shape is located in EditorTemplates folder and match exact file name
                    TemplateName: "Parts/ContactInformation",
                    Model: part,
                    Prefix: Prefix
                )
            );
        }

        protected override DriverResult Editor(ContactInformationPart part, IUpdateModel updater, dynamic shapeHelper) {
            // Fill form data to part
            updater.TryUpdateModel(part, Prefix, null, null);
            return Editor(part, shapeHelper);
        }
    }
}