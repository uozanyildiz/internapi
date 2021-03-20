using internapi.Model;

namespace internapi.Repository.IRepository
{
    public interface IAuthenticationRepository
    {
        string Authenticate(string mail, string password);
        //TODO: Hocaya soralım
        //Manager RegisterManager(Manager manager);
        Student RegisterStudent(Student student);
        bool IsUnique(string mail);

        
    }
}