namespace biddMVC6boiler.Controllers
{
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Boilerplate.AspNetCore;
    using Boilerplate.AspNetCore.Filters;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using biddMVC6boiler.Constants;
    using biddMVC6boiler.Services;
    using biddMVC6boiler.Settings;

    public class HomeController : Controller
    {
        #region Fields

        private readonly IOptions<AppSettings> appSettings;
        private readonly IBrowserConfigService browserConfigService;
        private readonly IManifestService manifestService;

        #endregion

        #region Constructors

        public HomeController(
            IBrowserConfigService browserConfigService,
            IManifestService manifestService,
            IOptions<AppSettings> appSettings)
        {
            this.appSettings = appSettings;
            this.browserConfigService = browserConfigService;
            this.manifestService = manifestService;
        }

        #endregion

        [HttpGet("", Name = HomeControllerRoute.GetIndex)]
        public IActionResult Index()
        {
            return this.View(HomeControllerAction.Index);
        }

        [HttpGet("about", Name = HomeControllerRoute.GetAbout)]
        public IActionResult About()
        {
            return this.View(HomeControllerAction.About);
        }

        /// <summary>
        /// Gets the browserconfig XML for the current site. This allows you to customize the tile, when a user pins
        /// the site to their Windows 8/10 start screen. See http://www.buildmypinnedsite.com and
        /// https://msdn.microsoft.com/en-us/library/dn320426%28v=vs.85%29.aspx
        /// </summary>
        /// <returns>The browserconfig XML for the current site.</returns>
        [NoTrailingSlash]
        [ResponseCache(CacheProfileName = CacheProfileName.BrowserConfigXml)]
        [Route("browserconfig.xml", Name = HomeControllerRoute.GetBrowserConfigXml)]
        public ContentResult BrowserConfigXml()
        {
            string content = this.browserConfigService.GetBrowserConfigXml();
            return this.Content(content, ContentType.Xml, Encoding.UTF8);
        }

        /// <summary>
        /// Gets the manifest JSON for the current site. This allows you to customize the icon and other browser
        /// settings for Chrome/Android and FireFox (FireFox support is coming). See https://w3c.github.io/manifest/
        /// for the official W3C specification. See http://html5doctor.com/web-manifest-specification/ for more
        /// information. See https://developer.chrome.com/multidevice/android/installtohomescreen for Chrome's
        /// implementation.
        /// </summary>
        /// <returns>The manifest JSON for the current site.</returns>
        [NoTrailingSlash]
        [ResponseCache(CacheProfileName = CacheProfileName.ManifestJson)]
        [Route("manifest.json", Name = HomeControllerRoute.GetManifestJson)]
        public ContentResult ManifestJson()
        {
            string content = this.manifestService.GetManifestJson();
            return this.Content(content, ContentType.Json, Encoding.UTF8);
        }
    }
}