using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mev_lab5
{
    enum QuestionType
    {
        OneAnswer,
        Answers,
        FreeAnswer
    }

    [Serializable]
    class Question
    {
        public string QuestionText { get; set; }
        public List<string> Answers { get; set; }
        public QuestionType questionType { get; set; }

        public Question()
        {
            QuestionText = "";
            Answers = new List<string>();
            questionType = QuestionType.OneAnswer;
        }

        public Question(string QuestionText, QuestionType AnswerType, List<System.Windows.Forms.TextBox> AnsBoxes)
        {
            this.QuestionText = QuestionText;
            this.questionType = questionType;
            Answers = new List<string>();
            foreach (var answ in AnsBoxes) //Перенносим ответы из боксов в список ответов.
                Answers.Add(answ.Text);
        }

        public void SaveAnswers(List<System.Windows.Forms.TextBox> AnsBoxes)
        {
            Answers.Clear();
            foreach (var answ in AnsBoxes) //Перенносим ответы из боксов в список ответов.
                Answers.Add(answ.Text);
        }

    }
}
