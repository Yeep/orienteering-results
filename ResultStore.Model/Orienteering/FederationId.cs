using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ResultStore.Model.Orienteering
{
    public class FederationId
    {
        public virtual int? Id { get; set; }
        public virtual Federation Federation { get; set;}
        public virtual string Code { get; set; }
    }
}
