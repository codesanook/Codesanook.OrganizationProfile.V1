﻿using System.ComponentModel;
using Orchard.ContentManagement;

namespace Codesanook.OrganizationProfile.Models {
    public class ContactFormPart: ContentPart {
        public string Name { get; set; }

        [DisplayName("Email Address")]
        public string EmailAddress { get; set; }

        [DisplayName("Phone Number")]
        public string MobilePhoneNumber { get; set; }

        public string Message { get; set; }
    }
}