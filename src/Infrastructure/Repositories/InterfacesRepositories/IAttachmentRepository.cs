using Domain.Entities;

namespace Infrastructure.Repositories.InterfacesRepositories
{
    public interface IAttachmentRepository
    {
        Task<Attachment?> Get(int id);
        Task<int> Save(Attachment attachment);
        Task Delete(int id);
    }
}
