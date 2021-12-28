using Microsoft.AspNetCore.Authorization;
using back_end.Repository.Interface;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using back_end.Extension;
using back_end.Models;
using back_end.DTO;
using System.Linq;
using AutoMapper;
using System;

namespace back_end.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
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
    public async Task<CheckOutInfor> CheckOut(string UserId)
    {
        IEnumerable<ShoppingCartDTO> cart = (await _db.ShoppingCarts
            .GetAll(c => c.UserId == UserId, includeProperties: "Product"))
            .Select(c => _mapper.Map<ShoppingCartDTO>(c));

        User user = await _db.Users.GetFirstOrDefault(c => c.Id == UserId);

        CheckOutInfor checkoutInfor = new CheckOutInfor
        {
            ListCart = cart,
            Name = user.Name,
            PhoneNumber = user.PhoneNumber,
            Address = user.Address,
            OrderTotal = cart.Sum(c => c.Total)
        };
        return checkoutInfor;
    }

    [HttpPost("{UserId}")]
    public async Task<IActionResult> CheckOutPost(string UserId, string stripeToken,
                                                     CheckOutAlterInfor checkOutAlterInfor)
    {
        OrderHeader orderHeader = new OrderHeader
        {
            UserId = UserId,
            Name = checkOutAlterInfor.Name,
            PhoneNumber = checkOutAlterInfor.PhoneNumber,
            Address = checkOutAlterInfor.Address,
            PaymentStatus = ConstantValue.PaymentStatusPending,
            OrderStatus = ConstantValue.OrderStatusPending,
            OrderDate = DateTime.Now
        };
        await _db.OrderHeaders.Add(orderHeader);
        await _db.SaveChanges();


        IEnumerable<ShoppingCart> cart = await _db.ShoppingCarts
                        .GetAll(c => c.UserId == UserId, includeProperties: "Product");

        foreach (ShoppingCart sc in cart)
        {
            OrderDetail orderDetail = new OrderDetail
            {
                ProductId = sc.ProductId,
                OrderHeaderId = orderHeader.Id,  //orderHeader has Id after SaveChanges
                Price = sc.Product.Price,
                Count = sc.Count
            };
            orderHeader.OrderTotal += orderDetail.Count * orderDetail.Price; //update orderHeader Total
            await _db.OrderDetails.Add(orderDetail);
            await _db.ShoppingCarts.Remove(sc.Id);
        }
        await _db.SaveChanges();

        // Stripe
        if (stripeToken == null)
        {
            // Delay
            orderHeader.PaymentDueDate = DateTime.Now.AddDays(30);
            orderHeader.PaymentStatus = ConstantValue.PaymentStatusDelayedPayment;
            orderHeader.OrderStatus = ConstantValue.OrderStatusApproved;
        }
        else
        {
            // TODO
        }
        await _db.SaveChanges();

        return Ok(orderHeader.Id);
    }
}