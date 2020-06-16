using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Codesanook.OrganizationProfile.Models;
using Orchard.ContentManagement;
using Orchard.DisplayManagement;
using Orchard.Email.Services;
using Orchard.Localization;
using Orchard.Messaging.Services;
using Orchard.Mvc.Extensions;
using Orchard.Settings;
using Orchard.Themes;
using Orchard.UI.Notify;

namespace Codesanook.OrganizationProfile.Controllers {

    [Themed]
    public class ContactUsController : Controller, IUpdateModel {
        private readonly INotifier notifier;
        private readonly IContentManager contentManager;
        private readonly dynamic shapeFactory;
        private readonly IShapeDisplay shapeDisplay;
        private readonly ISiteService siteService;
        private readonly IMessageService messageService;

        public Localizer T { get; set; }
        public ContactUsController(
            INotifier notifier,
            IContentManager contentManager,
            IShapeFactory shapeFactory,
            IShapeDisplay shapeDisplay,
            ISiteService siteService,
            IMessageService messageService
        ) {
            this.notifier = notifier;
            this.contentManager = contentManager;
            this.shapeFactory = shapeFactory;
            this.shapeDisplay = shapeDisplay;
            this.siteService = siteService;
            this.messageService = messageService;
            T = NullLocalizer.Instance;
        }

        // URL: contact-us
        public ActionResult Index() {
            /* content item has multiple parts
             * content item => content item shape
             * content item shape has multiple content zone shape
             * each zone shape has content part
             */
            var contactForm = contentManager.New(contentType: "ContactForm");
            var contactFormShape = contentManager.BuildEditor(contactForm);

            var viewModel = CreateViewModel(contactFormShape);
            return View(viewModel);
        }

        [HttpPost]
        [ActionName(nameof(Index))]
        public ActionResult SendMessage(string returnUrl) {
            // New ContactForm item but not save to database
            var contactForm = contentManager.New(contentType: "ContactForm");

            //TODO
            // verify if data bind to contact form object
            // test form validator

            // Call driver editor, and return a item which is a shape that contain form data
            var contactFormShape = contentManager.UpdateEditor(contactForm, this);
            if (ModelState.IsValid) {
                //SendEmailToAdmin(contactForm);
            }

            var viewModel = CreateViewModel(contactFormShape);
            return View(nameof(Index), viewModel);
        }

        private void SendEmailToAdmin(ContentItem contactForm) {
            //Send email
            var template = shapeFactory.Create(
                "Emails_Template_ContactUs",
                Arguments.From(new {
                    ContactForm = contactForm.As<ContactFormPart>(),
                })
            );

            //template.Metadata.Wrappers.Add("Template_User_Wrapper");
            var contactInformation = contentManager.Query("ContactInformation").List().First();
            var contactInformationPart = contactInformation.As<ContactInformationPart>();

            var parameters = new Dictionary<string, object>
            {
                { "Subject", T("New contact us").Text },
                { "Body", shapeDisplay.Display(template) },//tranform to HTML with shapeDisplay
                { "Recipients",  contactInformationPart.EmailAddress } // CSV for multiple email
            };
            // Then underlying class is SmtpMessageChannel
            messageService.Send(
                DefaultEmailMessageChannelSelector.ChannelName,
                parameters
            );
        }

        bool IUpdateModel.TryUpdateModel<TModel>(
            TModel model,
            string prefix,
            string[] includeProperties,
            string[] excludeProperties
        ) => TryUpdateModel(model, prefix, includeProperties, excludeProperties);

        void IUpdateModel.AddModelError(string key, LocalizedString errorMessage)
            => ModelState.AddModelError(key, errorMessage);

        private dynamic CreateViewModel(dynamic contactFormShape) {
            var contactInformation = contentManager.Query("ContactInformation").List().First();
            var contactInformationShape = contentManager.BuildDisplay(contactInformation);

            var viewModel = shapeFactory.ViewModel(
                ContactForm: contactFormShape,
                ContactInformation: contactInformationShape
            );
            return viewModel;
        }
    }
}