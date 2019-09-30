using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mev_lab5
{
    class OprCreator
    {
        private Opros opros;                //Опрос.
        private int RightClmnX;             //Координата правой колонны.
        private int MiddleClmnX;            //Координата центральной колонны.
        private int AnswCount;              //Количество ответов.
        private int AnswersYPos;            //Высота последнего бокса с ответом.
        private int SelectedQsn;            //Индекс предыдущего выбранного вопроса.
        private bool was_del;               //Флаг операции удаления вопроса.
        private bool OpenFromFile;

        //Объявление всех элементов управления.
        private Panel CreatorPanel = new Panel();                   //Основная панель создания вопросов.
        private Label QuestTextlabel = new Label();                 //Надпись "Текст вопроса".
        private TextBox QuestionBox = new TextBox();                //Текстовая зона для записи вопроса.
        private ListBox Questions = new ListBox();                  //Лист бокс с вопросами.
        private Label AnswTypeLabel = new Label();                  //Надпись "Выберите тип ответа".
        private ComboBox AnswTypeCB = new ComboBox();               //Выпадающий список с варинатами типа ответа.
        private Label AnswLabel = new Label();                      //Надпись "Варианты ответа".
        private TableLayoutPanel AnswersP = new TableLayoutPanel(); //Вспомогательная панель для записи вариантов ответа.
        private List<TextBox> Answers = new List<TextBox>();        //Список ответов.
        private Button AddQuestion = new Button();                  //Кнопка добавления вопроса.
        private Button DelQuestion = new Button();                  //Кнопка удаления вопроса.

        public OprCreator(ref Opros opros)
        {
            this.opros = opros;
            AnswCount = 0;
            AnswersYPos = -20;
            MiddleClmnX = 340;
            RightClmnX = 460;
            SelectedQsn = 0;
            was_del = false;
            OpenFromFile = false;
        }

        public Panel CreateCrPanel(bool IsReady) //Создание панели создания вопроса в главном окне программы.
        {

            //Инициализация панели.
            CreatorPanel.SetBounds(13, 28, 775, 410);
            CreatorPanel.BorderStyle = BorderStyle.FixedSingle;

            //Инициализация элементов управления.

            //Текст вопроса. (Надпись)

            QuestTextlabel.SetBounds(RightClmnX, 90, 90, 15);
            QuestTextlabel.Text = "Текст вопроса:";
            CreatorPanel.Controls.Add(QuestTextlabel);

            //Текст вопроса. (TextBox)

            QuestionBox.Multiline = true;
            QuestionBox.ScrollBars = ScrollBars.Vertical;
            QuestionBox.WordWrap = true;
            QuestionBox.SetBounds(RightClmnX, 110, 255, 75);
            QuestionBox.Text = "Введите вопрос...";
            QuestionBox.ForeColor = Color.Gray;
            QuestionBox.Enter += (sender, e) =>
            {
                if (QuestionBox.Text == "Введите вопрос...")
                {
                    QuestionBox.ForeColor = Color.Black;
                    QuestionBox.Text = "";
                }
            };
            QuestionBox.Leave += (sender, e) =>
            {
                if (String.IsNullOrWhiteSpace(QuestionBox.Text))
                {
                    QuestionBox.ForeColor = Color.Gray;
                    QuestionBox.Text = "Введите вопрос...";
                }
            };
            CreatorPanel.Controls.Add(QuestionBox);

            //Список вопросов.

            Questions.SetBounds(30, 15, 300, 380);
            Questions.SelectedIndexChanged += (sender, e) =>
            {
                //Сохраняем изменения в текущем вопросе.
                if (!OpenFromFile)
                    SaveQstn();
                UpdateP();
                if (Questions.SelectedIndex == -1) StandartP();
            };
            CreatorPanel.Controls.Add(Questions);

            //Тип ответа. (Надпись)

            AnswTypeLabel.SetBounds(RightClmnX, 15, 90, 30);
            AnswTypeLabel.Text = "Выберите тип ответа:";
            CreatorPanel.Controls.Add(AnswTypeLabel);

            //Тип ответа. (Выпадающий список)

            AnswTypeCB.SetBounds(RightClmnX, 50, 205, 50);
            AnswTypeCB.FormattingEnabled = true;
            AnswTypeCB.DropDownStyle = ComboBoxStyle.DropDownList;
            AnswTypeCB.Items.AddRange(new object[]
                {
                    "С одним вариантом ответа",
                    "С несколькими вариантами ответа",
                    "Свободный ответ"
                });
            CreatorPanel.Controls.Add(AnswTypeCB);

            //Варианты ответа. (Надпись)

            AnswLabel.SetBounds(RightClmnX, 195, 100, 15);
            AnswLabel.Text = "Варианты ответа:";
            CreatorPanel.Controls.Add(AnswLabel);

            //////////////////////////////////////////////////////////////

            //Инициализация дополнительной панели с вариантами ответа.

            AnswersP.SetBounds(RightClmnX - 5, 215, 280, 190);
            AnswersP.HorizontalScroll.Enabled = false;
            AnswersP.HorizontalScroll.Visible = false;
            AnswersP.VerticalScroll.Enabled = true;
            AnswersP.AutoScroll = true;
            AnswersP.BorderStyle = BorderStyle.None;
            CreatorPanel.Controls.Add(AnswersP);

            //Инициализация элементов аправления дополнительной панели
            //с вариантами ответа.

            //Инициализация списка ответов.

            Answers.Add(NewAnswer());
            AnswersP.Controls.Add(Answers[AnswCount++]);

            //////////////////////////////////////////////////////

            //Инициализация кнопок.

            //Кнопка добвления вопроса.

            AddQuestion.SetBounds(MiddleClmnX, 110, 110, 25);
            AddQuestion.BackColor = Color.LightGray;
            AddQuestion.Text = "Добавить вопрос";
            AddQuestion.Click += (sender, e) =>
            {
                //Сохраняем текущий вопрос.
                SaveQstn();
                opros.questions.Add(new Question("", QuestionType.Answers, Answers));
                Questions.Items.Add("Вопрос " + (Questions.Items.Count + 1).ToString());
                if (Questions.Items.Count > 0)
                    Questions.SelectedIndex = Questions.Items.Count - 1;
                else Questions.SelectedIndex = 0;

                //Приводим основные поля в стандартное положение.
                StandartP();
            };
            CreatorPanel.Controls.Add(AddQuestion);

            //Кнопка удаления вопроса.

            DelQuestion.SetBounds(MiddleClmnX, 145, 110, 25);
            DelQuestion.BackColor = Color.LightGray;
            DelQuestion.Text = "Удалить вопрос";
            CreatorPanel.Controls.Add(DelQuestion);
            DelQuestion.Click += (sender, e) =>
            {
                if (Questions.SelectedIndex > -1)//Проверка на то выбран ли какой-либо вопрос.
                {
                    was_del = true;
                    SelectedQsn = Questions.SelectedIndex;
                    opros.questions.RemoveAt(SelectedQsn);
                    Questions.Items.RemoveAt(SelectedQsn);
                }
                was_del = false;
            };

            if (IsReady == true)
                FillFromReady();

            return CreatorPanel;
        }

        private TextBox NewAnswer(string answstr = " Введите вариант ответа...")
        {
            TextBox Answ = new TextBox();
            AnswersYPos += 50;
            Answ.SetBounds(5, AnswersYPos, 255, 40);
            Answ.Multiline = true;
            Answ.ScrollBars = ScrollBars.Vertical;
            Answ.WordWrap = true;
            Answ.Text = answstr;
            if (answstr == " Введите вариант ответа...")
                Answ.ForeColor = Color.Gray;
            else Answ.ForeColor = Color.Black;
            Answ.Enter += (sender, e) =>
            {
                if ((sender as TextBox).Text == " Введите вариант ответа...")
                {
                    (sender as TextBox).ForeColor = Color.Black;
                    (sender as TextBox).Text = "";
                }
                if (Answers.Count > 1)
                {
                    if (Answers[AnswCount - 1].Text != " Введите вариант ответа...")
                    {
                        Answers.Add(NewAnswer());
                        AnswersP.Controls.Add(Answers[AnswCount++]);
                    }
                }
                else
                {
                    Answers.Add(NewAnswer());
                    AnswersP.Controls.Add(Answers[AnswCount++]);
                }
            };
            Answ.Leave += (sender, e) =>
            {
                if (String.IsNullOrWhiteSpace((sender as TextBox).Text))
                {
                    int LastAnswIndx = Answers.Count - 1;
                    if ((sender as TextBox) == Answers[LastAnswIndx - 1]) //Если этот ТекстБокс был последним.
                    {
                        (sender as TextBox).ForeColor = Color.Gray;
                        (sender as TextBox).Text = " Введите вариант ответа...";
                        AnswersP.Controls.Remove(Answers[LastAnswIndx]);
                        Answers.RemoveAt(LastAnswIndx);
                        AnswCount--;
                        AnswersYPos -= 50;
                    }
                    else
                    {
                        AnswersP.Controls.Remove((sender as TextBox));
                        Answers.RemoveAt(Answers.IndexOf((sender as TextBox)));
                        AnswCount--;
                        AnswersYPos -= 50;
                    }
                    if (Answers.Count < 4)
                    {
                        AnswersP.AutoScroll = false;
                        AnswersP.AutoScroll = true;
                    }
                }
            };
            return Answ;
        }

        private void StandartP() //Приводит панель в стандартный вид.
        {
            Answers.Clear();
            AnswersP.Controls.Clear();
            AnswCount = 0;
            Answers.Add(NewAnswer());
            AnswersP.Controls.Add(Answers[AnswCount++]);
            QuestionBox.ForeColor = Color.Gray;
            QuestionBox.Text = "Введите вопрос...";
            AnswTypeCB.SelectedIndex = -1;
        }

        private void SaveQstn() //Сохраняет вопрос.
        {
            if (!was_del)
                if (Questions.Items.Count > 0)//Если есть вопросы.
                {
                    opros.questions[SelectedQsn].QuestionText = QuestionBox.Text; //Сохраняем текст вопроса.
                    opros.questions[SelectedQsn].SaveAnswers(Answers);            //Сохраняем ответы.
                    switch (AnswTypeCB.SelectedIndex)                       //Сохраняем тип ответа.
                    {
                        case 0:
                            opros.questions[SelectedQsn].questionType = QuestionType.OneAnswer;
                            break;
                        case 1:
                            opros.questions[SelectedQsn].questionType = QuestionType.Answers;
                            break;
                        case 2:
                            opros.questions[SelectedQsn].questionType = QuestionType.FreeAnswer;
                            break;
                    }
                }
        }

        private void UpdateP() //Обновляет панель.
        {
            int i = Questions.SelectedIndex;
            if (Questions.Items.Count > 0)
            {
                //Если предыдущая операция была удалением.
                if (was_del)
                    if (SelectedQsn > 0)
                        i = Questions.SelectedIndex = SelectedQsn - 1;
                    else i = Questions.SelectedIndex = 0;

                //Загрузка полей соотвествующего вопроса.
                QuestionBox.Text = opros.questions[i].QuestionText;
                if (opros.questions[i].QuestionText == "Введите вопрос...")
                    QuestionBox.ForeColor = Color.Gray;
                else QuestionBox.ForeColor = Color.Black;
                Answers.Clear();
                AnswersP.Controls.Clear();
                AnswCount = 0;
                foreach (var str in opros.questions[i].Answers)
                {
                    Answers.Add(NewAnswer(str));
                    AnswersP.Controls.Add(Answers[AnswCount++]);
                }
                AnswTypeCB.SelectedIndex = (int)opros.questions[i].questionType;

                //Запоминаем индекс последнего выбранного вопроса.
                SelectedQsn = Questions.SelectedIndex;
            }
        }

        private void FillFromReady()
        {
            OpenFromFile = true;
            for (int i = 0; i < opros.questions.Count; i++)
            {
                Questions.Items.Add("Вопрос " + (i + 1).ToString());
                Questions.SelectedIndex = i;
            }
            OpenFromFile = false;
        }
    }
}
