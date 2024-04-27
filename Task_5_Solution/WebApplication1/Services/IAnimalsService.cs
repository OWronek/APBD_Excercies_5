public interface IAnimalsService
{
    IEnumerable<Animal> GetAnimals(string orderBy = "Name");
    int CreateAnimal(Animal animal);
    int UpdateAnimal(Animal animal);
    int DeleteAnimal(int idAnimal);
}