using Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

public partial class Sales : BaseClass
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Session["Sales"] = null;
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
        var user = Utils.GetUserData();
        var data = new List<UserSale>();

        if (user.IsSuperAdmin)
        {
            data = MyDBContext.UserSales.Where(x => String.IsNullOrEmpty(txtSearch.Text)
            || x.SalesNo.ToLower().Contains(txtSearch.Text.ToLower())
            || x.Product.ProductName.ToLower().Contains(txtSearch.Text.ToLower())
            || x.UserDetail1.FirstName.ToLower().Contains(txtSearch.Text.ToLower())
            || x.UserDetail1.LastName.ToLower().Contains(txtSearch.Text.ToLower())
            || x.UserDetail1.Username.ToLower().Contains(txtSearch.Text.ToLower())

            ).OrderByDescending(x => x.AddedOn).ToList();
        }
        else
        {
            data = MyDBContext.UserSales.Where(x =>
            x.SalesComissionNthLevels.Where(y => y.BeneficierUserID == user.ID).ToList().Count > 0
            && (String.IsNullOrEmpty(txtSearch.Text)
            || x.SalesNo.ToLower().Contains(txtSearch.Text.ToLower())
            || x.Product.ProductName.ToLower().Contains(txtSearch.Text.ToLower())
            || x.UserDetail1.FirstName.ToLower().Contains(txtSearch.Text.ToLower())
            || x.UserDetail1.LastName.ToLower().Contains(txtSearch.Text.ToLower())
            || x.UserDetail1.Username.ToLower().Contains(txtSearch.Text.ToLower()))

            ).OrderByDescending(x => x.AddedOn).ToList();
        }

        Session["Sales"] = data;
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
        if (Session["Sales"] != null)
        {
            var list = (List<UserSale>)Session["Sales"];
            gvwList.DataSource = list;
            gvwList.DataBind();
        }
        else
        {
            gvwList.DataSource = null;
            gvwList.DataBind();
        }
    }

    protected void txtSearch_TextChanged(object sender, EventArgs e)
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
}