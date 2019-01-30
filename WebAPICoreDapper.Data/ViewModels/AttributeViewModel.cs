using System;
using System.Collections.Generic;
using System.Text;

namespace WebAPICoreDapper.Data.ViewModels
{
    public class AttributeViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public int SortOrder { get; set; }

        public string BackendType { get; set; }

        public bool IsActive { get; set; }

        public bool HasOption { get; set; }

        public string Values { get; set; }
    }
}
