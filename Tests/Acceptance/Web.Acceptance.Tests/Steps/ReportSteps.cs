﻿using NUnit.Framework;
using RestSharp;
using SecurityEssentials.Acceptance.Tests.Extensions;
using SecurityEssentials.Acceptance.Tests.Model;
using SecurityEssentials.Acceptance.Tests.Utility;
using SecurityEssentials.Model;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace SecurityEssentials.Acceptance.Tests.Steps
{
    [Binding]
    public class ReportSteps : TechTalk.SpecFlow.Steps
    {
        private readonly FeatureContext _featureContext;
        private readonly ScenarioContext _scenarioContext;

        public ReportSteps(FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            _featureContext = featureContext;
            _scenarioContext = scenarioContext;
        }
        [Given(@"I have a content security policy violation with details:")]
        public void GivenIHaveAContentSecurityPolicyViolationWithDetails(Table table)
        {
            var cspInstance = table.CreateInstance<CspModel>();
            var cspReport = new CspReport
            {
                BlockedUri = cspInstance.BlockedUri,
                DocumentUri = cspInstance.DocumentUri,
                LineNumber = cspInstance.LineNumber,
                OriginalPolicy = cspInstance.OriginalPolicy,
                Referrer = cspInstance.Referrer,
                ScriptSample = cspInstance.ScriptSample,
                SourceFile = cspInstance.SourceFile,
                ViolatedDirective = cspInstance.ViolatedDirective
            };
            _scenarioContext.SetCspReport(cspReport);
        }
        [Given(@"I have a http public key pinning violation with details:")]
        public void GivenIHaveAHttpPublicKeyPinningViolationWithDetails(Table table)
        {
            var hpkpModel = table.CreateInstance<HpkpModel>();
            var hpkpReport = new HpkpReport
            {
                DateTime = hpkpModel.DateTime.ToString(CultureInfo.CurrentCulture),
                ExpirationDate = hpkpModel.ExpirationDate.ToString(CultureInfo.CurrentCulture),
                HostName = hpkpModel.HostName,
                IncludeSubDomains = hpkpModel.IncludeSubDomains.ToString(),
                KnownPins = hpkpModel.KnownPinsDelimited.Split(','),
                NotedHostName = hpkpModel.NotedHostName,
                Port = hpkpModel.Port,
                ServedCertificateChain = hpkpModel.ServedCertificateChainDelimited.Split(','),
                ValidatedCertificateChain = hpkpModel.ValidatedCertificateChainDelimited.Split(',')
            };
            _scenarioContext.SetHpkpReport(hpkpReport);
        }
        [Given(@"I have a certificate policy violation with details:")]
        public void GivenIHaveACertificatePolicyViolationWithDetails(Table table)
        {
            var ctInstance = table.CreateInstance<CtModel>();
            var ctReport = new CtReport
            {
                FailureDate = TextParser.ConvertDescriptionToDate(ctInstance.FailureDate),
                EffectiveExpirationDate = TextParser.ConvertDescriptionToDate(ctInstance.ExpirationDate),
                HostName = ctInstance.HostName,
                Port = ctInstance.Port
            };
            _scenarioContext.SetCtReport(ctReport);
        }
        [Given(@"I have the following certificate policy violation scts:")]
        public void GivenIHaveTheFollowingCertificatePolicyViolationScts(Table table)
        {
            var ctReport = _scenarioContext.GetCtReport();
            var scts = table.CreateSet<Sct>();
            ctReport.Scts = scts.ToArray();
            _scenarioContext.SetCtReport(ctReport);
        }

        [When(@"I post the content security policy violation to the website")]
        public void WhenIPostTheContentSecurityPolicyViolationToTheWebsite()
        {
            var cspReport = _scenarioContext.GetCspReport();
            var url = $"{ConfigurationManager.AppSettings["WebServerUrl"]}Security/CspReporting/";
            var response = HttpWeb.PostJsonStream(url, new CspHolder { CspReport = cspReport });
            Assert.That(response.ResponseStatus, Is.EqualTo(ResponseStatus.Completed));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
        [When(@"I post the http public key pinning violation to the website")]
        public void WhenIPostTheHttpPublicKeyPinningViolationToTheWebsite()
        {
            var hpkpReport = _scenarioContext.GetHpkpReport();
            var url = $"{ConfigurationManager.AppSettings["WebServerUrl"]}Security/HpkpReporting/";
            var response = HttpWeb.PostJsonStream(url, hpkpReport);
            Assert.That(response.ResponseStatus, Is.EqualTo(ResponseStatus.Completed));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
        [When(@"I post the certificate policy violation to the website")]
        public void WhenIPostTheCertificatePolicyViolationToTheWebsite()
        {
            var ctReport = _scenarioContext.GetCtReport();
            var url = $"{ConfigurationManager.AppSettings["WebServerUrl"]}Security/CtReporting/";
            var response = HttpWeb.PostJsonStream(url, new CtHolder { CtReport = ctReport });
            Assert.That(response.ResponseStatus, Is.EqualTo(ResponseStatus.Completed));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

    }
}
