using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mango.Web.ViewModels
{
    public class EditDocsRequestModel
    {
        public int DocsId { get; set; }
        public string Title { get; set; }
        public string ShortTitle { get; set; }
        public string Contents { get; set; }
    }
}
