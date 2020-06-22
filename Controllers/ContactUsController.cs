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
        public ActionResult SendMessage() {
            // Create a new ContactForm item but not save to database
            var contactForm = contentManager.New(contentType: "ContactForm");
            // Call driver editor, and return a item which is a shape that contain form data
            var contactFormShape = contentManager.UpdateEditor(contactForm, this);
            dynamic viewModel;  

            if (!ModelState.IsValid) {
                viewModel = CreateViewModel(contactFormShape);
                return View(viewModel);
            }

                SendEmailToAdmin(contactForm);
                notifier.Information(T("Your message was sent successfully. We will contact you back shortly."));

            contactFormShape = contentManager.UpdateEditor(contactForm, this);
            viewModel = CreateViewModel(contactFormShape);
                return View(viewModel);
        }

        private void SendEmailToAdmin(ContentItem contactForm) {
            // Send an email
            // !!! Folder look works only Parts folder !!!
            var template = shapeFactory.Email_Template_ContactUs(
                ContactForm : contactForm.As<ContactFormPart>() 
            );

            // FYI in case we need wrapper template.Metadata.Wrappers.Add("Emails_Template_Wrapper");
            var contactInformation = contentManager.Query("ContactInformation").List().First();
            var contactInformationPart = contactInformation.As<ContactInformationPart>();

            var bodyHtml = shapeDisplay.Display(template);
            var parameters = new Dictionary<string, object>
            {
                { "Subject", T("New contact us").Text },
                { "Body", bodyHtml },// Tranform to HTML with shapeDisplay.Display
                { "Recipients",  contactInformationPart.EmailAddress } // CSV for multiple email
            };

            // The underlying class is SmtpMessageChannel
            // It handles exception internally and not throw up to a caller.
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

           /// <see cref="Orchard.DisplayManagement.Implementation.DefaultShapeFactory.Create(string, INamedEnumerable{object}, System.Func{dynamic})"/>
           /// Here is why we can access parater as a proper in CSHTML file
           /// createdContext.Shape[prop.Name] = prop.GetValue(initializer, null);
           /// shapeFactory.ViewModel return createdContext.Shape
            var viewModel = shapeFactory.ViewModel(
                ContactForm: contactFormShape,
                ContactInformation: contactInformationShape
            );
            return viewModel;
        }
    }
}