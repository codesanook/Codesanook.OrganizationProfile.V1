using System;
using System.Linq;
using Codesanook.OrganizationProfile.Models;
using Orchard;
using Orchard.Autoroute.Services;
using Orchard.ContentManagement;
using Orchard.ContentManagement.MetaData;
using Orchard.Core.Contents.Extensions;
using Orchard.Core.Navigation.Models;
using Orchard.Core.Navigation.Services;
using Orchard.Core.Navigation.Settings;
using Orchard.Data.Migration;
using Orchard.Localization;
using Orchard.Logging;
using Orchard.Security;
using Orchard.Settings;
using Orchard.UI.Navigation;
using Orchard.Utility;

namespace Codesanook.OrganizationProfile {
    public class Migrations : DataMigrationImpl {
        private readonly IOrchardServices orchardServices;
        private readonly IAuthenticationService authenticationService;
        private readonly IContentManager contentManager;
        private readonly INavigationManager navigationManager;
        private readonly Lazy<IAutorouteService> autorouteService;
        private readonly ISiteService siteService;
        private readonly IMembershipService membershipService;
        private readonly IMenuService menuService;

        protected Localizer T { get; set; }
        protected ILogger Logger { get; set; }

        public Migrations(
            IOrchardServices orchardServices,
            IAuthenticationService authenticationService,
            IContentManager contentManager,
            INavigationManager navigationManager,
            Lazy<IAutorouteService> autorouteService,
            ISiteService siteService,
            IMembershipService membershipService,
            IMenuService menuService
        ) {
            this.orchardServices = orchardServices;
            this.authenticationService = authenticationService;
            this.contentManager = contentManager;
            this.navigationManager = navigationManager;
            this.autorouteService = autorouteService;
            this.siteService = siteService;
            this.membershipService = membershipService;
            this.menuService = menuService;
            T = NullLocalizer.Instance;
            Logger = NullLogger.Instance;
        }

        public int Create() {

            // Define ContactForm part 
            ContentDefinitionManager.AlterPartDefinition(
                nameof(ContactFormPart),
                builder => builder
                    .Attachable()
                    .WithDescription("Contact form for sending email message")
            );

            // Define ContactForm Type
            const string contactFormTypeName = "ContactForm";
            ContentDefinitionManager.AlterTypeDefinition(
                contactFormTypeName,
                builder => builder.WithPart(nameof(ContactFormPart))
            );

            // Define Address part
            ContentDefinitionManager.AlterPartDefinition(
                nameof(AddressPart),
                builder => builder
                    .Attachable()
                    .WithDescription("A part for storing address")
            );

            // Define ContactInformation part
            ContentDefinitionManager.AlterPartDefinition(
                nameof(ContactInformationPart),
                builder => builder
                    .Attachable()
                    .WithDescription("A part for storing contact information")
            );

            // Define SocialLinks part
            ContentDefinitionManager.AlterPartDefinition(
                nameof(SocialLinksPart),
                builder => builder
                    .Attachable()
                    .WithDescription("A part for storing social links, e.g. Facebook")
            );

            // Define ContactInformation type
            const string contactInformationType = "ContactInformation";
            ContentDefinitionManager.AlterTypeDefinition(
                contactInformationType,
                builder => builder
                    .WithPart(nameof(AddressPart))
                    .WithPart(nameof(ContactInformationPart))
                    .WithPart(nameof(SocialLinksPart))
                    // For edit content item in admin panel
                    .WithPart(
                        nameof(AdminMenuPart),
                        config => config.WithSetting("AdminMenuPartTypeSettings.DefaultPosition", "10")
                    )
            );

            // Configure admin menu
            var adminMenuPart = contentManager.New<AdminMenuPart>(contactInformationType);
            adminMenuPart.AdminMenuPosition = GetMenuPosition(adminMenuPart);
            adminMenuPart.OnAdminMenu = true;
            adminMenuPart.AdminMenuText = "Contact information";

            // Create and save ContactInformation content item to a database
            contentManager.Create(adminMenuPart.ContentItem);

            return 1;
        }

        public int UpdateFrom1() {
            var menus = menuService.GetMenus().ToArray();
            var mainMenu = menus[0];// On assumption that the first menu is main menu
            // Alternative way to the main menu by name
            // var mainMenu = menuService.GetMenu("Main Menu");
            var menuItem = contentManager.Create("MenuItem");

            var menuPart = menuItem.As<MenuPart>();
            menuPart.Menu = mainMenu; // Link to main menu 
            menuPart.MenuText = "Contact us";
            // Get the next menu position from existing menu item in main menu
            menuPart.MenuPosition = Position.GetNext(navigationManager.BuildMenu(mainMenu));

            var menuItemPart = menuItem.As<MenuItemPart>();
            menuItemPart.Url = "/ContactUs";

            return 2;
        }

        private string GetMenuPosition(ContentPart part) {
            var settings = part.Settings.GetModel<AdminMenuPartTypeSettings>();
            var menuPosition = settings == null ? "" : settings.DefaultPosition;
            var adminMenu = navigationManager.BuildMenu("admin");

            if (!string.IsNullOrEmpty(menuPosition)) {
                return int.TryParse(menuPosition, out int major)
                    ? Position.GetNextMinor(major, adminMenu)
                    : menuPosition;
            }

            return Position.GetNext(adminMenu);
        }
    }
}
