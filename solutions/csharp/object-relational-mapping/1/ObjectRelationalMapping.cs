using System;

public class Orm : IDisposable
{
    private readonly Database _database;

    public Orm(Database database)
    {
        _database = database;
    }

    public void Begin() => _database.BeginTransaction();

    public void Write(string data)
    {
        try
        {
            _database.Write(data);
        }
        catch (InvalidOperationException)
        {
            _database.Dispose();
        }
    }

    public void Commit()
    {
        using (_database)
        {
            try
            {
                _database.EndTransaction();
            }
            catch (InvalidOperationException)
            {
            }
        }
    }

    public void Dispose() => _database?.Dispose();
}
