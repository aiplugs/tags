using System;

namespace Sample.Models
{
    public class FileViewModel
    {
        public string PreviewLink { get; set; }
        public string Name { get; set; }
        public long Size { get; set; }
        public DateTimeOffset LastModifiedAt { get; set; }
    }
}