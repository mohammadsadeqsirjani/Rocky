using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Rocky.Application.ViewModels.Dtos.InquiryHeader;
using Rocky.Domain.Interfaces.InquiryDetail;
using Rocky.Domain.Interfaces.InquiryHeader;

namespace Rocky.Controllers
{
    public class InquiryController : Controller
    {
        private readonly IInquiryHeaderRepository _inquiryHeaderRepository;
        private readonly IInquiryDetailRepository _inquiryDetailRepository;
        private readonly IMapper _mapper;

        public InquiryController(IInquiryHeaderRepository inquiryHeaderRepository, IInquiryDetailRepository inquiryDetailRepository, IMapper mapper)
        {
            _inquiryHeaderRepository = inquiryHeaderRepository;
            _inquiryDetailRepository = inquiryDetailRepository;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region API Calls

        [HttpGet]
        public IActionResult GetInquiries()
        {
            var inquiries = _inquiryHeaderRepository.Select();

            var inquiriesDto = _mapper.Map<IEnumerable<InquiryHeaderGetDto>>(inquiries);

            return Ok(new { Data = inquiriesDto });
        }

        #endregion
    }
}
