using Context;
using System;
using System.Linq;
using System.Web.UI.WebControls;

public partial class CreateRequest : BaseClass
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                SetTypeOptions();
                LoadProducts();
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

    protected void btnAddRequest_Click(object sender, EventArgs e)
    {
        try
        {
            var context = MyDBContext;

            var req = new UserRequest();
            req.RequestByUserID = Utils.GetUserData().ID;
            req.RequestTitle = ddlType.SelectedItem.Text;
            req.RequestDescription = txtRequest.Text;
            req.RequestType = "User";
            req.CreatedOn = DateTime.Now;
            req.Status = "Pending";
            req.TransactionID = txtTransactionID.Text;

            if (ddlType.SelectedValue.ToLower() == "add product")
            {
                req.ProductID = int.Parse(ddlProduct.SelectedValue);
                req.Quantity = int.Parse(txtQuantity.Text);
            }


            context.UserRequests.Add(req);
            context.SaveChanges();



            Utils.ShowAlert(this, "Success", "Request added successfully.", true);
        }
        catch (Exception ex)
        {
            Utils.ShowAlert(this, "Error", ex.Message, false);
        }
    }

    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SetTypeOptions();
        }
        catch (Exception ex)
        {
            Utils.ShowAlert(this, "Error", ex.Message, false);
        }
    }

    private void SetTypeOptions()
    {
        if (ddlType.SelectedValue.ToLower() == "add product")
        {
            divProduct.Visible = true;
            txtRequest.Enabled = false;
            txtRequest.Text = "Dear super admin, \n\r Please add a number of " + txtQuantity.Text + " " + ddlProduct.SelectedItem.Text + " on my account.";
        }
        else
        {
            txtRequest.Enabled = true;
            divProduct.Visible = false;
        }
    }

    protected void txtQuantity_TextChanged(object sender, EventArgs e)
    {
        try
        {
            txtRequest.Text = "Dear super admin, \n\r Please add a number of " + txtQuantity.Text + " " + ddlProduct.SelectedItem.Text + " on my account.";
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
            txtRequest.Text = "Dear super admin, \n\r Please add a number of " + txtQuantity.Text + " " + ddlProduct.SelectedItem.Text + " on my account.";
        }
        catch (Exception ex)
        {
            Utils.ShowAlert(this, "Error", ex.Message, false);
        }
    }
}