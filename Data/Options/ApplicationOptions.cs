using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Options
{
    public class ApplicationOptions
    {
        public string DefaultAdminEmail { get; set; }
        public string DefaultAdminPassword { get; set; }
        public string BaseImagePath { get; set; }
        public List<string> AllowedImageExtensions { get; set; } = new List<string>();
        public int MaxCategoryTier { get; set; }
        public string JwtAuthKey { get; set; }
    }
}
