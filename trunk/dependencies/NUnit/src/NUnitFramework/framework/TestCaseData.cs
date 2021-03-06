// ****************************************************************
// Copyright 2008, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Collections;
using System.Collections.Specialized;

namespace NUnit.Framework
{
    /// <summary>
    /// The TestCaseData class represents a set of arguments
    /// and other parameter info to be used for a parameterized
    /// test case. It provides a number of instance modifiers
    /// for use in initializing the test case.
    /// 
    /// Note: Instance modifiers are getters that return
    /// the same instance after modifying it's state.
    /// </summary>
    public class TestCaseData : ITestCaseData
    {
        #region Constants
        //private static readonly string DESCRIPTION = "_DESCRIPTION";
        //private static readonly string IGNOREREASON = "_IGNOREREASON";
        private static readonly string CATEGORIES = "_CATEGORIES";
        #endregion

        #region Instance Fields
        /// <summary>
        /// The argument list to be provided to the test
        /// </summary>
        private object[] arguments;

        /// <summary>
        /// The expected result to be returned
        /// </summary>
        private object result;

        /// <summary>
        ///  The expected exception Type
        /// </summary>
        private Type expectedExceptionType;

        /// <summary>
        /// The FullName of the expected exception
        /// </summary>
        private string expectedExceptionName;

        /// <summary>
        /// The name to be used for the test
        /// </summary>
        private string testName;

        /// <summary>
        /// The description of the test
        /// </summary>
        private string description;

        /// <summary>
        /// A dictionary of properties, used to add information
        /// to tests without requiring the class to change.
        /// </summary>
        private IDictionary properties;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="T:TestCaseData"/> class.
        /// </summary>
        /// <param name="arg1">The first argument</param>
        /// <param name="args">The remaining arguments.</param>
        public TestCaseData(object arg1, params object[] args)
        {
            this.arguments = new object[args.Length + 1];
            arguments[0] = arg1;
            int index = 1;
            foreach (object arg in args)
                arguments[index++] = arg;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TestCaseData"/> class.
        /// </summary>
        /// <param name="arg">The argument.</param>
        public TestCaseData(object arg)
        {
            this.arguments = new object[] { arg };
        }
        #endregion

        #region ITestCaseData Members
        /// <summary>
        /// Gets the argument list to be provided to the test
        /// </summary>
        public object[] Arguments
        {
            get { return arguments; }
        }

        /// <summary>
        /// Gets the expected result
        /// </summary>
        public object Result
        {
            get { return result; }
        }

        /// <summary>
        ///  Gets the expected exception Type
        /// </summary>
        public Type ExpectedException
        {
            get { return expectedExceptionType; }
        }

        /// <summary>
        /// Gets the FullName of the expected exception
        /// </summary>
        public string ExpectedExceptionName
        {
            get { return expectedExceptionName; }
        }

        /// <summary>
        /// Gets the name to be used for the test
        /// </summary>
        public string TestName
        {
            get { return testName; }
        }

        /// <summary>
        /// Gets the description of the test
        /// </summary>
        public string Description
        {
            get { return description; }
        }
        #endregion

        #region Additional Public Properties
        /// <summary>
        /// Gets a list of categories associated with this test.
        /// </summary>
        public IList Categories
        {
            get
            {
                if (Properties[CATEGORIES] == null)
                    Properties[CATEGORIES] = new ArrayList();

                return (IList)Properties[CATEGORIES];
            }
        }

        /// <summary>
        /// Gets the property dictionary for this test
        /// </summary>
        public IDictionary Properties
        {
            get
            {
                if (properties == null)
                    properties = new ListDictionary();

                return properties;
            }
        }
        #endregion

        #region Fluent Instance Modifiers
        /// <summary>
        /// Sets the expected result for the test
        /// </summary>
        /// <param name="result">The expected result</param>
        /// <returns>A modified TestCaseData</returns>
        public TestCaseData Returns(object result)
        {
            this.result = result;
            return this;
        }

        /// <summary>
        /// Sets the expected exception type for the test
        /// </summary>
        /// <param name="exceptionType">Type of the expected exception.</param>
        /// <returns>The modified TestCaseData instance</returns>
        public TestCaseData Throws(Type exceptionType)
        {
            this.expectedExceptionType = exceptionType;
            this.expectedExceptionName = exceptionType.FullName;
            return this;
        }

        /// <summary>
        /// Sets the expected exception type for the test
        /// </summary>
        /// <param name="exceptionName">FullName of the expected exception.</param>
        /// <returns>The modified TestCaseData instance</returns>
        public TestCaseData Throws(string exceptionName)
        {
            this.expectedExceptionName = exceptionName;
            return this;
        }

        /// <summary>
        /// Sets the name of the test case
        /// </summary>
        /// <returns>The modified TestCaseData instance</returns>
        public TestCaseData SetName(string name)
        {
            this.testName = name;
            return this;
        }

        /// <summary>
        /// Sets the description for the test case
        /// being constructed.
        /// </summary>
        /// <param name="description">The description.</param>
        /// <returns>The modified TestCaseData instance.</returns>
        public TestCaseData SetDescription(string description)
        {
            this.description = description;
            return this;
        }

        /// <summary>
        /// Applies a category to the test
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public TestCaseData SetCategory(string category)
        {
            this.Categories.Add(category);
            return this;
        }

        /// <summary>
        /// Applies a named property to the test
        /// </summary>
        /// <param name="propName"></param>
        /// <param name="propValue"></param>
        /// <returns></returns>
        public TestCaseData SetProperty(string propName, string propValue)
        {
            this.Properties.Add(propName, propValue);
            return this;
        }

        /// <summary>
        /// Applies a named property to the test
        /// </summary>
        /// <param name="propName"></param>
        /// <param name="propValue"></param>
        /// <returns></returns>
        public TestCaseData SetProperty(string propName, int propValue)
        {
            this.Properties.Add(propName, propValue);
            return this;
        }

        /// <summary>
        /// Applies a named property to the test
        /// </summary>
        /// <param name="propName"></param>
        /// <param name="propValue"></param>
        /// <returns></returns>
        public TestCaseData SetProperty(string propName, double propValue)
        {
            this.Properties.Add(propName, propValue);
            return this;
        }
        #endregion
    }
}
