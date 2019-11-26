using Context;
using System;
using System.Linq;
using System.Web.Configuration;
using System.Web.UI.WebControls;

public partial class Masters_Admin : System.Web.UI.MasterPage
{
    private string ParentMenu = @"<li class=''><a href='#' class='dropdown-toggle'><span class='menu-text'>{0}</span><b class='arrow fa fa-angle-down'></b></a><b class='arrow'></b>";
    private string ul = @"<ul class='submenu'>{0}</ul>";
    private string childMenu = @" <li class=''><a href='../{0}'><i class='menu-icon fa fa-caret-right'></i>{1}</a><b class='arrow'></b></li>";

    protected void Page_Init(object sender, EventArgs e)
    {

        if (Session["UserData"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        else
        {
            if (!IsPostBack)
            {
                IsUserAllowedToViewPage();

                UserDetail user = Utils.GetUserData();
                img.Src = "/UserImages/" + user.ImagePath;
                lblUserName.Text = user.FirstName;
                LoadMenu();
            }
        }
    }

    private void LoadCheckOutButton()
    {
        ScentaurusEntities2 entity = new ScentaurusEntities2();
        int userID = Utils.GetUserData().ID;

        if (entity.CheckOutRequests.Where(x => x.UserID == userID && x.Status == "Pending").ToList().Count > 0)
        {
            btnCheckOut.Text = "Check Out Pending";
            btnCheckOut.Enabled = false;
            btnCheckOut.ToolTip = "A check out request is already in pending state";
        }
        else
        {
            Ledger ledger = entity.Ledgers.OrderByDescending(x => x.ID).FirstOrDefault(x => x.UserID == userID);

            if (ledger != null && ledger.Balance > 0)
            {
                decimal amount = ledger.Balance;
                btnCheckOut.Text = "Check Out RS/" + amount.ToString();
            }
            else
            {
                btnCheckOut.Text = "No Amount to Check Out";
                btnCheckOut.Enabled = false;
            }
        }
    }

    private void LoadMenu()
    {
        LoadCheckOutButton();

        string menuContent = string.Empty;
        ScentaurusEntities2 entity = new ScentaurusEntities2();
        string userName = Utils.GetUserName();

        System.Collections.Generic.List<UserPage> pMenu = entity.UserPages.Where(x => x.UserDetail.Username.ToLower().Trim() == userName.ToLower().Trim()
        && (x.IsAllowed.HasValue ? x.IsAllowed.Value : false) && x.Page.ParentID == null).OrderBy(x => x.Page.SortOrder).ToList();

        foreach (UserPage item in pMenu)
        {
            menuContent += LoadMenuContent(item.PageID.Value);
        }

        lMenu.Text = menuContent;
    }

    private string LoadMenuContent(int pageID)
    {
        int userID = Utils.GetUserData().ID;
        string content = string.Empty;
        ScentaurusEntities2 entity = new ScentaurusEntities2();
        System.Collections.Generic.List<UserPage> pages = entity.UserPages.Where(x => x.UserID == userID && x.Page.ParentID == pageID).OrderBy(x => x.Page.SortOrder).ToList();

        if (pages.Count > 0)
        {
            Page cpage = entity.Pages.Where(x => x.ID == pageID).FirstOrDefault();
            content += string.Format(ParentMenu, cpage.Title);
            string childM = string.Empty;

            foreach (UserPage item in pages)
            {
                childM += LoadMenuContent(item.Page.ID);
            }

            content += string.Format(ul, childM);
        }
        else
        {
            Page cpage = entity.Pages.Where(x => x.ID == pageID).FirstOrDefault();
            content += string.Format(childMenu, cpage.URL, cpage.Title);
        }

        return content;
    }

    protected void Page_Load(object sender, EventArgs e)
    {



    }

    private void IsUserAllowedToViewPage()
    {

        string BaseUrl = WebConfigurationManager.AppSettings["BaseURL"];
        string Pagename = Request.Url.ToString().Replace(BaseUrl, "").Trim();
        string segmentPageName = Request.Url.Segments[Request.Url.Segments.Length - 1];

        if (!new string[] { "notauthorized.aspx", "dashboardsettings.aspx", "createrequest.aspx", "logout.aspx", "home.aspx" }.Any(x => x == segmentPageName.ToLower()))
        {
            ScentaurusEntities2 entity = new ScentaurusEntities2();
            string userName = Utils.GetUserName();
            System.Collections.Generic.List<UserPage> pages = entity.UserPages.Where(x => x.UserDetail.Username.ToLower().Trim() == userName.ToLower().Trim()
            && (segmentPageName.ToLower().Trim().Contains(x.Page.URL.ToLower().Trim()) || Pagename.ToLower().Trim().Contains(x.Page.URL.ToLower().Trim()))
            ).ToList();

            if (pages.Count == 0)
            {
                if (new string[] { "customerledger.aspx", "editmember.aspx", "addadjustment.aspx", "feelist.aspx", "stdledger.aspx", "addproduct.aspx" }.Any(x => x == segmentPageName.ToLower()))
                {
                    System.Collections.Generic.List<UserPage> p = entity.UserPages.Where(x => x.UserDetail.Username.ToLower().Trim() == userName.ToLower().Trim() && "customers.aspx".Contains(x.Page.URL.ToLower().Trim())).ToList();

                    if (p.Count == 0)
                    {
                        Response.Redirect("NotAuthorized.aspx");
                    }
                }
                else
                {
                    Response.Redirect("NotAuthorized.aspx");
                }
            }

        }
    }

    protected void btnCheckOut_Click(object sender, EventArgs e)
    {
        try
        {
            ScentaurusEntities2 context = new ScentaurusEntities2();

            Config checkOutLimit = context.Configs.Where(x => x.KeyName.ToLower() == "checkoutlimit").FirstOrDefault();

            int userID = Utils.GetUserData().ID;
            IQueryable<SalesComissionNthLevel> com = context.SalesComissionNthLevels.Where(x => x.BeneficierUserID == userID && x.IsCheckOut == false);
            //decimal? amount = com.Sum(x => x.ComissionAmount);

            decimal? amount = context.Ledgers.OrderByDescending(x => x.ID).FirstOrDefault(x => x.UserID == userID).Balance;

            if (amount >= decimal.Parse(checkOutLimit.KeyValue))
            {
                foreach (SalesComissionNthLevel c in com)
                {
                    c.Status = "Pending";
                }

                context.SaveChanges();

                CheckOutRequest coRequest = new CheckOutRequest
                {
                    UserID = userID,
                    TimeStamp = DateTime.Now,
                    Status = "Pending",
                    Amount = amount.HasValue ? amount.Value : 0,
                    CreatedBy = Utils.GetUserName(),
                    Description = "Checking out request of amount " + amount.ToString()
                };

                context.CheckOutRequests.Add(coRequest);
                context.SaveChanges();
                LoadCheckOutButton();
                Utils.ShowAlert(this, "A Check out request generated against your amount successfully. Please collect your amount after it gets approved.");
            }
            else
            {
                Utils.ShowAlert(this, "Error", "Cannot check out request until you have atleast RS:" + checkOutLimit.KeyValue + " in your account.", false);
            }
        }
        catch (Exception ex)
        { Utils.ShowAlert(this, "Error", ex.Message, false); }
    }
}
