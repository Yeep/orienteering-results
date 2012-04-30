namespace ResultStore.Model.Orienteering
{
    public class Association
    {
        public virtual int? Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string ShortName { get; set; }
        public virtual string Url { get; set; }
        public virtual string FederationName { get; set; }
        public virtual Federation Federation { get; set; }
    }
}
