using Codesanook.OrganizationProfile.Models;
using Codesanook.OrganizationProfile.ViewModels;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Localization;

namespace Codesanook.OrganizationProfile.Drivers {
    public class ContactInformationPartDriver : ContentPartDriver<ContactInformationPart> {
        private readonly IContentManager contentManager;
        public Localizer T { get; set; }

        protected override string Prefix => nameof(ContactInformationPart);

        public ContactInformationPartDriver(IContentManager contentManager) {
            this.contentManager = contentManager;
            T = NullLocalizer.Instance;
        }

        protected override DriverResult Display(
            ContactInformationPart part,
            string displayType,
            dynamic shapeHelper
        ) {

            return ContentShape(
                "Parts_ContactInformation",
                () => {
                    ContactInformationDisplayViewModel viewModel = shapeHelper.Parts_ContactInformation(
                        typeof(ContactInformationDisplayViewModel)
                    );
                    viewModel.PhoneNumber = part.PhoneNumber;
                    viewModel.EmailAddress = part.EmailAddress;
                    return viewModel;
                }
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