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
    public class FoodsController : ApiController
    {
        private ICountingKsRepository _countingKsRepository;
        private ModelFactory _modelFactory;
        public FoodsController(ICountingKsRepository countingKsRepository)
        {
            _countingKsRepository = countingKsRepository;
            _modelFactory = new ModelFactory();
        }

        public IEnumerable<FoodModel> Get()
        {
            var result = _countingKsRepository.GetAllFoodsWithMeasures()
                                                .OrderBy(x => x.Description)
                                                .Take(25)
                                                .ToList()
                                                .Select(x => _modelFactory.Create(x));

            return result;
        }

        public FoodModel Get(int id)
        {
            return _modelFactory.Create(_countingKsRepository.GetFood(id));
        }
    }
}
