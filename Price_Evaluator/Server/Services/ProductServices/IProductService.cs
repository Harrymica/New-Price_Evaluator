namespace Price_Evaluator.Server.Services.ProductServices
{
    public interface IProductService
    {
        Task<List<Root>> GetProduct(string search);
        List<Root> result { get; set; }
        string Search { get; set; }
        Task AddCart(Cart cart);
        Task <List<Cart>> GetCart();
        Task<Root> GetProductbyName(string name);
    }
}
