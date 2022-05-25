using CachingSystems.InMemory.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CachingSystems.InMemory.Controllers
{
    public class HomeController : Controller
    {
       
        private readonly IMemoryCache _memoryCache;

        public HomeController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public IActionResult Index()
        {
            MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions();
            cacheOptions.SetPriority(CacheItemPriority.Low);
            cacheOptions.SetSlidingExpiration(TimeSpan.FromSeconds(10));
            cacheOptions.SetAbsoluteExpiration(TimeSpan.FromMinutes(4));
            cacheOptions.SetSize(20);
           
            _memoryCache.Set<string>("name", "emrullah",cacheOptions);                        
            _memoryCache.Remove("name2");            
            ViewBag.Name = _memoryCache.Get<string>("name");            
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
