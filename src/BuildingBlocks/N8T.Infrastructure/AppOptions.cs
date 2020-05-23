using System.Collections.Generic;

namespace N8T.Infrastructure
{
    public class AppOptions
    {
        public NoTyeOptions NoTye { get; set; } = new NoTyeOptions();
    }

    public class NoTyeOptions
    {
        public bool Enabled { get; set; } = false;
    }
}
