using Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Registration : BaseClass
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                SetPercentageTextboxVisibility();
                LoadProducts();
            }
        }

        catch (Exception ex)
        {
            Utils.ShowAlert(this, "Error", ex.Message, false);
        }
    }

    private void SaveImage(out string fileName)
    {
        fileName = "20180146030646.jpg";
        //fileName = String.Concat(txtFirstName.Text, "_", txtLastName.Text, "_", DateTime.Now.ToString("ddMMyyyyhhmmss"), ".png");
        //var base64Data = Regex.Match(hdnImagePath.Value, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value;
        //byte[] imageBytes = Convert.FromBase64String(base64Data);
        //File.WriteAllBytes(String.Concat(Server.MapPath("UserImages/"), fileName), imageBytes);
    }


    protected void btnAddMember_Click(object sender, EventArgs e)
    {
        try
        {
            int userID = 0;
            var context = MyDBContext;

            if (context.UserDetails.Where(x => x.Username.ToLower() == txtUsername.Text.ToLower()).FirstOrDefault() == null)
            {
                var refer = context.UserDetails.Where(x => x.Username.ToLower() == txtReferBY.Text.ToLower()).ToList().FirstOrDefault();

                if (refer != null)
                {
                    var percentage = 0.0M;
                    string fileName;
                    SaveImage(out fileName);
                    decimal.TryParse(txtPercentageOfSale.Text, out percentage);
                    var user = new UserDetail();
                    user.FirstName = txtFirstName.Text;
                    user.LastName = txtLastName.Text;
                    user.Email = txtEmail.Text;
                    user.Username = txtUsername.Text;
                    user.Password = txtUsername.Text;
                    user.ReferrerID = refer.ID;
                    user.ImagePath = fileName;
                    user.CNIC = txtCNIC.Text;
                    user.FatherName = txtFatherName.Text;
                    user.IsActive = true;
                    user.IsSuperAdmin = false;
                    user.UserType = ddlUserType.SelectedValue;
                    user.ContactNo = txtPhone.Text;
                    user.ContactNo2 = txtPhone2.Text;
                    user.PercentageOfSale = percentage;
                    user.CreatedBy = Utils.GetUserName();
                    user.CreatedOn = DateTime.Now;
                    context.UserDetails.Add(user);

                    if (context.SaveChanges() > 0)
                    {
                        if (ddlProduct.SelectedValue != "0")
                        {
                            var productID = int.Parse(ddlProduct.SelectedValue);
                            var objUser = context.UserDetails.Where(x => x.Username.ToLower() == txtUsername.Text.ToLower()).OrderByDescending(x => x.ID).FirstOrDefault();
                            var up = new UserProduct();

                            AssignDefaultPages(objUser.ID);

                            userID = objUser.ID;
                            hdnUserID.Value = objUser.ID.ToString();
                            up.Quantity = int.Parse(txtQuantity.Text);
                            up.UserID = objUser.ID;
                            up.ProductID = int.Parse(ddlProduct.SelectedValue);
                            up.IsActive = true;
                            up.CreatedBy = Utils.GetUserName();
                            up.CreatedOn = DateTime.Now;

                            context.UserProducts.Add(up);
                            context.SaveChanges();

                            var product = context.Products.Where(x => x.ID == productID).FirstOrDefault();

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

                                var firstLevel = context.UserNthLevels.Where(x => x.UserID == refer.ID && x.LevelNumber == 1).FirstOrDefault();

                                if (firstLevel != null)
                                {
                                    //var count = context.UserSales.Where(x => x.AddedOn.Value.Date == DateTime.Now.Date).ToList().Count();
                                    var totalPrice = product.FinalSalePrice * int.Parse(txtQuantity.Text);
                                    var comissionAmount = firstLevel.Percentage * (totalPrice / 100);
                                    var us = new UserSale();

                                    // count++;
                                    us.UserID = refer.ID;
                                    us.ReferredUserID = objUser.ID;
                                    us.Quantity = int.Parse(txtQuantity.Text);
                                    us.ProductPrice = product.FinalSalePrice;
                                    us.SaleProductID = product.ID;
                                    us.ComissionPercentage = firstLevel.Percentage;
                                    us.ComissionAmount = comissionAmount;
                                    us.TotalPrice = product.FinalSalePrice * int.Parse(txtQuantity.Text);
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
                                                var referrerNthLevel = context.UserNthLevels.Where(x => x.UserID == objUser.ID && x.LevelNumber == index).FirstOrDefault();

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

                                var desc = "Dear super admin, \n\r Please add a number of " + txtQuantity.Text + " " + ddlProduct.SelectedItem.Text + " on my account.";
                                var req = new UserRequest();
                                req.RequestByUserID = Utils.GetUserData().ID;
                                req.RequestTitle = "Add Product";
                                req.RequestDescription = desc;
                                req.RequestType = "User";
                                req.CreatedOn = DateTime.Now;
                                req.Status = "Approved";
                                req.TransactionID = txtTransactionID.Text;
                                req.ProductID = int.Parse(ddlProduct.SelectedValue);
                                req.Quantity = int.Parse(txtQuantity.Text);

                                context.UserRequests.Add(req);
                                context.SaveChanges();

                            }

                        }
                    }

                    var levels = context.UserNthLevels.Where(x => x.UserID == userID).ToList();
                    rptLevels.DataSource = levels;
                    rptLevels.DataBind();
                    Session["MemberLevels"] = levels;
                    lblLevel.Text = (rptLevels.Items.Count + 1).ToString();
                    divLevels.Visible = true;
                    divAddMember.Visible = false;
                    Utils.ShowAlert(this, "Success", "User added successfully. Please adjust commission levels.", true);
                }

                else
                {
                    Utils.ShowAlert(this, "Error", "Referrer not found. Please enter a valid referrer.", false);
                }
            }

            else
            {
                Utils.ShowAlert(this, "Error", "User already exists.", false);
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

    void SetPercentageTextboxVisibility()
    {
        if (String.Equals(ddlUserType.SelectedValue, "admin", StringComparison.OrdinalIgnoreCase))
        {
            txtPercentageOfSale.Visible = true;
            divPer.Visible = true;
        }
        else
        {
            txtPercentageOfSale.Visible = false;
            divPer.Visible = false;
        }

    }

    protected void ddlUserType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SetPercentageTextboxVisibility();
        }
        catch (Exception ex)
        {
            Utils.ShowAlert(this, "Error", ex.Message, false);
        }
    }

    protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            Utils.ShowAlert(this, "Error", ex.Message, false);
        }
    }

    protected void btnAddLevel_Click(object sender, EventArgs e)
    {
        try
        {
            int userID = int.Parse(hdnUserID.Value);
            var context = MyDBContext;
            var levels = Session["MemberLevels"] != null ? (List<UserNthLevel>)Session["MemberLevels"] : new List<UserNthLevel>();
            context.UserNthLevels.Add(new UserNthLevel()
            {
                UserID = userID
                  ,
                LevelNumber = int.Parse(lblLevel.Text)
                  ,
                IsActive = true
                  ,
                CreatedBy = Utils.GetUserName()
                  ,
                Percentage = decimal.Parse(txtPercentage.Text)
                  ,
                TimeStamp = DateTime.Now
            });
            var result = context.SaveChanges() > 0;

            levels = context.UserNthLevels.Where(x => x.UserID == userID).ToList();
            rptLevels.DataSource = levels;
            rptLevels.DataBind();
            Session["MemberLevels"] = levels;
            lblLevel.Text = (rptLevels.Items.Count + 1).ToString();
            Utils.ShowAlert(this, "Success", "Level added successfully.", true);
        }
        catch (Exception ex)
        {
            Utils.ShowAlert(this, "Error", ex.Message, false);
        }
    }

    protected void ibDeleteLevel_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            int userID = int.Parse(hdnUserID.Value);
            var levels = Session["MemberLevels"] != null ? (List<UserNthLevel>)Session["MemberLevels"] : new List<UserNthLevel>();
            RepeaterItem item = ((ImageButton)sender).NamingContainer as RepeaterItem;

            var hdnLevelID = int.Parse((item.FindControl("hdnLevelID") as HiddenField).Value);

            var context = new ScentaurusEntities2();
            var pLevel = context.UserNthLevels.Where(x => x.ID == hdnLevelID).FirstOrDefault();
            context.UserNthLevels.Remove(pLevel);
            context.SaveChanges();
            levels = context.UserNthLevels.Where(x => x.UserID == userID).OrderBy(x => x.LevelNumber).ToList();
            int levelNumber = 1;

            foreach (var level in levels)
            {
                level.LevelNumber = levelNumber;
                levelNumber++;
                context.SaveChanges();
            }

            rptLevels.DataSource = levels;
            rptLevels.DataBind();
            Session["MemberLevels"] = levels;

            Utils.ShowAlert(this, "Success", "Record deleted successfully.", true);
        }
        catch (Exception ex)
        {
            Utils.ShowAlert(this, "Error", ex.Message.Replace("'", ""), false);
        }
    }

    protected void ibEditLevel_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            var levels = Session["MemberLevels"] != null ? (List<UserNthLevel>)Session["MemberLevels"] : new List<UserNthLevel>();
            RepeaterItem item = ((ImageButton)sender).NamingContainer as RepeaterItem;
            var txtLevelPercentage = item.FindControl("txtLevelPercentage") as TextBox;

            var ibUpdateLevel = item.FindControl("ibUpdateLevel") as ImageButton;
            var ibEditLevel = item.FindControl("ibEditLevel") as ImageButton;

            txtLevelPercentage.Enabled = true;
            ibUpdateLevel.Visible = true;
            ibEditLevel.Visible = false;
        }
        catch (Exception ex)
        {
            Utils.ShowAlert(this, "Error", ex.Message, false);
        }
    }

    protected void ibUpdateLevel_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            int userID = int.Parse(hdnUserID.Value);
            var levels = Session["MemberLevels"] != null ? (List<UserNthLevel>)Session["MemberLevels"] : new List<UserNthLevel>();
            RepeaterItem item = ((ImageButton)sender).NamingContainer as RepeaterItem;

            var hdnLevelID = int.Parse((item.FindControl("hdnLevelID") as HiddenField).Value);
            var txtLevelPercentage = item.FindControl("txtLevelPercentage") as TextBox;

            var ibUpdateLevel = item.FindControl("ibUpdateLevel") as ImageButton;
            var ibEditLevel = item.FindControl("ibEditLevel") as ImageButton;

            var context = MyDBContext;
            var pLevel = context.UserNthLevels.Where(x => x.ID == hdnLevelID).FirstOrDefault();

            pLevel.Percentage = decimal.Parse
                (txtLevelPercentage.Text);
            context.SaveChanges();
            levels = context.UserNthLevels.Where(x => x.UserID == userID).OrderBy(x => x.LevelNumber).ToList();

            rptLevels.DataSource = levels;
            rptLevels.DataBind();
            Session["MemberLevels"] = levels;

            Utils.ShowAlert(this, "Success", "Record updated successfully.", true);

        }
        catch (Exception ex)
        {
            Utils.ShowAlert(this, "Error", ex.Message, false);
        }
    }

    private void LoadProducts()
    {
        var products = MyDBContext.Products.Where(x => x.IsActive).Select(x => new { ID = x.ID, Name = x.ProductName + " - " + x.FinalSalePrice.ToString() }).ToList();
        ddlProduct.DataSource = products;
        ddlProduct.DataTextField = "Name";
        ddlProduct.DataValueField = "ID";
        ddlProduct.DataBind();
    }
}