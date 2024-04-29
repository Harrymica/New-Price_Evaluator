namespace Price_Evaluator.Server.Services
{
    public interface IRegisterServices
    {
        Task<UserModel> Login(Login _login);
        Task Register(UserModel user);
    }
}
