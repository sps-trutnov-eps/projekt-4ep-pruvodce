using Microsoft.AspNetCore.Mvc;

namespace PruvodceProject.Controllers
{
    public class OdkazyController : Controller
    {
        public IActionResult Index() => View();

        public IActionResult Isic() => Redirect("https://www.isic.cz/prukazy/isic/");

        public IActionResult Bakalari() => Redirect("https://bakalari.spstrutnov.cz");

        public IActionResult Sps() => Redirect("https://spstrutnov.cz");
        
        public IActionResult KeStazeni() => Redirect("https://www.spstrutnov.cz/ke-stazeni");
        
        public IActionResult Mail() => Redirect("http://mail.spstrutnov.cz");
        
        public IActionResult Youtube() => Redirect("https://www.youtube.com/@@spstrutnov");
        
        public IActionResult Github() => Redirect("https://github.com/enterprises/sps-trutnov");

        public IActionResult GithubStudent() => Redirect("https://education.github.com/pack");
    }
}
