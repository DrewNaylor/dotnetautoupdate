// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Collections;
using NUnit.Framework;
using NUnit.Core;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Globalization;

namespace NUnit.Util.Tests
{
	[TestFixture]
	public class XmlResultWriterTest
	{
		private XmlDocument resultDoc;

		[TestFixtureSetUp]
		public void RunMockTests()
		{
			string testsDll = "mock-assembly.dll";
			TestSuiteBuilder suiteBuilder = new TestSuiteBuilder();
			Test suite = suiteBuilder.Build( new TestPackage( testsDll ) );

            TestResult result = suite.Run(NullListener.NULL, TestFilter.Empty);
			StringBuilder builder = new StringBuilder();
			new XmlResultWriter(new StringWriter(builder)).SaveTestResult(result);

			string resultXml = builder.ToString();

			resultDoc = new XmlDocument();
			resultDoc.LoadXml(resultXml);
		}

		[Test]
		public void SuiteResultHasCategories()
		{
			XmlNodeList categories = resultDoc.SelectNodes("//test-suite[@name=\"MockTestFixture\"]/categories/category");
			Assert.IsNotNull(categories);
			Assert.AreEqual(1, categories.Count);
			Assert.AreEqual("FixtureCategory", categories[0].Attributes["name"].Value);
		}

		[Test]
		public void HasSingleCategory()
		{
			XmlNodeList categories = resultDoc.SelectNodes("//test-case[@name=\"NUnit.Tests.Assemblies.MockTestFixture.MockTest2\"]/categories/category");
			Assert.IsNotNull(categories);
			Assert.AreEqual(1, categories.Count);
			Assert.AreEqual("MockCategory", categories[0].Attributes["name"].Value);
		}

		[Test]
		public void HasSingleProperty()
		{
			XmlNodeList properties = resultDoc.SelectNodes("//test-case[@name=\"NUnit.Tests.Assemblies.MockTestFixture.MockTest2\"]/properties/property[@name=\"Severity\"]");
			Assert.IsNotNull(properties);
			Assert.AreEqual(1, properties.Count);
			Assert.AreEqual("Critical",properties[0].Attributes["value"].Value);
		}

		[Test]
		public void HasMultipleCategories()
		{
			XmlNodeList categories = resultDoc.SelectNodes("//test-case[@name=\"NUnit.Tests.Assemblies.MockTestFixture.MockTest3\"]/categories/category[@name=\"MockCategory\"]");
			Assert.IsNotNull(categories, "MockCategory not found");
			categories = resultDoc.SelectNodes("//test-case[@name=\"NUnit.Tests.Assemblies.MockTestFixture.MockTest3\"]/categories/category[@name=\"AnotherCategory\"]");
			Assert.IsNotNull(categories, "AnotherCategory not found");
		}

		[Test]
		public void HasMultipleProperties()
		{
			XmlNodeList properties = resultDoc.SelectNodes("//test-case[@name=\"NUnit.Tests.Assemblies.MockTestFixture.TestWithManyProperties\"]/properties/property[@name=\"TargetMethod\"]");
			Assert.AreEqual("SomeClassName", properties[0].Attributes["value"].Value);
            //properties = resultDoc.SelectNodes("//test-case[@name=\"NUnit.Tests.Assemblies.MockTestFixture.TestWithManyProperties\"]/properties/property[@name=\"TargetType\"]");
            //Assert.AreEqual("System.Threading.Thread", properties[0].Attributes["value"].Value);
			properties = resultDoc.SelectNodes("//test-case[@name=\"NUnit.Tests.Assemblies.MockTestFixture.TestWithManyProperties\"]/properties/property[@name=\"Size\"]");
			Assert.AreEqual("5", properties[0].Attributes["value"].Value);
		}

		[Test]
		public void TestHasEnvironmentInfo() 
		{
			XmlNode sysinfo = resultDoc.SelectSingleNode("//environment");
			Assert.IsNotNull(sysinfo);
			// In theory, we could do some validity checking on the values
			// of the attributes, but that seems redundant.
			Assert.IsNotNull(sysinfo.Attributes["nunit-version"]);
			Assert.IsNotNull(sysinfo.Attributes["clr-version"]);
			Assert.IsNotNull(sysinfo.Attributes["os-version"]);
			Assert.IsNotNull(sysinfo.Attributes["platform"]);
			Assert.IsNotNull(sysinfo.Attributes["cwd"]);
			Assert.IsNotNull(sysinfo.Attributes["machine-name"]);
			Assert.IsNotNull(sysinfo.Attributes["user"]);
			Assert.IsNotNull(sysinfo.Attributes["user-domain"]);
			
		}

		[Test]
		public void TestHasCultureInfo() 
		{
			XmlNode cultureInfo = resultDoc.SelectSingleNode("//culture-info");
			Assert.IsNotNull(cultureInfo);
			Assert.IsNotNull(cultureInfo.Attributes["current-culture"]);
			Assert.IsNotNull(cultureInfo.Attributes["current-uiculture"]);

			String currentCulture = cultureInfo.Attributes["current-culture"].Value;
			String currentUiCulture = cultureInfo.Attributes["current-uiculture"].Value;

			String currentCultureOnMachine = CultureInfo.CurrentCulture.ToString();
			String currentUiCultureOnMachine = CultureInfo.CurrentUICulture.ToString();
			Assert.AreEqual(currentCultureOnMachine, currentCulture,
				"Current Culture node did not contain the same culture name as the machine");
			Assert.AreEqual(currentUiCultureOnMachine, currentUiCulture,
				"Current UI Culture node did not contain the same Culture UI name as the machine");
		}
	}
}
