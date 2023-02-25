using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Project_3
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private Dictionary<string, string> InfoStore = new Dictionary<string, string>();
        private Dictionary<string, string> BirthStore = new Dictionary<string, string>();
        private Dictionary<string, string> GenderStore = new Dictionary<string, string>();
        private Dictionary<string, string> PositionStore = new Dictionary<string, string>();
        private Dictionary<string, string> IDStore = new Dictionary<string, string>();

        private void Form2_Load(object sender, EventArgs e)
        {
            try
            {
                FileName fn = new FileName();
                FileStream fs = new FileStream(FileName.fileName, FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fs);

                employeeInfo EI = new employeeInfo();
                int lines = 0;
                string reader;
                string fname;
                string data;

                while ((reader = sr.ReadLine()) != null)
                {
                    if (reader.Substring(0,reader.IndexOf(" ")) == "Name:")
                    {
                        EI.FullName = fname = reader.Substring(reader.IndexOf(" ") + 1);

                        EI.Name = EI.FullName.Substring(0,fname.IndexOf(" "));
                        EI.Surname = EI.FullName.Substring(fname.IndexOf(" ") + 1);

                        string name = EI.Name.Substring(0, 2);
                        string surname = EI.Surname.Substring(0, 3);
                        string param = name + surname;
                        

                        InfoStore.Add(param, EI.FullName);
                        RoleGender.AllParametres.Add(param);
                    }

                    else if (reader.Substring(0, reader.IndexOf(" ")) == "ID:")
                    {

                        EI.IDdata = data = reader.Substring(reader.IndexOf(" ") + 1);

                        EI.Parameter = EI.IDdata.Split('-')[0];
                        EI.BrokenDateOfBirth = EI.IDdata.Split('-')[1];
                        EI.ShortRole = EI.IDdata.Split('-')[2];
                        EI.ShortGender = EI.IDdata.Split('-')[3];
                        
                        string birth = EI.BrokenDateOfBirth.Insert(4, "-");
                        CorrectInfo.DateOfBirth = birth.Insert(7, "-");

                        switch (EI.ShortRole)
                        {
                            case "PM":
                                CorrectInfo.Role = RoleGender.Roles[3];
                                break;
                            case "SM":
                                CorrectInfo.Role = RoleGender.Roles[2];
                                break;
                            case "SD":
                                CorrectInfo.Role = RoleGender.Roles[1];
                                break;
                            case "JD":
                                CorrectInfo.Role = RoleGender.Roles[0];
                                break;
                        }

                        switch (EI.ShortGender)
                        {
                            case "M":
                                CorrectInfo.Gender = RoleGender.Gender[0];
                                break;
                            case "F":
                                CorrectInfo.Gender = RoleGender.Gender[1];
                                break;
                        }

                        BirthStore.Add(EI.Parameter, CorrectInfo.DateOfBirth);
                        PositionStore.Add(EI.Parameter, CorrectInfo.Role);
                        GenderStore.Add(EI.Parameter, CorrectInfo.Gender);

                        string paramForID = EI.Parameter.Substring(0, 4);
                        string birthYear = BirthStore[EI.Parameter].Substring(0, 4);

                        CorrectInfo.ID = EI.ShortRole + paramForID + birthYear;

                        IDStore.Add(EI.Parameter, CorrectInfo.ID);

                    }


                    lines++;
                }
                LabelAllRecords.Text = lines.ToString();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (InfoStore.ContainsValue(textBox1.Text))
            {
                listBox1.Items.Clear();
                string param = InfoStore.FindTheKey(textBox1.Text);
                listBox1.Items.Add(PositionStore[param] + " : " + InfoStore[param]);

            }
            else
            {
                MessageBox.Show("Employee does not exist");
            }
        }


        private void BtnSelectCombo_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex > -1)
            {

                switch (comboBox1.SelectedItem)
                {
                    case "All employees":
                        listBox1.Items.Clear();
                        for (int i = 0; i < RoleGender.AllParametres.Count; i++)
                        {
                            listBox1.Items.Add(PositionStore[RoleGender.AllParametres[i]] + " : " + InfoStore[RoleGender.AllParametres[i]]);
                        }
                        LabelRecords.Text = listBox1.Items.Count.ToString();
                        break;
                    case "Junior Developer":
                        listBox1.Items.Clear();
                        for (int i = 0; i < RoleGender.AllParametres.Count; i++)
                        {
                            if (PositionStore[RoleGender.AllParametres[i]] == RoleGender.Roles[0])
                            {
                                listBox1.Items.Add(PositionStore[RoleGender.AllParametres[i]] + " : " + InfoStore[RoleGender.AllParametres[i]]);
                            }
                        }
                        LabelRecords.Text = listBox1.Items.Count.ToString();
                        break;
                    case "Senior Developer":
                        listBox1.Items.Clear();
                        for (int i = 0; i < RoleGender.AllParametres.Count; i++)
                        {
                            if (PositionStore[RoleGender.AllParametres[i]] == RoleGender.Roles[1])
                            {
                                listBox1.Items.Add(PositionStore[RoleGender.AllParametres[i]] + " : " + InfoStore[RoleGender.AllParametres[i]]);
                            }
                        }
                        LabelRecords.Text = listBox1.Items.Count.ToString();
                        break;
                    case "Sales Manager":
                        listBox1.Items.Clear();
                        for (int i = 0; i < RoleGender.AllParametres.Count; i++)
                        {
                            if (PositionStore[RoleGender.AllParametres[i]] == RoleGender.Roles[2])
                            {
                                listBox1.Items.Add(PositionStore[RoleGender.AllParametres[i]] + " : " + InfoStore[RoleGender.AllParametres[i]]);
                            }
                        }
                        LabelRecords.Text = listBox1.Items.Count.ToString();
                        break;
                    case "Product Manager":
                        listBox1.Items.Clear();
                        for (int i = 0; i < RoleGender.AllParametres.Count; i++)
                        {
                            if (PositionStore[RoleGender.AllParametres[i]] == RoleGender.Roles[3])
                            {
                                listBox1.Items.Add(PositionStore[RoleGender.AllParametres[i]] + " : " + InfoStore[RoleGender.AllParametres[i]]);
                            }
                        }
                        LabelRecords.Text = listBox1.Items.Count.ToString();
                        break;
                }
            }
            else
            {
                MessageBox.Show("Please, first select the Categories");
            }
            
        }

        private void BtnSelectEmp_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBox1.SelectedItem == null)
                {
                    MessageBox.Show("Please, first select the Candidate!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {

                    int found = 0;
                    int indx = 0;
                    string[] selected = { listBox1.SelectedItem.ToString() };
                    foreach (string s in selected)
                    {
                        found = s.IndexOf(":");
                        string fullname = s.Substring(found + 2);

                        indx = fullname.IndexOf(" ");
                        CorrectInfo.Parameter = InfoStore.FindTheKey(fullname);
                        CorrectInfo.Role = PositionStore[CorrectInfo.Parameter];
                        CorrectInfo.Name = fullname.Substring(0, indx + 1);
                        CorrectInfo.Surname = fullname.Substring(indx + 1);
                        CorrectInfo.DateOfBirth = BirthStore[CorrectInfo.Parameter];
                        CorrectInfo.Gender = GenderStore[CorrectInfo.Parameter];
                        CorrectInfo.ID = IDStore[CorrectInfo.Parameter];

                        LabelParameter.Text = CorrectInfo.Parameter;
                        LabelID.Text = CorrectInfo.ID;
                        LabelName.Text = CorrectInfo.Name;
                        LabelSurname.Text = CorrectInfo.Surname;
                        LabelDateOfBirth.Text = CorrectInfo.DateOfBirth;
                        LabelGender.Text = CorrectInfo.Gender;
                        LabelRole.Text = CorrectInfo.Role;
                    }
                }
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Your List is empty!");
            }

        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string filename = saveFileDialog1.FileName;
                    FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(fs);
                    sw.WriteLine("+++++++++++++++++++ FIXED DATA +++++++++++++++++++ " +
                        "\n# Number Of Records: " + LabelRecords.Text +
                        "\n# Job Role: " + comboBox1.SelectedItem.ToString() + "\n\n");

                    if (listBox1.SelectedItem == null || comboBox1.SelectedItem ==null)
                    {
                        MessageBox.Show("Please, first upload the corresponding employee categories");
                    }
                    else
                    {
                        switch (comboBox1.SelectedItem)
                        {
                            case "All employees":
                                for (int i = 0; i < RoleGender.AllParametres.Count; i++)
                                {
                                    string fname = InfoStore[RoleGender.AllParametres[i]];

                                    sw.WriteLine("Parameter: " + RoleGender.AllParametres[i] +
                                        "\nID: " + IDStore[RoleGender.AllParametres[i]] +
                                        "\nFirst Name: " + InfoStore[RoleGender.AllParametres[i]].Substring(0, fname.IndexOf(" ")) +
                                        "\nLast Name: " + InfoStore[RoleGender.AllParametres[i]].Substring(fname.IndexOf(" ") + 1) +
                                        "\nGender: " + GenderStore[RoleGender.AllParametres[i]] +
                                        "\nDate Of Birth: " + BirthStore[RoleGender.AllParametres[i]] +
                                        "\nRole: " + PositionStore[RoleGender.AllParametres[i]] +
                                        "\n-------------------------------------------------------");
                                }
                                MessageBox.Show("File Saved Successfully!");
                                break;
                            case "Junior Developer":
                                for (int i = 0; i < RoleGender.AllParametres.Count; i++)
                                {
                                    string fname = InfoStore[RoleGender.AllParametres[i]];
                                    if (PositionStore[RoleGender.AllParametres[i]] == RoleGender.Roles[0])
                                    {
                                        sw.WriteLine("Parameter: " + RoleGender.AllParametres[i] +
                                                "\nID: " + IDStore[RoleGender.AllParametres[i]] +
                                                "\nFirst Name: " + InfoStore[RoleGender.AllParametres[i]].Substring(0, fname.IndexOf(" ")) +
                                                "\nLast Name: " + InfoStore[RoleGender.AllParametres[i]].Substring(fname.IndexOf(" ") + 1) +
                                                "\nGender: " + GenderStore[RoleGender.AllParametres[i]] +
                                                "\nDate Of Birth: " + BirthStore[RoleGender.AllParametres[i]] +
                                                "\nRole: " + PositionStore[RoleGender.AllParametres[i]] +
                                                "\n-------------------------------------------------------");
                                    }
                                }
                                MessageBox.Show("File Saved Successfully!");
                                break;

                            case "Senior Developer":
                                for (int i = 0; i < RoleGender.AllParametres.Count; i++)
                                {
                                    string fname = InfoStore[RoleGender.AllParametres[i]];
                                    if (PositionStore[RoleGender.AllParametres[i]] == RoleGender.Roles[1])
                                    {
                                        sw.WriteLine("Parameter: " + RoleGender.AllParametres[i] +
                                                "\nID: " + IDStore[RoleGender.AllParametres[i]] +
                                                "\nFirst Name: " + InfoStore[RoleGender.AllParametres[i]].Substring(0, fname.IndexOf(" ")) +
                                                "\nLast Name: " + InfoStore[RoleGender.AllParametres[i]].Substring(fname.IndexOf(" ") + 1) +
                                                "\nGender: " + GenderStore[RoleGender.AllParametres[i]] +
                                                "\nDate Of Birth: " + BirthStore[RoleGender.AllParametres[i]] +
                                                "\nRole: " + PositionStore[RoleGender.AllParametres[i]] +
                                                "\n-------------------------------------------------------");
                                    }

                                }
                                MessageBox.Show("File Saved Successfully!");
                                break;
                            case "Sales Manager":
                                for (int i = 0; i < RoleGender.AllParametres.Count; i++)
                                {
                                    string fname = InfoStore[RoleGender.AllParametres[i]];
                                    if (PositionStore[RoleGender.AllParametres[i]] == RoleGender.Roles[2])
                                    {
                                        sw.WriteLine("Parameter: " + RoleGender.AllParametres[i] +
                                                "\nID: " + IDStore[RoleGender.AllParametres[i]] +
                                                "\nFirst Name: " + InfoStore[RoleGender.AllParametres[i]].Substring(0, fname.IndexOf(" ")) +
                                                "\nLast Name: " + InfoStore[RoleGender.AllParametres[i]].Substring(fname.IndexOf(" ") + 1) +
                                                "\nGender: " + GenderStore[RoleGender.AllParametres[i]] +
                                                "\nDate Of Birth: " + BirthStore[RoleGender.AllParametres[i]] +
                                                "\nRole: " + PositionStore[RoleGender.AllParametres[i]] +
                                                "\n-------------------------------------------------------");
                                    }

                                }
                                MessageBox.Show("File Saved Successfully!");
                                break;

                            case "Product Manager":
                                for (int i = 0; i < RoleGender.AllParametres.Count; i++)
                                {
                                    string fname = InfoStore[RoleGender.AllParametres[i]];
                                    if (PositionStore[RoleGender.AllParametres[i]] == RoleGender.Roles[3])
                                    {
                                        sw.WriteLine("Parameter: " + RoleGender.AllParametres[i] +
                                                "\nID: " + IDStore[RoleGender.AllParametres[i]] +
                                                "\nFirst Name: " + InfoStore[RoleGender.AllParametres[i]].Substring(0, fname.IndexOf(" ")) +
                                                "\nLast Name: " + InfoStore[RoleGender.AllParametres[i]].Substring(fname.IndexOf(" ") + 1) +
                                                "\nGender: " + GenderStore[RoleGender.AllParametres[i]] +
                                                "\nDate Of Birth: " + BirthStore[RoleGender.AllParametres[i]] +
                                                "\nRole: " + PositionStore[RoleGender.AllParametres[i]] +
                                                "\n-------------------------------------------------------");
                                    }
                                }
                                MessageBox.Show("File Saved Successfully!");
                                break;
                        }
                    }
                    sw.Close();
                    fs.Close();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you Sure?!", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                this.Close();
            }
            else
            {
                return;
            }
        }
    }
}
