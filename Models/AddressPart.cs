using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Orchard.ContentManagement;

namespace Codesanook.OrganizationProfile.Models {
    public class AddressPart : ContentPart {

        [DisplayName("House number")]
        [Required]
        public string HouseNumber {
            get => this.Retrieve(x => x.HouseNumber);
            set => this.Store(x => x.HouseNumber, value);
        }

        [DisplayName("Village name")]
        public string VillageName {
            get => this.Retrieve(x => x.VillageName);
            set => this.Store(x => x.VillageName, value);
        }

        [DisplayName("Village number")]
        public int? VillageNumber {
            get => this.Retrieve(x => x.VillageNumber);
            set => this.Store(x => x.VillageNumber, value);
        }

        [DisplayName("Building name")]
        [Required]
        public string BuildingName {
            get => this.Retrieve(x => x.BuildingName);
            set => this.Store(x => x.BuildingName, value);
        }

        [DisplayName("Room number")]
        public string RoomNumber {
            get => this.Retrieve(x => x.RoomNumber);
            set => this.Store(x => x.RoomNumber, value);
        }

        public int? Floor {
            get => this.Retrieve(x => x.Floor);
            set => this.Store(x => x.Floor, value);
        }

        public string Lane {
            get => this.Retrieve(x => x.Lane);
            set => this.Store(x => x.Lane, value);
        }

        [Required]
        public string Street {
            get => this.Retrieve(x => x.Street);
            set => this.Store(x => x.Street, value);
        }

        [Required]
        public string Subdistrict {
            get => this.Retrieve(x => x.Subdistrict);
            set => this.Store(x => x.Subdistrict, value);
        }

        [Required]
        public string District {
            get => this.Retrieve(x => x.District);
            set => this.Store(x => x.District, value);
        }

        [Required]
        public string Province {
            get => this.Retrieve(x => x.Province);
            set => this.Store(x => x.Province, value);
        }

        [DisplayName("Zip code")]
        [RegularExpression(@"^\d{5}$", ErrorMessage = "Format zip code is incorrect")]
        [Required]
        public string ZipCode {
            get => this.Retrieve(x => x.ZipCode);
            set => this.Store(x => x.ZipCode, value);
        }
    }
}