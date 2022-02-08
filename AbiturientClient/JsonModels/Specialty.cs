using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbiturientClient.JsonModels
{
    public class Specialty
    {
        public Specialty()
        {
            SpecialtyFormOfEducationCodes = new List<string>();
            SpecialtyContestCodes = new List<string>();
            SpecialtyExams = new List<SpecialtyExam>();
        }

        public string Name { get; set; } //название
        public string Code { get; set; } //код
        public string Cypher { get; set; } //шифр
        public string DepartmentCode { get; set; } //код подразделения (из выпадающего списка)
        public int BudgetPlaces { get; set; } //количество бюджетных мест
        public int EducationLevelId { get; set; } //id уровня образования (из выпадающего списка)
        public int CommercialPlaces { get; set; } //количество коммерческих мест
        public List<string> SpecialtyFormOfEducationCodes { get; set; } //коды форм обучения (из листбокса)
        public List<string> SpecialtyContestCodes { get; set; } //коды видов конкурса (из листбокса)
        public List<SpecialtyExam> SpecialtyExams { get; set; } //информация об экзаменах
    }
}
