using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using okLims.Data;
using okLims.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace okLims.Services
{

    public class Functional : IFunctional
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ApplicationDbContext _context;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IRoles _roles;
    private readonly SuperAdminDefaultOptions _superAdminDefaultOptions;


    public Functional(UserManager<ApplicationUser> userManager,
       RoleManager<IdentityRole> roleManager,
       ApplicationDbContext context,
       SignInManager<ApplicationUser> signInManager,
       IRoles roles,
       IOptions<SuperAdminDefaultOptions> superAdminDefaultOptions)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _context = context;
        _signInManager = signInManager;
        _roles = roles;
        _superAdminDefaultOptions = superAdminDefaultOptions.Value;
    }

    public async Task InitAppData()
    {
        try
        {
            await _context.Instrument.AddAsync(new Instrument { InstrumentName = "Bioreactor" });
            await _context.Laboratory.AddAsync(new Laboratory { LaboratoryName = "Default" });
            await _context.SaveChangesAsync();
            await _context.FilterSize.AddAsync(new FilterSize { SizeID = 1, filterSize = "30 kDa" });
            await _context.FilterSize.AddAsync(new FilterSize { SizeID = 2, filterSize = "75 kDa" });
            await _context.FilterSize.AddAsync(new FilterSize
            {
                SizeID = 3,
                filterSize =
                "0.2 um"
            });
            await _context.FilterSize.AddAsync(new FilterSize
            {
                SizeID = 4,
                filterSize =

                ".45 um"
            });
            await _context.SaveChangesAsync();

            await _context.FilterType.AddAsync(new FilterType { FilterID = 1, filterType = "FedBatch" });
            await _context.FilterType.AddAsync(new FilterType { FilterID = 2, filterType = "FedBatch Mod" });
            await _context.FilterType.AddAsync(new FilterType { FilterID = 3, filterType = "Atf Single" });
            await _context.FilterType.AddAsync(new FilterType { FilterID = 4, filterType = "ATF t" });
            await _context.FilterType.AddAsync(new FilterType { FilterID = 5, filterType = "ATF mod" });
            await _context.SaveChangesAsync();
            await _context.ControllerType.AddAsync(new ControllerType { ControllerID = 1, controllerType = "Finesse" });
            await _context.ControllerType.AddAsync(new ControllerType { ControllerID = 2, controllerType = "In-Control" });
            await _context.SaveChangesAsync();
          
            await _context.SaveChangesAsync();


            List<Request> requests = new List<Request>()
                {
                    new Request{RequesterEmail = "crossmedders@gmail.com"},

                    new Request{RequesterEmail = "cmedders@amgen.com"},

                };
            await _context.Request.AddRangeAsync(requests);
            await _context.SaveChangesAsync();
            await _context.ApplicationUser.AddAsync(new ApplicationUser { Email = "crossmedders@gmail.com", EmailConfirmed = true, UserName = "crossmedders@gmail.com" });
            await _context.SaveChangesAsync();
            await _context.ApplicationUser.AddAsync(new ApplicationUser { Email = "crossmedders@gmail.com", Password = "qzpm1056", FirstName = "Cross", LastName = "Medders" });
            await _context.SaveChangesAsync();
                await _context.Request.AddAsync(new Request { ControllerID= 1, RequestId=1, FilterID=1, SizeID=1, RequesterEmail="crossmedders@gmail.com", LaboratoryId=1, StateId=0,  })
        }


        catch (Exception)
        {

            throw;
        }
    }
    public async Task SendEmailBySendGridAsync(string apiKey,
          string fromEmail,
          string fromFullName,
          string subject,
          string message,
          string email)
    {
        var client = new SendGridClient(apiKey);
        var msg = new SendGridMessage()
        {
            From = new EmailAddress(fromEmail, fromFullName),
            Subject = subject,
            PlainTextContent = message,
            HtmlContent = message
        };
        msg.AddTo(new EmailAddress(email, email));
        await client.SendEmailAsync(msg);

    }

    public async Task SendEmailByGmailAsync(string fromEmail,
        string fromFullName,
        string subject,
        string messageBody,
        string toEmail,
        string toFullName,
        string smtpUser,
        string smtpPassword,
        string smtpHost,
        int smtpPort,
        bool smtpSSL)
    {
        var body = messageBody;
        var message = new MailMessage();
        message.To.Add(new MailAddress(toEmail, toFullName));
        message.From = new MailAddress(fromEmail, fromFullName);
        message.Subject = subject;
        message.Body = body;
        message.IsBodyHtml = true;

        using (var smtp = new SmtpClient())
        {
            var credential = new NetworkCredential
            {
                UserName = smtpUser,
                Password = smtpPassword
            };
            smtp.Credentials = credential;
            smtp.Host = smtpHost;
            smtp.Port = smtpPort;
            smtp.EnableSsl = smtpSSL;
            await smtp.SendMailAsync(message);

        }

    }
    public async Task CreateDefaultSuperAdmin()
    {
        try
        {
            await _roles.GenerateRolesFromPagesAsync();

            ApplicationUser superAdmin = new ApplicationUser();
            superAdmin.Email = _superAdminDefaultOptions.Email;
            superAdmin.UserName = superAdmin.Email;
            superAdmin.EmailConfirmed = true;

            var result = await _userManager.CreateAsync(superAdmin, _superAdminDefaultOptions.Password);

            if (result.Succeeded)
            {
                //add to user profile
                ApplicationUser profile = new ApplicationUser();
                profile.FirstName = "Super";
                profile.LastName = "Admin";
                profile.Email = superAdmin.Email;
                profile.ApplicationUserId = superAdmin.Id;
                await _context.ApplicationUser.AddAsync(profile);
                await _context.SaveChangesAsync();

                await _roles.AddToRoles(superAdmin.Id);
            }
        }
        catch (Exception)
        {

            throw;
        }
    }


    public async Task<string> UploadFile(List<IFormFile> files, IHostingEnvironment env, string uploadFolder)
    {
        var result = "";

        var webRoot = env.WebRootPath;
        var uploads = Path.Combine(webRoot, uploadFolder);
        var extension = "";
        var filePath = "";
        var fileName = "";


        foreach (var formFile in files)
        {
            if (formFile.Length > 0)
            {
                extension = Path.GetExtension(formFile.FileName);
                fileName = Guid.NewGuid().ToString() + extension;
                filePath = Path.Combine(uploads, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }

                result = fileName;

            }
        }

        return result;
    }

}
}