using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.DTO.PrescreenQuestion
{
    public class ApplicationPreScreenQuestionResponse
    {
        public ApplicationPreScreenQuestionResponse()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public virtual Application.Application Application { get; set; }
        public virtual PrescreenQuestion PrescreenQuestion { get; set; }
        public virtual PrescreenQuestionResponseOption SelectedResponse { get; set; }
        public string CapturedResponseText { get; set; }
    }
}
