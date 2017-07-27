using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data.Entity.Validation;
using p_0009_WEBFE_T003_Contacts.CommonClasses;

namespace p_0009_WEBFE_T003_Contacts.ViewModel
{
    public class vmContacts
    {
        #region Constructor
        public vmContacts()
        {
            Init();
        }
        #endregion //Constructor

        #region Public Properties

        //Project Specific

        public T003TelephoneContact ContactSearchRecord { get; set; }
        public T003TelephoneContact ContactRecord { get; set; }
        public List<T003TelephoneContact> ListOfContactSearchRecords { get; set; }
        public List<T003TelephoneContact> ListOfContactRecords { get; set; }

        //Common for Logging
        public string IP { get; set; }
        public string UserLogin { get; set; }
        public string Browser { get; set; }

        //Common     
        public string EventCommand { get; set; }    //<=== Which button was just clicked
        public string EventArgument { get; set; }
        public bool IsValid { get; set; }           //<=== Whether model is valid at a particular point
        public string PageMode { get; set; }        //<=== List mode, add mode, edit mode
        public bool IsDetailAreaVisible { get; set; }       //<=== Whether to display that section
        public bool IsListAreaVisible { get; set; }       //<=== Whether to display that section
        public bool IsSearchAreaVisible { get; set; }       //<=== Whether to display that section
        public ModelStateDictionary Messages { get; set; }       //<=== to report back additional messages
        #endregion //Public Properties

        #region Init Method
        public void Init()
        {
            //Project Specific

            ContactSearchRecord = new T003TelephoneContact();
            ContactRecord = new T003TelephoneContact();
            ListOfContactRecords = new List<T003TelephoneContact>();
            ListOfContactSearchRecords = new List<T003TelephoneContact>();


            //Common - Set the defaults
            EventCommand = string.Empty;
            EventArgument = string.Empty;
            IsValid = true;
            IsDetailAreaVisible = false;
            IsListAreaVisible = true;
            IsSearchAreaVisible = true;
            PageMode = PageConstants.LIST;
            Messages = new ModelStateDictionary();
        }
        #endregion  //Init Method

        #region HandleRequest Method

        public void HandleRequest()
        {

            LoadListOfSearchEntities();
            //Load_DropDownEntities();

            switch (EventCommand.ToLower())
            {
                case "":                        //<=== This is how it is initialized, so it drops to [case "list":] => [Get();]
                case "list":
                    Get();                      //<=== In here it instantiates a class that inherits from DbContext
                    break;                      //		 and populates the public (List) property

                case "search":
                    Search();
                    break;

                case "resetsearch":
                    ResetSearch();
                    break;

                case "cancel":
                    Get();
                    break;

                case "add":
                    AddMode();
                    break;

                case "edit":
                    EditMode(Convert.ToInt32(EventArgument));
                    break;

                case "save":
                    Save();
                    break;

                case "delete":
                    Delete(Convert.ToInt32(EventArgument));
                    break;

                default:
                    break;
            }

        }

        #endregion //HandleRequest Method 

        #region Load_DropDownEntities Method
        //public void Load_DropDownEntities()
        //{

        //  contactDBContext dc = new contactDBContext(); 
        //  ListOfContactRecords.AddRange(dc.T003TelephoneContact);

        //}
        #endregion \\ Load_DropDownEntities Method

        #region LoadListOfSearchEntities Method
        public void LoadListOfSearchEntities()
        {
            contactDBContext dc = new contactDBContext();

            if (ListOfContactRecords.Count == 0)
            {
                //Load Parents
                ListOfContactSearchRecords.AddRange(dc.T003TelephoneContact);

            }
            else
            {
                ListOfContactSearchRecords.AddRange(ListOfContactRecords);
            }
            // Add category for 'Search All'
            T003TelephoneContact contactRecord = new T003TelephoneContact
            {
                T003Pk = 0,
                ToshibaDisplayName = "-- Search All Parents --"         //<====  I may not need this for a single table
            };
            ListOfContactSearchRecords.AddRange(ListOfContactRecords);
            // Insert "Search" at the top
            ListOfContactSearchRecords.Insert(0, contactRecord);

        }
        #endregion

        #region Get Methods

        public void Get()
        {
            contactDBContext db = new contactDBContext();

            ListOfContactRecords = db.T003TelephoneContact.OrderBy(s => s.ToshibaDisplayName).ToList();
            LogTheAction(eNums.eReadUpdateCreateDelete.Read);

            SetUIState(PageConstants.LIST);
        }

        public T003TelephoneContact Get(int serverPK)
        {
            contactDBContext dc = new contactDBContext();

            ContactRecord = dc.T003TelephoneContact.Find(serverPK);

            return ContactRecord;
        }

        #endregion \\Get Methods

        #region Search Method
        private void Search()
        {

            contactDBContext db = new contactDBContext();





            // Perform Search
            ListOfContactRecords = db.T003TelephoneContact.Where(s =>

                                                        (
                                                            string.IsNullOrEmpty(ContactSearchRecord.ToshibaDisplayName)
                                                            &&
                                                            string.IsNullOrEmpty(ContactSearchRecord.DisplayForUsers)
                                                            //&&
                                                            //string.IsNullOrEmpty(ContactSearchRecord.ShowUsers01)
                                                            &&
                                                            string.IsNullOrEmpty(ContactSearchRecord.ExtOld)
                                                            &&
                                                            string.IsNullOrEmpty(ContactSearchRecord.ExtToshiba)
                                                            &&
                                                            string.IsNullOrEmpty(ContactSearchRecord.Position)
                                                            &&
                                                            string.IsNullOrEmpty(ContactSearchRecord.Department)
                                                            &&
                                                            string.IsNullOrEmpty(ContactSearchRecord.Fax)
                                                            &&
                                                            string.IsNullOrEmpty(ContactSearchRecord.Email)
                                                            &&
                                                            string.IsNullOrEmpty(ContactSearchRecord.PhoneNumber)
                                                            &&
                                                            string.IsNullOrEmpty(ContactSearchRecord.LastName)
                                                            &&
                                                            string.IsNullOrEmpty(ContactSearchRecord.FirstName)
                                                            &&
                                                            string.IsNullOrEmpty(ContactSearchRecord.EbWb)
                                                            &&
                                                            string.IsNullOrEmpty(ContactSearchRecord.ToshibaDirectoryType)
                                                            &&
                                                            string.IsNullOrEmpty(ContactSearchRecord.ItNotes)
                                                        )

                                                        ||
                                                        (
                                                            s.ToshibaDisplayName.Contains(ContactSearchRecord.ToshibaDisplayName.Trim())
                                                            ||
                                                            s.DisplayForUsers.Contains(ContactSearchRecord.DisplayForUsers.Trim())
                                                            //||
                                                            //s.ShowUsers01.Contains(ContactSearchRecord.ShowUsers01)
                                                            ||
                                                            s.ExtOld.Contains(ContactSearchRecord.ExtOld.Trim())
                                                            ||
                                                            s.ExtToshiba.Contains(ContactSearchRecord.ExtToshiba.Trim())
                                                            ||
                                                            s.Position.Contains(ContactSearchRecord.Position.Trim())
                                                            ||
                                                            s.Department.Contains(ContactSearchRecord.Department.Trim())
                                                            ||
                                                            s.Fax.Contains(ContactSearchRecord.Fax.Trim())
                                                            ||
                                                            s.Email.Contains(ContactSearchRecord.Email.Trim())
                                                            ||
                                                            s.PhoneNumber.Contains(ContactSearchRecord.PhoneNumber.Trim())
                                                            ||
                                                            s.LastName.Contains(ContactSearchRecord.LastName.Trim())
                                                            ||
                                                            s.FirstName.Contains(ContactSearchRecord.FirstName.Trim())
                                                            ||
                                                            s.EbWb.Contains(ContactSearchRecord.EbWb.Trim())
                                                            ||
                                                            s.ToshibaDirectoryType.Contains(ContactSearchRecord.ToshibaDirectoryType.Trim())
                                                            ||
                                                            s.ItNotes.Contains(ContactSearchRecord.ItNotes.Trim())

                                                        )

                                                    )
                                                    .OrderBy(s => s.ToshibaDisplayName)
                                                    .ToList();


            SetUIState(PageConstants.LIST);
        }
        #endregion

        #region ResetSearch Method
        private void ResetSearch()
        {
            ContactSearchRecord = new T003TelephoneContact();
            Get();
        }
        #endregion  //ResetSearch Method

        #region AddMode Method
        private void AddMode()
        {
            ContactRecord = new T003TelephoneContact();
            SetUIState(PageConstants.ADD);
        }
        #endregion  //AddMode Method

        #region EditMode Method
        private void EditMode(int serverPk)
        {
            ContactRecord = Get(serverPk);
            SetUIState(PageConstants.EDIT);
        }
        #endregion

        #region SetUIState Method
        protected void SetUIState(string state)
        {
            PageMode = state;

            IsDetailAreaVisible = (PageMode == "Add" || PageMode == "Edit");
            IsListAreaVisible = (PageMode == "List");
            IsSearchAreaVisible = (PageMode == "List");
        }
        #endregion

        #region Save Method
        private void Save()
        {
            Messages.Clear();
            contactDBContext dc = new contactDBContext();
            try
            {
                //============================================
                //http://patrickdesjardins.com/blog/entity-framework-ef-modifying-an-instance-that-is-already-in-the-context
                // you have to detach the local version and set to modify the entity you are modifying
                //we first verify if the entity is present inside the DbSet. If it is NOT null, than we detach the local entity. 
                //This scenario is pretty common if you receive the object from a web request and you want to save the entity. 

                var local = dc.Set<T003TelephoneContact>()
                         .Local
                         .FirstOrDefault(s => s.T003Pk == ContactRecord.T003Pk);
                if (local != null)
                {
                    dc.Entry(local).State = EntityState.Detached;
                }
                //=============================================
                // Either Update or Insert child
                if (PageMode == PageConstants.EDIT)
                {

                    dc.Entry(ContactRecord).State = EntityState.Modified;
                    //by setting the state, then the 'SaveChanges' is able to save it's changes
                    dc.SaveChanges();


                    LogTheAction(eNums.eReadUpdateCreateDelete.Update);

                }
                else if (PageMode == PageConstants.ADD)
                {
                    dc.T003TelephoneContact.Add(ContactRecord);
                    dc.SaveChanges();

                    LogTheAction(eNums.eReadUpdateCreateDelete.Create);
                }
                // Get all the data again in case anything changed
                Get();

            }
            catch (DbEntityValidationException ex)
            {
                IsValid = false;
                // Validation errors
                foreach (var errors in ex.EntityValidationErrors)
                {
                    foreach (var item in errors.ValidationErrors)
                    {
                        Messages.AddModelError(item.PropertyName, item.ErrorMessage);
                    }
                }
            }
            // Set page state
            SetUIState(PageMode);
        }
        #endregion  //Save Method

        #region Delete Method
        public void Delete(int serverPK)
        {
            contactDBContext db = new contactDBContext();

            T003TelephoneContact server = db.T003TelephoneContact.Find(serverPK);

            if (server != null) db.T003TelephoneContact.Remove(server);

            db.SaveChanges();

            LogTheAction(eNums.eReadUpdateCreateDelete.Delete);


            Get();
        }
        #endregion  //Delete Method


        private void LogTheAction(eNums.eReadUpdateCreateDelete actionType)
        {
            //START LOGGING ================================================================================================================<<<
            string sX = ContactRecord.GetStringWith_RecordProperties();

            contactDBContext dcLog = new contactDBContext();

            T000MvcLogging logRecord = new T000MvcLogging();
            logRecord.ApplicationAssemblyName = General_Application_Extensions.fn_ReturnApplicationName();
            logRecord.Browser = Browser;
            logRecord.CreateUpdateDeleteRead = actionType.ToString();
            if (actionType != eNums.eReadUpdateCreateDelete.Read)
            {
                logRecord.Note = sX; //need to add serialized values  
            }
            else
            {
                actionType.ToString();
            }

            logRecord.UserLogIn = General_ActiveDirectory_Extensions.fn_sUser();
            string localIP = "";
            localIP = IP;
            logRecord.ComputerName = General_String_Extensions.General_functions.fn_ComputerName(localIP);
            dcLog.T000MvcLogging.Add(logRecord);
            dcLog.SaveChanges();

            //END LOGGING ================================================================================================================<<<
        }





    }
}