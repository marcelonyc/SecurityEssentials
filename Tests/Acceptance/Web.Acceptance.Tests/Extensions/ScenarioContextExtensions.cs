﻿using NUnit.Framework;
using SecurityEssentials.Acceptance.Tests.Pages;
using SecurityEssentials.Model;
using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace SecurityEssentials.Acceptance.Tests.Extensions
{
    public static class ScenarioContextExtensions
	{

		public const string KeyHttpHeaders = "Http_Headers";
		public const string KeyHash = "Hash";
		public const string KeySalt = "Salt";
		public const string CspReport = "CspReport";
        public const string CtReport = "CtReport";
        public const string HpkpReport = "HpkpReport";

		public static CspReport GetCspReport(this ScenarioContext scenarioContext)
		{
			return scenarioContext.Get<CspReport>(CspReport);
		}
		public static void SetCspReport(this ScenarioContext scenarioContext, CspReport cspReport)
		{
			scenarioContext.Set(cspReport, CspReport);
		}
        public static CtReport GetCtReport(this ScenarioContext scenarioContext)
        {
            return scenarioContext.Get<CtReport>(CtReport);
        }
        public static void SetCtReport(this ScenarioContext scenarioContext, CtReport ctReport)
        {
            scenarioContext.Set(ctReport, CtReport);
        }
        public static HpkpReport GetHpkpReport(this ScenarioContext scenarioContext)
		{
			return scenarioContext.Get<HpkpReport>(HpkpReport);
		}
		public static void SetHpkpReport(this ScenarioContext scenarioContext, HpkpReport hpkpReport)
		{
			scenarioContext.Set(hpkpReport, HpkpReport);
		}
		public static IEnumerable<Tuple<string, string>> GetHttpHeaders(this ScenarioContext scenarioContext)
		{
			return scenarioContext.Get<IEnumerable<Tuple<string, string>>>(KeyHttpHeaders);
		}
		public static void SetHttpHeaders(this ScenarioContext scenarioContext, IEnumerable<Tuple<string, string>> value)
		{
			scenarioContext.Set(value, KeyHttpHeaders);
		}
		public static string GetHash(this ScenarioContext scenarioContext)
		{
			return scenarioContext.Get<string>(KeyHash);
		}
		public static void SetSalt(this ScenarioContext scenarioContext, string value)
		{
			scenarioContext.Set(value, KeySalt);
		}

		public static string GetSalt(this ScenarioContext scenarioContext)
		{
			return scenarioContext.Get<string>(KeySalt);
		}

		public static void SetHash(this ScenarioContext scenarioContext, string value)
		{
			scenarioContext.Set(value, KeyHash);
		}

		public static T GetPage<T>(this ScenarioContext scenarioContext) where T : BasePage
		{
			T page = scenarioContext.Get<T>();
			Assert.That(page.IsCurrentPage, Is.True, "Unable to load page");
			return page;
		}

		public static bool HasPage<T>(this ScenarioContext scenarioContext) where T : BasePage
		{
            return scenarioContext.TryGetValue(out T _);
		}

	}
}
