using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mev_lab5
{
    public partial class MainForm : Form
    {
        private Panel WorkPanel; //Рабоачая панель
        private Opros opros;     //Опрос.
        private bool FileCreated;
        private string FileName;

        public MainForm()
        {
            InitializeComponent();
            FileCreated = false;
            WorkPanel = new Panel();
        }

        private void OpenFileMI_Click(object sender, EventArgs e)//Вызывается при нажатии кнопки открытия файла.
        {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)//Вызывается при нажатии кнопки открытия файла.
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream(openFileDialog1.FileName, FileMode.Open))
                opros = (Opros)formatter.Deserialize(fs);
            FileName = openFileDialog1.FileName;

            this.Controls.Remove(WorkPanel);
            OprCreator oprCreator = new OprCreator(ref opros);
            WorkPanel = oprCreator.CreateCrPanel(true);
            this.Controls.Add(WorkPanel);
            FileCreated = true;
        }

        private void CreatFileMI_Click(object sender, EventArgs e)//Вызывается при нажатии кнопки создания файла
        {
            this.Controls.Remove(WorkPanel);
            opros = new Opros(new List<Question>());
            OprCreator oprCreator = new OprCreator(ref opros);
            WorkPanel = oprCreator.CreateCrPanel(false);
            this.Controls.Add(WorkPanel);
        }

        private void SaveAsMI_Click(object sender, EventArgs e)//Вызывается при нажатии кнопки "Сохранить как".
        {
            saveFileDialog1.ShowDialog();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)//Вызывается при нажатии кнопки "Сохранить как".
        {
            BinaryFormatter formatter = new BinaryFormatter();
            // получаем поток, куда будем записывать сериализованный объект
            using (FileStream fs = new FileStream(saveFileDialog1.FileName, FileMode.Create))
                formatter.Serialize(fs, opros);
            FileCreated = true;
            FileName = saveFileDialog1.FileName;
        }

        private void SaveFileMI_Click(object sender, EventArgs e)
        {
            if (FileCreated)
            {
                BinaryFormatter formatter = new BinaryFormatter();
                // получаем поток, куда будем записывать сериализованный объект
                using (FileStream fs = new FileStream(FileName, FileMode.Create))
                    formatter.Serialize(fs, opros);
            }
            else SaveAsMI_Click(sender, e);
        }

        private void OpenForUsingMI_Click(object sender, EventArgs e)
        {
            openFileDialog2.ShowDialog();
        }

        private void openFileDialog2_FileOk(object sender, CancelEventArgs e)
        {
            //Достаём опрос из файла.
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream(openFileDialog2.FileName, FileMode.Open))
                opros = (Opros)formatter.Deserialize(fs);
            FileName = openFileDialog2.FileName;
            FileCreated = true;

            //Очищаем и заполняем рабочую панель.
            this.Controls.Remove(WorkPanel);
            WorkPanel = new Panel();
            this.MainMenuStrip.Visible = false;
            OprFill oprFill = new OprFill(ref opros, ref WorkPanel);
            oprFill.StartPanel();
            this.Controls.Add(WorkPanel);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (FileCreated)
            {
                BinaryFormatter formatter = new BinaryFormatter();
                // получаем поток, куда будем записывать сериализованный объект
                using (FileStream fs = new FileStream(FileName, FileMode.Create))
                    formatter.Serialize(fs, opros);
            }
        }
    }
}
