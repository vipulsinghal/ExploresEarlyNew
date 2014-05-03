using System.Web.UI;
using DataAccess;
using DataAccess.RepoServices;
using Explorers.Infrastructure.Entities;
using Explorers.Web.Areas.Admin.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Explorers.Web.Areas.Admin.Models.PageModel;
using Explorers.Web.Infrastructure;
using Infrastructure;
using Explorers.Web.Areas.Admin.Controllers.BaseController;

namespace Explorers.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = ExplorerUserRoles.SuperAdmin + "," + ExplorerUserRoles.Admin)]
    public class PageController : ExplorersControllerBase
    {
        private RepoWebPage _repoWebPage;

        public PageController()
        {
            _repoWebPage = new RepoWebPage(new ExDatabaseContext());
        }

        private PagesIndex GetPageModel()
        {
            var pagesTreeView = _repoWebPage.GetAllPages();
            var pageModel = new PagesIndex
            {
                TreeView = new PagesTreeView { CurrentPages = pagesTreeView }
            };
            return pageModel;
        }

        public ActionResult Index()
        {
            var viewModel = GetPageModel();
            return View(viewModel);
        }

        public JsonResult Edit(int pageId)
        {
            var getPage = _repoWebPage.DbContext.WebPages.FirstOrDefault(pg => pg.Id == pageId);
            if (getPage == null)
                return Json(new JsonResponse("Invalid Page!", "Invalid page id."));

            var viewModel = new EditPageViewModel { 
             PageId = getPage.Id,
             Title = getPage.Title,
             Contents = getPage.Contents
            };

            var viewEdit = RenderPartialViewToString("Edit", viewModel);
            return Json(new JsonResponse((object)viewEdit), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Edit(EditPageViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return Json(new JsonResponse("Invalid Data", "Please provide correct details for page"));

            var getPage = _repoWebPage.DbContext.WebPages.FirstOrDefault(pg => pg.Id == viewModel.PageId);
            if (getPage == null)
                return Json(new JsonResponse("Invalid Page!", "Invalid page id."));

            if (_repoWebPage.DbContext.WebPages.FirstOrDefault(pg => pg.Id != viewModel.PageId && pg.Title == viewModel.Title) != null)
                return Json(new JsonResponse("Page exists Already!", "A page with same title/name already exists"));

            var pageTitleChanged = getPage.Title != viewModel.Title;

            getPage.Title = viewModel.Title;
            getPage.Contents = viewModel.Contents;
            _repoWebPage.DbContext.SaveChanges();

            return Json(new JsonResponse((object)null)
            {
                Message = "Page updated successfully!",
                Description = "Page details have been updated successfully!",
                Content = pageTitleChanged ?
                new { 
                 TreeView= RenderPartialViewToString("PagesTreeView", new PagesTreeView{CurrentPages =  _repoWebPage.GetAllPages()}),
                 Page = RenderPartialViewToString("Edit", viewModel)
                }
                : null
            });

        }

        public JsonResult Add(int parentId = 0)
        {
            var viewModel = new AddPageViewModel { ParentId = parentId };
            var viewEdit = RenderPartialViewToString("Add", viewModel);
            return Json(new JsonResponse((object)viewEdit), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Add(AddPageViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return Json(new JsonResponse("Invalid Data", "Please provide correct details for page"));

            if (_repoWebPage.DbContext.WebPages.FirstOrDefault(pg => pg.Title == viewModel.Title) != null)
                return Json(new JsonResponse("Page exists Already!", "A page with same title/name already exists"));

            WebPage getPage = null;
            if (viewModel.ParentId != 0)
            {
                getPage = _repoWebPage.DbContext.WebPages.FirstOrDefault(pg => pg.Id == viewModel.ParentId && pg.Parent == null);
                if (getPage == null)
                    return Json(new JsonResponse("Invalid parent page!", "The chosen parent page doesn't exist or is not root page."));
            }

            var newPage = new WebPage
            {
                Title = viewModel.Title,
                Parent = getPage,
                Contents = viewModel.Contents,
                IsPublished = false
            };
            _repoWebPage.DbContext.WebPages.Add(newPage);
            _repoWebPage.DbContext.SaveChanges();

            var modelTreeView = new PagesTreeView{CurrentPages =  _repoWebPage.GetAllPages()};
            return Json(new JsonResponse(
                new
                {
                    TreeView = RenderPartialViewToString("PagesTreeView", modelTreeView),
                    Page = RenderPartialViewToString("Add", viewModel)
                }) { 
            Message = "Page Added Successfully!",
            Description = "New Page has been added succesfully."
            });
        }

        public JsonResult Delete(int pageId)
        {
            var getPage = _repoWebPage.DbContext.WebPages.FirstOrDefault(pg => pg.Id == pageId);
            if (getPage == null)
                return Json(new JsonResponse("Invalid Page!", "Invalid page id."));

            var viewModel = new DeletePageViewModel
            {
                PageId = getPage.Id,
                Title = getPage.Title
            };

            var viewDelete = RenderPartialViewToString("Delete", viewModel);
            return Json(new JsonResponse((object)viewDelete), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Delete(DeletePageViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return Json(new JsonResponse("Invalid Data", "This is an invalid page"));

            var getPage = _repoWebPage.DbContext.WebPages.FirstOrDefault(pg => pg.Id == viewModel.PageId);
            if (getPage == null)
                return Json(new JsonResponse("Invalid Page!", "Invalid page id."));

            _repoWebPage.DbContext.WebPages.Remove(getPage);
            _repoWebPage.DbContext.SaveChanges();

            return Json(new JsonResponse((object)null)
            {
                Message = "Page deleted successfully!",
                Description = "Page has been deleted successfully!",
                Content = 
                new
                {
                    TreeView = RenderPartialViewToString("PagesTreeView", new PagesTreeView { CurrentPages = _repoWebPage.GetAllPages() })
                }
            });

        }

        public JsonResult Publish(int pageId)
        {
            var getPage = _repoWebPage.DbContext.WebPages.FirstOrDefault(pg => pg.Id == pageId);
            if (getPage == null)
                return Json(new JsonResponse("Invalid Page!", "Invalid page id."));

            var viewModel = new PublishPageViewModel
            {
                PageId = getPage.Id,
                Title = getPage.Title
            };

            var viewDelete = RenderPartialViewToString("Publish", viewModel);
            return Json(new JsonResponse((object)viewDelete), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Publish(PublishPageViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return Json(new JsonResponse("Invalid Data", "This is an invalid page"));

            var getPage = _repoWebPage.DbContext.WebPages.FirstOrDefault(pg => pg.Id == viewModel.PageId);
            if (getPage == null)
                return Json(new JsonResponse("Invalid Page!", "Invalid page id."));

            getPage.IsPublished = true;
            _repoWebPage.DbContext.SaveChanges();

            return Json(new JsonResponse((object)null)
            {
                Message = "Page published successfully!",
                Description = "Page has been published successfully!",
                Content =
                new
                {
                    TreeView = RenderPartialViewToString("PagesTreeView", new PagesTreeView { CurrentPages = _repoWebPage.GetAllPages() })
                }
            });

        }

        public JsonResult Unpublish(int pageId)
        {
            var getPage = _repoWebPage.DbContext.WebPages.FirstOrDefault(pg => pg.Id == pageId);
            if (getPage == null)
                return Json(new JsonResponse("Invalid Page!", "Invalid page id."));

            var viewModel = new UnpublishPageViewModel
            {
                PageId = getPage.Id,
                Title = getPage.Title
            };

            var viewDelete = RenderPartialViewToString("Unpublish", viewModel);
            return Json(new JsonResponse((object)viewDelete), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Unpublish(UnpublishPageViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return Json(new JsonResponse("Invalid Data", "This is an invalid page"));

            var getPage = _repoWebPage.DbContext.WebPages.FirstOrDefault(pg => pg.Id == viewModel.PageId);
            if (getPage == null)
                return Json(new JsonResponse("Invalid Page!", "Invalid page id."));

            getPage.IsPublished = false;
            _repoWebPage.DbContext.SaveChanges();

            return Json(new JsonResponse((object)null)
            {
                Message = "Page unpublished successfully!",
                Description = "Page has been unpublished successfully!",
                Content =
                new
                {
                    TreeView = RenderPartialViewToString("PagesTreeView", new PagesTreeView { CurrentPages = _repoWebPage.GetAllPages() })
                }
            });

        }

    }
}
