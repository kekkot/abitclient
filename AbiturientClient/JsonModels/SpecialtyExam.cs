using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbiturientClient.JsonModels
{
    public partial class SpecialtyExam
    {
        public int ExamCode { get; set; } //код экзамена
        public int Priority { get; set; } //приоритет (номер строки с экзом)
        public int MinimalScore { get; set; } //минимальный проходной балл
    }
}
