using System.Linq;
using System.Web.Mvc;
using Orchard.ContentManagement;
using Orchard.DisplayManagement;
using Orchard.Localization;
using Orchard.Mvc.Extensions;
using Orchard.Themes;
using Orchard.UI.Notify;

namespace Codesanook.OrganizationProfile.Controllers {

    [Themed]
    public class ContactUsController : Controller, IUpdateModel {
        private readonly INotifier notifier;
        private readonly IContentManager contentManager;
        private readonly dynamic shapeFactory;

        public Localizer T { get; set; }
        public ContactUsController(
            INotifier notifier,
            IContentManager contentManager,
            IShapeFactory shapeFactory
        ) {
            this.notifier = notifier;
            this.contentManager = contentManager;
            this.shapeFactory = shapeFactory;
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

        private dynamic CreateViewModel(dynamic contactFormShape) {
            var contactInformation = contentManager.Query("ContactInformation").List().First();
            var contactInformationShape = contentManager.BuildDisplay(contactInformation);

            var viewModel = shapeFactory.ViewModel(
                ContactForm: contactFormShape,
                ContactInformation: contactInformationShape
            );
            return viewModel;
        }

        [HttpPost]
        [ActionName(nameof(Index))]
        public ActionResult SendMessage(string returnUrl) {
            // New ContactForm item but not save to database
            var contactForm = contentManager.New(contentType: "ContactForm");

            // Call driver editor, and return a item which is a shape that contain form data
            var contactFormShape = contentManager.UpdateEditor(contactForm, this);
            if (ModelState.IsValid) {
                //Send email
            }

            var viewModel = CreateViewModel(contactFormShape);
            return View(nameof(Index), viewModel);
        }

        bool IUpdateModel.TryUpdateModel<TModel>(
            TModel model,
            string prefix,
            string[] includeProperties,
            string[] excludeProperties
        ) => TryUpdateModel(model, prefix, includeProperties, excludeProperties);

        void IUpdateModel.AddModelError(string key, LocalizedString errorMessage)
            => ModelState.AddModelError(key, errorMessage);
    }
}