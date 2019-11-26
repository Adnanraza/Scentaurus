using Context;
using System;
using System.Linq;
using System.Web.UI.WebControls;

public partial class _Default : BaseClass
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                LoadProducts();
            }
        }
        catch (Exception ex)
        {
            Utils.ShowAlert(this, "Error", "Something went wrong. Please contact administrator.", false);
        }
    }

    protected void btnAddMember_Click(object sender, EventArgs e)
    {
        try
        {
            var context = MyDBContext;

            if (context.UserDetails.Where(x => x.Username.ToLower() == txtUsername.Text.ToLower()).FirstOrDefault() == null)
            {
                var refer = context.UserDetails.Where(x => x.Username.ToLower() == txtReferBY.Text.ToLower()).ToList().FirstOrDefault();

                if (refer != null)
                {
                    var user = new UserDetail();
                    user.FirstName = txtFirstName.Text;
                    user.LastName = txtLastName.Text;
                    user.Email = txtEmail.Text;
                    user.Username = txtUsername.Text;
                    user.Password = txtUsername.Text;
                    user.ReferrerID = refer.ID;
                    user.ImagePath = "20180146030646.jpg";
                    user.CNIC = txtCNIC.Text;
                    user.FatherName = txtFatherName.Text;
                    user.IsActive = false;
                    user.IsSuperAdmin = false;
                    user.UserType = "Normal";
                    user.ContactNo = txtPhone.Text;
                    user.ContactNo2 = txtPhone2.Text;
                    user.PercentageOfSale = 0;
                    user.CreatedBy = "Web";
                    user.CurrentAddress = txtCurrentAddress.Text;
                    user.PermanentAddress = txtPermanentAddress.Text;
                    user.CreatedOn = DateTime.Now;
                    context.UserDetails.Add(user);

                    if (context.SaveChanges() > 0)
                    {
                        var objUser = context.UserDetails.Where(x => x.Username.ToLower() == txtUsername.Text.ToLower()).OrderByDescending(x => x.ID).FirstOrDefault();
                        var desc = "Dear super admin, \n\r Please add a number of " + txtQuantity.Text + " " + ddlProduct.SelectedItem.Text + " on my account.";
                        var req = new UserRequest();
                        req.RequestByUserID = objUser.ID;
                        req.RequestTitle = "Add Product";
                        req.RequestDescription = desc;
                        req.RequestType = "Web Request";
                        req.CreatedOn = DateTime.Now;
                        req.Status = "Pending";
                        req.ProductID = int.Parse(ddlProduct.SelectedValue);
                        req.Quantity = int.Parse(txtQuantity.Text);
                        req.TransactionID = txtTransactionID.Text;

                        context.UserRequests.Add(req);
                        context.SaveChanges();


                        var up = new UserProduct();

                        up.Quantity = int.Parse(txtQuantity.Text);
                        up.UserID = objUser.ID;
                        up.ProductID = int.Parse(ddlProduct.SelectedValue);
                        up.IsActive = false;
                        user.CreatedBy = "Web";
                        up.CreatedOn = DateTime.Now;

                        context.UserProducts.Add(up);
                        context.SaveChanges();


                        txtFirstName.Text = String.Empty;
                        txtLastName.Text = String.Empty;
                        txtEmail.Text = String.Empty;
                        txtUsername.Text = String.Empty;
                        txtReferBY.Text = String.Empty;
                        user.Password = txtUsername.Text;
                        txtCNIC.Text = String.Empty;
                        txtFatherName.Text = String.Empty;
                        txtPhone.Text = String.Empty;
                        txtPhone2.Text = String.Empty;
                        txtCurrentAddress.Text = String.Empty;
                        txtPermanentAddress.Text = String.Empty;

                        Utils.ShowAlert(this, "Success", "Your registration completed successfully. We will contact you in sometime.", true);
                    }
                }
                else
                {
                    Utils.ShowAlert(this, "Error", "Referrer not found. Please enter a valid username", false);
                }
            }
            else
            {
                Utils.ShowAlert(this, "Error", "Username already in use. Please select a different username.", false);
            }
        }
        catch (Exception ex)
        {
            Utils.ShowAlert(this, "Error", "Something went wrong. Please contact administrator.", false);
        }
    }

    private void LoadProducts()
    {
        var products = MyDBContext.Products.Where(x => x.IsActive).Select(x => new { ID = x.ID, Name = x.ProductName + " - " + x.FinalSalePrice.ToString() }).ToList();
        ddlProduct.DataSource = products;
        ddlProduct.DataTextField = "Name";
        ddlProduct.DataValueField = "ID";
        ddlProduct.DataBind();
        Utils.AddSelectProductItemAtBegining(ddlProduct);
    }
}