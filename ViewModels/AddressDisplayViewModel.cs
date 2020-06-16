using System.ComponentModel;
using Orchard.DisplayManagement.Shapes;

namespace Codesanook.OrganizationProfile.ViewModels {
    public class AddressDisplayViewModel : Shape
    {
        [DisplayName("House number")]
        public string HouseNumber { get; set; }

        [DisplayName("Village name")]
        public string VillageName { get; set; }

        [DisplayName("Village number")]
        public int? VillageNumber { get; set; }

        [DisplayName("Building number")]
        public string BuildingName { get; set; }

        [DisplayName("Room number")]
        public string RoomNumber { get; set; }

        public int? Floor { get; set; }

        public string Lane { get; set; }
        public string Street { get; set; }

        public string Subdistrict { get; set; }
        public string District { get; set; }
        public string Province { get; set; }

        [DisplayName("Zip code")]
        public string ZipCode { get; set; }
    }
}