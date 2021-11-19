using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ECommerce.Models;

namespace ECommerce.Controllers
{
    public class XacNhanDonHangsController : Controller
    {
        private CSDLContext db = new CSDLContext();

        // GET: XacNhanDonHangs
        public ActionResult Index()
        {
            Session["tmp"] = "9";
            var result = from g in db.XacNhanDonHangs
                         join c in db.OrderConfirmedDetails on g.MaDonHang equals c.MaDonHang
                         join s in db.SanPhams on c.MaSanPham equals s.MaSanPham
                         select new Tmp
                         {
                             Hinh = s.Hinh,
                             TenSanPham = s.TenSanPham,
                             SoLuong = c.SoLuong,
                             MaDonHang = g.MaDonHang,
                         };
            var xacNhanDonHangs = db.XacNhanDonHangs.Include(x => x.KhachHang).Include(x => x.Voucher);
            ViewBag.ds = result;
            return View(xacNhanDonHangs);
        }

        public class Tmp
        {
            public int MaDonHang { get; set; }
            public int SoLuong { get; set; }
            public string TenSanPham { get; set; }
            public string Hinh { get; set; }
        }

        // GET: XacNhanDonHangs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            XacNhanDonHang xacNhanDonHang = db.XacNhanDonHangs.Find(id);
            if (xacNhanDonHang == null)
            {
                return HttpNotFound();
            }
            return View(xacNhanDonHang);
        }

        // GET: XacNhanDonHangs/Create
        public ActionResult Create()
        {
            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "TenKH");
            ViewBag.VoucherID = new SelectList(db.Vouchers, "VoucherID", "TenVoucher");
            return View();
        }

        // POST: XacNhanDonHangs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDonHang,MaKH,VoucherID,TongTien,TinhTrang,NgayXacNhan,DiaChi,SDT,HoTen")] XacNhanDonHang xacNhanDonHang)
        {
            if (ModelState.IsValid)
            {
                db.XacNhanDonHangs.Add(xacNhanDonHang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "TenKH", xacNhanDonHang.MaKH);
            ViewBag.VoucherID = new SelectList(db.Vouchers, "VoucherID", "TenVoucher", xacNhanDonHang.VoucherID);
            return View(xacNhanDonHang);
        }

        // GET: XacNhanDonHangs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            XacNhanDonHang xacNhanDonHang = db.XacNhanDonHangs.Find(id);
            if (xacNhanDonHang == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "TenKH", xacNhanDonHang.MaKH);
            ViewBag.VoucherID = new SelectList(db.Vouchers, "VoucherID", "TenVoucher", xacNhanDonHang.VoucherID);
            return View(xacNhanDonHang);
        }

        // POST: XacNhanDonHangs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDonHang,MaKH,VoucherID,TongTien,TinhTrang,NgayXacNhan,DiaChi,SDT,HoTen")] XacNhanDonHang xacNhanDonHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(xacNhanDonHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "TenKH", xacNhanDonHang.MaKH);
            ViewBag.VoucherID = new SelectList(db.Vouchers, "VoucherID", "TenVoucher", xacNhanDonHang.VoucherID);
            return View(xacNhanDonHang);
        }

        // GET: XacNhanDonHangs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            XacNhanDonHang xacNhanDonHang = db.XacNhanDonHangs.Find(id);
            if (xacNhanDonHang == null)
            {
                return HttpNotFound();
            }
            return View(xacNhanDonHang);
        }

        // POST: XacNhanDonHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            XacNhanDonHang xacNhanDonHang = db.XacNhanDonHangs.Find(id);
            db.XacNhanDonHangs.Remove(xacNhanDonHang);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
