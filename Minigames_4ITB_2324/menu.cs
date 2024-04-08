using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minigames_4ITB_2324
{
    public partial class menu : Form
    {
        public menu()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<Type> Games = new List<Type>();
            foreach (var item in checkedListBox1.CheckedItems)
            {
                Type itemType = (Type)item;
                Games.Add(Assembly.LoadFrom(itemType.Assembly.Location).GetType(itemType.FullName!)!);
            }
            if (Games.Count > 0)
            {
                new Form1(Games);
                this.Hide();
            }
            else
            {
                MessageBox.Show("Vyberte hru prosím", "dialog", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Dll files (*.dll)|*.dll";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                Assembly assembly = Assembly.LoadFrom(filePath);

                foreach (Type type in assembly.GetTypes())
                {
                    foreach (Type interfaceType in type.GetInterfaces())
                    {
                        if (interfaceType == typeof(IMinigame))
                        {
                            checkedListBox1.Items.Add(type);
                        }
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            checkedListBox1.Items.Clear();
        }
    }
}
