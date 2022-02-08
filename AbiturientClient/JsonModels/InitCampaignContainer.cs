using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbiturientClient.JsonModels
{
    public class InitCampaignContainer
    {
        public List<Department> Departments { get; set; }
        public List<Exam> Exams { get; set; }
        public List<Specialty> Specialties { get; set; }

        public InitCampaignContainer(List<Department> departments, List<Exam> exams, List<Specialty> specialties)
        {
            Departments = departments;
            Exams = exams;
            Specialties = specialties;
        }
    }
}
