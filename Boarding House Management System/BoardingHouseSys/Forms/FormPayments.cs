using System.Windows.Forms;
using BoardingHouseSys.Models;

namespace BoardingHouseSys.Forms
{
    public class FormPayments : Form
    {
        public FormPayments(User user)
        {
            this.Text = "Manage Payments";
            this.Size = new System.Drawing.Size(600, 400);
            Label lbl = new Label { Text = "Payment Management Module", AutoSize = true, Location = new System.Drawing.Point(20, 20) };
            this.Controls.Add(lbl);
        }
    }
}
