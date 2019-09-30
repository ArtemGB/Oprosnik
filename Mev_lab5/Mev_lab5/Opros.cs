using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mev_lab5
{
    [Serializable]
    class Opros
    {
        public List<Question> questions { get; set; }    //Список вопросов. 
        public List<Respondent> Resps { get; set; }      //Список репсондетов.

        public Opros(List<Question> questions)
        {
            Resps = new List<Respondent>();
            this.questions = questions;
        }

    }
}
