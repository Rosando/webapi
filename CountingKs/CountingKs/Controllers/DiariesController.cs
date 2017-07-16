using CountingKs.Data;
using CountingKs.Models;
using CountingKs.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;

namespace CountingKs.Controllers
{
    public class DiariesController : BaseApiController
    {
        private ICountingKsRepository _countingKsRespository;
        private ICountingKsIdentityService _countingKsIdentityService;

        public DiariesController(ICountingKsRepository countingKsRepository, ICountingKsIdentityService countingKsIdentityService)
        {
            _countingKsRespository = countingKsRepository;
            _countingKsIdentityService = countingKsIdentityService;
        }

        public IEnumerable<DiaryModel> Get()
        {
            //var username = Thread.CurrentPrincipal.Identity.Name;
            var username = _countingKsIdentityService.CurrentUser;
            var results = _countingKsRespository.GetDiaries(username)
                                                .OrderByDescending(d => d.CurrentDate)
                                                .Take(10)
                                                .ToList()
                                                .Select(d => ModelFactory.Create(d));
            return results;
        }

        public HttpResponseMessage Get(DateTime diaresid)
        {
            var username = _countingKsIdentityService.CurrentUser;
            var result = _countingKsRespository.GetDiary(username, diaresid);

            if(result == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, ModelFactory.Create(result));
        }
    }
}
