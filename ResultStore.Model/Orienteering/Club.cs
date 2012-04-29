namespace ResultStore.Model.Orienteering
{
    public class Club
    {
        public virtual int? Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string ShortName { get; set; }
        public virtual string Url { get; set; }
        public virtual Association Association { get; set; }
    }
}
