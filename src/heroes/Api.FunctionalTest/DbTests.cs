using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Api.FunctionalTest
{
    [Collection(nameof(DbTestsCollection))]
    public class DbTests
    {
        private readonly ServerFixture _serverFixture;

        public DbTests(ServerFixture serverFixture)
        {
            _serverFixture = serverFixture ?? throw new ArgumentNullException(nameof(serverFixture));
        }
        
        [Fact]
        public async Task db_test_first()
        {
            _serverFixture.Given.InitialHeroes();

            var heroes = await _serverFixture.DbContext.Heroes.ToListAsync();

            heroes.Count.Should().BeGreaterThan(0);
        }
    }
}