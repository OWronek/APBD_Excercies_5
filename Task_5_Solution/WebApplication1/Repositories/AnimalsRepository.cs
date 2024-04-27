using System.Data.SqlClient;

public class AnimalsRepository : IAnimalsRepository
{
    private IConfiguration _configuration;

    public AnimalsRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IEnumerable<Animal> GetAnimals(string orderBy = "Name")
    {
        using SqlConnection connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();

        using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = $"SELECT * FROM Animals ORDER BY {orderBy}";

        SqlDataReader dataReader = command.ExecuteReader();
        List<Animal> animals = new List<Animal>();
        while (dataReader.Read())
        {
            Animal animal = new Animal
            {
                Id = (int)dataReader["Id"],
                Name = dataReader["Name"].ToString(),
                Description = dataReader["Description"].ToString(),
                Category = dataReader["Category"].ToString(),
                Area = dataReader["Area"].ToString()
            };
            animals.Add(animal);
        }

        return animals;
    }

    public int CreateAnimal(Animal animal)
    {
        using SqlConnection connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();

        using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = $"INSERT INTO Animals (Name, Description, Category, Area) VALUES ('{animal.Name}', '{animal.Description}', '{animal.Category}', '{animal.Area}')";

        int affectedCount = command.ExecuteNonQuery();
        return affectedCount;
    }

    public int UpdateAnimal(Animal animal)
    {
        using SqlConnection connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();

        using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = $"UPDATE Animals SET Name='{animal.Name}', Description='{animal.Description}', Category='{animal.Category}', Area='{animal.Area}' WHERE Id = '{animal.Id}'";
        int affectedCount = command.ExecuteNonQuery();
        return affectedCount;
    }
    
    public int DeleteAnimal(int idAnimal)
    {
        using SqlConnection connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();

        using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = $"DELETE FROM Animals WHERE Id = {idAnimal}";
        
        int affectedCount = command.ExecuteNonQuery();
        return affectedCount;
    }
}