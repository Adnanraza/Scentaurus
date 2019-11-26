using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Logout : BaseClass
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Utils.CleanUserData();
        Session.Abandon();
        Session.Clear();
        Response.Redirect("Default.aspx");
    }
}