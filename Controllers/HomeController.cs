using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GenerateQrCode.Models;
using System.IO;
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;

namespace GenerateQrCode.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string inputText)
        {
            using ( MemoryStream ms = new MemoryStream() )
            {
                var qrGenerator = new QRCodeGenerator();
                var qrCodeData = qrGenerator.CreateQrCode(inputText, QRCodeGenerator.ECCLevel.Q);
                var qrCode = new QRCode(qrCodeData);

                using (Bitmap oBitmap = qrCode.GetGraphic(20))
                {
                    oBitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    ViewBag.QRCode = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
                }

            }

            return View();
        }

    }
}
