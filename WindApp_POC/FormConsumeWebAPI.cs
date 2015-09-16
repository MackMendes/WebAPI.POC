using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindApp_POC.RepositoryService;

namespace WindApp_POC
{
    public partial class FormConsumeWebAPI : Form
    {
        WebAPICliente _webAPICliente = new WebAPICliente();

        public FormConsumeWebAPI()
        {
            InitializeComponent();
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            ICollection<Cliente> result = _webAPICliente.GetAllByHttpClient().Result;

            bindingSourceConsultar.DataSource = result;
        }
    }
}
