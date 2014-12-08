using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Twilio;

namespace MiPrimerMVC.ProyectHelpers
{
    public class ApiTwilio 
    {
        //
        // GET: /ApiTwilio/

       public void mensajeTwilio(string mensaje)
        {
            string AccountSid = "ACa1384d316abd9839a7952581e2133aec";
            string AuthToken = "c678c88353df4247acca00596de505d3";

            var twilio = new TwilioRestClient(AccountSid, AuthToken);
            var message = twilio.SendMessage("+12673624767", "+50499073419", mensaje);

        }
        
	}
}