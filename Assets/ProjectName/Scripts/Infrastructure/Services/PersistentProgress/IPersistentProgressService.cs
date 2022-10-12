using ProjectName.Scripts.Data;

namespace ProjectName.Scripts.Infrastructure.Services.PersistentProgress
{
    public interface IPersistentProgressService : IService
    {
        PlayerProgress Progress { get; set; }
    }
}