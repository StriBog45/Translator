using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RemoteKeyValueStorage
{
    public class StorageRepository : IStorageRepository
    {
        private static ConcurrentDictionary<string, StorageItem> storageDictionary = new ConcurrentDictionary<string, StorageItem>();

        public StorageRepository()
        {
            AddOrUpdate(new StorageItem { Key = Guid.NewGuid().ToString(), Name = "TAXTELECOM LLC" });
        }

        public IEnumerable<StorageItem> GetAll()
        {
            return storageDictionary.Values;
        }

        public void AddOrUpdate(StorageItem item)
        {
            storageDictionary[item.Key] = item;
        }

        public StorageItem Find(string key)
        {
            StorageItem item;
            storageDictionary.TryGetValue(key, out item);
            return item;
        }

        public StorageItem Remove(string key)
        {
            StorageItem item;
            storageDictionary.TryRemove(key, out item);
            return item;
        }
    }
}
