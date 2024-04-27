using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class AnimalsController : ControllerBase
{
    IAnimalsService _animalsService;

    public AnimalsController(IAnimalsService animalsService)
    {
        _animalsService = animalsService;
    }

    [HttpGet]
    public IActionResult GetAnimals(string orderBy = "Name")
    {
        if (orderBy != "Name" && orderBy != "Description" && orderBy != "Category" && orderBy != "Area")
        {
            return BadRequest("Order allowed by: Name, Description, Category, Area");
        }

        IEnumerable<Animal> animals = _animalsService.GetAnimals(orderBy);
        return Ok(animals);
    }

    [HttpPost]
    public IActionResult CreateAnimal(Animal animal)
    {
        int affectedCount = _animalsService.CreateAnimal(animal);
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPut("{idAnimal:int}")]
    public IActionResult UpdateAnimal(int idAnimal, Animal animal)
    {
        int affectedCount = _animalsService.UpdateAnimal(animal);
        return NoContent();
    }

    [HttpDelete("{idAnimal:int}")]
    public IActionResult DeleteAnimal(int idAnimal)
    {
        int affectedCount = _animalsService.DeleteAnimal(idAnimal);
        if (affectedCount != 0)
        {
            return Ok($"Deleted: {affectedCount} animals");
        }

        return BadRequest($"ID: {idAnimal} not exists");
    }
}