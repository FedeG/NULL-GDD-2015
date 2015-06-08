using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PagoElectronico.ABM_Cliente
{
    public partial class ClienteData : Form
    {
        public DbComunicator db;
        
        public ClienteData(){
            InitializeComponent();
        }

    }
}
