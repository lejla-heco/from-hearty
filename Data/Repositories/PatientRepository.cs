using Data.Repositories.Base;


namespace Data.Repositories
{
    public interface IPatientRepository
    {

    }
    public class PatientRepository : Repo<Patient>, IPatientRepository
    {
        public PatientRepository(MyContext context) : base(context)
        {
        }
    }
}
