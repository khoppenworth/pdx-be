using System.Collections.Generic;
using System.Threading.Tasks;
using PDX.Domain.License;

namespace PDX.BLL.Services.Interfaces
{
    public interface IMAReviewService:IService<MAReview>
    {
         Task<IEnumerable<MAReview>> GetMAReviewByMA(int maID, int userID, int? statusID);
         Task<bool> MAReviewCreateOrUpdateAsync(MAReview maReview, bool? changeStatus = false);
    }
}