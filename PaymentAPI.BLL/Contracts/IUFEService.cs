namespace PaymentAPI.BLL.Contracts
{
    /// <summary>
    /// Contract for UFE Service
    /// </summary>
    public interface IUFEService
    {
        public Task<decimal> GetFee();
    }
}
