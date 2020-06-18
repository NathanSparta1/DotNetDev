using SP_ASPNET_1.DbFiles.Operations;
using SP_ASPNET_1.Models;
using SP_ASPNET_1.ViewModels;
using System.Web.Mvc;
using System.Web.Routing;
using SP_ASPNET_1.BusinessLogic;
using System;
using System.Net;
using PagedList;
using PagedList.Mvc;
using System.Data.Entity;

namespace SP_ASPNET_1.Controllers
{
    [RoutePrefix("Blog")]
    public class BlogPostController : Controller
    {
        private readonly BlogPostOperations _blogPostOperations = new BlogPostOperations();

        private DbFiles.Contexts.IceCreamBlogContext db = new DbFiles.Contexts.IceCreamBlogContext();

        [Route("")]
        [HttpGet]
        public ActionResult Index(string searchBy, string search, int? page)
        {
            //return this.View();
            BlogIndexViewModel result = this._blogPostOperations.GetBlogIndexViewModel();

            ViewBag.Title = "Blog";
            return this.View(result);
        }
        
        public ActionResult Index( int? page)
        {
            //return this.View();
            BlogIndexViewModel result = this._blogPostOperations.GetBlogIndexViewModel();
            DbFiles.Contexts.IceCreamBlogContext db = new DbFiles.Contexts.IceCreamBlogContext();

            ViewBag.Title = "Blog";
            return this.View(db.BlogPosts.ToPagedList(page ?? 1,3));
        }


        


        /*public ActionResult Index(string sortOrder)
        {
            ViewData["BlogNameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["AuthorNameSortParm"] = sortOrder == "AuthorName" ? "AName_desc" : "AuthorName";
            BlogPost post;
            var Blogs = from b in 
                           select b;
            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(s => s.LastName);
                    break;
                case "Date":
                    students = students.OrderBy(s => s.EnrollmentDate);
                    break;
                case "date_desc":
                    students = students.OrderByDescending(s => s.EnrollmentDate);
                    break;
                default:
                    students = students.OrderBy(s => s.LastName);
                    break;
            }
            return View(await students.AsNoTracking().ToListAsync());
        }*/



        [Route("Detail/{id:int?}")]
        [HttpGet]
        public ActionResult SinglePost(int? id)
        {
            ViewBag.Title = "single post";

            
            BlogSinglePostViewModel modelView;

            if (id == null)
            {
                modelView = this._blogPostOperations.GetLatestBlogPost();
            }
            else
            {
                modelView = this._blogPostOperations.GetBlogPostByIdFull((int)id);
            }

            return View(modelView);
        }

        [Route("Detail/Random")]
        [HttpGet]
        public ActionResult RandomPost()
        {
            ViewBag.Title = "Random post";

            var viewModel = this._blogPostOperations.GetRandomBlogPost();

            return View(viewModel);
        }

        [Route("LatestPost")]
        [HttpGet]
        public ActionResult LatestPost()
        {
            var viewModel = this._blogPostOperations.GetLatestBlogPost();

            return this.PartialView("~/Views/BlogPost/_BlogPostRecentPartialView.cshtml", viewModel);
        }
        public ActionResult Create()
        {
            ViewBag.GenreId = new SelectList(db.Authors, "AuthorId", "Name");
            return View();
        }

        // POST: Films/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BlogPostID,Title,DateTime,Content,Rating,ImageURL,AuthorId")] BlogPost post)
        {
              db.BlogPosts.Add(post);
               db.SaveChanges();
               return RedirectToAction("Index");
            

            //ViewBag.AuthorId = new SelectList(db.Authors, "AuthorId", "Name", post.AuthorId);
           // return View(post);
        }


      

        // GET: Films/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost post = db.BlogPosts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuthorId = new SelectList(db.Authors, "AuthorId", "Name", post.AuthorId);
            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BlogPostID,Title,DateTime,Content,Rating,ImageURL,AuthorId")] BlogPost post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GenreId = new SelectList(db.Authors, "AuthorId", "Name", post.AuthorId);
            return View(post);
        }

        



     
        // GET: Films/Delete/5
        public ActionResult Delete(int? id)
        {
            /*if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }*/
            BlogPost post = db.BlogPosts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Films/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BlogPost post = db.BlogPosts.Find(id);
            db.BlogPosts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        /*public ActionResult LikePost()
        {

            Contexts.IceCreamBlogContext db = new Contexts.IceCreamBlogContext();



        }*/


    }
}
