using PuppeteerExtraSharp.Plugins.Recaptcha;
using PuppeteerSharp;
using Xunit;
using Xunit.Abstractions;
using Task = System.Threading.Tasks.Task;

namespace Extra.Tests.Recaptcha.AntiCaptcha
{
    [Collection("Captcha")]
    public class AntiCaptchaTests : BrowserDefault
    {
        private readonly ITestOutputHelper _logger;

        public AntiCaptchaTests(ITestOutputHelper _logger)
        {
            this._logger = _logger;
        }
        

        private async Task CheckSuccessVerify(Page page)
        {
            var successElement = await page.QuerySelectorAsync("div[id='main'] div[class='description'] h2");
            var elementValue = await (await successElement.GetPropertyAsync("textContent")).JsonValueAsync<string>();
            Assert.NotNull(successElement);
            Assert.Equal("Успешная верификация!", elementValue);
        }
    }
}
