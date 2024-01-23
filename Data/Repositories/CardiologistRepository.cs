using Data.Repositories.Base;


namespace Data.Repositories
{
    public interface ICardiologistRepository
    {

    }
    public class CardiologistRepository : Repo<Cardiologist>, ICardiologistRepository
    {
        public CardiologistRepository(MyContext context) : base(context)
        {
        }
    }
}
