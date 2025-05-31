using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

[Route("api/[controller]")]
[ApiController]
public class PetController : ControllerBase
{
    public static List<Pet> pets = new()
    {
        new Pet { Id = 1, Nome = "Oculos canino", Descricao = "Para deixar seu pet estiloso na hora da foto!", Imagem = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRy_hnVY1qmzs3VHc0srWFezCzs2SgAi0kndw&s", Valor = 15.99 },
        new Pet { Id = 2, Nome = "Oculos de gato", Descricao = "Para deixar seu pet estiloso na hora da foto!", Imagem = "https://photos.enjoei.com.br/acessorios-para-pet-oculos-de-sol-para-gato-93498935/800x800/czM6Ly9waG90b3MuZW5qb2VpLmNvbS5ici9wcm9kdWN0cy8xMTM1NTQ5OC82MDk1YmI1MGY0NzQyMDZhNmZhMzQyNGNkNjM1YjNjZS5qcGc", Valor = 15.99 }
    };

    [HttpGet]
    public ActionResult<List<Pet>> GetPets() => pets;

    [HttpGet("{id}")]
    public ActionResult<Pet> GetPet(int id)
    {
        var pet = pets.FirstOrDefault(p => p.Id == id);
        if (pet == null)
            return NotFound("Pet não encontrado");
        return Ok(pet);
    }

    [HttpPost]
    public ActionResult AddPet(Pet pet)
    {
        pet.Id = pets.Any() ? pets.Max(p => p.Id) + 1 : 1;
        pets.Add(pet);
        return CreatedAtAction(nameof(GetPet), new { id = pet.Id }, pet);
    }

    [HttpPut("{id}")]
    public IActionResult EditarPet(int id, [FromBody] Pet petAtualizado)
    {
        var pet = pets.FirstOrDefault(p => p.Id == id);
        if (pet == null)
            return NotFound("Pet não encontrado");

        pet.Nome = petAtualizado.Nome;
        pet.Descricao = petAtualizado.Descricao;
        pet.Imagem = petAtualizado.Imagem;
        pet.Valor = petAtualizado.Valor; // 🔥 Atualizando o valor

        return Ok(pet);
    }

    [HttpDelete("{id}")]
    public IActionResult ExcluirPet(int id)
    {
        var pet = pets.FirstOrDefault(p => p.Id == id);
        if (pet == null)
            return NotFound("Pet não encontrado");

        pets.Remove(pet);
        return NoContent();
    }
}
