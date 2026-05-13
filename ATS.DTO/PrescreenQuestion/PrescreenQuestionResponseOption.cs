using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.DTO.PrescreenQuestion
{
    public class PrescreenQuestionResponseOption
    {
        public int Id { get; set; }
        public int PrescreenQuestionId { get; set; }
        public int Order { get; set; }
        public string ResponseText { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsCorrectAnswer { get; set; }
    }

    public enum PrescreenQuestionDisplayType
    {
        MultipleChoice = 1,
        Text = 2
    }
}
