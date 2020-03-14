namespace TM.PartnerStores.WebAPI
{
    using System;
    using System.Buffers;
    using System.Buffers.Text;
    using System.IO;
    using System.Linq;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using TM.PartnerStores.Application.Contracts;
    using TM.PartnerStores.Application.Partner.Models.PartnerList;
    using TM.PartnerStores.IoC;
    using TM.PartnerStores.Repository.MongoDB.Migration;
    using Microsoft.OpenApi.Models;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.AddControllers().AddJsonOptions(c =>
            {
                c.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            });

            services.AddPartnerStoresComponents(Configuration);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Partner Stores",
                    Description = "Partner Stores Web API",
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "PartnerStores API V1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            serviceProvider.GetService<DatabaseCreator>().CreateIfNotExistsAsync(async () => await StoreInitialLoad(serviceProvider.GetService<IPartnerApplicationService>())).GetAwaiter().GetResult();
        }

        private async Task StoreInitialLoad(IPartnerApplicationService partnerApplicationService)
        {
            var data = File.ReadAllText("pdvs.json");
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            options.Converters.Add(new IntToStringConverter());

            var dataObject = System.Text.Json.JsonSerializer.Deserialize<PartnerListOutput>(data, options);

            foreach (var item in dataObject.Pdvs)
            {
                var result = await partnerApplicationService.CreateAsync(new Application.Partner.Models.PartnerCreation.PartnerCreationInput
                {
                    Id = item.Id,
                    Address = item.Address,
                    CoverageArea = item.CoverageArea,
                    Document = item.Document,
                    OwnerName = item.OwnerName,
                    TradingName = item.TradingName
                });

                if (!result.Successful)
                {
                    var descriptor = result as IErrorDescriptor;
                    throw new Exception("Error while inserting initial data.", descriptor.ExceptionOutput.Exception);
                }
            }
        }

        public class IntToStringConverter : JsonConverter<int>
        {
            public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                if (reader.TokenType == JsonTokenType.String)
                {
                    ReadOnlySpan<byte> span = reader.HasValueSequence ? reader.ValueSequence.ToArray() : reader.ValueSpan;
                    if (Utf8Parser.TryParse(span, out int number, out int bytesConsumed) && span.Length == bytesConsumed)
                    {
                        return number;
                    }

                    if (int.TryParse(reader.GetString(), out number))
                    {
                        return number;
                    }
                }

                return reader.GetInt32();
            }

            public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
            {
                writer.WriteStringValue(value.ToString());
            }
        }
    }
}
