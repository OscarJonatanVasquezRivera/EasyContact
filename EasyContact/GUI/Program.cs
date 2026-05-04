using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using EL;
namespace GUI
{
    internal class Program
    {
        [STAThread] //propiedad que pusimos para que la app no se cayera al copiar y pegar texto en los textbox
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

           
            Application.Run(new Form3());
        }
    }
}
