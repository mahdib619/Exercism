using System;

public class Orm
{
    private readonly Database _database;

    public Orm(Database database) => _database = database;

    public void Write(string data)
    {
        using (_database)
        {
            _database.BeginTransaction();
            _database.Write(data);
            _database.EndTransaction();
        }
    }

    public bool WriteSafely(string data)
    {
        using (_database)
        {
            try
            {
                _database.BeginTransaction();
                _database.Write(data);
                _database.EndTransaction();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
