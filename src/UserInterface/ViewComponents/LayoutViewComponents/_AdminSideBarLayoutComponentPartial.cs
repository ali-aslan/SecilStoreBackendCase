using Microsoft.AspNetCore.Mvc;

namespace UserInterface.ViewComponents.LayoutViewComponents
{
    public class _AdminSidebarLayoutComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
