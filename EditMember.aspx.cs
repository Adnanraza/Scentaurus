using Context;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EditMember : BaseClass
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Request["i"] == null)
                {
                    Response.Redirect("Customers.aspx");
                }
                else
                {
                    hdnUserID.Value = Request["i"].ToString();
                    LoadCustomer();
                    SetPercentageTextboxVisibility();
                }
            }
        }

        catch (Exception ex)
        {
            Utils.ShowAlert(this, "Error", ex.Message, false);
        }
    }

    private void SaveImage(out string fileName)
    {
        fileName = String.Concat(txtFirstName.Text, "_", txtLastName.Text, "_", DateTime.Now.ToString("ddMMyyyyhhmmss"), ".png");
        var base64Data = Regex.Match(hdnImagePath.Value, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value;
        byte[] imageBytes = Convert.FromBase64String(base64Data);
        File.WriteAllBytes(String.Concat(Server.MapPath("UserImages/"), fileName), imageBytes);
    }

    private void LoadCustomer()
    {
        var userID = int.Parse(hdnUserID.Value);
        var context = MyDBContext;
        var user = context.UserDetails.Where(x => x.ID == userID).FirstOrDefault();

        if (user != null)
        {
            txtFirstName.Text = user.FirstName;
            txtLastName.Text = user.LastName;
            txtEmail.Text = user.Email;
            lblUserName.Text = user.Username;
            txtCNIC.Text = user.CNIC;
            txtFatherName.Text = user.FatherName;
            txtPhone.Text = user.ContactNo;
            txtPhone2.Text = user.ContactNo2;
            txtPercentageOfSale.Text = user.PercentageOfSale.HasValue ? user.PercentageOfSale.ToString() : String.Empty;
            txtCOmissionUpto.Text = user.ComissionUptoLevel.HasValue ? user.ComissionUptoLevel.ToString() : String.Empty;
            txtCurrentAddress.Text = user.CurrentAddress;
            txtPermanentAddress.Text = user.PermanentAddress;

            if (ddlUserType.Items.FindByValue(user.UserType) != null)
            {
                ddlUserType.Items.FindByValue(user.UserType).Selected = true;
            }

            var refer = context.UserDetails.Where(x => x.ID == user.ReferrerID).FirstOrDefault();

            if (refer != null)
            {
                lblReferBy.Text = refer.Username;
            }
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            var userID = int.Parse(hdnUserID.Value);
            var context = MyDBContext;
            var percentage = 0.0M;
            var user = context.UserDetails.Where(x => x.ID == userID).FirstOrDefault();

            decimal.TryParse(txtPercentageOfSale.Text, out percentage);
            user.FirstName = txtFirstName.Text;
            user.LastName = txtLastName.Text;
            user.Email = txtEmail.Text;
            user.CNIC = txtCNIC.Text;
            user.FatherName = txtFatherName.Text;
            user.UserType = ddlUserType.SelectedValue;
            user.ContactNo = txtPhone.Text;
            user.ContactNo2 = txtPhone2.Text;
            user.PercentageOfSale = percentage;
            user.CurrentAddress = txtCurrentAddress.Text;
            user.PermanentAddress = txtPermanentAddress.Text;
            context.SaveChanges();

            var levels = context.UserNthLevels.Where(x => x.UserID == userID).ToList();
            rptLevels.DataSource = levels;
            rptLevels.DataBind();
            Session["MemberLevels"] = levels;
            lblLevel.Text = (rptLevels.Items.Count + 1).ToString();
            divLevels.Visible = true;
            divAddMember.Visible = false;
            Utils.ShowAlert(this, "Success", "User updated successfully. Please adjust commission levels.", true);

        }
        catch (Exception ex)
        {
            Utils.ShowAlert(this, "Error", ex.Message, false);
        }
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
}