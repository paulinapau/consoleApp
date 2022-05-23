using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Web;
using System.Web.Script.Serialization;

namespace ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Meeting> allmeetings = new List<Meeting>();

            bool Continue = true;

            while (Continue)
            {
                Continue = false;
                string input = "";
                Console.WriteLine("If you want to create new meeting write:" +
                    "New meeting, Name of the meeting, Responsible Person, Description of the meeting, Category, Is the meeting in person? (true/false), StartDate, EndDate\n");

                Console.WriteLine("If you want to delete a meeting write: " +
                   "Delete meeting, your Name, name of the meeting\n");

                Console.WriteLine("If you want to add a person to the meeting write: " +
                    "Add person, name of the person, time person is joining the meeting, name of the meeting\n");
                Console.WriteLine("If you want to remove person from the meeting write: " +
                    "Remove person, your name, name of the meeting, person that needs to be removed\n");

                Console.WriteLine("If you want to filter all meetings write :" +
                    "Filter by,  Description/Responsible person/Category/InPerson/Dates/Number of attendees, value of that type");

                Console.WriteLine("Information must be seperated by commas\n");
                input = Console.ReadLine();
                string[] inputs = input.Split(',');
                if (inputs[0].Equals("New meeting"))
                {
                    Meeting newMeeting = new Meeting(inputs[1], inputs[2], inputs[3], inputs[4],
                    Convert.ToBoolean(inputs[5]), Convert.ToDateTime(inputs[6]), Convert.ToDateTime(inputs[7]));
                    for (int i = 8; i < inputs.Length; i++)
                    {
                        newMeeting.AddPerson(inputs[i], Convert.ToDateTime(inputs[6]));
                    }
                    allmeetings.Add(newMeeting);
                    var json = new JavaScriptSerializer().Serialize(newMeeting);
                    File.AppendAllText("../../app_Data/output.json", json);
                }

                else if (inputs[0].Equals("Delete meeting"))
                {
                    foreach (Meeting newMeeting in allmeetings)
                    {
                        if (newMeeting.Name.Equals(inputs[2]) && newMeeting.ResponsiblePerson.Equals(input[1]))
                        {
                            allmeetings.Remove(newMeeting);
                            break;
                        }
                    }
                }

                else if (inputs[0].Equals("Add person"))
                {
                    foreach (Meeting newMeeting in allmeetings)
                    {
                        if (newMeeting.Name.Equals(inputs[3]))
                        {
                            newMeeting.AddPerson(inputs[1], Convert.ToDateTime(inputs[2]));
                        }
                        var json = new JavaScriptSerializer().Serialize(newMeeting);
                        File.AppendAllText("../../app_Data/output.json", json);
                    }
                }

                else if (inputs[0].Equals("Remove person"))
                {
                    foreach (Meeting newMeeting in allmeetings)
                    {
                        if (newMeeting.Name.Equals(inputs[2]) && !newMeeting.ResponsiblePerson.Equals(inputs[1]))
                        {
                            newMeeting.AddPerson(inputs[3], Convert.ToDateTime(inputs[2]));
                        }
                    }
                }

                else if (inputs[0].Equals("Filter by:"))
                {
                    List<Meeting> newlist = new List<Meeting>();
                    if (inputs[1].Equals("ResponsiblePerson"))
                        foreach (Meeting newMeeting in allmeetings)
                        {
                            if (newMeeting.ISResponsiblePerson(inputs[2]))
                                newlist.Add(newMeeting);
                        }
                    if (inputs[1].Equals("Category"))
                        foreach (Meeting newMeeting in allmeetings)
                        {
                            if (newMeeting.ISCategory(inputs[2]))
                                newlist.Add(newMeeting);
                        }
                    if (inputs[1].Equals("Inperson"))
                        foreach (Meeting newMeeting in allmeetings)
                        {
                            if (newMeeting.ISInPerson(inputs[2]))
                                newlist.Add(newMeeting);
                        }
                    if (inputs[1].Equals("Number of attendees"))
                        foreach (Meeting newMeeting in allmeetings)
                        {
                            if (newMeeting.IsMeetingMembers(Convert.ToInt32(inputs[2])))
                                newlist.Add(newMeeting);
                        }
                    if (inputs[1].Equals("Date"))
                        foreach (Meeting newMeeting in allmeetings)
                        {
                            if (newMeeting.IsMeetingMembers(Convert.ToInt32(inputs[2])))
                                newlist.Add(newMeeting);
                        }
                }

                Console.WriteLine("Do you want to continue? (YES/NO)\n");
                string confim = Console.ReadLine();
                if (confim.Equals("NO"))
                    break;
                else if (confim.Equals("YES"))
                {
                    Continue = true;
                }

            }
        }
    }
}
