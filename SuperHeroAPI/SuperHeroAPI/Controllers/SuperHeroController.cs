using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SuperHeroAPI.Controllers;


[Route("api/[controller]")]
[ApiController]
public class SuperHeroController : ControllerBase
{
    private readonly DataContext _context;

    public SuperHeroController(DataContext context)
    {
        _context = context;
    }


    /// <summary>
    /// pega todos os registros disponiveis no banco de dados
    /// </summary>
    /// <returns>Retorna a lista de heróis no banco de dados</returns>
    [HttpGet]
    public async Task<ActionResult<List<SuperHero>>> GetAll() => Ok(await _context.SuperHeroes.ToListAsync());


    /// <summary>
    /// Procura um herói no banco de dados através do id
    /// </summary>
    /// <param name="id">id do registro que deve ser procurado</param>
    /// <returns>se encontrou o mesmo ID retorna o JSON do herói senão retorna uma badRequest</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<SuperHero>> GetById(int id)
    {
        var selectedHero = await _context.SuperHeroes.FindAsync(id);

        if (selectedHero == null)
            return BadRequest();


        return Ok(selectedHero);
    }


    /// <summary>
    /// Adiciona um novo herói ao banco de dados
    /// </summary>
    /// <param name="hero">Herói que deve ser adicionado ao banco de dados</param>
    /// <returns>Retorna a lista de heróis no banco de dados</returns>
    [HttpPost]
    public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
    {
        _context.SuperHeroes.Add(hero);
        await _context.SaveChangesAsync(); 
        return await GetAll();
    }


    /// <summary>
    /// Atualiza os dados de algum héroi existente no banco de dados através do ID
    /// </summary>
    /// <param name="hero">Herói com o mesmo id e os dados que devem ser alterados</param>
    /// <returns>Retorna a lista de heróis no banco de dados</returns>
    [HttpPut]
    public async Task<ActionResult<List<SuperHero>>> updateHero(SuperHero hero)
    {
        var selectedHero = await _context.SuperHeroes.FindAsync(hero.Id);

        if (selectedHero == null)
            return BadRequest("SuperHeroi não existe");

        selectedHero.Name = hero.Name;
        selectedHero.FirstName = hero.FirstName;
        selectedHero.LastName = hero.LastName;
        selectedHero.City = hero.City;

        await _context.SaveChangesAsync();

        return await GetAll();
    }


    /// <summary>
    /// Exclui um herói a partir do ID
    /// </summary>
    /// <param name="id">Id do herói que deve ser excluido</param>
    /// <returns>Retorna a lista de heróis no banco de dados</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<List<SuperHero>>> DeleteHero(int id)
    {
        var selectedHero = await _context.SuperHeroes.FindAsync(id);
        if (selectedHero == null)
            return BadRequest("SuperHeroi não existe");

        _context.SuperHeroes.Remove(selectedHero);
        await _context.SaveChangesAsync();

        return await GetAll();
    }

}

