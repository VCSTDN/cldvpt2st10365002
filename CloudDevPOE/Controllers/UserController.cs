﻿// Ignore Spelling: Accessor

using Microsoft.AspNetCore.Mvc;
using CloudDevPOE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace CloudDevPOE.Controllers
{
	public class UserController : Controller
	{
		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IConfiguration _configuration;

		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		// Constructor to inject IHttpContextAccessor and IConfiguration
		public UserController(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
		{
			_httpContextAccessor = httpContextAccessor;
			_configuration = configuration;
		}

		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		[HttpGet]
		public ActionResult SignUp()
		{
			return View();
		}

		//--------------------------------------------------------------------------------------------------------------------------//
		[HttpPost]
		public IActionResult SignUp(Tbl_Users user)
		{
			if (ModelState.IsValid)
			{
				var connectionString = _configuration.GetConnectionString("DefaultConnection");

				// Pass the connection string to Insert_User
				int rowsAffected = user.InsertUser(user, connectionString);
				if (rowsAffected > 0)
				{
					return RedirectToAction("Index", "Home");
				}
			}
			return View(user);
		}

		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		[HttpGet]
		public ActionResult Login()
		{
			ViewBag.IsValidUser = true;
			return View();
		}

		//--------------------------------------------------------------------------------------------------------------------------//
		[HttpPost]
		public IActionResult Login(Tbl_Users user)
		{
			var connectionString = _configuration.GetConnectionString("DefaultConnection");

			// Pass the connection string to Validate_User
			int? userId = user.ValidateUser(user, connectionString);
			if (userId.HasValue)
			{
				_httpContextAccessor.HttpContext.Session.SetInt32("UserId", userId.Value);
				return RedirectToAction("Index", "Home");
			}
			else
			{
				ViewBag.ErrorMessage = "Incorrect email or password.";
				return View(user);
			}
		}

		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		[HttpGet]
		public IActionResult Logout()
		{
			_httpContextAccessor.HttpContext.Session.Clear();
			return RedirectToAction("Login", "User");
		}
		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
	}
}