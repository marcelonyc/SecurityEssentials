﻿using NUnit.Framework;
using SecurityEssentials.Acceptance.Tests.Extensions;
using SecurityEssentials.Acceptance.Tests.Model;
using SecurityEssentials.Acceptance.Tests.Utility;
using System;
using System.Configuration;
using System.Linq;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace SecurityEssentials.Acceptance.Tests.Steps
{


    [Binding]
    public class HttpSteps : TechTalk.SpecFlow.Steps
    {
        private readonly FeatureContext _featureContext;
        private readonly ScenarioContext _scenarioContext;

        public HttpSteps(FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            _featureContext = featureContext;
            _scenarioContext = scenarioContext;
        }

        [When(@"I call http get on the website")]
        public void WhenICallHttpGetOnTheWebsite()
        {
            var response = HttpWeb.Get(ConfigurationManager.AppSettings["WebServerUrl"]);
            var headers = response.Headers.Select(a => new Tuple<string, string>(a.Name, a.Value.ToString()));
            _scenarioContext.SetHttpHeaders(headers);
        }

        [Then(@"the response headers will contain:")]
        public void ThenTheResponseHeadersWillContain(Table table)
        {
            var actualHeaders = _scenarioContext.GetHttpHeaders().ToList();
            var expectedHeaders = table.CreateSet<HttpHeader>().ToList();
            foreach (var expectedHeader in expectedHeaders)
            {
                Assert.That(actualHeaders.ToList().Any(a => a.Item1 == expectedHeader.Key), $"Headers do not contain the key '{expectedHeader.Key}'");
                var actualHeader = actualHeaders.First(a => a.Item1 == expectedHeader.Key);
                if (actualHeader.Item1 == "Content-Security-Policy" && ConfigurationManager.AppSettings["WebServerUrl"].StartsWith("https:"))
                {
                    expectedHeader.Value = expectedHeader.Value.Replace("font-src 'self' https:", "font-src https:").Replace("*", "https:").Replace("'self'", "https:");
                }
                Assert.That(expectedHeader.Value, Is.EqualTo(actualHeader.Item2), $"Header values do not match for key '{actualHeader.Item1}'");
            }
        }

        [Then(@"the response headers will not contain:")]
        public void ThenTheResponseHeadersWillNotContain(Table table)
        {
            var actualHeaders = _scenarioContext.GetHttpHeaders().ToList();
            var excludedHeaders = table.CreateSet<HttpHeader>();
            foreach (var excludedHeader in excludedHeaders)
            {
                Assert.That(actualHeaders.Any(a => a.Item1 == excludedHeader.Key), Is.False, $"Headers contain a header key '{excludedHeader.Key}' when it should not");
            }
        }
    }
}
