﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PienCeramic.DataAccess.Repository.IRepository;
using PienCeramic.Models;
using PienCeramic.Models.ViewModels;
using System.Security.Claims;

namespace PienCeramic.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUnitofWork _unitOfWork;
        public ShoppingCartVM ShoppingCartVM { get; set; }
        public CartController(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            ShoppingCartVM = new()
            {
                ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == userId, includeProperties: "Product")
            };

            foreach(var cart in ShoppingCartVM.ShoppingCartList)
            {
                cart.Price = GetPriceBasedOnQuantity(cart);
                ShoppingCartVM.OrderTotal += (cart.Price*cart.Count);

            }

            return View(ShoppingCartVM);
        }

        public IActionResult Plus(int cardId) 
        {
            var cartFromDb = _unitOfWork.ShoppingCart.Get(u=>u.Id == cardId);
            cartFromDb.Count += 1;
            _unitOfWork.ShoppingCart.Update(cartFromDb);
            _unitOfWork.Save();
            return RedirectToAction
                (nameof(Index));
        }
        public IActionResult Minus(int cardId)
        {
            var cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.Id == cardId);
            if(cartFromDb.Count <= 1) 
            {
                _unitOfWork.ShoppingCart.Remove(cartFromDb);
            }
            else
            {
                cartFromDb.Count -= 1;
                _unitOfWork.ShoppingCart.Update(cartFromDb);
                
            }
            _unitOfWork.Save();
            return RedirectToAction
                (nameof(Index));
        }
        public IActionResult Remove(int cardId)
        {
            var cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.Id == cardId);
            
            _unitOfWork.ShoppingCart.Remove(cartFromDb);
            _unitOfWork.Save();
            return RedirectToAction
                (nameof(Index));
        }
        public IActionResult Summary() {
        
            return View();
        }
        private double GetPriceBasedOnQuantity(ShoppingCart shoppingCart)
        {
            if(shoppingCart.Count <= 50)
            {
                return shoppingCart.Product.Price;
            }
            else
            {
                if(shoppingCart.Count <= 100)
                {
                    return shoppingCart.Product.Price50;
                }
                else
                {
                    return shoppingCart.Product.Price100;
                }
            }
        }
    }
}
