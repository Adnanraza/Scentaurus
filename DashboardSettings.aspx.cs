using System;
using System.Web;
using System.IO;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web.UI.HtmlControls;
using Context;

public partial class DashboardSettings : BaseClass
{
    #region Events

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["Message"] != null)
                {
                    Utils.ShowAlert(this, Session["Message"].ToString());
                    Session["Message"] = null;
                }

                if (Utils.GetUserData().ParentAccountID != null)
                {
                    var parentAccountID = Utils.GetUserData().ParentAccountID;
                    var user = MyDBContext.UserDetails.FirstOrDefault(x => x.ID == parentAccountID);
                    txtParentAccount.Text = user.Username;
                }
                else
                {
                    txtParentAccount.Text = String.Empty;
                }
            }
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
            Response.Redirect("Home.aspx");
        }
        catch (Exception ex)
        {
            Utils.ShowAlert(this, "Error", ex.Message, false);
        }
    }

    public static System.Drawing.Image ScaleImage(System.Drawing.Image image, int maxHeight, out bool IsWrongSize)
    {
        bool wrongSize = false;
        int original_width = image.Width;
        int original_height = image.Height;
        var newWidth = 90;
        var newHeight = 90;

        if (original_width != 90 || original_height != 90)
        {
            wrongSize = true;
        }

        var newImage = new Bitmap(newWidth, newHeight);
        using (var g = Graphics.FromImage(newImage))
        {
            g.DrawImage(image, 0, 0, newWidth, newHeight);
        }

        IsWrongSize = wrongSize;
        return newImage;
    }

    public byte[] imageToByteArray(System.Drawing.Image imageIn)
    {
        MemoryStream ms = new MemoryStream();
        imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
        return ms.ToArray();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            bool isWrongSize = false;

            Byte[] imgByte = null;
            string fileName = String.Concat(DateTime.Now.ToString("yyyyMMsshhmmss"), ".jpg");

            if (FileUploadImage.HasFile && FileUploadImage.PostedFile != null)
            {
                int fileLenght = FileUploadImage.PostedFile.ContentLength;
                // if (fileLenght <= 1048576)
                {
                    System.Drawing.Bitmap bmpPostedImage = new System.Drawing.Bitmap(FileUploadImage.PostedFile.InputStream);
                    System.Drawing.Image objImage = ScaleImage(bmpPostedImage, 90, out isWrongSize);
                    //if (isWrongSize)
                    //{
                    //    DisplayMessage("Image size must be 90 x 90 and cannot be more than 1 MB.");
                    //    return;
                    //}

                    imgByte = imageToByteArray(objImage);
                    FileUploadImage.PostedFile.SaveAs(String.Concat(Server.MapPath("UserImages"), @"/", fileName));
                }
                //else
                //{
                //    DisplayMessage("Image size must be 90 x 90 and cannot be more than 1 MB.");
                //    return;
                //}
            }

            var userName = Utils.GetUserName();
            var entity = MyDBContext;
            var obj = entity.UserDetails.Where(x => x.Username.ToLower().Trim() == userName.ToLower().Trim()).FirstOrDefault();
            obj.ImagePath = fileName;
            entity.SaveChanges();

            var img = Master.FindControl("img") as HtmlImage;
            img.Src = "/UserImages/" + fileName;

            HttpContext.Current.ApplicationInstance.CompleteRequest();
            Response.Redirect(@"DashboardSettings.aspx", false);

        }
        catch (Exception ex)
        {
            Utils.ShowAlert(this, "Error", ex.Message, false);
        }
    }

    #endregion Events

    #region Methods

    /// <summary>
    /// This method is used to display exception.
    /// </summary>
    public void DisplayMessage(string message)
    {
        lblErrorMsg.Text = message;
        mpeErrorMsg.Show();
    }

    #endregion  Methods

    protected void btnUpdateSettings_Click(object sender, EventArgs e)
    {
        try
        {
            int userID = Utils.GetUserData().ID;
            var parentAccount = MyDBContext.UserDetails.FirstOrDefault(x => x.Username.ToLower() == txtParentAccount.Text.ToLower());

            if (parentAccount == null)
            {
                Utils.ShowAlert(this, "Error", "No or invalid parent account", false);
            }
            else
            {
                var user = MyDBContext.UserDetails.FirstOrDefault(x => x.ID == userID);

                if (user != null)
                {
                    user.ParentAccountID = parentAccount.ID;
                    MyDBContext.SaveChanges();

                    Utils.SetUserData(user);

                    Session["Message"] = "Parent account activated.";
                    Response.Redirect("DashboardSettings.aspx");
                }
            }
        }
        catch (Exception ex)
        {
            Utils.ShowAlert(this, "Error", ex.Message, false);
        }
    }

    protected void btnMoveAmount_Click(object sender, EventArgs e)
    {
        try
        {
            int userID = Utils.GetUserData().ID;
            var parentID = Utils.GetUserData().ParentAccountID;

            if (parentID != null)
            {
                var context = MyDBContext;
                var com = context.SalesComissionNthLevels.Where(x => x.IsCheckOut == false && x.BeneficierUserID == userID).ToList();
                //var amount = com.Where(x => x.ComissionAmount != null).Sum(x => x.ComissionAmount).Value;
                var ledger = context.Ledgers.Where(x => x.UserID == userID).OrderByDescending(x => x.ID).FirstOrDefault();

                if (ledger != null && ledger.Balance > 0)
                {
                    var amount = ledger.Balance;

                    Utils.AddEntryInCustomerLedger(parentID.Value, amount, null, Utils.AdjustmentType.GetAmountToChildAccount, "");
                    Utils.AddEntryInCustomerLedger(userID, amount * -1, null, Utils.AdjustmentType.MoveAmountToParentAccount, "");

                    foreach (var c in com)
                    {
                        c.IsCheckOut = true;
                        c.Status = "Approved";
                    }

                    context.SaveChanges();
                    Session["Message"] = "(" + amount.ToString() + ") Amount moved to parent account successfully";
                    Response.Redirect("DashboardSettings.aspx");
                }
                else
                {
                    Utils.ShowAlert(this, "Error", "No Amount available.", false);
                }
            }
            else
            {
                Utils.ShowAlert(this, "Error", "No or invalid parent account", false);
            }
        }
        catch (Exception ex)
        {
            Utils.ShowAlert(this, "Error", ex.Message, false);
        }
    }

    protected void btnChangePassword_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtPassword.Text == txtConfirmPassword.Text)
            {
                var context = MyDBContext;
                var userID = Utils.GetUserData().ID;

                var obj = context.UserDetails.Where(x => x.ID == userID).FirstOrDefault();
                obj.Password = txtPassword.Text;
                context.SaveChanges();
                Utils.ShowAlert(this, "Password updated successfully");
            }
            else
            {
                Utils.ShowAlert(this, "Error", "Password mismatch. Please type same password in both.", false);
            }

        }
        catch (Exception ex)
        {
            Utils.ShowAlert(this, "Error", ex.Message, false);
        }
    }
}
