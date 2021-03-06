﻿namespace TM.PartnerStores.IoC
{
    using System;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using TM.PartnerStores.Application.Contracts;
    using TM.PartnerStores.Application.Parsers;
    using TM.PartnerStores.Application.Partner;
    using TM.PartnerStores.Domain.Repositories;
    using TM.PartnerStores.Repository.MongoDB.Connection;
    using TM.PartnerStores.Repository.MongoDB.Migration;
    using TM.PartnerStores.Repository.MongoDB.Parsers;
    using TM.PartnerStores.Repository.MongoDB.Partner;
    using MongoDBModel = TM.PartnerStores.Repository.MongoDB.Model;

    public static class DependencyInjectionExtension
    {
        public static void AddPartnerStoresComponents(this IServiceCollection services, IConfiguration configuration)
        {
            AddMongoDBRepository(services, configuration);
            AddApplication(services);
        }

        public static void AddUnitTestsPartnerStoresComponents(this IServiceCollection services, IPartnerRepository mockedPartnerRepository)
        {
            AddUnitTestsMongoDBRepository(services, mockedPartnerRepository);
            AddApplication(services);
        }

        private static void AddApplication(IServiceCollection services)
        {
            services.AddSingleton<IPartnerApplicationService, PartnerApplicationService>();
            services.AddSingleton<IPartnerApplicationServiceParser, PartnerApplicationServiceParser>();
        }

        private static void AddMongoDBRepository(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoDBConnectionSettings>(configuration.GetSection("MongoDBConnectionSettings"));
            services.AddSingleton(provider => provider.GetService<IOptions<MongoDBConnectionSettings>>().Value);
            services.AddSingleton<DbContext>();
            services.AddSingleton<DatabaseCreator>();
            services.AddSingleton<ICollectionContext<MongoDBModel.PartnerModel>, CollectionContext<MongoDBModel.PartnerModel>>();
            services.AddSingleton<ICoordinateParser, CoordinateParser>();
            services.AddSingleton<IPartnerParser, PartnerParser>();
            services.AddSingleton<IPartnerRepository, PartnerRepository>();
        }

        private static void AddUnitTestsMongoDBRepository(IServiceCollection services, IPartnerRepository mockedPartnerRepository)
        {
            services.AddSingleton(provider => provider.GetService<IOptions<MongoDBConnectionSettings>>().Value);
            services.AddSingleton<ICoordinateParser, CoordinateParser>();
            services.AddSingleton<IPartnerParser, PartnerParser>();
            services.AddSingleton(mockedPartnerRepository);
        }
    }
}
