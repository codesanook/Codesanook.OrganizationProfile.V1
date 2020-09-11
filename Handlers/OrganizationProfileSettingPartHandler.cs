using Codesanook.OrganizationProfile.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Handlers;
using Orchard.Localization;

namespace Codesanook.Common.Handlers {
    public class OrganizationProfileSettingPartHandler : ContentHandler {
        public Localizer T { get; set; }

        private const string groupId = "Organization profile settings";

        public OrganizationProfileSettingPartHandler() {
            T = NullLocalizer.Instance;

            // Attach a part to a content item Site
            Filters.Add(new ActivatingFilter<OrganizationProfileSettingPart>("Site"));

            // Set a view for content part 
            Filters.Add(new TemplateFilterForPart<OrganizationProfileSettingPart>(
               prefix: "OrganizationProfileSetting",
               templateName: "Parts/OrganizationProfileSetting", // Part in EditorTemplates
               groupId: groupId // Same as name parameter of GroupInfo but ignore case
            ));
        }

        protected override void GetItemMetadata(GetContentItemMetadataContext context) {
            if (context.ContentItem.ContentType != "Site") {
                return;
            }

            base.GetItemMetadata(context);
            context.Metadata.EditorGroupInfo.Add(new GroupInfo(T(groupId)));
        }
    }
}
