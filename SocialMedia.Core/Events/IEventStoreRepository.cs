using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMedia.Core.Events;

public interface IEventStoreRepository
{
    Task InsertManyAsync(IEnumerable<EventStore> eventStores);
}