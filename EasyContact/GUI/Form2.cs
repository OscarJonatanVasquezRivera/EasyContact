using BLL;
using EL;
using System;
using Helpers;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Collections.Specialized.BitVector32;

namespace GUI
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtTelefono_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void txtNombres_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCorreo_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDireccion_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                // Crear objeto contacto con los datos del formulario
                ContactosEL contacto = new ContactosEL
                {
                    Nombres = txtNombres.Text.Trim(),
                    Telefono = txtTelefono.Text.Trim(),
                    Correo = txtCorreo.Text.Trim(),
                    Direccion = txtDireccion.Text.Trim(),
                    IdUsuario = Sesion.IdUsuario
                };

                // Guardar contacto usando BLL
                ContactosBLL contactosBLL = new ContactosBLL();
                int resultado = contactosBLL.Guardar(contacto);

                if (resultado > 0)
                {
                    MessageBox.Show("Contacto agregado correctamente.");

                    // Regresar a Form1 y refrescar lista
                    Form1 f1 = new Form1();
                    f1.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No se pudo agregar el contacto.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar contacto: " + ex.Message);
            }
        }



        private void btnRegresar_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            this.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
