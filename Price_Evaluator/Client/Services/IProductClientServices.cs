using Price_Evaluator.Shared;

namespace Price_Evaluator.Client.Services
{
    public interface IProductClientServices
    {
              event Action ProductChanged;
              List<Root> result { get; set; }
                 string Search { get; set; }
                Task <List<Root>> GetProduct();
                Task AddCart(Cart cart);
                Task<Root> GetProductbyName(string name);
               


    }
}
