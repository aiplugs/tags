using System.Collections.Generic;

namespace Sample.Models
{
    public class FolderViewModel
    {
        public string Link { get; set; }
        public string Name { get; set; }
        public IEnumerable<FileViewModel> Files { get; set; }
        public IEnumerable<FolderViewModel> Folders { get; set; }
        public IEnumerable<(string name, string link)> Breadcrumbs { get; set; }
        public string Callback { get; set;}
    }
}