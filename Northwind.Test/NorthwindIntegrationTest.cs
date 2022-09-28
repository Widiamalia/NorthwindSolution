using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Northwind.Domain.Base;
using Northwind.Domain.Models;
using Northwind.Persistence;
using Northwind.Persistence.Base;
using Northwind.Services;
using Northwind.Services.Abstraction;
using Northwind.Test.Mapping;
using Northwind.Contracts.Dto.Category;
using Shouldly;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Northwind.Test
{
    public class NorthwindIntegrationTest
    {
        private static IConfigurationRoot Configuration;
        private static DbContextOptionsBuilder<NorthwindContext> optionsBuilder;
        private static MapperConfiguration mapperConfig;
        private static IMapper mapper;
        private static IServiceProvider serviceProvider;
        private static IRepositoryManager _repositoryManager;

        public NorthwindIntegrationTest()
        {
            BuildConfiguration();
            SetupOptions();
        }


        [Fact]
        public void TestCreateCategoryService()
        {
            using (var context = new NorthwindContext(optionsBuilder.Options))
            {
                _repositoryManager = new RepositoryManager(context);
                IServiceManager serviceManager = new ServiceManager(_repositoryManager, mapper);

                var categoryForCreate = new CategoryForCreateDto
                {
                    CategoryName = "Toys",
                    Description = "Mainan anak"
                };
                serviceManager.CategoryService.Insert(categoryForCreate);

                //assert
                var category = serviceManager.CategoryService.GetAllCategory(false);
                category.ShouldNotBeNull();
                category.Result.Count().ShouldBe(13);

            }
        }

        [Fact]
        public void TestGetCategoryService()
        {
            using (var context = new NorthwindContext(optionsBuilder.Options))
            {
                _repositoryManager = new RepositoryManager(context);
                IServiceManager serviceManager = new ServiceManager(_repositoryManager, mapper);
                var category = serviceManager.CategoryService.GetAllCategory(false);

                //assert
                category.ShouldNotBeNull();
                category.Result.Count().ShouldBe(8);
            }
        }

        [Fact]
        public void TestCreateCategoryRepo()
        {
            using (var context = new NorthwindContext(optionsBuilder.Options))
            {
                _repositoryManager = new RepositoryManager(context);
                //define model category
                var categoryModel = new Category
                {
                    CategoryName = "Movie",
                    Description = "Movie entertaintment"
                };

                _repositoryManager.CategoryRepository.Insert(categoryModel);
                _repositoryManager.Save();
                categoryModel.CategoryId.ShouldBeEquivalentTo(23);

                //assert
                var category = _repositoryManager.CategoryRepository.GetAllCategory(false);
                category.ShouldNotBeNull();
                category.Result.Count().ShouldBe(12);
            }
        }

        [Fact]
        public void TestGetCategoryRepo()
        {
            using (var context = new NorthwindContext(optionsBuilder.Options))
            {
                //act
                _repositoryManager = new RepositoryManager(context);
                var category = _repositoryManager.CategoryRepository.GetAllCategory(false);

                //assert
                category.ShouldNotBeNull();
                category.Result.Count().ShouldBe(8);
            }
        }

        private void BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                     .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            Configuration = builder.Build();
        }

        private void SetupOptions()
        {
            optionsBuilder = new DbContextOptionsBuilder<NorthwindContext>();
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("NorthwindDb"));

            var services = new ServiceCollection();
            services.AddAutoMapper(typeof(MappingProfile));
            serviceProvider = services.BuildServiceProvider();

            mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            mapperConfig.AssertConfigurationIsValid();
            mapper = mapperConfig.CreateMapper();
        }
    }
}