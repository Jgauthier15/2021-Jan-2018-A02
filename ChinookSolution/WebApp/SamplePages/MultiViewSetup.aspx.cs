using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp.SamplePages
{
    public partial class MultiViewSetup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
        {
            //to switch views
            //grab a value from e representing the new view to display.
            int index = Int32.Parse(e.Item.Value);
            //assign the index to ActiveViewIndex
            MultiView1.ActiveViewIndex = index;
        }

        protected void SendTo2From1_Click(object sender, EventArgs e)
        {
            IODataV2.Text = IODataV1.Text;
            //can I automatically go to the second view?
            MultiView1.ActiveViewIndex = 1;
            //if you alter the page, you sh ould consider altering the menu button also.
            Menu1.Items[1].Selected = true;
        }

        protected void SendTo3From1_Click(object sender, EventArgs e)
        {
            IODataV3.Text = IODataV1.Text;
            //can I automatically go to the second view?
            MultiView1.ActiveViewIndex = 2;
            //if you alter the page, you sh ould consider altering the menu button also.
            Menu1.Items[2].Selected = true;
        }

        protected void SendTo1From2_Click(object sender, EventArgs e)
        {

        }

        protected void SendTo3From2_Click(object sender, EventArgs e)
        {

        }

        protected void SendTo1From3_Click(object sender, EventArgs e)
        {

        }

        protected void SendTo2From3_Click(object sender, EventArgs e)
        {

        }
    }
}