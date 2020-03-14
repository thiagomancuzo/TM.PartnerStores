// namespace TM.PartnerStores.UnitTests.Domain
// {
//     using Xunit;
//     using TM.PartnerStores.Domain.Partner.Entities;
//     using System;
//     using System.Collections.Generic;
//     using Newtonsoft.Json;
//     using System.Linq;
//     using Moq;
//     using TM.PartnerStores.IoC;
//     using Microsoft.Extensions.DependencyInjection;
//     using Microsoft.Extensions.Configuration;
//     using TM.PartnerStores.Domain.Repositories;
//     using System.Threading.Tasks;
//     using TM.PartnerStores.Domain.Services;
//     using TM.PartnerStores.Domain.Exceptions;

//     public class PartnerServiceUnitTests
//     {
//         private readonly IPartnerService _service;

//         private Partner GetValidPartner(int id = 1, string document = "30.617.984/0001-15")
//         {
//             return new Partner
//             (
//                 id,
//                 "Bar da esquina",
//                 "Donizeti",
//                 Document.NewDocument(document),
//                 new Multipolygon(JsonConvert.DeserializeObject<IEnumerable<IEnumerable<IEnumerable<IEnumerable<double>>>>>("[[[[-43.36556,-22.99669],[-43.36539,-23.01928],[-43.26583,-23.01802],[-43.25724,-23.00649],[-43.23355,-23.00127],[-43.2381,-22.99716],[-43.23866,-22.99649],[-43.24063,-22.99756],[-43.24634,-22.99736],[-43.24677,-22.99606],[-43.24067,-22.99381],[-43.24886,-22.99121],[-43.25617,-22.99456],[-43.25625,-22.99203],[-43.25346,-22.99065],[-43.29599,-22.98283],[-43.3262,-22.96481],[-43.33427,-22.96402],[-43.33616,-22.96829],[-43.342,-22.98157],[-43.34817,-22.97967],[-43.35142,-22.98062],[-43.3573,-22.98084],[-43.36522,-22.98032],[-43.36696,-22.98422],[-43.36717,-22.98855],[-43.36636,-22.99351],[-43.36556,-22.99669]]]]")),
//                 new Point(-49.1656, -25.66874)
//             );
//         }

//         public PartnerServiceUnitTests()
//         {
//             var defaultPartner = GetValidPartner();
//             var services = new ServiceCollection();

//             var mockedRepository = new Mock<IPartnerRepository>();

//             mockedRepository.Setup((r) => r.CreateAsync(It.IsAny<Partner>())).Returns(Task.CompletedTask);
//             mockedRepository.Setup((r) => r.GetAsync()).Returns(Task.FromResult(new Partner[] { GetValidPartner() }.AsEnumerable()));
//             mockedRepository.Setup((r) => r.GetAsync(It.IsIn<int>(new[] { 1 }))).Returns(Task.FromResult(GetValidPartner()));
//             mockedRepository.Setup((r) => r.GetNearstAsync(It.IsIn<Point>(new[] { defaultPartner.Address }))).Returns(Task.FromResult(GetValidPartner()));
//             mockedRepository.Setup((r) => r.GetAsync(It.IsIn<Document>(new[] { defaultPartner.Document }))).Returns(Task.FromResult(GetValidPartner()));

//             services.AddUnitTestsPartnerStoresComponents(mockedRepository.Object);

//             var provider = services.BuildServiceProvider();

//             _service = provider.GetService<IPartnerService>();
//         }

//         [Fact]
//         public async Task Create_Partner_Should_Succeed()
//         {
//             //Given
//             var differentDocumentPartner = GetValidPartner(2, "30.617.984/0001-16");

//             //When
//             await _service.CreateAsync(differentDocumentPartner);

//             //Then
//             Assert.True(true);
//         }

//         [Fact]
//         public async Task Create_Partner_With_Duplicated_Document_Should_Throws_AlreadyCreatedPartnerException()
//         {
//             //Given
//             var partner = GetValidPartner(2);

//             //When
//             var action = new Func<Task>(() => _service.CreateAsync(partner));

//             //Then
//             await Assert.ThrowsAsync<AlreadyCreatedPartnerException>(action);
//         }

//         [Fact]
//         public async Task Create_Partner_With_Duplicated_Id_Should_Throws_AlreadyCreatedPartnerException()
//         {
//             //Given
//             var partner = GetValidPartner(document: "30.617.984/0001-16");

//             //When
//             var action = new Func<Task>(() => _service.CreateAsync(partner));

//             //Then
//             await Assert.ThrowsAsync<AlreadyCreatedPartnerException>(action);
//         }

//         [Fact]
//         public async Task Partner_GetById_Should_Succeed()
//         {
//             //Given
//             int id = 1;

//             //When
//             var partner = await _service.GetByIdAsync(id);

//             //Then
//             Assert.NotNull(partner);
//         }

//         [Fact]
//         public async Task Partner_GetByInvalidId_Should_Return_Null()
//         {
//             //Given
//             int id = 2;

//             //When
//             var partner = await _service.GetByIdAsync(id);

//             //Then
//             Assert.Null(partner);
//         }

//         [Fact]
//         public async Task Partner_Search_Should_Succeed()
//         {
//             //Given
//             var point = GetValidPartner().Address;

//             //When
//             var partner = await _service.SearchAsync(point);

//             //Then
//             Assert.NotNull(partner);
//         }
//     }
// }