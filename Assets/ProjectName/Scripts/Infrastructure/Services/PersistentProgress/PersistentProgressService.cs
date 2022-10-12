using ProjectName.Scripts.Data;

namespace ProjectName.Scripts.Infrastructure.Services.PersistentProgress
{
    public class PersistentProgressService : IPersistentProgressService
    {
        public PlayerProgress Progress { get; set; }
    }
}