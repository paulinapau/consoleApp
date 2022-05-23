using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class Meeting
    {
        public string Name { get; set; }
        public string ResponsiblePerson { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public bool InPerson { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Dictionary<String, DateTime> WorkerAndJoinTime { get; set; }

        public Meeting(string name, string responsiblePerson, string description, string category, bool inPerson, DateTime startDate, DateTime endDate)
        {
            Name = name;
            ResponsiblePerson = responsiblePerson;
            Description = description;
            Category = category;
            InPerson = inPerson;
            StartDate = startDate;
            EndDate = endDate;
            WorkerAndJoinTime = new Dictionary<String, DateTime>();
        }


        public void AddPerson(string person, DateTime joinTime)
        {
            this.WorkerAndJoinTime.Add(person, joinTime);
        }
        public void RemovePerson(string person)
        {
            this.WorkerAndJoinTime.Remove(person);
        }

        public bool ISResponsiblePerson(string value)
        {
            if (this.ResponsiblePerson.Equals(value))
                return true;
            else
                return false;
        }
        public bool ISCategory(string value)
        {
            if (this.Category.Equals(value))
                return true;
            else
                return false;
        }
        public bool ISInPerson(string value)
        {
            if (this.InPerson.Equals(value))
                return true;
            else
                return false;
        }
        public bool IsMeetingMembers(int value)
        {
            if (this.WorkerAndJoinTime.Count > value)
                return true;
            else
                return false;
        }
        public override string ToString()
        {
            return string.Format("{0, -20} {1,-15}", Name, ResponsiblePerson);
        }
    }
}
