using Microsoft.Extensions.Configuration;
using Safit.Core.Domain.Service;
using StackExchange.Redis;

namespace Safit.Core.Services.Content;

public class FileService : IFileService
{
    private static readonly long maxFileSize = 536870912;
    private static ConnectionMultiplexer? redisConnection = null;

    private IDatabase redisDatabase;

    public FileService(IConfiguration configuration)
    {
        if (redisDatabase == null)
        {
            redisConnection = ConnectionMultiplexer.Connect(configuration["Safit:Redis:ConnectionString"]);
            redisDatabase = redisConnection.GetDatabase();
        }
    }
    public async Task<ReadOnlyMemory<byte>> GetAsync(string name, CancellationToken ct = default)
    {
        ReadOnlyMemory<byte> objectBlob = await redisDatabase.StringGetAsync($"{name}_blob");
        if (objectBlob.IsEmpty) throw new ArgumentException("no object with such name");
        return objectBlob;
    }

    public async Task<Domain.Service.Entities.FileInfo> GetFileInfoAsync(string name, CancellationToken ct = default)
    {
        var objectInfo = await redisDatabase.StringGetAsync($"{name}_type");
        if (objectInfo.HasValue == false) throw new ArgumentException("no object with such name");
        var objectInfoData = objectInfo.ToString().Split(":");
        var fileInfo = new Domain.Service.Entities.FileInfo()
        {
            Name = name,
            Type = objectInfoData[0],
            Size = Convert.ToInt64(objectInfoData[1])
        };
        return fileInfo;
    }

    public async Task<Domain.Service.Entities.FileInfo> UploadFileAsync(ReadOnlyMemory<byte> data, string type, CancellationToken ct = default)
    {
        if (data.Length > maxFileSize) throw new ArgumentException("file size is bigger than 512MB");
        var fileInfo = new Domain.Service.Entities.FileInfo()
        {
            Type = type,
            Name = Ulid.NewUlid().ToString(),
            Size = data.Length
        };
        if (await redisDatabase.StringSetAsync($"{fileInfo.Name}_blob", data) == false) throw new ApplicationException("unable to upload file blob");
        if (await redisDatabase.StringSetAsync($"{fileInfo.Name}_type", $"{type}:{fileInfo.Size}") == false) throw new ApplicationException("unable to upload file type");
        return fileInfo;
    }
}
