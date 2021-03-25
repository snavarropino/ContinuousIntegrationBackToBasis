using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Model;

namespace Api.Repositories
{
    public interface IHeroesRepository
    {
        Task<IEnumerable<Hero>> GetAllAsync();
        Task<Hero> GetByIdAsync(Guid id);
        void Update(Hero hero);
        Task SaveAsync();
        void Delete(Hero hero);
        Task AddAsync(Hero hero);
        Task<IEnumerable<Hero>> FilterByNameAsync(string name); 
    }
}