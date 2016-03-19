using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstaSync.Models.Enums;

namespace InstaSync.Models.Models
{
    public class ContentItem
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public bool Redundand { get; set; }
        public ContentType Type { get; set; }
    }
}
