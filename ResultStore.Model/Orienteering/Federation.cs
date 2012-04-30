using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ResultStore.Model.Orienteering
{
    public class Federation
    {
        public virtual int? Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string ShortName { get; set; }
        public virtual string Url { get; set; }
    }
}
