﻿using Data;
using Data.Entities;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    
    public class RepositoryTests
    {
        
        private PostgreRepositoryWrapper repositoryWrapper;

        public RepositoryTests() 
        {
            SeedData();
        }
        private void SeedData()
        {
            var services = new ServiceCollection();
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql("Host = localhost;Port = 5432; Database = MarketplaceAppDb; UserId = postgres; Password = 1385620;");
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
            var serviceprovider = services.BuildServiceProvider();
            var context = serviceprovider.GetRequiredService<ApplicationDbContext>();
            repositoryWrapper = new PostgreRepositoryWrapper(context);
        }

        [Fact]
        public void GetUsersTest()
        {
            var result = repositoryWrapper.Users.GetAll();

            Assert.NotNull(result);
            Assert.True(result.Any());
          
        }
        [Fact]
        public void GetReviwByNotValidId()
        {
            var result = repositoryWrapper.Reviews.GetById(Guid.Empty);

            Assert.Null(result);
        
        }
    }
}
