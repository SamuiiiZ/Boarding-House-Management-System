using System.Windows.Forms;
using BoardingHouseSys.Models;

namespace BoardingHouseSys.Forms
{
    public class FormUsers : Form
    {
        public FormUsers(User user)
        {
            this.Text = "Manage Users";
            this.Size = new System.Drawing.Size(600, 400);
            Label lbl = new Label { Text = "User Management Module", AutoSize = true, Location = new System.Drawing.Point(20, 20) };
            this.Controls.Add(lbl);
        }
    }
}
