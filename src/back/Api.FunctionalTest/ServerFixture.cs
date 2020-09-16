using System;
using Api.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Api.FunctionalTest
{
    public class ServerFixture
    {
        public HeroesContext DbContext { get; private set; }
        public Given Given { get; private set; }

        public ServerFixture()
        {
            InitializeTestServer();

            Given = new Given(this);
        }

        private void InitializeTestServer()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", false, true)
                .AddEnvironmentVariables()
                .Build();

            var connectionString = config["ConnectionStrings:heroesConnection"];
            var optionsBuilder = new DbContextOptionsBuilder<HeroesContext>();
            optionsBuilder.UseSqlServer(connectionString);

            DbContext = new HeroesContext(optionsBuilder.Options);
            DbContext.Database.Migrate();
        }
    }

    [CollectionDefinition(nameof(DbTestsCollection))]
    public class DbTestsCollection
        : ICollectionFixture<ServerFixture>
    {
    }
}