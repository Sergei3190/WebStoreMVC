namespace WebStoreMVC.Interfaces.TestApi;

public interface IValueService
{
    IEnumerable<string> GetValues();

    string? GetById(int id);

    void Add(string value); 

    void Edit(int id, string value); 

    bool Delete(int id);    
}
