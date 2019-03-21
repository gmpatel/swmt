using System;
using Xunit;
using Swmt.Concretes.Helpers;
using Swmt.Objects;
using System.IO;

namespace Swmt.Tests.Concretes
{
    public class TestsHelpersSettings
    {
        public TestsHelpersSettings()
        {
            // Test class level setup             
        }

        [Fact]
        public void TestSettingsReturnsNullIfJSONSourceNotSetForGivenObject()
        {
            // Setup // Target Object >> Swmt.Objects.Gender
            var expectedVarName = "SWMT_JSON_FILE_DATA_SOURCE_GENDER";
            var expectedValue = default(string);

            // Perform
            var value = Settings.GetJsonFileDataSourceFromEnvironmentVariable<Gender>(out string varName);
            Console.WriteLine("{0}, {1}", value ?? "NULL", varName ?? "NULL");
            
            // Assertions
            Assert.Equal(expectedValue, value);
            Assert.Equal(expectedVarName, varName);
        }

        [Fact]
        public void TestSettingsReturnsCorrectValueIfJSONSourceIsSetForGivenObject()
        {
            // Setup // Target Object >> Swmt.Objects.Person
            var expectedVarName = "SWMT_JSON_FILE_DATA_SOURCE_FILEINFO";
            var expectedValue = "people.json";

            Environment.SetEnvironmentVariable(expectedVarName, expectedValue);

            // Perform
            var value = Settings.GetJsonFileDataSourceFromEnvironmentVariable<FileInfo>(out string varName);
            Console.WriteLine("{0}, {1}", value ?? "NULL", varName ?? "NULL");
            
            // Assertions
            Assert.Equal(expectedValue, value);
            Assert.Equal(expectedVarName, varName);
        }
    }
}
