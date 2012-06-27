using System.Collections.Generic;
namespace ResultStore.Model.Orienteering
{
    public class Person
    {
        public virtual int? Id { get; set; }

        public virtual IList<Club> Clubs { get; set; }
        public virtual IList<string> Names { get; set; }
        public virtual IList<FederationId> NationalIds { get; set; }

        public virtual IList<ECard> ECards { get; set; }
    }
}
