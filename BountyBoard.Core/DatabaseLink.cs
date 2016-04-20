namespace BountyBoard.Core
{
    public abstract class DatabaseLink
    {
        private readonly IDatabaseContext _context;
        protected IDatabaseContext Context { get { return _context; } }
        protected DatabaseLink(IDatabaseContext context)
        {
            _context = context;
            //set the database item in here
        }
    }
}