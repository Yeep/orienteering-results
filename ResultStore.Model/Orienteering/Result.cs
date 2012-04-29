using System;

namespace ResultStore.Model.Orienteering
{
    public class Result
    {
        public virtual int? Id { get; set; }
        public virtual int? Position { get; set; }
        public virtual string Name { get; set; }
        public virtual TimeSpan? Time { get; set; }
        public virtual string Code {get;set;}
        public virtual Course Course { get; set; }
        public virtual string ClubName { get; set; }
        public virtual string Age { get; set; }
        public virtual string Class { get; set; }

        public virtual Person Person { get; set; }
        public virtual Club Club { get; set; }
    }
}
