using Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

public partial class CustomerLedger : BaseClass
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Session["CustomerLedger"] = null;

                if (Request["i"] == null)
                {
                    Response.Redirect("Customers.aspx");
                }
                else
                {
                    int userID = int.Parse(Request["i"].ToString());
                    hdnUserID.Value = Request["i"];
                    var led = MyDBContext.Ledgers.Where(x => x.UserID == userID).OrderByDescending(x => x.ID).ToList();

                    if (led != null && led.Count > 0)
                    {
                        lblBalance.Text = led.FirstOrDefault().Balance.ToString();
                        lblUserName.Text = led.FirstOrDefault().UserDetail.Username;
                        lblName.Text = led.FirstOrDefault().UserDetail.FirstName + " " + led.FirstOrDefault().UserDetail.LastName;
                        LoadData(led);
                    }
                    else
                    {
                        Utils.ShowAlert(this, "Error", "No ledger data found for this customer");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Utils.ShowAlert(this, "Error", ex.Message, false);
        }
    }

    private void LoadData(List<Ledger> list)
    {
        var userID = int.Parse(hdnUserID.Value);
        Session["CustomerLedger"] = list;
        LoadDataFromSession();
    }

    protected void gvwList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvwList.PageIndex = e.NewPageIndex;
        LoadDataFromSession();
    }

    protected void gvwList_PageIndexChanged(object sender, EventArgs e)
    {

    }

    private void LoadDataFromSession()
    {
        if (Session["CustomerLedger"] != null)
        {
            var list = (List<Ledger>)Session["CustomerLedger"];
            gvwList.DataSource = list;
            gvwList.DataBind();
        }
        else
        {
            gvwList.DataSource = null;
            gvwList.DataBind();
        }
    }
}