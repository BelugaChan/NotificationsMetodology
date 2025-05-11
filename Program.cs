using NotificationsService.Configurations;
using NotificationsService.DatabaseActions;
using NotificationsService.Hubs;
using NotificationsService.Interfaces;
using NotificationsService.Listeners;
using NotificationsService.Models;
using NotificationsService.Services;
using RabbitMqListener.Interfaces;

namespace NotificationsService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddSignalR();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
           
            builder.Services.Configure<DynamoDbOptions>(builder.Configuration.GetSection(nameof(DynamoDbOptions)));
            builder.Services.Configure<JWTOptions>(builder.Configuration.GetSection(nameof(JWTOptions)));

            builder.Services.AddSingleton<IDynamoDbConfigure, DynamoDbConfigure>();
            builder.Services.AddScoped<INotificationService, NotificationService>();
            builder.Services.AddScoped<INotificationCommandsService, NotificationCommandService>();
            builder.Services.AddScoped<IUserBioCommandService, UserBioCommandService>();
            builder.Services.AddScoped<INotificationQueryService, NotificationQueryService>();
            builder.Services.AddHostedService<RabbitMqUserNotificationListener>()
                .AddSingleton<IRabbitMqListenerBase, RabbitMqUserNotificationListener>();

            var app = builder.Build();


            app.MapHub<NotificationHub>("/notificationhub");
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
