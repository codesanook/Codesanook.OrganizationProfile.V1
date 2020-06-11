using Codesanook.OrganizationProfile.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;

namespace Codesanook.OrganizationProfile.Drivers {
    public class ContactFormPartDriver : ContentPartDriver<ContactFormPart> {
        private readonly IContentManager contentManager;
        protected override string Prefix => nameof(ContactFormPart);

        public ContactFormPartDriver(IContentManager contentManager) =>
            this.contentManager = contentManager;

        protected override DriverResult Editor(ContactFormPart part, dynamic shapeHelper) {
            return ContentShape(
                "Parts_ContactForm_Edit",
                () => shapeHelper.EditorTemplate(
                    TemplateName: "Parts/ContactForm", // The shape is located in EditorTemplates folder
                    Model: part,
                    Prefix: Prefix
                )
            );
        }

        protected override DriverResult Editor(ContactFormPart part, IUpdateModel updater, dynamic shapeHelper) {

            // Fill form data to part
            updater.TryUpdateModel(part, Prefix, null, null);
            return Editor(part, shapeHelper);

        }
    }
}