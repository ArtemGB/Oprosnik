using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mev_lab5
{
    class OprFill
    {
        //Класс, предназначенный для создания форм прохождения опросов, сбора статистики.
        private Opros opros;        //Опрос.
        private Panel WorkPanel;    //Рабочая панель.
        private int i;              //Индекс текущего вопроса.

        public OprFill(ref Opros opros, ref Panel StartPanel)
        {
            this.opros = opros;
            this.WorkPanel = StartPanel;
            i = 0;
        }

        public void StartPanel() //Панель приветствия.
        {
            GroupBox StartGB = new GroupBox(); //ГБ для стартовой панели.
            Button StartB = new Button();      //Кнопка начала опроса.
            TextBox NameTB = new TextBox();    //Текстбокс для имени репсондента.
            TextBox SecNameTB = new TextBox(); //Текстбокс для фамилии респондента.
            Label NameL = new Label();
            Label SecNameL = new Label();

            WorkPanel.SetBounds(0, 0, 816, 489);
            StartGB.SetBounds(100, 50, 600, 300);
            StartGB.Text = "Здравствуйте, введите ваши данные";

            NameL.SetBounds(200, 60, 100, 30);
            NameL.Text = "Введите ваше имя:";
            StartGB.Controls.Add(NameL);

            NameTB.SetBounds(200, 90, 150, 30);
            StartGB.Controls.Add(NameTB);

            SecNameL.SetBounds(200, 120, 100, 30);
            SecNameL.Text = "Введите вашу фамилию:";
            StartGB.Controls.Add(SecNameL);

            SecNameTB.SetBounds(200, 150, 150, 30);
            StartGB.Controls.Add(SecNameTB);

            StartB.SetBounds(200, 190, 150, 30);
            StartB.Text = "Начать опрос.";
            StartB.Click += (sender, e) =>
            {
                if (!String.IsNullOrWhiteSpace(NameTB.Text) && !String.IsNullOrWhiteSpace(SecNameTB.Text))
                    opros.Resps.Add(new Respondent(NameTB.Text, SecNameTB.Text));
                else MessageBox.Show("Введены не все данные.");
                for (int k = 0; k < opros.questions.Count; k++) //Здесь создаём массивы для запоминания ответов.
                {
                    if (opros.questions[k].questionType != QuestionType.FreeAnswer)
                        opros.Resps[opros.Resps.Count - 1].Answs.Add(new bool[opros.questions[k].Answers.Count - 1]);
                    else
                    {
                        opros.Resps[opros.Resps.Count - 1].Answs.Add(null);
                        opros.Resps[opros.Resps.Count - 1].FreeAnsws.Add(k, "Введите ваш ответ...");
                    }
                }
                WorkPanel.Controls.Clear();
                WorkPanel.Controls.Add(QuestPanel());
            };
            StartGB.Controls.Add(StartB);

            WorkPanel.Controls.Add(StartGB);
        }

        private void EndPanel() //Панель прощания.
        {
            GroupBox EndGB = new GroupBox();
            Label EndL = new Label();

            WorkPanel.SetBounds(0, 0, 816, 489);
            EndGB.SetBounds(100, 50, 600, 300);

            EndL.SetBounds(250, 130, 100, 40);
            EndL.Text = "Благодарим вас за прохождение опроса.";
            EndGB.Controls.Add(EndL);

            WorkPanel.Controls.Add(EndGB);
        
        }

        private Panel QuestPanel() //Панель с вопросом.
        {
            Panel QuestPanel = new Panel();                     //Панель с вопросом и всеми элементами управления.
            Button BackB = new Button(), NextB = new Button();  //Кнопки назад и вперёд.

            QuestPanel.SetBounds(10, 10, 800, 400);

            QuestPanel.Controls.Add(QuestGbox());

            //Кнопка возврата к предыдущему вопросу.
            BackB.SetBounds(120, 370, 130, 20);
            BackB.Text = "Предыдущий вопрос";
            BackB.Click += (sender, e) =>
            {
                if (i > 0) //Если вопрос не первый.
                {
                    i--;
                    QuestPanel.Controls.Remove(QuestPanel.Controls[QuestPanel.Controls.IndexOfKey("QuestGbox")]);
                    QuestPanel.Controls.Add(QuestGbox());
                }
            };
            QuestPanel.Controls.Add(BackB);

            //Кнопка перехода к следующему вопросу.
            NextB.SetBounds(550, 370, 130, 20);
            NextB.Text = "Следующий вопрос";
            NextB.Click += (sender, e) =>
            {
                if (i < opros.questions.Count - 1) //Если вопрос не последний.
                {
                    i++;
                    QuestPanel.Controls.Remove(QuestPanel.Controls[QuestPanel.Controls.IndexOfKey("QuestGbox")]);
                    QuestPanel.Controls.Add(QuestGbox());
                }
                else //Если вопрос был последни, то проверям, на все ли вопросы ответил пользователь.
                {
                    bool AreAllAnswered = false;
                    foreach (var Answ in opros.Resps[opros.Resps.Count - 1].Answs)
                    {
                        AreAllAnswered = false;
                        if (Answ != null)
                            foreach (var CurrentAnsw in Answ)
                                if (CurrentAnsw)
                                    AreAllAnswered = true;
                    }
                    foreach (var FreeAnsw in opros.Resps[opros.Resps.Count - 1].FreeAnsws)
                    {
                        AreAllAnswered = false;
                        if (FreeAnsw.Value != "Введите ваш ответ..." && !String.IsNullOrWhiteSpace(FreeAnsw.Value))
                            AreAllAnswered = true;
                    }
                    if (!AreAllAnswered)
                        MessageBox.Show("Вы ответили не на все вопросы.");
                    else
                    {
                        WorkPanel.Controls.Clear();
                        EndPanel();
                    }
                }
            };
            QuestPanel.Controls.Add(NextB);

            return QuestPanel;
        }

        private GroupBox QuestGbox() //Панель с вопросом и вариантами ответа.
        {
            GroupBox QuestGbox = new GroupBox();                //Зона с вопросами и ответами.
            List<CheckBox> MoreAnsws = new List<CheckBox>();    //Для вопросов с несколькими варинатами ответа.
            List<RadioButton> OneAnsw = new List<RadioButton>();//Для вопросов с одним варинатом ответа.
            TextBox FreeAnsw = new TextBox();                   //Для вопроса со свободным ответом.
            Label QuestionL = new Label();                      //Текст вопроса.

            QuestGbox.SetBounds(100, 50, 600, 300);
            QuestGbox.Name = "QuestGbox";
            QuestGbox.Text = "Вопрос " + (i + 1).ToString() + " из " + opros.questions.Count.ToString();

            QuestionL.SetBounds(10, 20, 300, 30);
            QuestionL.Text = opros.questions[i].QuestionText;
            QuestGbox.Controls.Add(QuestionL);

            int AnswY = 0;
            switch (opros.questions[i].questionType)
            {
                case QuestionType.OneAnswer:
                    for (int j = 0; j < opros.questions[i].Answers.Count - 1; j++)
                    {
                        OneAnsw.Add(new RadioButton());
                        OneAnsw[j].Text = opros.questions[i].Answers[j];
                        OneAnsw[j].SetBounds(10, 60 + AnswY, 300, 20);
                        OneAnsw[j].Click += (sender, e) =>
                        {
                            for (int k = 0; k < OneAnsw.Count; k++)
                            {
                                if (OneAnsw[k].Checked)
                                    opros.Resps[opros.Resps.Count - 1].Answs[i][k] = true;
                            }
                        };
                        for (int k = 0; k < OneAnsw.Count; k++)
                        {
                            if (opros.Resps[opros.Resps.Count - 1].Answs[i][k])
                                OneAnsw[k].Checked = true;
                        }
                        QuestGbox.Controls.Add(OneAnsw[OneAnsw.Count - 1]);
                        AnswY += 20;
                    }
                    break;
                case QuestionType.Answers:
                    for (int j = 0; j < opros.questions[i].Answers.Count - 1; j++)
                    {
                        MoreAnsws.Add(new CheckBox());
                        MoreAnsws[j].Text = opros.questions[i].Answers[j];
                        MoreAnsws[j].SetBounds(10, 60 + AnswY, 300, 20);
                        MoreAnsws[j].Click += (sender, e) =>
                        {
                            for (int k = 0; k < MoreAnsws.Count; k++)
                            {
                                if (MoreAnsws[k].Checked)
                                    opros.Resps[opros.Resps.Count - 1].Answs[i][k] = true;
                            }
                        };
                        for (int k = 0; k < MoreAnsws.Count; k++)
                        {
                            if (opros.Resps[opros.Resps.Count - 1].Answs[i][k])
                                MoreAnsws[k].Checked = true;
                        }
                        QuestGbox.Controls.Add(MoreAnsws[j]);
                        AnswY += 20;
                    }
                    break;
                case QuestionType.FreeAnswer:
                    FreeAnsw.SetBounds(10, 60, 300, 100);
                    FreeAnsw.Multiline = true;
                    FreeAnsw.Enter += (sender, e) =>
                    {
                        if (FreeAnsw.Text == "Введите ваш ответ...")
                        {
                            FreeAnsw.Text = "";
                            FreeAnsw.ForeColor = Color.Black;
                        }
                    };
                    FreeAnsw.Leave += (sender, e) =>
                    {
                        opros.Resps[opros.Resps.Count - 1].FreeAnsws[i] = FreeAnsw.Text;
                    };
                    FreeAnsw.Text = opros.Resps[opros.Resps.Count - 1].FreeAnsws[i];
                    if (FreeAnsw.Text == "Введите ваш ответ...")
                        FreeAnsw.ForeColor = Color.Gray;
                    else if (String.IsNullOrWhiteSpace(FreeAnsw.Text))
                    {
                        FreeAnsw.Text = "Введите ваш ответ...";
                        FreeAnsw.ForeColor = Color.Gray;
                    }
                    QuestGbox.Controls.Add(FreeAnsw);
                    break;
                default:
                    break;
            }

            return QuestGbox;
        }
    }
}
