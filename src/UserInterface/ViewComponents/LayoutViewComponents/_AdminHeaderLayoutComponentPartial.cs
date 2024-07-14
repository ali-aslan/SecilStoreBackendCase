using Microsoft.AspNetCore.Mvc;

namespace UserInterface.ViewComponents.LayoutViewComponents
{
    public class _AdminHeaderLayoutComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
