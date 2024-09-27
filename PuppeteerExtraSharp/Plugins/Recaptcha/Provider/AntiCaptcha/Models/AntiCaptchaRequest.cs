

// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo

namespace PuppeteerExtraSharp.Plugins.Recaptcha.Provider.AntiCaptcha.Models
{
    public class AntiCaptchaRequest
    {
        public string clientKey { get; set; }
        public string appId { get; set; }

        public AntiCaptchaTask task { get; set; }
    }

    public class RequestForResultTask
    {
        public string clientKey { get; set; }
        public string taskId { get; set; }
    }

    public class AntiCaptchaTaskResult
    {
        public int errorId { get; set; }
        public string status { get; set; }
        public string taskId { get; set; }
    }


    public class AntiCaptchaTask
    {
        public string type { get; set; }
        public string websiteURL { get; set; }
        public string websiteKey { get; set; }
    }
}
