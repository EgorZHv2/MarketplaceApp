using Dadata;
using WebAPi.Interfaces;

namespace WebAPi.Services
{
    public class INNService : IINNService
    {
        public bool CheckINN(string inn)
        {
            var token = "5a2b4c3961b70ecf5a4a07d457b4bcb8819762a8";
            var api = new SuggestClientAsync(token);
            var result = api.FindParty(inn);
            if (result.Result.suggestions.Count == 0)
            {
                return true;
                //return false;
            }
            else
            {
                return true;
            }
        }
    }
}