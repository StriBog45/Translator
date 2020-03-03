using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RemoteKeyValueStorage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StorageController : ControllerBase
    {
        public StorageController(IStorageRepository storageItems)
        {
            StorageItems = storageItems;
        }
        public IStorageRepository StorageItems { get; set; }

        public IEnumerable<StorageItem> GetAll()
        {
            return StorageItems.GetAll();
        }

        [HttpGet("{key}", Name = "GetStorage")]
        public IActionResult GetByKey(string key)
        {
            var item = StorageItems.Find(key);
            if (item == null)
                return NoContent();
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] StorageItem item)
        {
            if (item == null)
                return BadRequest();
            if (String.IsNullOrEmpty(item.Key) || String.IsNullOrEmpty(item.Name))
                return BadRequest();
            StorageItems.AddOrUpdate(item);
            return CreatedAtRoute("GetStorage", new { key = item.Key }, item);
        }

        [HttpDelete("{key}")]
        public IActionResult Delete(string key)
        {
            var todo = StorageItems.Find(key);
            if (todo == null)
                return NotFound();

            StorageItems.Remove(key);
            return new OkObjectResult(key);
        }
    }
}