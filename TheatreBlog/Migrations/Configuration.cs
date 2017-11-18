namespace TheatreBlog.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.SqlClient;
    using System.Linq;
    /// <summary>
    /// Class Configuration. This class cannot be inherited.
    /// The Seed method will populate the database with initial users
    /// </summary>
    /// <seealso cref="System.Data.Entity.Migrations.DbMigrationsConfiguration{TheatreBlog.Models.ApplicationDbContext}" />
    internal sealed class Configuration : DbMigrationsConfiguration<TheatreBlog.Models.ApplicationDbContext>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Configuration"/> class.
        /// here we will set automigrations 
        /// </summary>
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }
        /// <summary>
        /// Runs after upgrading to the latest migration to allow seed data to be updated.
        /// </summary>
        /// <param name="context">Context to be used for updating seed data.</param>
        /// <remarks>Note that the database may already contain seed data when this method runs. This means that
        /// implementations of this method must check whether or not seed data is present and/or up-to-date
        /// and then only make changes if necessary and in a non-destructive way. The
        /// <see cref="M:System.Data.Entity.Migrations.DbSetMigrationsExtensions.AddOrUpdate``1(System.Data.Entity.IDbSet{``0},``0[])" />
        /// can be used to help with this, but for seeding large amounts of data it may be necessary to do less
        /// granular checks if performance is an issue.
        /// If the <see cref="T:System.Data.Entity.MigrateDatabaseToLatestVersion`2" /> database
        /// initializer is being used, then this method will be called each time that the initializer runs.
        /// If one of the <see cref="T:System.Data.Entity.DropCreateDatabaseAlways`1" />, <see cref="T:System.Data.Entity.DropCreateDatabaseIfModelChanges`1" />,
        /// or <see cref="T:System.Data.Entity.CreateDatabaseIfNotExists`1" /> initializers is being used, then this method will not be
        /// called and the Seed method defined in the initializer should be used instead.</remarks>
        protected override void Seed(TheatreBlog.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            SeedMembership( context);
            SeedBlog( context);
          
        }

        private void SeedBlog(TheatreBlog.Models.ApplicationDbContext context)
        {
            var post1 = new Post();
            post1.PostID = 1;
            post1.Author = "admin@localtheatre.com" ;
            post1.Title = " my day";
            post1.PostBody = " games design at clyde";
            post1.PublishDate = DateTime.Now;
            try { 
                context.Posts.AddOrUpdate(post1);
            }catch (SqlException ex)
            {
               var error = ex.Message;
            }
            var post2 = new Post();
            post2.PostID = 2;
            post2.Author = "admin@localtheatre.com";
            post2.Title = " my day";
            post2.PostBody = " games design at clyde";
            post2.PublishDate = DateTime.Now;
            try { 
                context.Posts.AddOrUpdate(post2);
            }catch (SqlException ex)
            {
               var error = ex.Message;
            }


    var comment1 = new Comment();

            comment1.CommentID = 1;
            comment1.PostID = 2;
            comment1.CommentText = "nice";

            comment1.Author = "testuser@localtheatre.com";
            comment1.CommentDate = DateTime.Now;
            try
            {
                context.Comments.AddOrUpdate(comment1);
            }
            catch (Exception ex)
            {
                var error = ex.InnerException;
            }

            var comment2 = new Comment();
            comment2.CommentID = 2;
            comment2.PostID = 2;
            comment2.CommentText = "nice";
            comment2.Author = "testuser@localtheatre.com";
            comment2.CommentDate = DateTime.Now;

            try
            {
                context.Comments.AddOrUpdate(comment2);

                context.SaveChanges();
            }
            catch (Exception ex)
            {
                var error = ex.InnerException;
            }
        }

        private void SeedMembership(TheatreBlog.Models.ApplicationDbContext context)
        {
             

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            // In Startup iam creating first Admin Role and creating a default Admin User    
            if (!roleManager.RoleExists("Admin"))
            {

                // first we create Admin role  
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
            }

                var user = new ApplicationUser();
                user.UserName = "testuser";
                user.Email = "testuser@localtheatre.com";

                string userPWD = "Password123!";

                UserManager.Create(user, userPWD);

                //Here we create a Admin super user who will maintain the website                  

                var adminuser = new ApplicationUser();
                adminuser.UserName = "admin@localtheatre.com";
                adminuser.Email = "admin@localtheatre.com";

                 userPWD = "Password123!";

                var chkUser = UserManager.Create(adminuser, userPWD);

                //Add default User to Role Admin   
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(adminuser.Id, "Admin");

                }
            

            // creating Creating Manager role    
            if (!roleManager.RoleExists("Restricted"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Restricted";
                roleManager.Create(role);

            }

           
        }
    }
}
