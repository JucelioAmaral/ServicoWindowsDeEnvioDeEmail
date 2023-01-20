using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServicoDeEnvioDeEmail.TestesDebug
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnIniciarProcessamento_Click(object sender, EventArgs e)
        {
            clsControleProcessamento controle = new clsControleProcessamento();
            controle.IniciarProcessamento();
        }

        private void btnFinalizarProcessamento_Click_Click(object sender, EventArgs e)
        {
            clsControleProcessamento controle = new clsControleProcessamento();
            controle.FinalizarProcessamento();
        }
    }
}
