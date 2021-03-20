using System;
using System.Diagnostics;
using Xunit;
using Xunit.Abstractions;

namespace xUnitTests
{

    [Collection("TestGroup")]
    public class XUnitShowCaseTests : IClassFixture<ClassSharedTestFixture>, IDisposable
    {
        private readonly TestFixture _nonSharedFixture;

        private readonly ClassSharedTestFixture _classSharedFixture;

        private readonly GroupSharedFixture _groupSharedFixture;

        public XUnitShowCaseTests(ClassSharedTestFixture classSharedFixture, GroupSharedFixture groupSharedFixture)
        {
            //instance for each individual test run
            this._nonSharedFixture = new TestFixture();

            //shared instance between all tests in this class
            this._classSharedFixture = classSharedFixture;

            //shared between all tests in the same group
            this._groupSharedFixture = groupSharedFixture;
        }

        [Fact]
        public void BasicTestMethod()
        {
            Debug.WriteLine("Running test method");
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void InlineTheoryTestMethod(int number)
        {
            Debug.WriteLine($"Running theory test method with parameter: {number}");
        }

        [Fact(Skip = "This test is ignored.")]
        public void SkippedTest()
        {
            //This test will not run until the skip attribute is not removed
        }

        public void Dispose()
        {
            _nonSharedFixture.Dispose();
        }
    }

    [Collection("TestGroup")]
    public class OtherTestClass
    {
        private readonly GroupSharedFixture _groupSharedFixture;

        public OtherTestClass(GroupSharedFixture groupSharedFixture)
        {
            this._groupSharedFixture = groupSharedFixture;
        }

        [Fact]
        public void TestMethod()
        {
            Debug.WriteLine("Running test method from other test class");
        }
    }

    public class TestFixture : IDisposable
    {
        public TestFixture()
        {
            Debug.WriteLine("Creating single use test fixture instance");
        }

        public void Dispose()
        {
            Debug.WriteLine("Disposing single use test  fixture instance");
        }
    }

    public class ClassSharedTestFixture : IDisposable
    {
        public ClassSharedTestFixture()
        {
            Debug.WriteLine("Creating class shared test fixture instance");
        }

        public void Dispose()
        {
            Debug.WriteLine("Disposing class shared test fixture instance");
        }
    }
    public class GroupSharedFixture : IDisposable
    {
        public GroupSharedFixture()
        {
            Debug.WriteLine("Creating group shared test fixture instance");
        }

        public void Dispose()
        {
            Debug.WriteLine("Disposing group shared test fixture instance");
        }
    }

    [CollectionDefinition("TestGroup")]
    public class TestGroupTestCollection : ICollectionFixture<GroupSharedFixture> { }
}
