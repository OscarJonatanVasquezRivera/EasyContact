using BLL;
using System;
using System.Linq;
using System.Windows.Forms;
using EL;
using Helpers;

namespace GUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Método para cargar contactos en el BindingSource
        public void CargarContactos()
        {
            bindingSource1.DataSource = new ContactosBLL().Listar(Sesion.IdUsuario);
            dgv.DataSource = bindingSource1;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CargarContactos();
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && dgv.CurrentRow != null)
                {
                    DataGridViewRow fila = dgv.CurrentRow;

                    string idContacto = fila.Cells["IdContacto"].Value?.ToString() ?? "";
                    string nombres = fila.Cells["Nombres"].Value?.ToString() ?? "";
                    string telefono = fila.Cells["Telefono"].Value?.ToString() ?? "";
                    string correo = fila.Cells["Correo"].Value?.ToString() ?? "";
                    string direccion = fila.Cells["Direccion"].Value?.ToString() ?? "";
                    string idUsuario = fila.Cells["IdUsuario"].Value?.ToString() ?? "";

                 
                }
            }
            catch (Exception ex)
            {
                // Registrar el error para depuración
                Console.WriteLine("Error en dgv_CellContentClick: " + ex.Message);
            }
        }

        private void btnBuscarContacto_Click(object sender, EventArgs e)
        {
            // Pasamos referencia de Form1 a Form2
            Form2 f2 = new Form2(this);
            f2.Show();
            this.Close();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string texto = barraBusqueda.Text.Trim();
            if (!string.IsNullOrEmpty(texto))
            {
                bindingSource1.DataSource = new ContactosBLL().BuscarPorNombre(texto, Sesion.IdUsuario);
                dgv.DataSource = bindingSource1;
            }
            else
            {
                CargarContactos();
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentRow != null)
            {
                int idContacto = Convert.ToInt32(dgv.CurrentRow.Cells["IdContacto"].Value);
                string nombres = dgv.CurrentRow.Cells["Nombres"].Value?.ToString() ?? "";
                string telefono = dgv.CurrentRow.Cells["Telefono"].Value?.ToString() ?? "";
                string correo = dgv.CurrentRow.Cells["Correo"].Value?.ToString() ?? "";
                string direccion = dgv.CurrentRow.Cells["Direccion"].Value?.ToString() ?? "";
                int idUsuario = Convert.ToInt32(dgv.CurrentRow.Cells["IdUsuario"].Value);

                // Pasamos referencia de Form1 a Form4
                Form4 f4 = new Form4(this, idContacto, nombres, telefono, correo, direccion, idUsuario);
                f4.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Seleccione un contacto para actualizar.");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentRow == null)
            {
                MessageBox.Show("Debe seleccionar un contacto para eliminar.");
                return;
            }

            int idContacto = Convert.ToInt32(dgv.CurrentRow.Cells["IdContacto"].Value);
            new ContactosBLL().Eliminar(idContacto, Sesion.IdUsuario);

            MessageBox.Show("Contacto eliminado correctamente.");
            CargarContactos(); // refresca lista sin abrir Form1 nuevo
        }

        private void barraBusqueda_TextChanged(object sender, EventArgs e)
        {
            string texto = barraBusqueda.Text.Trim();
            if (!string.IsNullOrEmpty(texto))
            {
                bindingSource1.DataSource = new ContactosBLL().BuscarPorNombre(texto, Sesion.IdUsuario);
                dgv.DataSource = bindingSource1;
            }
            else
            {
                CargarContactos();
            }
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            var confirmar = MessageBox.Show("¿Seguro que deseas cerrar sesión?",
                                            "Cerrar sesión",
                                            MessageBoxButtons.YesNo,
                                            MessageBoxIcon.Question);

            if (confirmar == DialogResult.Yes)
            {
                // Limpia la sesión
                Sesion.IdUsuario = 0;
                Sesion.Usuario = string.Empty;
                Sesion.Rol = string.Empty;

                // Regresa al login
                Form3 login = new Form3();
                login.Show();

                // Cierra el formulario actual
                this.Close();
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {
            
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }
    }
}
