using System.Collections.Generic;

namespace ResultStore.Model.Authentication
{
    public class Right
    {
        public virtual string Name { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
    }
}
