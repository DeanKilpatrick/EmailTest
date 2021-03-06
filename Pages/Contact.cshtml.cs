using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using EmailTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EmailTest.Pages
{
    public class ContactModel : PageModel
    {
        [BindProperty]
        public ContactFormModel Contact { get; set; }

        public void OnGet()
        {
            
        }
        public IActionResult OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var mailbody = $@"Hello website owner,

            This is a new contact request from your website:

            Name: {Contact.Name}
            LastName: {Contact.LastName}
            Email: {Contact.Email}
            Message: ""{Contact.Message}""


            Cheers,
            The websites contact form";

            SendMail(mailbody);

            return RedirectToPage("Index");
        }
        private void SendMail(String mailbody)
        {
            using (var message = new MailMessage(Contact.Email, "Dean.Kilpatrick7@hotmail.com"))
            {
                //message.To.Add(new MailAddress("Dean.Kilpatrick7@hotmail.com"));
                message.From = new MailAddress("Dean.Kilpatrick7@hotmail.com");
                message.Subject = "New email from my website";
                message.Body = mailbody;

                using (var smtpClient = new SmtpClient("smtp.office365.com"))
                {
                    smtpClient.EnableSsl = true;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Port = 587;
                    smtpClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    smtpClient.Credentials = new NetworkCredential("Dean.Kilpatrick7@hotmail.com", "Maximus77");
                    //NEVER_EAT_POISON_Disable_CertificateValidation();
                    smtpClient.Send(message);
                     
                }
            }
        }
        static void NEVER_EAT_POISON_Disable_CertificateValidation()
        {
            // Disabling certificate validation can expose you to a man-in-the-middle attack
            // which may allow your encrypted message to be read by an attacker
            // https://stackoverflow.com/a/14907718/740639
            ServicePointManager.ServerCertificateValidationCallback =
                delegate (
                    object s,
                    X509Certificate certificate,
                    X509Chain chain,
                    SslPolicyErrors sslPolicyErrors
                ) {
                    return true;
                };
        }
    }
}
