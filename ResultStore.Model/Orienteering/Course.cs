namespace ResultStore.Model.Orienteering
{
    public class Course
    {
        public virtual int? Id { get; set; }
        public virtual string Name { get; set; }
        public virtual decimal Length { get; set; }
        public virtual decimal Climb { get; set; }
        public virtual Event Event { get; set; }
    }
}
