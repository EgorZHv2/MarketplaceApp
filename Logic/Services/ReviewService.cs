using AutoMapper;
using Data.DTO;
using Data.Entities;
using Data.IRepositories;
using Data.Models;
using Data.Repositories;
using Logic.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Services
{
    public class ReviewService:BaseService<Review,ReviewDTO,ReviewModel,ReviewModel>,IReviewService
    {
        public ReviewService(IReviewRepository repository,IMapper mapper, ILogger<ReviewService> logger):base(repository, mapper, logger) { }
    }
}
