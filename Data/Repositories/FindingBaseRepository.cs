using Data.Repositories.Base;


namespace Data.Repositories
{
    public interface IFindingBaseRepository
    {

    }
    public class FindingBaseRepository : Repo<FindingBase>, IFindingBaseRepository
    {
        public FindingBaseRepository(MyContext context) : base(context)
        {
        }
    }
}

