using Data.Repositories.Base;


namespace Data.Repositories
{
    public interface IHouseDoctorRepository
    {

    }
    public class HouseDoctorRepository : Repo<HouseDoctor>, IHouseDoctorRepository
    {
        public HouseDoctorRepository(MyContext context) : base(context)
        {
        }
    }
}

