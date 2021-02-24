using System;
using System.Collections.Generic;
using LotteryNumbers.Libs;
using LotteryNumbers.Models;
using Microsoft.AspNetCore.Mvc;

namespace LotteryNumbers.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILineGenerator _numbersGenerator;

        private static readonly List<LotteryLine> _lotteryLines = new List<LotteryLine>();

        public HomeController(ILineGenerator numbersGenerator)
        {
            _numbersGenerator = numbersGenerator;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("lotterylines")]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public ActionResult LotteryLines()
        {
            try
            {
                return Json(_lotteryLines);
            }
            catch (Exception /*ex*/)
            {
                return new BadRequestResult();
            }
        }

        [Route("lotterylines/new")]
        [HttpPost]
        public ActionResult AddComment(Request comment)
        {
            _lotteryLines.AddRange(_numbersGenerator.GenerateNumbers(comment.NumberOfLines, comment.BonusBall));
            return Content("Success :)");
        }
    }
}
