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
    public class MeasuresController : BaseApiController
    {
        private ICountingKsRepository _countingKsRepository;
        
        public MeasuresController(ICountingKsRepository countingKsRepository)
        {
            _countingKsRepository = countingKsRepository;
        }

        public IEnumerable<MeasureModel> Get(int foodId)
        {
            var result = _countingKsRepository.GetMeasuresForFood(foodId)
                                                .ToList()
                                                .Select(x => ModelFactory.Create(x));

            return result;
        }

        public MeasureModel Get(int foodId, int measureId)
        {
            var measure = _countingKsRepository.GetMeasure(measureId);

            if(measure.Food.Id != foodId)
            {
                return null;
            }

            return ModelFactory.Create(measure);
        }
    }
}
