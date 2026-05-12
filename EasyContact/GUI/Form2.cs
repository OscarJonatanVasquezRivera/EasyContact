using BLL;
using EL;
using Helpers;
using System;
using System.Windows.Forms;

namespace GUI
{
    public partial class Form2 : Form
    {
        // En este caso, como Form2 debe refrescar la lista de contactos en Form1
        // después de agregar un nuevo registro, necesitamos recibir una referencia
        // de Form1 en el constructor.

        private Form1 _form1;
        public Form2(Form1 form1)
        {
            InitializeComponent();
            _form1 = form1;
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
                    MessageBox.Show("✅ Contacto agregado correctamente.",
                                    "Agregar Contacto",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);

                    // Regresar a Form1 y refrescar lista
                    Form1 f1 = new Form1();
                    f1.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("❌ No se pudo agregar el contacto.",
                                    "Agregar Contacto",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("⚠️ Error al agregar contacto: " + ex.Message,
                                "Agregar Contacto",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
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
