using GeorgiaVoterRegistration.BLL;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Admin.Security
{
    // This class will work with the ApplicationDbContext class to "seed" the database
    // when it generates the database tables if they do not exist.
    public class SecurityDbInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            #region Phase A - First, we'll set up our Security Roles
            // 1. Instantiate a Controller class from ASP.Net Identity to add roles
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            // 2. Grab our list of security roles from the web.config
            var startupRoles = ConfigurationManager.AppSettings["startupRoles"].Split(';');
            // 3. Loop through and create the security roles
            foreach (var role in startupRoles)
                roleManager.Create(new IdentityRole { Name = role });
            #endregion

            #region Phase B - Next, we'll add in our Website Administrator
            // 1. Get the values from the <appSettings>
            string adminUser = ConfigurationManager.AppSettings["adminUserName"];
            string adminEmail = ConfigurationManager.AppSettings["adminEmail"];
            string adminPass = ConfigurationManager.AppSettings["adminPassword"];
            
            // 2. Instantiate my Controller to manage Users
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            //                \   IdentityConfig.cs    /             \IdentityModels.cs/
            // 3. Add the web admin to the database
            var result = userManager.Create(new ApplicationUser
            {
                UserName = adminUser,
                Email = adminEmail
            }, adminPass);
            if (result.Succeeded)
                userManager.AddToRole(userManager.FindByName(adminUser).Id, DefaultRoles.AdminRole);
            #endregion

            #region Phase C - Lastly, we'll add login users for each of the Voters in our database
            // 1. Get the values from the <appSettings> in the web.config
            string newUserPassword = ConfigurationManager.AppSettings["newUserPassword"];
            // 2. Instantiate my controller to get voters
            var controller = new DominionController();
            var voters = controller.ListAllVoters();
            // 3. Add the users to my database
            foreach(var person in voters)
            {
                result = userManager.Create(new ApplicationUser
                {
                    UserName = $"{person.FirstName}.{person.LastName}",
                    Email = person.Email,
                    VoterId = person.VoterId,
                    RegisteredAs = "Democrat"
                }, newUserPassword);
                if (result.Succeeded)
                    userManager.AddToRole(userManager.FindByEmail(person.Email).Id, DefaultRoles.DefaultRole);
            }
            #endregion

            base.Seed(context);
        }
    }
}