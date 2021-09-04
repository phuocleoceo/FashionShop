using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using back_end.DTO;
using back_end.Models;
using back_end.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace back_end.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderController : ControllerBase
	{
		private readonly IUnitOfWork _db;
		private readonly IMapper _mapper;

		public OrderController(IUnitOfWork db, IMapper mapper)
		{
			_db = db;
			_mapper = mapper;
		}

		[HttpGet("{UserId}")]
		public async Task<IActionResult> CheckOut(string UserId)
		{
			IEnumerable<ShoppingCartDTO> sc = (await _db.ShoppingCarts
				.GetAll(c => c.UserId == UserId, includeProperties: "Product"))
				.Select(c => _mapper.Map<ShoppingCartDTO>(c));

			User user = await _db.Users.GetFirstOrDefault(c => c.Id == UserId);

			var checkoutInfor = new
			{
				ListCart = sc,
				Name = user.Name,
				PhoneNumber = user.PhoneNumber,
				Address = user.Address,
				OrderTotal = sc.Sum(c => c.Total)
			};
			return Ok(checkoutInfor);
		}

		[HttpPost("{UserId}")]
		public async Task<IActionResult> CheckOutPost(string UserId)
		{
			return NotFound();
		}
	}
}