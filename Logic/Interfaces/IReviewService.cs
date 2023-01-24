using Data.DTO;
using Data.Entities;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Interfaces
{
    public interface IReviewService:IBaseService<Review,ReviewDTO,ReviewModel,ReviewModel>
    {
    }
}
