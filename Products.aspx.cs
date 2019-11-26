
using Context;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Products : BaseClass
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
                if (Request["i"] != null)
                {
                    int productID = int.Parse(Request["i"].ToString());
                    hdnProductID.Value = Request["i"].ToString();
                    hdnMode.Value = "Update";
                    btnAddProduct.Visible = false;
                    btnUpdate.Visible = true;

                    var obj = MyDBContext.Products.Where(x => x.ID == productID).FirstOrDefault();

                    if (obj != null)
                    {
                        txtProductName.Text = obj.ProductName;
                        txtCost.Text = obj.FinalSalePrice.ToString();
                        chkIsActive.Checked = obj.IsActive;


                    }
                    else
                    {
                        Utils.ShowAlert(this, "Error", "Invalid Product", false);
                        btnUpdate.Visible = false;
                    }
                }

                Session["Levels"] = null;
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
            string fileName;
            SaveImage(out fileName);
            var context = new ScentaurusEntities2();

            var obj = new Product();

            obj.ProductName = txtProductName.Text;
            obj.FinalSalePrice = decimal.Parse(txtCost.Text);
            obj.IsActive = chkIsActive.Checked;
            obj.ImagePath = fileName;
            obj.CreatedBy = Utils.GetUserName();
            obj.CreatedOn = DateTime.Now;
            context.Products.Add(obj);

            if (context.SaveChanges() > 0)
            {
                hdnProductID.Value = context.Products.OrderByDescending(x => x.ID).FirstOrDefault().ID.ToString();
                divlevels.Visible = true;
                pnlAddProduct.Visible = false;

                lblLevel.Text = (rptLevels.Items.Count + 1).ToString();
                Utils.ShowAlert(this, "Success", "Product added successfully. Please add levels to continue.", true);
            }
            else
            {
                Utils.ShowAlert(this, "Error", "Unable to add product at this time", false);
            }

        }
        catch (Exception ex)
        {
            Utils.ShowAlert(this, "Error", ex.Message, false);
        }
    }

    private void SaveImage(out string fileName)
    {
        fileName = String.Concat(txtProductName.Text.Trim().Replace(" ", "_"), "_", DateTime.Now.ToString("ddMMyyyyhhmmss"), ".png");
        var base64Data = Regex.Match(hdnImagePath.Value, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value;
        byte[] imageBytes = Convert.FromBase64String(base64Data);
        File.WriteAllBytes(String.Concat(Server.MapPath("ProductImages/"), fileName), imageBytes);
    }


    protected void btnAddLevel_Click(object sender, EventArgs e)
    {
        try
        {
            int prodID = int.Parse(hdnProductID.Value);
            var context = new ScentaurusEntities2();
            var levels = Session["Levels"] != null ? (List<ProductLevel>)Session["Levels"] : new List<ProductLevel>();
            context.ProductLevels.Add(new ProductLevel()
            {
                ProductID = prodID
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

            levels = context.ProductLevels.Where(x => x.ProductID == prodID).ToList();
            rptLevels.DataSource = levels;
            rptLevels.DataBind();
            Session["Levels"] = levels;
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
            int prodID = int.Parse(hdnProductID.Value);
            var levels = Session["Levels"] != null ? (List<ProductLevel>)Session["Levels"] : new List<ProductLevel>();
            RepeaterItem item = ((ImageButton)sender).NamingContainer as RepeaterItem;

            var hdnLevelID = int.Parse((item.FindControl("hdnLevelID") as HiddenField).Value);

            var context = new ScentaurusEntities2();
            var pLevel = context.ProductLevels.Where(x => x.ID == hdnLevelID).FirstOrDefault();
            context.ProductLevels.Remove(pLevel);
            context.SaveChanges();

            levels = context.ProductLevels.Where(x => x.ProductID == prodID).OrderBy(x => x.LevelNumber).ToList();
            int levelNumber = 1;

            foreach (ProductLevel level in levels)
            {
                level.LevelNumber = levelNumber;
                levelNumber++;
                context.SaveChanges();
            }

            rptLevels.DataSource = levels;
            rptLevels.DataBind();
            Session["Levels"] = levels;

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

            int prodID = int.Parse(hdnProductID.Value);
            var levels = Session["Levels"] != null ? (List<ProductLevel>)Session["Levels"] : new List<ProductLevel>();
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
            int prodID = int.Parse(hdnProductID.Value);
            var levels = Session["Levels"] != null ? (List<ProductLevel>)Session["Levels"] : new List<ProductLevel>();
            RepeaterItem item = ((ImageButton)sender).NamingContainer as RepeaterItem;

            var hdnLevelID = int.Parse((item.FindControl("hdnLevelID") as HiddenField).Value);
            var txtLevelPercentage = item.FindControl("txtLevelPercentage") as TextBox;

            var ibUpdateLevel = item.FindControl("ibUpdateLevel") as ImageButton;
            var ibEditLevel = item.FindControl("ibEditLevel") as ImageButton;


            var context = new ScentaurusEntities2();
            var pLevel = context.ProductLevels.Where(x => x.ID == hdnLevelID).FirstOrDefault();

            pLevel.Percentage = decimal.Parse
                (txtLevelPercentage.Text);
            context.SaveChanges();
            levels = context.ProductLevels.Where(x => x.ProductID == prodID).OrderBy(x => x.LevelNumber).ToList();

            rptLevels.DataSource = levels;
            rptLevels.DataBind();
            Session["Levels"] = levels;

            Utils.ShowAlert(this, "Success", "Record updated successfully.", true);

        }
        catch (Exception ex)
        {
            Utils.ShowAlert(this, "Error", ex.Message, false);
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            int prodID = int.Parse(hdnProductID.Value);
            var context = new ScentaurusEntities2();
            var levels = Session["Levels"] != null ? (List<ProductLevel>)Session["Levels"] : new List<ProductLevel>();
            var prod = context.Products.Where(x => x.ID == prodID).FirstOrDefault();

            prod.ProductName = txtProductName.Text;
            prod.FinalSalePrice = decimal.Parse(txtCost.Text);
            prod.IsActive = chkIsActive.Checked;

            var result = context.SaveChanges() > 0;

            levels = context.ProductLevels.Where(x => x.ProductID == prodID).ToList();
            rptLevels.DataSource = levels;
            rptLevels.DataBind();
            Session["Levels"] = levels;
            lblLevel.Text = (rptLevels.Items.Count + 1).ToString();
            Utils.ShowAlert(this, "Success", "Product udpated successfully. Please add levels to continue.", true);
            divlevels.Visible = true;
            pnlAddProduct.Visible = false;
        }
        catch (Exception ex)
        {
            Utils.ShowAlert(this, "Error", ex.Message, false);
        }
    }
}