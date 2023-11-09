using System.Threading.Tasks;

namespace SocialMedia.Core.Interfaces;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
}