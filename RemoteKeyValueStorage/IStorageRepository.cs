using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RemoteKeyValueStorage
{
    public interface IStorageRepository
    {
        void AddOrUpdate(StorageItem item);
        IEnumerable<StorageItem> GetAll();
        StorageItem Find(string key);
        StorageItem Remove(string key);
    }
}
