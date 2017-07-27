using System.Web.Mvc;
using p_0009_WEBFE_T003_Contacts.ViewModel;

namespace p_0009_WEBFE_T003_Contacts.Controllers
{
    public class HomeController : Controller
    {
        [Authorize(Roles = "IT")]
        public ActionResult Index()
        {
            vmContacts vm = new vmContacts();

            vm.HandleRequest();   //<====  Initially, the default data is obtained, and the properties are set to their default states

            return View(vm);      //<====  Send the view model, to the view
        }

        [HttpPost]
        public ActionResult Index(vmContacts vm)
        {
            vm.IP = Request.UserHostName;
            var Test1 = Request.Browser.Browser;
            var Test2 = Request.LogonUserIdentity.Name;   //<==This one is good: computername/username
            vm.Browser = Request.Browser.Browser;

            vm.HandleRequest();//<====  So all the work is done in the view model, by sending it information via it's properties



            if (vm.IsValid)
            {
                ModelState.Clear();             // If everything is OK, update the model state on the page
            }
            else
            {
                ModelState.Merge(vm.Messages);  //Otherwise merge messages from VM to the Model state for display

                //The ModelState represents a collection of name and value pairs that were submitted to the server during a POST.
                //It also contains a collection of error messages for each value submitted.
                //Despite its name, it doesn't actually know anything about any model classes, it only has names, values, and errors.
                //ModelState has two purposes: to store the value submitted to the server, and to store the validation errors associated with those values.
                //In a POST, all values in <input> tags are submitted to the server as key-value pairs. 
                //When MVC receives a POST, it takes all of the post parameters and adds them to a ModelStateDictionary instance. When debugging the controller POST action in Visual Studio, we can use the Locals window to investigate this dictionary:
                // Locals => this => ModelState => ...
                //The Values property of the ModelStateDictionary contains instances that are of type System.Web.Mvc.ModelState
            }

            return View(vm);
        }
    }
}