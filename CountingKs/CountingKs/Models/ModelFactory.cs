using CountingKs.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;

namespace CountingKs.Models
{
    public class ModelFactory
    {
        private UrlHelper _urlHelper;
        public ModelFactory(HttpRequestMessage request)
        {
            _urlHelper = new UrlHelper(request);
        }

        public FoodModel Create(Food food)
        {
            return new FoodModel()
            {
                Url = _urlHelper.Link("Food", new { foodid = food.Id }),
                Description = food.Description,
                Measures = food.Measures.Select(m => Create(m))
            };
        }

        public MeasureModel Create(Measure measure)
        {
            return new MeasureModel()
            {
                Url = _urlHelper.Link("FoodMeasures", new { foodid = measure.Food.Id, measureid = measure.Id }),
                Description = measure.Description,
                Calories = Math.Round(measure.Calories)
            };
        }

        public DiaryModel Create(Diary diary)
        {
            return new DiaryModel()
            {
                Url = _urlHelper.Link("Diaries", new { diaresid = diary.CurrentDate.ToString("yyyy-MM-dd") }),
                CurrentDate = diary.CurrentDate,
                DiaryEntries = diary.Entries.Select(x => Create(x))
            };
        }

        public DiaryEntryModel Create(DiaryEntry diaryEntry)
        {
            return new DiaryEntryModel()
            {
                Url = _urlHelper.Link("DiariesDetails", new { diaresid = diaryEntry.Diary.CurrentDate.ToString("yyyy-MM-dd"), detailid = diaryEntry.Id }),
                FoodDescription = diaryEntry.FoodItem.Description,
                MeasureDescription = diaryEntry.Measure.Description,
                MeasureUrl = _urlHelper.Link("FoodMeasures", new { foodid = diaryEntry.Measure.Food.Id, measureid = diaryEntry.Measure.Id }),
                Quantity = diaryEntry.Quantity
            };
        }
    }
}