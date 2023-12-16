using FileInfo = Safit.Core.Domain.Service.Entities.FileInfo;

namespace Safit.Core.Domain.Service;

public interface IFileService
{
    public Task<ReadOnlyMemory<byte>> GetAsync(string name, CancellationToken ct = default);
    public Task<FileInfo> GetFileInfoAsync(string name, CancellationToken ct = default);
    public Task<FileInfo> UploadFileAsync(ReadOnlySpan<byte> data, CancellationToken ct = default);
}
