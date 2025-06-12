using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LandingPage.Models;
using System.Net;
using System.Net.Mail;

namespace LandingPage.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult SubmitForm(PurchaseModel model)
    {
        var smtpClient = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            Credentials = new NetworkCredential("PedalPulseBot@gmail.com", "yfju ynbq iglo niuh"),
            EnableSsl = true,
        };

        var mailMessage = new MailMessage()
        {
            From = new MailAddress("PedalPulseBot@gmail.com"),
            Subject = $"Message from{model.Name}",
            Body = $"Email: {model.Email}\n\nPhoneNumber : {model.PhoneNumber}\n\nInquiry : {model.Inquiry}",
            IsBodyHtml = false,
        };

        mailMessage.To.Add("anish@integratedwebworks.com");
        smtpClient.Send(mailMessage);
        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
