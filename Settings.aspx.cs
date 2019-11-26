using Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Settings : BaseClass
{
    public class Levels
    {
        public int LevelNumber { get; set; }
        public decimal Percentage { get; set; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Session["Settings"] = null;
                var config = MyDBContext.Configs.ToList();

                rptLevels.DataSource = config;
                rptLevels.DataBind();
            }
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
            int prodID = int.Parse(hdnProductID.Value);
            var context = new ScentaurusEntities2();
            var levels = Session["Settings"] != null ? (List<Config>)Session["Settings"] : new List<Config>();
            context.Configs.Add(new Config()
            {
                KeyName = txtKeyNames.Text,
                KeyValue = txtKeyValues.Text,
                IsActive = true
            });
            var result = context.SaveChanges() > 0;

            levels = context.Configs.ToList();
            rptLevels.DataSource = levels;
            rptLevels.DataBind();
            Session["Settings"] = levels;
            Utils.ShowAlert(this, "Success", "Key added successfully.", true);
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
         
            var levels = Session["Settings"] != null ? (List<Config>)Session["Settings"] : new List<Config>();
            RepeaterItem item = ((ImageButton)sender).NamingContainer as RepeaterItem;

            var hdnLevelID = int.Parse((item.FindControl("hdnID") as HiddenField).Value);

            var context = new ScentaurusEntities2();
            var pLevel = context.Configs.Where(x => x.Id == hdnLevelID).FirstOrDefault();
            context.Configs.Remove(pLevel);
            context.SaveChanges();

            levels = context.Configs.ToList();
            rptLevels.DataSource = levels;
            rptLevels.DataBind();
            Session["Settings"] = levels;

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
            var levels = Session["Settings"] != null ? (List<Config>)Session["Settings"] : new List<Config>();
            RepeaterItem item = ((ImageButton)sender).NamingContainer as RepeaterItem;
            var txtLevelPercentage = item.FindControl("txtKeyValue") as TextBox;

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
            var levels = Session["Settings"] != null ? (List<Config>)Session["Settings"] : new List<Config>();
            RepeaterItem item = ((ImageButton)sender).NamingContainer as RepeaterItem;

            var hdnLevelID = int.Parse((item.FindControl("hdnID") as HiddenField).Value);
            var txtValue = item.FindControl("txtKeyValue") as TextBox;

            var ibUpdateLevel = item.FindControl("ibUpdateLevel") as ImageButton;
            var ibEditLevel = item.FindControl("ibEditLevel") as ImageButton;


            var context = new ScentaurusEntities2();
            var obj= context.Configs.Where(x => x.Id == hdnLevelID).FirstOrDefault();

            obj.KeyValue = txtValue.Text;
            context.SaveChanges();
            levels = context.Configs.ToList();

            rptLevels.DataSource = levels;
            rptLevels.DataBind();
            Session["Settings"] = levels;

            Utils.ShowAlert(this, "Success", "Record updated successfully.", true);

        }
        catch (Exception ex)
        {
            Utils.ShowAlert(this, "Error", ex.Message, false);
        }
    }

    protected void btnAddKey_Click(object sender, EventArgs e)
    {
        try
        {
            var context = new ScentaurusEntities2();
            var levels = Session["Settings"] != null ? (List<Config>)Session["Settings"] : new List<Config>();
            context.Configs.Add(new Config()
            {
                KeyName = txtKeyNames.Text,
                KeyValue = txtKeyValues.Text,
                IsActive = true
            });
            var result = context.SaveChanges() > 0;

            levels = context.Configs.ToList();
            rptLevels.DataSource = levels;
            rptLevels.DataBind();
            Session["Settings"] = levels;
            Utils.ShowAlert(this, "Success", "Key added successfully.", true);
        }
        catch (Exception ex)
        {
            Utils.ShowAlert(this, "Error", ex.Message, false);
        }
    }
}