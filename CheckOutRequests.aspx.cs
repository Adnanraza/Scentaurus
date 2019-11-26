using Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

public partial class CheckOutRequests : BaseClass
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Session["CheckOutRequests"] = null;
                LoadData();
            }
        }
        catch (Exception ex)
        {
            Utils.ShowAlert(this, "Error", ex.Message, false);
        }
    }

    protected void ddlTeachers_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        { Utils.ShowAlert(this, "Error", ex.Message, false); }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            string userName = Utils.GetUserName();


        }
        catch (Exception ex)
        {
            Utils.ShowAlert(this, "Error", ex.Message, false);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            Utils.ShowAlert(this, "Error", ex.Message, false);
        }
    }

    private void LoadData()
    {
        var filter = txtFilter.Text.ToLower();

        var data = MyDBContext.CheckOutRequests.Where(x =>
            x.UserDetail.FirstName.ToLower().Contains(filter) ||
            x.UserDetail.LastName.ToLower().Contains(filter) ||
            x.UserDetail.Username.ToLower().Contains(filter) || String.IsNullOrEmpty(filter)
        ).OrderBy(x => x.Status).ToList();

        Session["CheckOutRequests"] = data;
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
        if (Session["CheckOutRequests"] != null)
        {
            var list = (List<CheckOutRequest>)Session["CheckOutRequests"];
            gvwList.DataSource = list;
            gvwList.DataBind();
        }
        else
        {
            gvwList.DataSource = null;
            gvwList.DataBind();
        }
    }

    protected void txtFilter_TextChanged(object sender, EventArgs e)
    {
        try
        {
            LoadData();

        }
        catch (Exception ex)
        {
            Utils.ShowAlert(this, "Error", ex.Message, false);
        }
    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            var context = MyDBContext;
            var item = ((Button)sender).NamingContainer as GridViewRow;
            var hdnID = int.Parse((item.FindControl("hdnID") as HiddenField).Value);
            var userID = int.Parse((item.FindControl("hdnUserID") as HiddenField).Value);
            var req = context.CheckOutRequests.Where(x => x.ID == hdnID).FirstOrDefault();
            req.Status = "Approved";
            context.SaveChanges();

            var com = context.SalesComissionNthLevels.Where(x => x.IsCheckOut == false && x.BeneficierUserID == userID && x.Status.ToLower() == "pending").ToList();

            foreach (var c in com)
            {
                c.IsCheckOut = true;
                c.Status = "Approved";
            }

            context.SaveChanges();

            Utils.AddEntryInCustomerLedger(req.UserID, req.Amount * -1, req.ID, Utils.AdjustmentType.CreditAdjustment, "Check out request for " + req.Amount.ToString() + " approved");

            LoadData();
            Utils.ShowAlert(this, "Request approved successfully.");
        }
        catch (Exception ex)
        {
            Utils.ShowAlert(this, "Error", ex.Message, false);
        }
    }

    protected void btnReject_Click(object sender, EventArgs e)
    {
        try
        {
            var context = MyDBContext;
            var item = ((Button)sender).NamingContainer as GridViewRow;
            var hdnID = int.Parse((item.FindControl("hdnID") as HiddenField).Value);
            var req = context.CheckOutRequests.Where(x => x.ID == hdnID).FirstOrDefault();
            req.Status = "Rejected";
            context.SaveChanges();
            LoadData();
            Utils.ShowAlert(this, "Request rejected successfully.");
        }
        catch (Exception ex)
        {
            Utils.ShowAlert(this, "Error", ex.Message, false);
        }
    }
}