namespace PaymentAPI.BLL.Contracts
{
    /// <summary>
    /// Contract for User Service
    /// </summary>
    public interface IUserService
    {
        public Task<bool> Authenticate(string username, string password);
        public Task<List<string>> GetUserNames();
    }
}
