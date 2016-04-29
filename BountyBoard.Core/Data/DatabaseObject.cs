namespace BountyBoard.Core.Data
{
    public abstract class DatabaseObject
    {
        protected DatabaseObject() { }
        public int Id { get; set; }
    }
}