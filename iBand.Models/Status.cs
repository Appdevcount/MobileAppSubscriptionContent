using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBand.Models
{
    public class Status
    {
        // Methods
        public Status(int i)
        {
            switch (i)
            {
                case 0:
                    this.statuscode = "0";
                    this.statusdescription = "SUCCESS";
                    return;

                case 1:
                    this.statuscode = "1";
                    this.statusdescription = "FAILED";
                    return;

                case 404:
                    this.statuscode = "404";
                    this.statusdescription = "UNAUTHORIZED";
                    return;

                case 500:
                    this.statuscode = "500";
                    this.statusdescription = "INVALID MOBILE NUMBER";
                    return;

                case 800:
                    this.statuscode = "800";
                    this.statusdescription = "REQUIRED PARAMETERS MISSING/EMPTY";
                    return;

                case 1000:
                    this.statuscode = "1000";
                    this.statusdescription = "SERVICE NOT FOUND";
                    return;

                case 600:
                    this.statuscode = "600";
                    this.statusdescription = "PINS UNAVAILABLE";
                    return;

                case 700:
                    this.statuscode = "700";
                    this.statusdescription = "REFERENCE NUMBER NOT FOUND";
                    return;

                case 1060:
                    this.statuscode = "1060";
                    this.statusdescription = "SERVICE DISABLED/INVALID SERVICE";
                    return;

                case 1070:
                    this.statuscode = "1070";
                    this.statusdescription = "USER IS ALREADY SUBSCRIBED WITH THE SERVICE";
                    return;

                case 1100:
                    this.statuscode = "1100";
                    this.statusdescription = "USER IS NOT SUBSCRIBED WITH THE SERVICE";
                    return;
                    
                case 1010:

                    statuscode = "1010";
                    statusdescription = "INVALID HASH";
                    return;

                case 1020:

                    statuscode = "1020";
                    statusdescription = "TAMPERED DATA";
                    return;

                case 1040:
                    {
                        statuscode = "1040";
                        statusdescription = "INVALID USER/LOGIN";
                        break;
                    }
                    
                case 1080:
                    {
                        statuscode = "1080";
                        statusdescription = "AMOUNT EXCEEDS";
                        break;
                    }
                case 1090:
                    {
                        statuscode = "1090";
                        statusdescription = "INSUFFICIENT FUNDS";
                        break;
                    }
                case 2000:
                    {
                        statuscode = "2000";
                        statusdescription = "MOBILE NUMBER ALREADY EXISTS";
                        break;
                    }
                case 2020:
                    {
                        statuscode = "2020";
                        statusdescription = "EMAIL ALREADY EXISTS";
                        break;
                    }
                case 2040:
                    {
                        statuscode = "2040";
                        statusdescription = "INVALID VERIFICATION CODE";
                        break;
                    }
                case 2060:
                    {
                        statuscode = "2060";
                        statusdescription = "USERNAME DOESN'T EXISTS";
                        break;
                    }
                case 2080:
                    {
                        statuscode = "2080";
                        statusdescription = "USER IS DISABLED. PLEASE CONTACT SUPPORT";
                        break;
                    }
                case 3000:
                    {
                        statuscode = "2000";
                        statusdescription = "MOBILE NUMBER & EMAIL ALREADY EXISTS";
                        break;
                    }
                case 3020:
                    {
                        statuscode = "3020";
                        statusdescription = "MOBILE NOT ACTIVATED";
                        break;
                    }
                case 3040:
                    {
                        statuscode = "3040";
                        statusdescription = "WALLET ACCOUNT DOESN'T EXISTS";
                        break;
                    }
                case 3060:
                    {
                        statuscode = "3060";
                        statusdescription = "ORDER NOT FOUND";
                        break;
                    }
                case 3070:
                    {
                        statuscode = "3070";
                        statusdescription = "ORDER ALREADY CANCELED";
                        break;
                    }
                case 3080:
                    {
                        statuscode = "3080";
                        statusdescription = "FAILED YOUR VOTE IS NOT COUNTED";
                        break;
                    }
                case 3090:
                    {
                        statuscode = "3090";
                        statusdescription = "FAILED YOUR MESSAGE WAS NOT SENT";
                        break;
                    }
            }
            this.statuscode = "";
            this.statusdescription = "";
        }

        // Properties
        public string info1 { get; set; }

        public string info2 { get; set; }

        public string statuscode { get; set; }

        public string statusdescription { get; set; }

        public Translations translations { get; set; }
    }


}
