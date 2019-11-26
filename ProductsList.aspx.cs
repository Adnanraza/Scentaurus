using Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

public partial class ProductsList : BaseClass
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Session["ProductsList"] = null;
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

    public class M_ProductsList
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int LevelCount { get; set; }
        public bool IsActive { get; set; }
        public int ID { get; set; }
        public DateTime CreatedOn { get; set; }
    }


    private void LoadData()
    {
        var data = MyDBContext.Products.Where(x => String.IsNullOrEmpty(txtSearch.Text)
        || x.ProductName.ToLower().Contains(txtSearch.Text.ToLower())
        || x.FinalSalePrice.ToString().Contains(txtSearch.Text.ToLower())
        ).Select(x => new M_ProductsList()
        {
            Name = x.ProductName,
            Price = x.FinalSalePrice.Value
            ,
            ID = x.ID
            ,
            IsActive = x.IsActive
            ,
            CreatedOn = x.CreatedOn.Value
            ,
            LevelCount = x.ProductLevels.Count
        }).OrderByDescending(x => x.ID).ToList();

        Session["ProductsList"] = data;
        LoadDataFromSession();
    }

    protected void gvwList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvwList.PageIndex = e.NewPageIndex;
        LoadDataFromSession();
    }

    protected void chkSelect_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            GridViewRow gvr = ((CheckBox)sender).NamingContainer as GridViewRow;
            var hdnID = gvr.FindControl("hdnID") as HiddenField;
            var chkSelect = gvr.FindControl("chkSelect") as CheckBox;
            int userID = int.Parse(hdnID.Value);

            if (chkSelect.Checked)
            {
                var entity = MyDBContext;
                var std = entity.Products.Where(x => x.ID == userID).FirstOrDefault();
                std.IsActive = true;
                entity.SaveChanges();
                LoadData();
                Utils.ShowAlert(this, "Product activated successfully.");
            }
            else
            {
                var entity = MyDBContext;
                var std = entity.Products.Where(x => x.ID == userID).FirstOrDefault();
                std.IsActive = false;
                entity.SaveChanges();
                LoadData();
                Utils.ShowAlert(this, "Product inactivated successfully.");
            }
        }
        catch (Exception ex)
        {
            Utils.ShowAlert(this, "Error", ex.Message, false);
        }
    }

    protected void gvwList_PageIndexChanged(object sender, EventArgs e)
    {

    }

    private void LoadDataFromSession()
    {
        if (Session["ProductsList"] != null)
        {
            var list = (List<M_ProductsList>)Session["ProductsList"];
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