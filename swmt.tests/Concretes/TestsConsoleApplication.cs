using System;
using Xunit;
using Swmt.Objects;
using Swmt.Interfaces;
using Swmt.Concretes;
using Moq;

namespace Swmt.Tests.Concretes
{
    public class TestsConsoleApplication
    {
        public TestsConsoleApplication() 
        {
            // Test class level setup 
        }

        [Fact]
        public void TestRunBehaviorOfConsoleApplicationComponent()
        {
            // Setup 
            var mockDataSource = new Mock<IDataSource<Person>>();
            //mockDataSource /* Setup not require */
            //    .Setup(m => m.FirstOrDefault(null, null))
            //    .Returns(new Person {Id=1, First="Gunjan", Last="Patel", Age=22, Gender=Gender.Male});

            var mockOutput = new Mock<IOutput>();
            //mockOutput /* Setup not require */
            //    .Setup(m => m.WriteLine(It.IsAny<string>(), null));

            var consoleApplication = new ConsoleApplication(mockDataSource.Object, mockOutput.Object);

            // Perform
            consoleApplication.Run();

            // Assertions
            mockDataSource.Verify(m => m.FirstOrDefault("Id", 42), Times.Exactly(1));
            mockDataSource.Verify(m => m.FirstOrDefault("Id", 41), Times.Exactly(1));
            mockDataSource.Verify(m => m.Find("Id", 99), Times.Exactly(1));
            mockDataSource.Verify(m => m.Find("Id", 31), Times.Exactly(1));
            mockDataSource.Verify(m => m.Find("Age", 99), Times.Exactly(1));
            mockDataSource.Verify(m => m.Find("Age", 23), Times.Exactly(1));
            mockDataSource.Verify(m => m.Find("Age", 66), Times.Exactly(1));
            mockDataSource.Verify(m => m.All("Age"), Times.Exactly(1));

            mockOutput.Verify(m => m.WriteLine(It.IsAny<string>(), It.IsAny<object[]>()), Times.Exactly(8));
        }
    }
}
