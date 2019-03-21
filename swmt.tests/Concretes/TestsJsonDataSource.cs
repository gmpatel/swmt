using System;
using Xunit;
using Swmt.Objects;
using Swmt.Interfaces;
using Swmt.Concretes;
using Moq;

namespace Swmt.Tests.Concretes
{
    public class TestsJsonDataSource
    {
        public TestsJsonDataSource()
        {
            // Test class level setup 
            
            var environmentVarName = "SWMT_JSON_FILE_DATA_SOURCE_PERSON";
            var environmentVarValue = "./../../../../people.json";
            
            Environment.SetEnvironmentVariable(environmentVarName, environmentVarValue);
        }

        [Theory]
        [InlineData("Id", 31)]
        [InlineData("Id", 53)]
        [InlineData("Id", 41)]
        public void TestJsonDataSourceFirstOrDefault(string key, dynamic val)
        {
            // Setup 
            var jsonDataSource = new JsonDataSource<Person>();

            // Perform
            var person = jsonDataSource.FirstOrDefault(key, val);

            // Assertions
            Assert.NotNull(person);
            Assert.Equal(val, person.Id);
        }

        [Fact]
        public void TestJsonDataSourceAll()
        {
            // Setup 
            var jsonDataSource = new JsonDataSource<Person>();

            // Perform
            var people = jsonDataSource.All();

            // Assertions
            Assert.NotNull(people);
            Assert.Equal(7, people.Count);
        }

        [Theory]
        [InlineData("Age")]
        [InlineData("Id")]
        [InlineData("Gender")]
        public void TestJsonDataSourceAllByKey(string key)
        {
            // Setup 
            var jsonDataSource = new JsonDataSource<Person>();

            // Perform
            var people = jsonDataSource.All(key);

            // Assertions
            Assert.NotNull(people);

            switch(key) 
            {
                case "Age":
                    Assert.Equal(4, people.Count);
                    Assert.Equal(3, people["66"].Count);
                    Assert.Equal(1, people["12"].Count);
                    Assert.Equal(2, people["23"].Count);
                    break;

                case "Id":
                    Assert.Equal(5, people.Count);
                    Assert.Equal(3, people["31"].Count);
                    Assert.Equal(1, people["53"].Count);
                    Assert.Equal(1, people["62"].Count);
                    break;

                case "Gender":
                    Assert.Equal(3, people.Count);
                    Assert.Equal(3, people["Unknown"].Count);
                    Assert.Equal(2, people["Male"].Count);
                    Assert.Equal(2, people["Female"].Count);
                    break;
            }
        }

        [Theory]
        [InlineData("Age", 66)]
        [InlineData("Id", 31)]
        [InlineData("Gender", Gender.Male)]
        public void TestJsonDataSourceFindByKey(string key, dynamic val)
        {
            // Setup 
            var jsonDataSource = new JsonDataSource<Person>();

            // Perform
            var people = jsonDataSource.Find(key, val);

            // Assertions
            Assert.NotNull(people);

            switch(key) 
            {
                case "Age":
                    Assert.Equal(3, people.Count);
                    Assert.Equal(val, people[0].Age);
                    Assert.Equal(val, people[1].Age);
                    Assert.Equal(val, people[2].Age);
                    break;

                case "Id":
                    Assert.Equal(3, people.Count);
                    Assert.Equal(val, people[0].Id);
                    Assert.Equal(val, people[1].Id);
                    Assert.Equal(val, people[2].Id);
                    break;

                case "Gender":
                    Assert.Equal(2, people.Count);
                    Assert.Equal(val, people[0].Gender);
                    Assert.Equal(val, people[1].Gender);
                    break;
            }
        }
    }
}
