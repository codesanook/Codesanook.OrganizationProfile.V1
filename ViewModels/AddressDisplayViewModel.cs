using Orchard.DisplayManagement.Shapes;

namespace Codesanook.OrganizationProfile.ViewModels {
    public class AddressDisplayViewModel : Shape
    {
        public string HouseNumber { get; set; }
        public string VillageName { get; set; }
        public int VillageNumber { get; set; }

        public string BuildingName { get; set; }
        public int Floor { get; set; }
        public string Lane { get; set; }
        public string Street { get; set; }
        public string Subdistrict { get; set; }
        public string District { get; set; }
        public string Province { get; set; }
        public string ZipCode { get; set; }
    }
}