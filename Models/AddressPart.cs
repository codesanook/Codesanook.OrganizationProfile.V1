using Orchard.ContentManagement;

namespace Codesanook.OrganizationProfile.Models {
    public class AddressPart : ContentPart {

        public string HouseNumber {
            get => this.Retrieve(x => x.HouseNumber);
            set => this.Store(x => x.HouseNumber, value);
        }

        public string VillageName {
            get => this.Retrieve(x => x.VillageName);
            set => this.Store(x => x.VillageName, value);
        }

        public string BuildingName {
            get => this.Retrieve(x => x.BuildingName);
            set => this.Store(x => x.BuildingName, value);
        }

        public int Floor { 
            get => this.Retrieve(x => x.Floor);
            set => this.Store(x => x.Floor, value);
        }

        public int VillageNumber {
            get => this.Retrieve(x => x.VillageNumber);
            set => this.Store(x => x.VillageNumber, value);
        }

        public string Lane { 
            get => this.Retrieve(x => x.Lane);
            set => this.Store(x => x.Lane, value);
        }

        public string Street { 
            get => this.Retrieve(x => x.Street);
            set => this.Store(x => x.Street, value);
        }

        public string Subdistrict { 
            get => this.Retrieve(x => x.Subdistrict);
            set => this.Store(x => x.Subdistrict, value);
        }

        public string District { 
            get => this.Retrieve(x => x.District);
            set => this.Store(x => x.District, value);
        }

        public string Province { 
            get => this.Retrieve(x => x.Province);
            set => this.Store(x => x.Province, value);
        }

        public string ZipCode { 
            get => this.Retrieve(x => x.ZipCode);
            set => this.Store(x => x.ZipCode, value);
        }
    }
}