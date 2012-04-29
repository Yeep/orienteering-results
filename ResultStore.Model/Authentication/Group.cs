using System.Collections.Generic;

namespace ResultStore.Model.Authentication
{
    public class Group
    {
        public virtual int? Id { get; set; }
        public virtual string Name { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<Right> Rights { get; set; }
    }
}
