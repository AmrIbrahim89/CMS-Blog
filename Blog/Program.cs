using Entities;
using Entities.IdentityEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repositories;
using RepositoryContracts;
using ServiceContracts;
using Services;

namespace TechBlog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<ApplicationDBContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            //Add Services to IOC Container
            builder.Services.AddScoped<IBlogPostGetterService, BlogPostGetter>();
            builder.Services.AddScoped<IBlogPostsRepository, BlogPostsRepository>();
            builder.Services.AddScoped<IBlogPostAdderService, BlogPostAdder>();
            builder.Services.AddScoped<IBlogPostDeleterService, BlogPostDeleter>();
            builder.Services.AddScoped<IBlogPostUpdaterService, BlogPostUpdater>();
            builder.Services.AddScoped<IBlogPostSorterService, BlogPostSorter>();
            builder.Services.AddScoped<IUsersRepository, UsersRepository>();
            builder.Services.AddScoped<IUserUpdaterService, UserUpdater>();
            builder.Services.AddScoped<IUserGetterService, UserGetter>();
            builder.Services.AddScoped<IMediaRepository, MediaRepository>();
            builder.Services.AddScoped<IMediaAdderService, MediaAdder>();
            builder.Services.AddScoped<IMediaGetterService,  MediaGetter>();
            builder.Services.AddScoped<IMediaDeleterService, MediaDeleter>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<ICategoryAdderService, CategoryAdder>();
            builder.Services.AddScoped<ICategoryGetterService, CategoryGetter>();
            builder.Services.AddScoped<ISubscribeRepository, SubscriberRepository>();
            builder.Services.AddScoped<ISubscriberAdderService, SubscriberAdder>();
            builder.Services.AddScoped<ICommentRepository , CommentRepository>();
            builder.Services.AddScoped<ICommentAdderService , CommentAdder>();
            builder.Services.AddScoped<ICommentGetterService , CommentGetter>();
            builder.Services.AddScoped<ICommentDeleterService , CommentDeleter>();
            builder.Services.AddScoped<IPageRepository, PageRepository>();
            builder.Services.AddScoped<IPageAdderService, PageAdder>();
            builder.Services.AddScoped<IPageGetterService, PageGetter>();
            builder.Services.AddScoped<IPageDeleterService, PageDeleter>();
            builder.Services.AddScoped<IMenuRepository, MenuRepository>();
            builder.Services.AddScoped<IMenuAdderService , MenuAdder>();
            builder.Services.AddScoped<IMenuGetterService , MenuGetter>();

            //Add Identity
            builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDBContext>()
                .AddDefaultTokenProviders();

            //Configure Role Policy
            builder.Services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //if (!app.Environment.IsDevelopment())
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //}

            if (builder.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // built-in middleware used to redirect to some page when you have an error.
                // it handles all exceptions including exceptions that happens from the custom middleware (see next line).
                // /Error -> is the route to redirect.
                // This middleware executes second.
                app.UseExceptionHandler("/Home/Error");

                // custom middleware used to handle exceptions. (executes first)
                //app.UseExceptionHandlingMiddleware();
            }


            //Configure HTTPS
            app.UseHsts();
            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseRouting();

            //Read identity cookie
            app.UseAuthentication();

            //Check user role
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
