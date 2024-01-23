using Data.Repositories.Base;


namespace Data.Repositories
{
    public interface IFindingRepository
    {

    }
    public class FindingRepository : Repo<Finding>, IFindingRepository
    {
        public FindingRepository(MyContext context) : base(context)
        {
        }
    }
}

