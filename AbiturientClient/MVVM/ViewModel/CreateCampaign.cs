using AbiturientClient.JsonModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AbiturientClient.MVVM.ViewModel
{
    class CreateCampaign : INotifyPropertyChanged
    {
        public static List<ExamForm> ExamForms;
        public static List<Contest> Contests;
        public static List<FormOfEducation> FormsOfEducation;
        public static List<EducationLevel> EducationLevels;
        public static List<Department> Departments;
        public static List<Exam> Exams;
        public static List<Specialty> Specialties;

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
