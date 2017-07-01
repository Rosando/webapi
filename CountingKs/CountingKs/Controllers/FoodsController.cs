using CountingKs.Data;
using CountingKs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CountingKs.Controllers
{
    public class FoodsController : BaseApiController
    {
        private ICountingKsRepository _countingKsRepository;

        public FoodsController(ICountingKsRepository countingKsRepository)
        {
            _countingKsRepository = countingKsRepository;
        }

        public IEnumerable<FoodModel> Get()
        {
            var result = _countingKsRepository.GetAllFoodsWithMeasures()
                                                .OrderBy(x => x.Description)
                                                .Take(25)
                                                .ToList()
                                                .Select(x => ModelFactory.Create(x));

            return result;
        }

        public FoodModel Get(int foodid)
        {
            return ModelFactory.Create(_countingKsRepository.GetFood(foodid));
        }
    }
}
