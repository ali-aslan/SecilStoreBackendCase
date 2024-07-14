using Microsoft.AspNetCore.Mvc;

namespace UserInterface.ViewComponents.LayoutViewComponents
{
    public class _AdminFooterLayoutComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
