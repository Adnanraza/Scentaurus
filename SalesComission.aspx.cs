using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

public partial class SalesComission : BaseClass
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Session["SalesComission"] = null;
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

    public class SalesObject
    {
        public int ID { get; set; }
        public string Beneficier { get; set; }
        public decimal? Percentage { get; set; }
        public decimal? Amount { get; set; }
        public bool CheckOut { get; set; }
        public string SaleTo { get; set; }
        public string Referrer { get; set; }
        public string Product { get; set; }
        public string SalesNo { get; set; }
        public int? Quantity { get; set; }
        public decimal? ProductPrice { get; set; }
        public decimal? Total { get; set; }
        public DateTime? TimeStamp { get; set; }
    }

    private void LoadData()
    {
        var data = new List<SalesObject>();
        var user = Utils.GetUserData();

        if (user.IsSuperAdmin)
        {
            data = MyDBContext.SalesComissionNthLevels.Where(x => x.IsActive
            && (x.UserSale.SalesNo.Contains(txtSalesNo.Text.Trim())
            || String.IsNullOrEmpty(txtSalesNo.Text))
            ).Select(
                  x => new SalesObject()
                  {
                      ID = x.ID,
                      Beneficier = x.UserDetail.FirstName + " " + x.UserDetail.LastName,
                      Percentage = x.ComissionPercentage,
                      Amount = x.ComissionAmount,
                      CheckOut = x.IsCheckOut,
                      SaleTo = x.UserSale.UserDetail1.FirstName + " " + x.UserSale.UserDetail1.LastName,
                      Referrer = x.UserSale.UserDetail.FirstName + " " + x.UserSale.UserDetail.LastName,
                      Product = x.UserSale.Product.ProductName,
                      Quantity = x.UserSale.Quantity,
                      ProductPrice = x.UserSale.ProductPrice,
                      Total = x.UserSale.TotalPrice,
                      SalesNo = x.UserSale.SalesNo,
                      TimeStamp = x.AddedOn
                  }
                  ).OrderByDescending(x => x.ID).ToList();
        }
        else
        {
            data = MyDBContext.SalesComissionNthLevels.Where(x => x.IsActive && x.BeneficierUserID == user.ID
          && ( x.UserSale.SalesNo.Contains(txtSalesNo.Text.Trim())
          || String.IsNullOrEmpty(txtSalesNo.Text))
          ).Select(
                x => new SalesObject()
                {
                    ID = x.ID,
                    Beneficier = x.UserDetail.FirstName + " " + x.UserDetail.LastName,
                    Percentage = x.ComissionPercentage,
                    Amount = x.ComissionAmount,
                    CheckOut = x.IsCheckOut,
                    SaleTo = x.UserSale.UserDetail1.FirstName + " " + x.UserSale.UserDetail1.LastName,
                    Referrer = x.UserSale.UserDetail.FirstName + " " + x.UserSale.UserDetail.LastName,
                    Product = x.UserSale.Product.ProductName,
                    Quantity = x.UserSale.Quantity,
                    ProductPrice = x.UserSale.ProductPrice,
                    Total = x.UserSale.TotalPrice,
                    SalesNo = x.UserSale.SalesNo,
                    TimeStamp = x.AddedOn
                }
                ).OrderByDescending(x => x.ID).ToList();
        }

        Session["SalesComission"] = data;
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
        if (Session["SalesComission"] != null)
        {
            var list = (List<SalesObject>)Session["SalesComission"];
            gvwList.DataSource = list;
            gvwList.DataBind();
        }
        else
        {
            gvwList.DataSource = null;
            gvwList.DataBind();
        }
    }

    protected void txtSalesNo_TextChanged(object sender, EventArgs e)
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