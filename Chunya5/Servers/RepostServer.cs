using Chunya5.Data;
using Chunya5.Models;

namespace Chunya5.Servers
{
    public class RepostServer
    {
        private readonly MyDbContext _context;

        public RepostServer(MyDbContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Positions GetFirstPositions(string account)
        {
            return new Positions();    
        }
        public Positions GetLastPositions()
        {
            return new Positions();
        }
        //public Statement CalculateReport(Positions firstPositions,Positions lastPositions)
        //{

        //}

    }
}
