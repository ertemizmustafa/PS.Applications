using GreenPipes;
using MassTransit;
using MassTransit.Definition;
using Microsoft.Extensions.DependencyInjection;
using PS.Core.Settings;
using PS.Notification.Application.Abstract;
using PS.Notification.Application.Consumers;
using PS.Notification.Application.Services;
using PS.Notification.Application.Settings;
using System;
using System.Reflection;

namespace PS.Notification.Application.Extensions
{

    public static class ConfigurationExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, MailSettings mailSettings, RabbitMqSettings rabbitMqSettings)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddSingleton(mailSettings);
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IMailService, MailService>();

            services.AddMassTransit(x =>
            {
                //x.AddConsumers(Assembly.GetExecutingAssembly());
                x.AddConsumer<MailCreatedConsumer>(c =>
                {
                    c.UseMessageRetry(r =>
                    {

                        r.Intervals(100, 500, 1000, 2000, 5000);
                    });

                    c.UseCircuitBreaker(cb =>
                    {
                        cb.TripThreshold = 15;
                        cb.ActiveThreshold = 10;
                        cb.ResetInterval = TimeSpan.FromMinutes(5);
                    });

                });

                x.AddConsumer<MailCreatedFaultConsumer>(c =>
                {
                    c.UseMessageRetry(r =>
                    {

                        r.Intervals(100, 500, 1000, 2000, 5000);
                    });

                    c.UseCircuitBreaker(cb =>
                    {
                        cb.TripThreshold = 15;
                        cb.ActiveThreshold = 10;
                        cb.ResetInterval = TimeSpan.FromMinutes(5);
                    });

                });

                x.AddConsumer<MailSentConsumer>(c =>
                {
                    c.UseMessageRetry(r =>
                    {

                        r.Intervals(100, 500, 1000, 2000, 5000);
                    });

                    c.UseCircuitBreaker(cb =>
                    {
                        cb.TripThreshold = 15;
                        cb.ActiveThreshold = 10;
                        cb.ResetInterval = TimeSpan.FromMinutes(5);
                    });

                });


                x.UsingRabbitMq((context, cfg) =>
                {
                    //x.SetKebabCaseEndpointNameFormatter();

                    cfg.Host(rabbitMqSettings.Host, configurator =>
                    {
                        configurator.Username(rabbitMqSettings.UserName);
                        configurator.Password(rabbitMqSettings.Password);
                    });

                    cfg.ClearMessageDeserializers();
                    cfg.UseRawJsonSerializer();

                    //cfg.ReceiveEndpoint("ps-notify-mail-created", e =>
                    // {
                    //     e.UseMessageRetry(r =>
                    //     {
                    //         r.Interval(1, 100);
                    //        //r.Intervals(100, 500, 1000, 2000, 5000);
                    //    });

                    //     e.UseCircuitBreaker(cb =>
                    //     {
                    //         cb.TripThreshold = 15;
                    //         cb.ActiveThreshold = 10;
                    //         cb.ResetInterval = TimeSpan.FromMinutes(5);
                    //     });

                    //     e.ConfigureConsumer<NotificationMailCreatedConsumer>(context);
                    // });

                    //cfg.ReceiveEndpoint("ps-notify-mail-created_error", e =>
                    //{
                    //    e.UseMessageRetry(r =>
                    //    {
                    //        r.Interval(1, 100);
                    //        //r.Intervals(100, 500, 1000, 2000, 5000);
                    //    });

                    //    e.UseCircuitBreaker(cb =>
                    //    {
                    //        cb.TripThreshold = 15;
                    //        cb.ActiveThreshold = 10;
                    //        cb.ResetInterval = TimeSpan.FromMinutes(5);
                    //    });

                    //    e.ConfigureConsumer<NotificationMailSentFaultConsumer>(context);
                    //});

                    //cfg.ReceiveEndpoint("ps-notify-mail-sent", e =>
                    //{
                    //    e.UseMessageRetry(r =>
                    //    {
                    //        r.Interval(1, 100);
                    //        //r.Intervals(100, 500, 1000, 2000, 5000);
                    //    });
                    //    e.UseCircuitBreaker(cb =>
                    //    {
                    //        cb.TripThreshold = 15;
                    //        cb.ActiveThreshold = 10;
                    //        cb.ResetInterval = TimeSpan.FromMinutes(5);
                    //    });
                    //    e.ConfigureConsumer<NotificationMailSentConsumer>(context);

                    //});


                    cfg.ConfigureEndpoints(context, SnakeCaseEndpointNameFormatter.Instance);
                });
            });

            services.AddMassTransitHostedService();
            return services;
        }
    }

}
