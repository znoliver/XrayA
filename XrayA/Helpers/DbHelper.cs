using System;
using System.Collections;
using System.Threading.Tasks;
using SQLite;

namespace XrayA.Helpers;

public sealed class DbHelper
{
    private static readonly Lazy<DbHelper> _instance = new(() => new());
    public static DbHelper Instance => _instance.Value;

    private readonly SQLiteConnection _db;
    private readonly SQLiteAsyncConnection _dbAsync;

    public DbHelper()
    {
        var dbFilePath = Utils.Utils.GetDataFilePath(Global.DataBaseName);
        _db = new SQLiteConnection(dbFilePath);
        _dbAsync = new SQLiteAsyncConnection(dbFilePath);
    }

    public CreateTableResult CreateTable<T>() => _db.CreateTable<T>();

    public Task<int> InsertAsync<T>(T model) => _dbAsync.InsertAsync(model);

    public int InsertAll(IEnumerable models) => _db.InsertAll(models);
    
    public Task<int> InsertAllAsync(IEnumerable models) => _dbAsync.InsertAllAsync(models);

    public TableQuery<T> Table<T>() where T : new() => _db.Table<T>();

    public AsyncTableQuery<T> TableAsync<T>() where T : new() => _dbAsync.Table<T>();
}