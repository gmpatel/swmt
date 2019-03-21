using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Swmt.Extras.Extensions;
using Swmt.Interfaces;
using Swmt.Objects;

namespace Swmt.Concretes 
{
    public class ConsoleApplication : IAppService
    {
        private readonly IDataSource<Person> dataSource;
        private readonly IOutput output;

        public ConsoleApplication(IDataSource<Person> dataSource, IOutput output) {
            this.dataSource = dataSource;
            this.output = output;
        }

        public void Run()
        {
            var tasks = new Task[] {
                Task.Factory.StartNew(async () => { await FirstFullNameById(42); }).Unwrap(),
                Task.Factory.StartNew(async () => { await FirstFullNameById(41); }).Unwrap(),
                Task.Factory.StartNew(async () => { await AllFullNamesById(31); }).Unwrap(),
                Task.Factory.StartNew(async () => { await AllFullNamesById(99); }).Unwrap(),
                Task.Factory.StartNew(async () => { await AllFirstNamesByAge(23); }).Unwrap(),
                Task.Factory.StartNew(async () => { await AllFirstNamesByAge(66); }).Unwrap(),
                Task.Factory.StartNew(async () => { await AllFirstNamesByAge(99); }).Unwrap(),
                Task.Factory.StartNew(async () => { await GenderCountByAgeAssending(); }).Unwrap()
            };

            Task.WaitAll(tasks);
        }

        public async Task<bool> FirstFullNameById(int id) 
        {
            var person = await Task.Run(() => dataSource.FirstOrDefault("Id", id));

            output.WriteLine("The first user's full name for id = {0}\n{1}\n", 
                id, 
                person != null ? person.GetFullName() : "Not Found!"
            );

            return true;
        }

        private async Task<bool> AllFullNamesById(int id) 
        {
            var people = await Task.Run(() => dataSource.Find("Id", id));
            var names = string.Empty;
    
            if (people != null) {
                foreach (var person in people) 
                    names = new StringBuilder().AppendFormat("{0}{1}\n", names, person.GetFullName()).ToString();
            }
            
            output.WriteLine("The all users' full name for id = {0} | Found = {1}\n{2}\n", 
                id, 
                people != null ? people.Count : 0,
                people != null ? names : "Not Found!"
            );

            return true;
        }

        private async Task<bool> AllFirstNamesByAge(int age) {
            var people = await Task.Run(() => dataSource.Find("Age", age));

            output.WriteLine("All the users first names (comma separated) who are {0} | Found = {1}\n{2}\n", 
                age, 
                people != null ? people.Count : 0,
                people != null ? string.Join(", ", people.Select(x => x.First)) : "Not Found!"
            );

            return true;
        }

        private async Task<bool> GenderCountByAgeAssending() {
            var dict = await Task.Run(() => dataSource.All("Age"));
            var text = string.Empty;

            if (dict != null) 
            {
                foreach (var element in dict) 
                    text = new StringBuilder().
                        AppendFormat(
                            "{0}Age: {1} Female: {2} Male: {3} Unknown: {4}\n", 
                            text,
                            element.Key, 
                            element.Value.Count(x=>x.Gender == Gender.Female), 
                            element.Value.Count(x=>x.Gender == Gender.Male),
                            element.Value.Count(x=>x.Gender == Gender.Unknown)
                    ).ToString();
            }

            output.WriteLine("The number of genders per Age, displayed from youngest to oldest | Found = {0}\n{1}\n", 
                dict != null ? dict.Count : 0,
                dict != null ? text : "Not Found!"
            );

            return true;
        }

        public void Dispose()
        {
            // Do ...
        }
    }
}