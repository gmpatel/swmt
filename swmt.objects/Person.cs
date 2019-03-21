using System;

namespace Swmt.Objects
{
    public class Person 
    {
        public int Id { get; set; }
        
        public string First { get; set; }
        
        public string Last { get; set; }
        
        public int Age { get; set; }

        public Gender Gender { get; set; }

        public override string ToString() {
            return string.Format(
                "{0} | {1} | {2} | {3} | {4}", 
                Id, 
                First, 
                Last, 
                Age, 
                Gender
            );
        }
    }
}