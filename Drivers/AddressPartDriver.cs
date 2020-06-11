using System.Linq;
using Codesanook.OrganizationProfile.Models;
using Codesanook.OrganizationProfile.ViewModels;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;

namespace Codesanook.OrganizationProfile.Drivers {
    public class AddressPartDriver : ContentPartDriver<AddressPart> {
        private readonly IContentManager contentManager;
        protected override string Prefix => nameof(AddressPartDriver);

        public AddressPartDriver(IContentManager contentManager) =>
            this.contentManager = contentManager;

        protected override DriverResult Display(
            AddressPart part,
            string displayType,
            dynamic shapeHelper
        ) {

            return ContentShape(
                "Parts_Address",
                () => {
                    AddressDisplayViewModel model = shapeHelper.Parts_Address(typeof(AddressDisplayViewModel));
                    model.HouseNumber = part.HouseNumber;
                    model.VillageName = part.VillageName;
                    model.VillageNumber = part.VillageNumber;

                    model.BuildingName = part.BuildingName;
                    model.Floor = part.Floor;
                    model.Lane = part.Lane;
                    model.Street = part.Street;
                    
                    model.Subdistrict = part.Subdistrict;
                    model.District = part.District;

                    model.Province = part.Province;
                    model.ZipCode = part.ZipCode;
                    return model;
                }
            );
        }

        protected override DriverResult Editor(AddressPart part, dynamic shapeHelper) {
            return ContentShape(
                "Parts_Address_Edit",
                () => shapeHelper.EditorTemplate(
                    TemplateName: "Parts/Address", // The shape is located in EditorTemplates folder
                    Model: part,
                    Prefix: Prefix
                )
            );
        }

        protected override DriverResult Editor(AddressPart part, IUpdateModel updater, dynamic shapeHelper) {

            // Fill form data to part
            updater.TryUpdateModel(part, Prefix, null, null);
            return Editor(part, shapeHelper);
        }
    }
}