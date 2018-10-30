using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Sample.Models;

namespace Sample.Controllers
{
    public class HomeController : Controller
    {
        private IHostingEnvironment _env;
        public HomeController(IHostingEnvironment env)
        {
            _env = env;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("/files/")]
        public IActionResult Files([FromRoute]string path)
        {
            return View(GetFolder("/files/", path));
        }

        [HttpGet("/modal/file/{*path}")]
        public IActionResult FileModal([FromRoute]string path, [FromQuery]string callback)
        {
            return View(GetFolder("/modal/file/", path, callback));
        }

        [HttpGet("/preview")]
        public IActionResult Preview(bool naked = false)
        {
            if (naked)
                return View("NakedPreview");

            return View();
        }

        public FolderViewModel GetFolder(string prefix, string path, string callback = "")
        {
            if (path == null)
                path = string.Empty;

            var local = Path.Combine(_env.WebRootPath, path);
            var self = new DirectoryInfo(local);
            var dirs = Directory.GetDirectories(local).Select(d => new DirectoryInfo(d));
            var files = Directory.GetFiles(local).Select(f => new FileInfo(f));
            var names = path.Split("/").Where(_ => !string.IsNullOrEmpty(_));

            return new FolderViewModel
            {
                Callback = callback,
                Link = Path.Combine(prefix, path) + $"?callback={callback}",
                Name = string.IsNullOrEmpty(path) ? "Home" : self.Name,
                Files = files.Select(f=>  new FileViewModel
                {
                    PreviewLink = "/preview",
                    Name = f.Name,
                    Size = f.Length,
                    LastModifiedAt = new DateTimeOffset(f.LastWriteTimeUtc, TimeSpan.FromTicks(0))
                }),
                Folders = dirs.Select(d => new FolderViewModel
                {
                    Link = Path.Combine(prefix, path, d.Name) + $"?callback={callback}",
                    Name = d.Name,
                }),
                Breadcrumbs = new []
                {
                    ("Home", prefix + $"?callback={callback}")
                }.Concat(names.Select((n, i) => (n, Path.Combine(prefix, string.Join("/", names.Take(i + 1))))))
            };
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
