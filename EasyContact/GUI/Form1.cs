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
        private void CargarContactos()
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
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgv.CurrentRow; // se usa currentRow para obtener la fila seleccionada y que el programa no se caiga si se hace click en otra parte del datagridview

                string idContacto = fila.Cells["IdContacto"].Value?.ToString() ?? "";
                string nombres = fila.Cells["Nombres"].Value?.ToString() ?? "";
                string telefono = fila.Cells["Telefono"].Value?.ToString() ?? "";
                string correo = fila.Cells["Correo"].Value?.ToString() ?? "";
                string direccion = fila.Cells["Direccion"].Value?.ToString() ?? "";
                string idUsuario = fila.Cells["IdUsuario"].Value?.ToString() ?? "";

               // MessageBox.Show($"Seleccionado: {nombres} - {telefono}");
            }
        }


        private void btnBuscarContacto_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
            this.Hide();
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
            if (dgv.CurrentRow == null)
            {
                MessageBox.Show("Debe seleccionar un contacto para actualizar.");
                return;
            }

            DataGridViewRow fila = dgv.CurrentRow;

            ContactosEL contacto = new ContactosEL
            {
                IdContacto = Convert.ToInt32(fila.Cells["IdContacto"].Value),
                Nombres = fila.Cells["Nombres"].Value.ToString(),
                Telefono = fila.Cells["Telefono"].Value.ToString(),
                Correo = fila.Cells["Correo"].Value.ToString(),
                Direccion = fila.Cells["Direccion"].Value.ToString(),
                IdUsuario = Convert.ToInt32(fila.Cells["IdUsuario"].Value)
            };

            new ContactosBLL().Actualizar(contacto);
            MessageBox.Show("Contacto actualizado correctamente.");
            CargarContactos();
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
            CargarContactos();
        }
        private void barraBusqueda_TextChanged(object sender, EventArgs e)
        {
            // Búsqueda en tiempo real
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
    }
}
