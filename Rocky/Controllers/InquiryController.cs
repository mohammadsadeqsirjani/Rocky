using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rocky.Application.Utilities;
using Rocky.Application.ViewModels;
using Rocky.Application.ViewModels.Dtos.InquiryDetail;
using Rocky.Application.ViewModels.Dtos.InquiryHeader;
using Rocky.Domain.Entities;
using Rocky.Domain.Interfaces.InquiryDetail;
using Rocky.Domain.Interfaces.InquiryHeader;
using System.Collections.Generic;
using System.Linq;

namespace Rocky.Controllers
{
    [Authorize(Roles = WebConstant.AdminRole)]
    public class InquiryController : Controller
    {
        private readonly IInquiryHeaderRepository _inquiryHeaderRepository;
        private readonly IInquiryDetailRepository _inquiryDetailRepository;
        private readonly IMapper _mapper;

        [BindProperty] public InquiryVm InquiryVm { get; set; }

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

        public IActionResult Details(int id)
        {
            var inquiryHeader = _inquiryHeaderRepository.FirstOrDefault(h => h.Id == id);
            var inquiryDetails = _inquiryDetailRepository.Select(d => d.InquiryHeaderId == id, d => d.Product);

            if (inquiryHeader == null)
            {
                TempData[WebConstant.Failed] = WebConstant.MissionFail;
                return NotFound();
            }

            var inquiryHeaderDto = _mapper.Map<InquiryHeaderGetDto>(inquiryHeader);
            var inquiryDetailsDto = _mapper.Map<List<InquiryDetailGetDto>>(inquiryDetails);

            InquiryVm = new InquiryVm
            {
                InquiryHeader = inquiryHeaderDto,
                InquiryDetails = inquiryDetailsDto
            };

            return View(InquiryVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details()
        {
            var inquiryDetails = _inquiryDetailRepository.Select(f => f.InquiryHeaderId == InquiryVm.InquiryHeader.Id);

            var inquiryDetailsDto = _mapper.Map<IEnumerable<InquiryDetailGetDto>>(inquiryDetails);

            InquiryVm.InquiryDetails = inquiryDetailsDto;

            var shoppingCarts = InquiryVm.InquiryDetails.Select(detail => new ShoppingCart { ProductId = detail.ProductId }).ToList();

            HttpContext.Session.Clear();
            HttpContext.Session.Set(WebConstant.SessionCart, shoppingCarts);
            HttpContext.Session.Set(WebConstant.SessionInquiryId, InquiryVm.InquiryHeader.Id);

            TempData[WebConstant.Succeed] = WebConstant.MissionComplete;

            return RedirectToAction(nameof(Index), "Cart");
        }

        [HttpPost]
        public IActionResult Delete()
        {
            var inquiryHeader = _inquiryHeaderRepository.FirstOrDefault(f => f.Id == InquiryVm.InquiryHeader.Id);

            if (inquiryHeader.IsNull())
            {
                TempData[WebConstant.Failed] = WebConstant.MissionFail;
                return NotFound();
            }

            _inquiryHeaderRepository.Delete(InquiryVm.InquiryHeader.Id);
            _inquiryDetailRepository.Delete(f => f.InquiryHeaderId == InquiryVm.InquiryHeader.Id);

            HttpContext.Session.Clear();

            return RedirectToAction(nameof(Index));
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
