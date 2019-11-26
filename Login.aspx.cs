using Context;
using System;
using System.Linq;
using System.Web.UI.WebControls;

public partial class Login : BaseClass
{
    public class CL
    {
        public string Name { get; set; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Login_Click(object sender, EventArgs e)
    {
        try
        {
            var db = new ScentaurusEntities2();

            var user = db.UserDetails.Where(x => x.Username.ToLower() == txtUserName.Text.ToLower() && x.Password == txtPassword.Text && x.IsActive);

            if (user.ToList().Count > 0)
            {
                var obj = user.ToList().FirstOrDefault();
                
                Utils.SetUserData(obj);
                Response.Redirect("Dashboard.aspx");
            }
            else
            {
                Utils.ShowAlert(this, "Error", "Invalid user credentials", false);
            }
        }
        catch (Exception ex)
        { }
    }

}