using BLL;
using EL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{


    public partial class Form4 : Form
    {   private Form1 _form1; // Referencia a Form1 para actualizar la lista de contactos
        private int _idContacto;
        private int _idUsuario;

        // Constructor que recibe los datos desde Form1
        public Form4(Form1 form1,int idContacto, string nombres, string telefono, string correo, string direccion, int idUsuario)
        {
            InitializeComponent();
            _form1 = form1; //se inicializa la referencia a Form1 para que pueda llamar a CargarContactos() después de actualizar y asi 
            //refrescar la lista de contactos en Form1
            _idContacto = idContacto;
            _idUsuario = idUsuario;

          
            txtNombres.Text = nombres;
            txtTelefono.Text = telefono;
            txtCorreo.Text = correo;
            txtDireccion.Text = direccion;
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            this.Close();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validación rápida en el formulario: no permitir campos vacíos
                if (string.IsNullOrWhiteSpace(txtNombres.Text) ||
                    string.IsNullOrWhiteSpace(txtTelefono.Text) ||
                    string.IsNullOrWhiteSpace(txtCorreo.Text) ||
                    string.IsNullOrWhiteSpace(txtDireccion.Text))
                {
                    MessageBox.Show("Todos los campos son obligatorios.");
                    return;
                }

                // Crear objeto contacto con los datos del formulario
                ContactosEL contacto = new ContactosEL
                {
                    IdContacto = _idContacto,
                    Nombres = txtNombres.Text.Trim(),
                    Telefono = txtTelefono.Text.Trim(),
                    Correo = txtCorreo.Text.Trim(),
                    Direccion = txtDireccion.Text.Trim(),
                    IdUsuario = _idUsuario
                };

                // Instancia de la capa de negocio
                ContactosBLL contactosBLL = new ContactosBLL();

                // Validaciones ya existentes en tu BLL en lugar de crear nuevas
                // usamos las funciones de validación que ya tienes implementadas
                //lo que seria usar el pol
                contactosBLL.ValidarCorreo(contacto.Correo);
                contactosBLL.ValidarDuplicados(contacto, contacto.IdUsuario);

                // Actualizar contacto
                int resultado = contactosBLL.Actualizar(contacto);

                if (resultado > 0)
                {
                    MessageBox.Show("Contacto actualizado correctamente.");
                    _form1.CargarContactos(); // refrescar lista en Form1
                    
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar el contacto.");
                }
            }
            catch (Exception ex)
            {
                // Si falla alguna validación o el DAL, se muestra el mensaje
                MessageBox.Show("Error al actualizar contacto: " + ex.Message);
            }
        }



        // Eventos de los TextBox (pueden quedar vacíos si no los usas)
        private void txtNombres_TextChanged(object sender, EventArgs e) { }
        private void txtTelefono_MaskInputRejected(object sender, MaskInputRejectedEventArgs e) { }
        private void txtCorreo_TextChanged(object sender, EventArgs e) { }
        private void txtDireccion_TextChanged(object sender, EventArgs e) { }
    }
}
