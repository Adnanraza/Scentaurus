using Context;
using System;
using System.Linq;
using System.Web.UI.WebControls;

public partial class AddProduct : BaseClass
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Request["i"] != null)
                {
                    hdnUserID.Value = Request["i"];
                    int userID = int.Parse(hdnUserID.Value);
                    var objUser = MyDBContext.UserDetails.Where(x => x.ID == userID).FirstOrDefault();

                    if (objUser != null)
                    {
                        lblName.Text = objUser.FirstName + " " + objUser.LastName;
                        lblUsername.Text = objUser.Username;
                    }

                    LoadProducts();
                }
                else
                {
                    Response.Redirect("Home");
                }
            }
        }
        catch (Exception ex)
        {
            Utils.ShowAlert(this, "Error", ex.Message, false);
        }
    }

    protected void btnAddProduct_Click(object sender, EventArgs e)
    {
        try
        {
            var context = MyDBContext;
            var productID = int.Parse(ddlProduct.SelectedValue);
            var up = new UserProduct();
            int userID = int.Parse(hdnUserID.Value);
            var objUser = context.UserDetails.Where(x => x.ID == userID).FirstOrDefault();

            if (objUser != null)
            {
                var refer = context.UserDetails.Where(x => x.ID == objUser.ReferrerID).ToList().FirstOrDefault();
                up.Quantity = int.Parse(txtQuantity.Text);
                up.UserID = userID;
                up.ProductID = int.Parse(ddlProduct.SelectedValue);
                up.IsActive = true;
                up.CreatedBy = Utils.GetUserName();
                up.CreatedOn = DateTime.Now;

                context.UserProducts.Add(up);
                context.SaveChanges();

                var product = context.Products.Where(x => x.ID == productID).FirstOrDefault();

                if (product != null)
                {
                    var firstLevel = context.UserNthLevels.Where(x => x.UserID == refer.ID && x.LevelNumber == 1).FirstOrDefault();

                    if (firstLevel != null)
                    {
                        //var count = context.UserSales.Where(x => x.AddedOn.Value.Date == DateTime.Now.Date).ToList().Count();
                        var totalPrice = product.FinalSalePrice * int.Parse(txtQuantity.Text);
                        var comissionAmount = firstLevel.Percentage * (totalPrice / 100);
                        var us = new UserSale();

                        // count++;
                        us.UserID = refer.ID;
                        us.ReferredUserID = userID;
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
                        nthLevel.BeneficierUserID = userID;
                        nthLevel.AddedOn = DateTime.Now;
                        nthLevel.ComissionAmount = comissionAmount;
                        nthLevel.ComissionPercentage = firstLevel.Percentage;
                        context.SalesComissionNthLevels.Add(nthLevel);
                        context.SaveChanges();

                        var refID = context.SalesComissionNthLevels.OrderByDescending(x => x.ID).FirstOrDefault().ID;
                        Utils.AddEntryInCustomerLedger(userID, comissionAmount.Value, refID, Utils.AdjustmentType.DebitAdjustment, String.Empty);


                        var admins = context.UserDetails.Where(x => x.IsActive && x.UserType.ToLower().Contains("admin")).ToList();

                        foreach (var admin in admins)
                        {
                            var adminComm = new SalesComissionNthLevel();

                            var refCommAmount = (admin.PercentageOfSale.HasValue ? admin.PercentageOfSale : 0)  * (totalPrice / 100);
                            adminComm.IsActive = true;
                            adminComm.IsCheckOut = false;
                            adminComm.UserSaleID = userSale.ID;
                            adminComm.BeneficierUserID = admin.ID;
                            adminComm.AddedOn = DateTime.Now;
                            adminComm.ComissionAmount = refCommAmount;
                            adminComm.ComissionPercentage = admin.PercentageOfSale;
                            context.SalesComissionNthLevels.Add(adminComm);
                            context.SaveChanges();

                            var adminRefID = context.SalesComissionNthLevels.OrderByDescending(x => x.ID).FirstOrDefault().ID;
                            Utils.AddEntryInCustomerLedger(admin.ID, refCommAmount.Value, adminRefID, Utils.AdjustmentType.DebitAdjustment, String.Empty);
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

                                        var referrerComID = context.SalesComissionNthLevels.OrderByDescending(x => x.ID).FirstOrDefault().ID;
                                        Utils.AddEntryInCustomerLedger(referrer.ID, refCommAmount.Value, referrerComID, Utils.AdjustmentType.DebitAdjustment, String.Empty);

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

                    Utils.ShowAlert(this, "Product added successfully.");
                }
            }
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