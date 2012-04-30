using System;

namespace ResultStore.Model.Orienteering
{
    public class Event
    {
        public virtual int? Id { get; set; }
        public virtual string Name { get; set; }
        public virtual DateTime? Date { get; set; }
        public virtual string Description { get; set; }
        public virtual string Url { get; set; }
        public virtual string ClubName { get; set; }
        public virtual Club Club { get; set; }
    }
}