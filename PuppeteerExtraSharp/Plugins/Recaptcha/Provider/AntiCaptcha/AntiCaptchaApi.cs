﻿using System.Threading;
using System.Threading.Tasks;
using PuppeteerExtraSharp.Plugins.Recaptcha.Provider.AntiCaptcha.Models;
using PuppeteerExtraSharp.Plugins.Recaptcha.RestClient;
using RestSharp;

namespace PuppeteerExtraSharp.Plugins.Recaptcha.Provider.AntiCaptcha
{
    public class AntiCaptchaApi
    {
        private readonly string _userKey;
        private readonly ProviderOptions _options;
        private readonly RestClient.RestClient _client = new RestClient.RestClient("https://api.capsolver.com");
        public AntiCaptchaApi(string userKey, ProviderOptions options)
        {
            _userKey = userKey;
            _options = options;
        }

        public Task<AntiCaptchaTaskResult> CreateTaskAsync(string pageUrl, string key, CancellationToken token = default)
        {
            var content = new AntiCaptchaRequest()
            {
                clientKey = _userKey,
                appId = "67C252AE-03F1-4EBF-9A45-EE22DA54F6CC",
                task = new AntiCaptchaTask()
                {
                    type = "RecaptchaV2TaskProxyless",
                    websiteURL = pageUrl,
                    websiteKey = key
                }
            };



            var result = _client.PostWithJsonAsync<AntiCaptchaTaskResult>("createTask", content, token);
            return result;
        }


        public async Task<TaskResultModel> PendingForResult(string taskId, CancellationToken token = default)
        {
            var content = new RequestForResultTask()
            {
                clientKey = _userKey,
                taskId = taskId
            };


            var request = new RestRequest("getTaskResult");
            request.AddJsonBody(content);
            request.Method = Method.POST;
       
            var result = await _client.CreatePollingBuilder<TaskResultModel>(request).TriesLimit(_options.PendingCount)
                .WithTimeoutSeconds(5).ActivatePollingAsync(
                    response =>
                    {
                        if (response.Data.status == "ready" || response.Data.errorId != 0)
                            return PollingAction.Break;

                        return PollingAction.ContinuePolling;
                    });
            return result.Data;
        }

    }
}
