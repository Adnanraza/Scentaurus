using Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

public partial class Customers : BaseClass
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Session["CustomerList"] = null;
                LoadData();
            }
        }
        catch (Exception ex)
        {
            Utils.ShowAlert(this, "Error", ex.Message, false);
        }
    }

    private void LoadData()
    {
        var data = new List<UserDetail>();
        var search = txtSearch.Text.ToLower();

        data = MyDBContext.UserDetails.Where(x => x.Username.ToLower().Contains(search)
                                                    || x.FirstName.ToLower().Contains(search)
                                                    || x.LastName.ToLower().Contains(search)
                                                    || String.IsNullOrEmpty(search)).OrderByDescending(x => x.ID).ToList();
        Session["CustomerList"] = data;
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
        if (Session["CustomerList"] != null)
        {
            var list = (List<UserDetail>)Session["CustomerList"];
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

    protected void ibEdit_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        try
        {
            GridViewRow gvr = ((CheckBox)sender).NamingContainer as GridViewRow;
            var hdnID = gvr.FindControl("hdnID") as HiddenField;
        }
        catch (Exception ex)
        {
            Utils.ShowAlert(this, "Error", ex.Message, false);
        }
    }

    protected void btnShowLedger_Click(object sender, EventArgs e)
    {
        try
        {
            GridViewRow gvr = ((Button)sender).NamingContainer as GridViewRow;
            var hdnID = gvr.FindControl("hdnID") as HiddenField;
            Response.Redirect("CustomerLedger.aspx?i=" + hdnID.Value);
        }
        catch (Exception ex)
        {
            Utils.ShowAlert(this, "Error", ex.Message, false);
        }
    }

    protected void btnApproveRegistration_Click(object sender, EventArgs e)
    {
        try
        {
            GridViewRow gvr = ((Button)sender).NamingContainer as GridViewRow;
            var id = int.Parse((gvr.FindControl("hdnID") as HiddenField).Value);
            var context = MyDBContext;

            var objUser = context.UserDetails.Where(x => x.ID == id).FirstOrDefault();
            var refer = context.UserDetails.Where(x => x.ID == objUser.ReferrerID).ToList().FirstOrDefault();
            var up = objUser.UserProducts.FirstOrDefault();
            var productID = up.ProductID;
            var product = context.Products.Where(x => x.ID == productID).FirstOrDefault();

            AssignDefaultPages(objUser.ID);

            if (product != null)
            {
                var productNth = context.ProductLevels.Where(x => x.ProductID == product.ID).OrderBy(x => x.LevelNumber).ToList();

                foreach (var p in productNth)
                {
                    var userNth = new UserNthLevel();
                    userNth.IsActive = true;
                    userNth.LevelNumber = p.LevelNumber;
                    userNth.Percentage = p.Percentage;
                    userNth.CreatedBy = Utils.GetUserName();
                    userNth.TimeStamp = DateTime.Now;
                    userNth.UserID = objUser.ID;
                    context.UserNthLevels.Add(userNth);
                }

                context.SaveChanges();

                var ur = context.UserRequests.Where(x => x.ID == objUser.ID && x.Status.ToLower() == "pending").FirstOrDefault();

                if (ur != null)
                {
                    ur.Status = "Approved";
                    context.SaveChanges();
                }

                var firstLevel = context.UserNthLevels.Where(x => x.UserID == refer.ID && x.LevelNumber == 1).FirstOrDefault();

                if (firstLevel != null)
                {
                    //var count = context.UserSales.Where(x => x.AddedOn.Value.Date == DateTime.Now.Date).ToList().Count();
                    var totalPrice = product.FinalSalePrice * up.Quantity;
                    var comissionAmount = firstLevel.Percentage * (totalPrice / 100);
                    var us = new UserSale();

                    // count++;
                    us.UserID = refer.ID;
                    us.ReferredUserID = objUser.ID;
                    us.Quantity = up.Quantity;
                    us.ProductPrice = product.FinalSalePrice;
                    us.SaleProductID = product.ID;
                    us.ComissionPercentage = firstLevel.Percentage;
                    us.ComissionAmount = comissionAmount;
                    us.TotalPrice = product.FinalSalePrice * up.Quantity;
                    us.IsActive = true;
                    us.AddedOn = DateTime.Now;
                    us.SalesNo = DateTime.Now.ToString("yyyyMMddhhmmss");// + "-" + count.ToString();

                    context.UserSales.Add(us);
                    context.SaveChanges();

                    var userSale = context.UserSales.OrderByDescending(x => x.ID).FirstOrDefault();
                    var nthLevel = new SalesComissionNthLevel();

                    nthLevel.IsActive = true;
                    nthLevel.IsCheckOut = false;
                    nthLevel.UserSaleID = userSale.ID;
                    nthLevel.BeneficierUserID = refer.ID;
                    nthLevel.AddedOn = DateTime.Now;
                    nthLevel.ComissionAmount = comissionAmount;
                    nthLevel.ComissionPercentage = firstLevel.Percentage;
                    context.SalesComissionNthLevels.Add(nthLevel);
                    context.SaveChanges();

                    var referenceID = context.SalesComissionNthLevels.OrderByDescending(x => x.ID).FirstOrDefault().ID;
                    Utils.AddEntryInCustomerLedger(refer.ID, comissionAmount.Value, referenceID, Utils.AdjustmentType.DebitAdjustment, String.Empty);

                    var admins = context.UserDetails.Where(x => x.IsActive && x.UserType.ToLower().Contains("admin")).ToList();

                    foreach (var admin in admins)
                    {
                        var adminComm = new SalesComissionNthLevel();

                        var refCommAmount = (admin.PercentageOfSale.HasValue ? admin.PercentageOfSale.Value : 0) * (totalPrice / 100);
                        adminComm.IsActive = true;
                        adminComm.IsCheckOut = false;
                        adminComm.UserSaleID = userSale.ID;
                        adminComm.BeneficierUserID = admin.ID;
                        adminComm.AddedOn = DateTime.Now;
                        adminComm.ComissionAmount = refCommAmount;
                        adminComm.ComissionPercentage = admin.PercentageOfSale;
                        context.SalesComissionNthLevels.Add(adminComm);
                        context.SaveChanges();

                        var adminReferenceID = context.SalesComissionNthLevels.OrderByDescending(x => x.ID).FirstOrDefault().ID;
                        Utils.AddEntryInCustomerLedger(admin.ID, refCommAmount.Value, adminReferenceID, Utils.AdjustmentType.DebitAdjustment, String.Empty);
                    }

                    if (refer.ReferrerID.HasValue)
                    {
                        var lastReffererID = refer.ReferrerID.Value;
                        int index = 2;

                        do
                        {
                            var referrer = context.UserDetails.Where(x => x.ID == lastReffererID && x.IsActive).FirstOrDefault();

                            if (referrer != null)
                            {
                                var referrerNthLevel = context.UserNthLevels.Where(x => x.UserID == referrer.ID && x.LevelNumber == index).FirstOrDefault();

                                if (referrerNthLevel != null)
                                {
                                    var referrerComm = new SalesComissionNthLevel();

                                    var refCommAmount = referrerNthLevel.Percentage * (totalPrice / 100);
                                    referrerComm.IsActive = true;
                                    referrerComm.IsCheckOut = false;
                                    referrerComm.UserSaleID = userSale.ID;
                                    referrerComm.BeneficierUserID = referrer.ID;
                                    referrerComm.AddedOn = DateTime.Now;
                                    referrerComm.ComissionAmount = refCommAmount;
                                    referrerComm.ComissionPercentage = referrerNthLevel.Percentage;
                                    context.SalesComissionNthLevels.Add(referrerComm);
                                    context.SaveChanges();

                                    var scReferenceID = context.SalesComissionNthLevels.OrderByDescending(x => x.ID).FirstOrDefault().ID;
                                    Utils.AddEntryInCustomerLedger(referrer.ID, refCommAmount.Value, scReferenceID, Utils.AdjustmentType.DebitAdjustment, String.Empty);
                                }

                                lastReffererID = referrer.ReferrerID.HasValue ? referrer.ReferrerID.Value : 0;

                                if (!referrer.ReferrerID.HasValue)
                                {
                                    break;
                                }
                            }

                            index++;
                        }
                        while (index < 15);


                    }
                }


                objUser.IsActive = true;
                up.IsActive = true;
                context.SaveChanges();
                LoadData();
                Utils.ShowAlert(this, "Success", "Approved successfully", true);
            }
        }
        catch (Exception ex)
        {
            Utils.ShowAlert(this, "Error", ex.Message, false);
        }
    }

    public void AssignDefaultPages(int userID)
    {
        var con = MyDBContext;
        var defPages = con.Pages.Where(x => x.Title.ToLower() == "sales" || x.URL.ToLower().Contains("salescomission.aspx") || x.URL.ToLower().Contains("dashboard.aspx") || x.URL.ToLower().Contains("sales.aspx")).ToList();

        foreach (var page in defPages)
        {
            con.UserPages.Add(new UserPage() { PageID = page.ID, IsAllowed = true, UserID = userID });
        }

        con.SaveChanges();
    }

    protected void btnDele_Click(object sender, EventArgs e)
    {
        try {
            MyDBContext.Database.ExecuteSqlCommand(@"Delete [dbo].[ProductLevels]
Delete [dbo].[SalesComissionNthLevel]
Delete [dbo].[UserSales]
Delete [dbo].[Userproducts]
Delete from products

Delete from [dbo].[Adjustments]

delete from  [dbo].[CheckOutRequests]

delete  [dbo].[UserNthLevels] where userid <> 1
delete [dbo].[Ledger] 
delete [dbo].[CheckOutrequests] 
delete [dbo].[Userrequests] 
delete userpages where userid <>1
update [dbo].[UserDetails] set parentaccountid = null
delete [dbo].[UserDetails] where id <>1 ");
        }
        catch { }
    }
}