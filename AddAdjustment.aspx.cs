using Context;
using System;
using System.Linq;
using System.Web.UI.WebControls;

public partial class AddAdjustment : BaseClass
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Request["i"] != null)
                {
                    hdnUserID.Value = Request["i"].ToString();
                    LoadData();
                }
                else
                {
                    Response.Redirect("Customers.aspx");
                }
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
            int userID = int.Parse(hdnUserID.Value);
            var entity = MyDBContext;

            var adj = new Adjustment();
            adj.UserID = userID;
            adj.CreatedBy = Utils.GetUserName();
            adj.CreatedTimeStamp = DateTime.Now;
            adj.Description = txtDescription.Text;
            adj.Amount = decimal.Parse(txtAmount.Text);
            adj.AdjustmentType = ddlAdjustmentType.SelectedItem.Text;
            adj.Type = decimal.Parse(txtAmount.Text) < 0 ? "Credit" : "Debit";
            entity.Adjustments.Add(adj);
            entity.SaveChanges();

            int adjID = entity.Adjustments.OrderByDescending(x => x.ID).FirstOrDefault().ID;
            Utils.AddEntryInCustomerLedger(userID, decimal.Parse(txtAmount.Text), adjID, ddlAdjustmentType.SelectedItem.Text, "Adjustment added");

            Utils.ShowAlert(this, "Adjustment processed successfully.");
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
        int userID = int.Parse(hdnUserID.Value);
        var entity = MyDBContext;

        var user = entity.UserDetails.FirstOrDefault(x => x.ID == userID);

        for (int i = 0; i < Utils.Adjustments.Length; i++)
        {
            ddlAdjustmentType.Items.Add(new ListItem(Utils.Adjustments[i]));
        }

        var ledger = entity.Ledgers.Where(x => x.UserID == userID);

        if (ledger != null && ledger.ToList().Count > 0)
        {
            var balance = ledger.OrderByDescending(x => x.ID).FirstOrDefault().Balance;
            lblBalance.Text = balance.ToString();
        }
        else
        {
            lblBalance.Text = "No balance";
        }

        lblUserName.Text = user.Username;
        lblName.Text = user.FirstName + " " + user.LastName;
    }

}