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

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {

        static Empleado[] Empleados;
        static int agregados = 0;

        public Form1()
        {
            InitializeComponent();
            Empleados = new Empleado[50];

        }

        private void ShowMessage(string s) {
                MessageBox.Show (s, "Error", 
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ShowText(string s)
        {
            MessageBox.Show(s, "-",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private int CheckAllFields() {
            String toAppend = "";
            if (txtCedula.Text.Length == 0)
            {
                toAppend += "* Cedula\n";
            }
            if (txtNombre.Text.Length == 0)
            {
                toAppend += "* Nombre\n";
            }
            if (txtApellidos.Text.Length == 0)
            {
                toAppend += "* Apellidos\n";
            }
            //Fecha
            if (txtday.Text.Length == 0)
            {
                toAppend += "* Fecha Dia\n";
            }
            if (txtmonth.Text.Length == 0)
            {
                toAppend += "* Fecha Mes\n";
            }
            if (txtyear.Text.Length == 0)
            {
                toAppend += "* Fecha Año\n";
            }
            //End Fecha
            if (txtEdad.Text.Length == 0)
            {
                toAppend += "* Edad\n";
            }
            if (ComboBox1.SelectedItem == null)
            {
                toAppend += "* Nombre Depto\n";
            }
            if (txtSalBase.Text.Length == 0)
            {
                toAppend += "* Salario Base\n";
            }
            if (toAppend.Length  != 0)
            {
                ShowMessage("Campos Vacios:\n"+toAppend);
                return 0;
            }
            return 1;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] employees = new string[]{"rrhh","proveduría","compras","mercadeo"};
            ComboBox1.Items.AddRange(employees);
            ComboBox1.DropDownStyle = ComboBoxStyle.DropDownList;

            //List<string> mylist = new List<string>() { "stealthy", "ninja", "panda" };
            //ListViewItem item = new ListViewItem();
            //item.Text = "bar";
            //item.SubItems.Add("foo");
            //item.SubItems.Add("foo2");
            //listView1.Items.Add(item);
            //List<string> mylist = new List<string>() { "stealthy", "ninja", "panda" };
            //listView1.DataSource = mylist;
            //listView1.DataBind();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                ////
                if (CheckAllFields() != 0 && CedulaDuplicada(int.Parse(txtCedula.Text)) == false )
                {
                    if (agregados < Empleados.Length)
                    {
                        Empleado x = new Empleado();
                        x.cedula = int.Parse(txtCedula.Text);
                        x.nombre = txtNombre.Text;
                        x.apellido = txtApellidos.Text;
                        x.edad = txtEdad.Text;
                        x.dia = int.Parse(txtday.Text);
                        x.mes = int.Parse(txtmonth.Text);
                        x.anno = int.Parse(txtyear.Text);
                        x.oficina = ComboBox1.Items[ComboBox1.SelectedIndex].ToString();
                        //debug
                        //ShowMessage(ComboBox1.SelectedIndex.ToString());
                        x.salariob = double.Parse(txtSalBase.Text);
                        x.calcularSalario();
                        Empleados[agregados] = x;                    
                        agregados++;
                        clearAllTexboxes();
                        ShowText("Agregado Empleado:\n"+x.nombre 
                                + " " + x.apellido+ "\n"
                                + x.cedula);
                    }
                    else
                    {
                        ShowMessage("No hay más espacios para nuevos empleados: tope 50");                    
                    }
                }
            }
            catch (System.Exception ex)
            {
                ShowMessage(ex.Message);
            }
        }

        private void ListarEmpleados() {
            txtSalida.Text = "";            
            string output = "";
            output += "Ced\tNom\tApell\tEdad\tFecha\t\tOficina\tSalBase\tSalario(Ded Inc seg)\n";
            output += Environment.NewLine;
            for (int i = 0; i < Empleados.Length; i++)
            {
                if (Empleados[i] != null)
                {
                    output += Empleados[i].cedula + "\t";
                    output += Empleados[i].nombre + "\t";
                    output += Empleados[i].apellido + "\t";
                    output += Empleados[i].edad + "\t";
                    output += Empleados[i].dia + "/" + Empleados[i].mes + "/" + Empleados[i].anno + "\t";
                    output += Empleados[i].oficina + "\t";
                    output += Empleados[i].salariob + "\t";
                    output += Empleados[i].salario + "\t";
                    output += Environment.NewLine;
                }
            }
            txtSalida.Text = output;        
        }

        //return true if cedula is already registered
        //return false if cedula is NOT registered
        private bool CedulaDuplicada(int ced)
        {
            int contEncontrado = 0;
            for (int i = 0; i < Empleados.Length; i++)
            {
                if (Empleados[i] != null)
                {
                    if (Empleados[i].cedula == ced)
                    {
                        contEncontrado++;
                        ShowMessage("Cedula repetida, no pueden haber usuarios con la misma cedula: " + ced);
                        return true;
                    }

                }
            }
            if (contEncontrado != 0)
            {
                ShowMessage("Cedula repetida, no pueden haber usuarios con la misma cedula: " + ced);
                return true;
            }
            else 
                return false;
        }

        private bool ExisteParaModificar(int ced)
        {
            int contEncontrado = 0;
            for (int i = 0; i < Empleados.Length; i++)
            {
                if (Empleados[i] != null)
                {
                    if (Empleados[i].cedula == ced)
                    {
                        contEncontrado++;
                        return true;
                    }

                }
            }
            if (contEncontrado != 0)
            {
                return false;
            }
            else
                return false;
        }



        private void button1_Click(object sender, EventArgs e)
        {
            ComboBox1.SelectedIndex = -1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ListarEmpleados();

            //BindingSource bs = new BindingSource();
            //bs.DataSource = typeof(Empleado);
            //foreach (Empleado item in Empleados)
            //{
            //    bs.Add(item);                
            //}
            //dataGridView1.DataSource = bs;
            //dataGridView1.AutoGenerateColumns = true;
            //dataGridView1.DataBindings.Add("ss", bs, "");
            //txtModel.DataBindings.Add("Text", bs, "Name");


            //
            var bindingList = new BindingList<Empleado>(Empleados);
            var source = new BindingSource(bindingList, null);
            dataGridView1.DataSource = source;


        }

        public void clearAllTexboxes() {
            txtNombre.Text = String.Empty;
            txtApellidos.Text = String.Empty;
            txtCedula.Text = String.Empty;
            txtday.Text = String.Empty;
            txtmonth.Text = String.Empty;
            txtyear.Text = String.Empty;
            txtEdad.Text = String.Empty;
            txtSalBase.Text = String.Empty;
            ComboBox1.SelectedIndex = -1;        
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                bool encontrado = false;
                if (txtABuscar.Text.Length != 0)
                {
                    int ced = int.Parse(txtABuscar.Text);
                    foreach (Empleado item in Empleados)
                    {
                        if (item != null)
                        {
                            if (item.cedula == ced)
                            {
                                txtNombre.Text = item.nombre;
                                txtApellidos.Text = item.apellido;
                                txtCedula.Text = item.cedula.ToString();
                                txtday.Text = item.dia.ToString();
                                txtmonth.Text = item.mes.ToString();
                                txtyear.Text = item.anno.ToString();
                                //{"rrhh","proveduría","compras","mercadeo"};
                                if (item.oficina.Equals("rrhh"))
                                    ComboBox1.SelectedIndex = 0;
                                else if (item.oficina.Equals("proveduría"))
                                    ComboBox1.SelectedIndex = 1;
                                else if (item.oficina.Equals("compras"))
                                    ComboBox1.SelectedIndex = 2;
                                else if (item.oficina.Equals("mercadeo"))
                                    ComboBox1.SelectedIndex = 3;
                                else
                                    ComboBox1.SelectedIndex = -1;
                                //
                                txtSalBase.Text = item.salariob.ToString();
                                txtEdad.Text = item.edad.ToString();
                                txtABuscar.Text = String.Empty;
                                ShowText("Encontrado Empleado con cedula " + ced);
                                encontrado = true;
                            }
                        }
                    }
                    //Si no se encontro 
                    if (encontrado == false)
                        ShowText("No se encontró ningun empleado con cedula "+ ced);
                }
                else
                    ShowMessage("Ingrese una cedula a buscar");
            }
            catch (System.Exception ex)
            {
                ShowMessage(ex.Message);
            }
        }

        private void btnELiminar_Click(object sender, EventArgs e)
        {
            try
            {
                bool encontrado = false;
                if (txtABuscar.Text.Length != 0)
                {
                    int ced = int.Parse(txtABuscar.Text);

                    for (int i = 0; i < 50; i++)
                    {
                        if (Empleados[i] != null)
                        {
                            if (Empleados[i].cedula == ced)
                            {
                                Empleados[i] = null;
                                ShowText("Encontrado Empleado con cedula " + ced);
                                encontrado = true;
                                txtABuscar.Text = String.Empty;
                                agregados--;
                            }                            
                        }                        
                    }
                    //Si no se encontro 
                    if (encontrado == false)
                        ShowText("No se encontró ningun empleado con cedula " + ced);
                }
                else
                    ShowMessage("Ingrese una cedula a buscar");
            }
            catch (System.Exception ex)
            {
                ShowMessage(ex.Message);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckAllFields() != 0 && ExisteParaModificar(int.Parse(txtCedula.Text)) == true)
                {

                    for (int i = 0; i < 50; i++)
                    {
                        if (Empleados[i] != null)
                        {
                            if (Empleados[i].cedula == int.Parse(txtCedula.Text))
                            {
                                Empleados[i].cedula = int.Parse(txtCedula.Text);
                                Empleados[i].nombre = txtNombre.Text;
                                Empleados[i].apellido = txtApellidos.Text;
                                Empleados[i].edad = txtEdad.Text;
                                Empleados[i].dia = int.Parse(txtday.Text);
                                Empleados[i].mes = int.Parse(txtmonth.Text);
                                Empleados[i].anno = int.Parse(txtyear.Text);
                                Empleados[i].oficina = ComboBox1.Items[ComboBox1.SelectedIndex].ToString();
                                Empleados[i].calcularSalario();
                                ShowText("Encontrado y modificado Empleado con cedula " + txtCedula.Text);                                
                                clearAllTexboxes();
                            }
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                ShowMessage(ex.Message);
            }
        }
    }
}
