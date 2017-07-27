using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web;
using System.DirectoryServices.AccountManagement;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;

namespace p_0009_WEBFE_T003_Contacts.ViewModel
{

    public static class General_Application_Extensions
    {
        public static string fn_ReturnApplicationName()
        {
            string sX = "";

            sX = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;

            return sX;

        }
    }

    public static class General_String_Extensions
    {


        public static class General_functions
        {
            public static string fn_ComputerName(string IP)
            {
                //USAGE
                //From: http://stackoverflow.com/questions/1444592/determine-clients-computer-name
                //string IP = Request.UserHostName;
                //string compName = CompNameHelper.DetermineCompName(IP);
                if (IP != null)
                {
                    IPAddress myIP = IPAddress.Parse(IP);
                    string sIP = myIP.ToString();
                    try
                    {

                        IPHostEntry GetIPHost = Dns.GetHostEntry(myIP);
                        List<string> compName = GetIPHost.HostName.ToString().Split('.').ToList();
                        return compName.First();
                    }
                    catch (ArgumentNullException e1ArgumentNullException)
                    {

                        return "ArgumentNullException: (" + sIP + ")";
                    }
                    catch (ArgumentOutOfRangeException e2ArgumentOutOfRangeException)
                    {

                        return "ArgumentOutOfRangeException: (" + sIP + ")";
                    }
                    catch (SocketException e3SocketException)
                    {
                        return "SocketException: (" + sIP + ")";
                    }
                    catch (ArgumentException e4ArgumentException)
                    {
                        return "ArgumentException: (" + sIP + ")";
                    }
                    catch (Exception e5Exception)
                    {
                        return "UnknownException: (" + sIP + ")";
                    }
                }
                else
                {
                    return "";
                }

            }


        }   //General_functions


        public static string ConvertNullOrEmptyStringTo(this string sStringToTest, string sDefaultIfNullOrEmpty)
        {
            //string sX = string.IsNullOrEmpty(sStringToTest) ? sStringToTest : sDefaultIfNullOrEmpty;
            string sX = string.IsNullOrEmpty(sStringToTest) ? sDefaultIfNullOrEmpty : sStringToTest;
            return sX;

        }


        /// <summary>
        /// Returns the String HASH of two integers entered as Integers
        /// </summary>
        /// <param name="iInteger1"></param>
        /// <param name="iInteger2"></param>
        /// <returns></returns>
        static public string fn_stringHash_ofSum_ofTwoIntegers(int iInteger1, int iInteger2)
        {
            string sX = "";

            try
            {

                int iParm_3 = 0;
                iParm_3 = iInteger1 + iInteger2;
                sX = General_String_Extensions.fn_ComputeStringOfHash(iParm_3.ToString());

            }
            catch (Exception e)
            {
                Debug.WriteLine("fn_stringHash_ofSum_ofTwoIntegers - Exception!" + e.ToString());
            }

            return sX;

        }

        /// <summary>
        /// Returns the String HASH of two integers entered as strings
        /// </summary>
        /// <param name="sInteger1"></param>
        /// <param name="sInteger2"></param>
        /// <returns></returns>
        static public string fn_stringHash_ofSum_ofTwoIntegers(string sInteger1, string sInteger2)
        {
            string sX = "";

            try
            {
                int iParm_1 = 0;
                int iParm_2 = 0;
                int iParm_3 = 0;
                bool bParm_1_isNumeric = int.TryParse(sInteger1, out iParm_1);
                bool bParm_2_isNumeric = int.TryParse(sInteger2, out iParm_2);

                if (bParm_1_isNumeric && bParm_2_isNumeric)
                {
                    iParm_3 = iParm_1 + iParm_2;
                    sX = General_String_Extensions.fn_ComputeStringOfHash(iParm_3.ToString());
                }


            }
            catch (Exception e)
            {
                Debug.WriteLine("fn_stringHash_ofSum_ofTwoIntegers - Exception!" + e.ToString());
            }

            return sX;

        }

        static public string fn_ReturnPortionOfStringBeforeLastOccuranceOfACharacter(string strInput, char cBreakCharacter)
        {
            // NOTE: for path backslash "/", set cBreakCharacter = '\\'
            string strX = null;

            //1] how long is the string
            int iStrLenth = strInput.Length;

            //2] How far from the end does the last occurance of the character occur

            int iLenthFromTheLeftOfTheLastOccurance = strInput.LastIndexOf(cBreakCharacter);

            strX = fn_ReturnFirstXLettersOfString(iLenthFromTheLeftOfTheLastOccurance, strInput);

            return strX;

        }

        static public string fn_ReturnPortionOfStringAfterLastOccuranceOfACharacter(string strInput, char cBreakCharacter)
        {
            // NOTE: for path backslash "/", set cBreakCharacter = '\\'
            string strX = null;

            //1] how long is the string
            int iStrLenth = strInput.Length;

            //2] How far from the end does the last occurance of the character occur

            int iLenthFromTheLeftOfTheLastOccurance = strInput.LastIndexOf(cBreakCharacter);

            int iLenthFromTheRightToUse = 0;
            iLenthFromTheRightToUse = iStrLenth - iLenthFromTheLeftOfTheLastOccurance;

            //3] Get the Portion of the string, that occurs after the last occurance of the character
            strX = fn_ReturnLastXLettersOfString(iLenthFromTheRightToUse, strInput);

            return strX;

        }

        static private string fn_ReturnLastXLettersOfString(int iNoOfCharToReturn, string strIn)
        {
            int iLenth = 0;
            string strX = null;
            int iNoOfCharacters = iNoOfCharToReturn;

            iLenth = strIn.Length;
            if (iLenth >= iNoOfCharacters)
            {
                strX = strIn.Substring(iLenth - iNoOfCharacters + 1);

            }
            else
            {
                strX = strIn;
            }


            return strX;
        }

        static private string fn_ReturnFirstXLettersOfString(int iNoOfCharToReturn, string strIn)
        {

            //const string k_SubName = "fn_ReturnFirstXLettersOfString";
            string strX = null;
            try
            {

                int iLenth = 0;

                int iNoOfCharacters = iNoOfCharToReturn;

                iLenth = strIn.Length;
                if (iLenth >= iNoOfCharacters)
                {
                    strX = strIn.Substring(0, iNoOfCharacters);

                    //strX = Strings.Mid(strIn, 1, iNoOfCharacters);
                }
                else
                {
                    strX = strIn;
                }




            }
            catch (Exception ex)
            {
                Debug.WriteLine("fn_ReturnFirstXLettersOfString - An error occurred! Better check the code!" + ex.ToString());
            }
            return strX;

        }

        /// <summary>
        /// returns boolean
        /// A basic ordinal comparison (System.StringComparison.Ordinal) is case-sensitive, 
        /// which means that the two strings must match character for character: "and" does not equal "And" or "AND".
        /// </summary>
        /// <param name="s1X"></param>
        /// <param name="s2X"></param>
        /// <returns></returns>
        public static bool fn_String1_equals_String2_CaseMatters(string s1X, string s2X)
        {
            bool bX = s1X.Equals(s2X, StringComparison.Ordinal);
            return bX;

        }

        /// <summary>
        /// Returns a string MD5CryptoServiceProvider hash of the input string
        /// From: http://support.microsoft.com/kb/307020
        /// using System.Security.Cryptography;
        /// using System.Text;
        /// </summary>
        /// <param name="sX"></param>
        /// <returns></returns>
        public static string fn_ComputeStringOfHash(string sX)
        {
            string sReturn = null;
            byte[] byteX = null;
            byte[] tmpSource;

            try
            {
                //Create a byte array from source data
                tmpSource = ASCIIEncoding.ASCII.GetBytes(sX);
                byteX = new MD5CryptoServiceProvider().ComputeHash(tmpSource);

            }
            catch (Exception e)
            {
                Debug.WriteLine("fn_ComputeStringOfHash - An error occurred! Better check the code!" + e.ToString());
            }

            sReturn = ByteArrayToString(byteX);

            return sReturn;
        }

        /// <summary>
        /// Returns an MD5CryptoServiceProvider byte Array (hash) of the input string
        /// From: http://support.microsoft.com/kb/307020
        /// using System.Security.Cryptography;
        /// using System.Text;
        /// </summary>
        /// <param name="sX"></param>
        /// <returns></returns>
        public static byte[] fn_ComputeByteArrayOfHash(string sX)
        {
            byte[] byteX = null;
            byte[] tmpSource;

            try
            {
                //Create a byte array from source data
                tmpSource = ASCIIEncoding.ASCII.GetBytes(sX);
                byteX = new MD5CryptoServiceProvider().ComputeHash(tmpSource);

            }
            catch (Exception e)
            {
                Debug.WriteLine("fn_ComputeByteArrayOfHash - An error occurred! Better check the code!" + e.ToString());
            }



            return byteX;
        }


        public static string ByteArrayToString(byte[] arrInput)
        {
            int i;
            StringBuilder sOutput = new StringBuilder(arrInput.Length);
            for (i = 0; i < arrInput.Length - 1; i++)
            {
                sOutput.Append(arrInput[i].ToString("X2"));
            }
            return sOutput.ToString();
        }
    }   //General_String_Extensions


    public static class General_ActiveDirectory_Extensions
    {

        /// <summary>
        /// Must use Windows Authentication in IIS, i.e. not anonymous access in IIS
        /// Must use the impersonate attribute in the ROOT web.config of the Web Application:
        ///     <authentication mode="Windows"/>"
        /// User.Identity.Name represents identity passed from IIS
        /// using System.DirectoryServices.AccountManagement;
        /// </summary>
        /// <param name="sGroup"> This is the name of the AD Security Group in Question</param>
        /// <returns>boolean</returns>
        public static bool fn_bIsTheWindowAuthenticatedUserInSecurityGroup(string sGroup)
        {
            string DomainName = "CCWNC01.accessiicarewnc.net";
            string ADGroupName = sGroup;
            Boolean bX = false;
            string sUser = fn_sUser();
            using (var ctx = new PrincipalContext(ContextType.Domain, DomainName, "testdu", "Asdf.1234"))
            {
                using (var grp = GroupPrincipal.FindByIdentity(ctx, IdentityType.Name, ADGroupName))
                {

                    foreach (var member in grp.GetMembers())
                    {
                        string sX = (string)member.SamAccountName;

                        if (sUser.Trim() == sX.Trim())
                        { bX = true; }
                    }
                }
            }
            return bX;
        }

        public static string fn_sUser()
        {
            char cX = '\\';
            string sUser = General_String_Extensions.fn_ReturnPortionOfStringAfterLastOccuranceOfACharacter(HttpContext.Current.User.Identity.Name, cX);
            return sUser;   //returns just the short logon name Example for 'accessiicarewnc\ggarson', it returns 'ggarson'   
        }


    }   //General_ActiveDirectory_Extensions


    public static class General_Object_Extensions
    {
        public static string NullSafeToString(this object obj)
        {
            return obj != null ? obj.ToString() : String.Empty;
        }

        public static string GetStringWith_RecordProperties(this object Record)
        {

            string sX = null;
            Dictionary<string, object> _record = GetDictionary_WithPropertiesForOneRecord(Record);
            int iPropertyCounter = 0;

            foreach (var KeyValuePair in _record)
            {

                iPropertyCounter += 1;
                object thePropertyValue = _record[KeyValuePair.Key];
                if (thePropertyValue != null)
                {
                    sX = sX + iPropertyCounter + ") Property: {" + KeyValuePair.Key + "} = [" + thePropertyValue + "] \r\n";
                }
                else
                {
                    sX = sX + iPropertyCounter + ") Property: {" + KeyValuePair.Key + "} = [{NULL}] \r\n";
                }

            }

            return sX;
        }

        public static Dictionary<string, object> GetDictionary_WithPropertiesForOneRecord(object atype)
        {
            if (atype == null) return new Dictionary<string, object>();
            Type t = atype.GetType();
            PropertyInfo[] props = t.GetProperties();
            Dictionary<string, object> dict = new Dictionary<string, object>();
            foreach (PropertyInfo prp in props)
            {
                object value = prp.GetValue(atype, new object[] { });
                dict.Add(prp.Name, value);
            }
            return dict;
        }

    }   //General_Object_Extensions

}