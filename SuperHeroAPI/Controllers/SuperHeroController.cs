using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        
        private readonly DataContext dataContext;

        public SuperHeroController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            return Ok(await dataContext.SuperHeroes.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> Get(int id)
        {
            var hero = await dataContext.SuperHeroes.FindAsync(id);
            if (hero == null)
                return BadRequest("Hero not found. ");
            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
           dataContext.SuperHeroes.Add(hero);
            await dataContext.SaveChangesAsync();
            return Ok(await dataContext.SuperHeroes.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero request)
        {
            var hero = await dataContext.SuperHeroes.FindAsync(request.Id);
            if (hero == null)
                return BadRequest("Hero not found. ");

            hero.Name = request.Name;
            hero.FirstName = request.FirstName; 
            hero.LastName = request.LastName;   
            hero.Place = request.Place;

            await dataContext.SaveChangesAsync();

            return Ok(await dataContext.SuperHeroes.ToListAsync());
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> Delete(int id)
        {
            var hero = await dataContext.SuperHeroes.FindAsync(id);
            if (hero == null)
                return BadRequest("Hero not found. ");

            dataContext.SuperHeroes.Remove(hero);
            await dataContext.SaveChangesAsync();
            return Ok(await dataContext.SuperHeroes.ToListAsync());
        }
    }

    }
