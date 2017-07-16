using CountingKs.Data;
using CountingKs.Models;
using CountingKs.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CountingKs.Controllers
{
    public class DiaryEntriesController : BaseApiController
    {
        private ICountingKsRepository _countingKsRepository;
        private ICountingKsIdentityService _countingKsIdentityService;

        public DiaryEntriesController(ICountingKsRepository countingKsRepository, ICountingKsIdentityService countingKsIdentityService)
        {
            _countingKsRepository = countingKsRepository;
            _countingKsIdentityService = countingKsIdentityService;
        }

        public IEnumerable<DiaryEntryModel> Get(DateTime diaryId)
        {
            var results = _countingKsRepository.GetDiaryEntries(_countingKsIdentityService.CurrentUser, diaryId)
                                                .ToList()
                                                .Select(d => ModelFactory.Create(d));

            return results;
        }

        public HttpResponseMessage Get(DateTime diaryId, int id)
        {
            var result = _countingKsRepository.GetDiaryEntry(_countingKsIdentityService.CurrentUser, diaryId, id);

            if(result == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, ModelFactory.Create(result));
        }
    }
}
