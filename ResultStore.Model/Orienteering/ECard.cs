using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ResultStore.Model.Orienteering
{
    public abstract class ECard
    {
        public virtual int? Id { get; set; }
        public virtual string Code { get; set; }
    }
}
