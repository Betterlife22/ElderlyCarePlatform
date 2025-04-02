﻿using BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BLL;

public static class DependencyInjection
{
    public static IServiceCollection AddBLL(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        //Register service
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IService, ServiceService>();
        services.AddScoped<IResourceService, ResourceService>();
        services.AddScoped<IReceiptService, ReceiptService>();
        services.AddScoped<IRatingService, RatingService>();
        services.AddScoped<IProfileService, ProfileService>();
        services.AddScoped<IHealthRecordService, HealthRecordService>();
        services.AddScoped<ICaregiverService, CaregiverService>();
        services.AddScoped<IBookingService, BookingService>();
        services.AddScoped<IPaymentService, PaymentService>();
        return services;
    }
}