using Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

public partial class UserRequests : BaseClass
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Session["UserRequests"] = null;
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
        string filter = txtFilter.Text.ToLower();

        List<UserRequest> data = MyDBContext.UserRequests.Where(x =>
            x.UserDetail.FirstName.ToLower().Contains(filter) ||
            x.UserDetail.LastName.ToLower().Contains(filter) ||
            x.UserDetail.Username.ToLower().Contains(filter) || string.IsNullOrEmpty(filter)
        ).OrderByDescending(x => x.CreatedOn)//.OrderByDescending(x => x.ID).OrderByDescending(x => x.Status)
        .ToList();

        Session["UserRequests"] = data;
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
        if (Session["UserRequests"] != null)
        {
            List<UserRequest> list = (List<UserRequest>)Session["UserRequests"];
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
            ScentaurusEntities2 context = MyDBContext;
            GridViewRow item = ((Button)sender).NamingContainer as GridViewRow;
            int hdnID = int.Parse((item.FindControl("hdnID") as HiddenField).Value);
            int userID = int.Parse((item.FindControl("hdnUserID") as HiddenField).Value);
            UserRequest req = context.UserRequests.Where(x => x.ID == hdnID).FirstOrDefault();
            req.Status = "Approved";
            context.SaveChanges();

           // AssignDefaultPages(userID);

            ApproveProductRequest(hdnID);

            LoadData();
            Utils.ShowAlert(this, "Request approved successfully.");
        }
        catch (Exception ex)
        {
            Utils.ShowAlert(this, "Error", ex.Message, false);
        }
    }

   

    private void ApproveProductRequest(int ID)
    {
        ScentaurusEntities2 context = MyDBContext;

        UserRequest userRequest = context.UserRequests.FirstOrDefault(x => x.ID == ID);

        if (userRequest != null && userRequest.ProductID != null)
        {
            int? productID = userRequest.ProductID;
            UserDetail objUser = context.UserDetails.Where(x => x.Username.ToLower() == userRequest.UserDetail.Username.ToLower()).OrderByDescending(x => x.ID).FirstOrDefault();
            int userID = userRequest.RequestByUserID;
            UserDetail refer = context.UserDetails.Where(x => x.ID == objUser.ReferrerID).ToList().FirstOrDefault();
            UserProduct up = new UserProduct();

            userID = objUser.ID;
            up.Quantity = userRequest.Quantity;
            up.UserID = objUser.ID;
            up.ProductID = productID;
            up.IsActive = true;
            up.CreatedBy = Utils.GetUserName();
            up.CreatedOn = DateTime.Now;

            context.UserProducts.Add(up);
            context.SaveChanges();

            Product product = context.Products.Where(x => x.ID == productID).FirstOrDefault();

            if (product != null)
            {
                List<ProductLevel> productNth = context.ProductLevels.Where(x => x.ProductID == product.ID).OrderBy(x => x.LevelNumber).ToList();

                foreach (ProductLevel p in productNth)
                {
                    UserNthLevel userNth = new UserNthLevel
                    {
                        IsActive = true,
                        LevelNumber = p.LevelNumber,
                        Percentage = p.Percentage,
                        CreatedBy = Utils.GetUserName(),
                        TimeStamp = DateTime.Now,
                        UserID = objUser.ID
                    };
                    context.UserNthLevels.Add(userNth);
                }

                context.SaveChanges();

                UserNthLevel firstLevel = context.UserNthLevels.Where(x => x.UserID == objUser.ID && x.LevelNumber == 1).FirstOrDefault();

                if (firstLevel != null)
                {
                    decimal? totalPrice = product.FinalSalePrice * userRequest.Quantity;
                    decimal? comissionAmount = firstLevel.Percentage * (totalPrice / 100);
                    UserSale us = new UserSale
                    {

                        // count++;
                        UserID = refer.ID,
                        ReferredUserID = objUser.ID,
                        Quantity = userRequest.Quantity,
                        ProductPrice = product.FinalSalePrice,
                        SaleProductID = product.ID,
                        ComissionPercentage = firstLevel.Percentage,
                        ComissionAmount = comissionAmount,
                        TotalPrice = product.FinalSalePrice * userRequest.Quantity,
                        IsActive = true,
                        AddedOn = DateTime.Now,
                        SalesNo = DateTime.Now.ToString("yyyyMMddhhmmss")// + "-" + count.ToString();
                    };

                    context.UserSales.Add(us);
                    context.SaveChanges();

                    UserSale userSale = context.UserSales.OrderByDescending(x => x.ID).FirstOrDefault();
                    SalesComissionNthLevel nthLevel = new SalesComissionNthLevel
                    {
                        IsActive = true,
                        IsCheckOut = false,
                        UserSaleID = userSale.ID,
                        BeneficierUserID = refer.ID,
                        AddedOn = DateTime.Now,
                        ComissionAmount = comissionAmount,
                        ComissionPercentage = firstLevel.Percentage
                    };
                    context.SalesComissionNthLevels.Add(nthLevel);
                    context.SaveChanges();

                    int referenceID = context.SalesComissionNthLevels.OrderByDescending(x => x.ID).FirstOrDefault().ID;
                    Utils.AddEntryInCustomerLedger(refer.ID, comissionAmount.Value, referenceID, Utils.AdjustmentType.DebitAdjustment, string.Empty);

                    List<UserDetail> admins = context.UserDetails.Where(x => x.IsActive && x.UserType.ToLower().Contains("admin")).ToList();

                    foreach (UserDetail admin in admins)
                    {
                        SalesComissionNthLevel adminComm = new SalesComissionNthLevel();

                        decimal? refCommAmount = (admin.PercentageOfSale.HasValue ? admin.PercentageOfSale.Value : 0) * (totalPrice / 100);
                        adminComm.IsActive = true;
                        adminComm.IsCheckOut = false;
                        adminComm.UserSaleID = userSale.ID;
                        adminComm.BeneficierUserID = admin.ID;
                        adminComm.AddedOn = DateTime.Now;
                        adminComm.ComissionAmount = refCommAmount;
                        adminComm.ComissionPercentage = admin.PercentageOfSale;
                        context.SalesComissionNthLevels.Add(adminComm);
                        context.SaveChanges();

                        int adminReferenceID = context.SalesComissionNthLevels.OrderByDescending(x => x.ID).FirstOrDefault().ID;
                        Utils.AddEntryInCustomerLedger(admin.ID, refCommAmount.Value, adminReferenceID, Utils.AdjustmentType.DebitAdjustment, string.Empty);
                    }



                    if (refer.ReferrerID.HasValue)
                    {

                        int lastReffererID = refer.ReferrerID.Value;
                        int index = 2;

                        do
                        {
                            UserDetail referrer = context.UserDetails.Where(x => x.ID == lastReffererID && x.IsActive).FirstOrDefault();

                            if (referrer != null)
                            {
                                UserNthLevel referrerNthLevel = context.UserNthLevels.Where(x => x.UserID == objUser.ID && x.LevelNumber == index).FirstOrDefault();

                                if (referrerNthLevel != null)
                                {
                                    SalesComissionNthLevel referrerComm = new SalesComissionNthLevel();

                                    decimal? refCommAmount = referrerNthLevel.Percentage * (totalPrice / 100);
                                    referrerComm.IsActive = true;
                                    referrerComm.IsCheckOut = false;
                                    referrerComm.UserSaleID = userSale.ID;
                                    referrerComm.BeneficierUserID = referrer.ID;
                                    referrerComm.AddedOn = DateTime.Now;
                                    referrerComm.ComissionAmount = refCommAmount;
                                    referrerComm.ComissionPercentage = referrerNthLevel.Percentage;
                                    context.SalesComissionNthLevels.Add(referrerComm);
                                    context.SaveChanges();

                                    int scReferenceID = context.SalesComissionNthLevels.OrderByDescending(x => x.ID).FirstOrDefault().ID;
                                    Utils.AddEntryInCustomerLedger(referrer.ID, refCommAmount.Value, scReferenceID, Utils.AdjustmentType.DebitAdjustment, string.Empty);
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

                context.SaveChanges();
            }
        }
    }

    protected void btnReject_Click(object sender, EventArgs e)
    {
        try
        {
            ScentaurusEntities2 context = MyDBContext;
            GridViewRow item = ((Button)sender).NamingContainer as GridViewRow;
            int hdnID = int.Parse((item.FindControl("hdnID") as HiddenField).Value);
            UserRequest req = context.UserRequests.Where(x => x.ID == hdnID).FirstOrDefault();
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