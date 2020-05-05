using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web.Mvc;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSModel.SparePart;
using ODMSModel.SparePartBarcode;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class SparePartBarcodeController : ControllerBase
    {
        readonly SparePartBarcodeBL _sparePartBarcodeService = new SparePartBarcodeBL();
        readonly ClaimDismantledPartsBL _claimDismantledPartService = new ClaimDismantledPartsBL();

        private void SetDefaults()
        {
        }

        #region SparePartBarcode Index

        //[OutputCache(Duration = 120, VaryByParam = "none")]
        public ActionResult Image(string barcode)
        {
            //Code39BarcodeDraw barcode39 = BarcodeDrawFactory.Code39WithoutChecksum;
            //Image image = barcode39.Draw(barcode, 40);

            //MemoryStream ms = new MemoryStream();
            //image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);

            //return File(ms.ToArray(), "image/png");

            // generating a barcode here. Code is taken from QrCode.Net library
            QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.H);
            QrCode qrCode = new QrCode();
            qrEncoder.TryEncode(barcode, out qrCode);
            GraphicsRenderer renderer = new GraphicsRenderer(new FixedModuleSize(4, QuietZoneModules.Four), Brushes.Black, Brushes.White);

            Stream memoryStream = new MemoryStream();
            renderer.WriteToStream(qrCode.Matrix, ImageFormat.Png, memoryStream);

            // very important to reset memory stream to a starting position, otherwise you would get 0 bytes returned
            memoryStream.Position = 0;

            var resultStream = new FileStreamResult(memoryStream, "image/png")
            {
                FileDownloadName = String.Format("{0}.png", barcode)
            };

            return resultStream;
        }

        private string GetImage(string barcode)
        {
            QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.H);
            QrCode qrCode = new QrCode();
            qrEncoder.TryEncode(barcode, out qrCode);
            GraphicsRenderer renderer = new GraphicsRenderer(new FixedModuleSize(4, QuietZoneModules.Four), Brushes.Black, Brushes.White);

            Stream memoryStream = new MemoryStream();
            renderer.WriteToStream(qrCode.Matrix, ImageFormat.Png, memoryStream);

            // very important to reset memory stream to a starting position, otherwise you would get 0 bytes returned
            memoryStream.Position = 0;


            byte[] buffer = new byte[memoryStream.Length];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = memoryStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }

            }
            return Convert.ToBase64String(buffer);

        }


        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartBarcode.SparePartBarcodeIndex)]
        [HttpGet]
        public ActionResult SparePartBarcodeIndex()
        {
            SparePartBarcodeIndexViewModel model = new SparePartBarcodeIndexViewModel();
            SetDefaults();
            return View(model);
        }


        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartBarcode.SparePartBarcodeIndex)]
        [HttpPost]
        public ActionResult SparePartBarcodeIndex(SparePartBarcodeIndexViewModel viewModel)
        {
            SetDefaults();
            SparePartBL bo = new SparePartBL();

            if (viewModel.PartId != 0)
            {
                if (Request.Params["action:SparePartBarcodeCreate"] != null)
                {
                    SparePartIndexViewModel model = new SparePartIndexViewModel();
                    model.PartId = viewModel.PartId;
                    bo.GetSparePart(UserManager.UserInfo, model);
                    viewModel.Barcode = model.Barcode;
                    viewModel.PartCode = model.PartCode;
                    ViewData["Barcode"] = viewModel.Barcode;
                    viewModel.PartName = model.PartNameInLanguage;
                    CheckErrorForMessage(viewModel, true);
                }

                ModelState.Clear();
            }
            else
            {
                SetMessage(MessageResource.SparePartBarcode_Warning_PartMustSelected, CommonValues.MessageSeverity.Fail);
            }


            return View(viewModel);
        }


        #endregion

        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartBarcode.SparePartBarcodeIndex)]
        public ActionResult PrintBarcode(int id, bool isPrinted)
        {
            var model = new List<SparePartBarcodeIndexViewModel>();

            model = _sparePartBarcodeService.List(UserManager.UserInfo, id, isPrinted).Data;
            model.ForEach(barcode => barcode.EncodedBarcodeImage = GetImage(barcode.Barcode));

            return PartialView("_PrintBarcode", model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartBarcode.SparePartBarcodeIndex)]
        public ActionResult PrintSpareBarcode(int partId)
        {
            SparePartBL bo = new SparePartBL();

            var model = new SparePartIndexViewModel() { PartId = partId };

            if (model.PartId != 0)
                bo.GetSparePart(UserManager.UserInfo, model);

            return PartialView("_PrintSpareBarcode", model);
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartBarcode.SparePartBarcodeIndex)]
        public ActionResult UpdateBarcode(string idList)
        {
            ODMSModel.ClaimDismantledParts.ClaimDismantledPartsViewModel model = new ODMSModel.ClaimDismantledParts.ClaimDismantledPartsViewModel()
            {
                IdList = idList,
                BarcodeFirstPrintDate = DateTime.Now
            };

            _claimDismantledPartService.UpdateClaimDismantledParts(UserManager.UserInfo, model);

            return Json(model);
        }
    }
}