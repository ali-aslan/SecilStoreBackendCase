using Microsoft.AspNetCore.Mvc;

namespace UserInterface.ViewComponents.LayoutViewComponents
{
    public class _AdminScriptLayoutComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
