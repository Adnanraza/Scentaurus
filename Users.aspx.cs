using Context;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Users : BaseClass
{
    #region Global Declarations

    // Image object for sorting
    private Image sortImage = new Image();

    //const string Ascending = Labels.ESIID.Ascending;
    //const string Descending = Labels.ESIID.Descending;

    private int? sortColumnIndex = null;
    private bool? isAscending = null;

    #endregion Global Declarations

    #region Properties


    #endregion

    #region Events


    protected void chkSelect_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            GridViewRow gvr = ((CheckBox)sender).NamingContainer as GridViewRow;
            HiddenField hdnID = gvr.FindControl("hdnID") as HiddenField;
            CheckBox chkSelect = gvr.FindControl("chkSelect") as CheckBox;
            int userID = int.Parse(hdnID.Value);

            if (chkSelect.Checked)
            {
                ScentaurusEntities2 entity = MyDBContext;
                UserDetail std = entity.UserDetails.Where(x => x.ID == userID).FirstOrDefault();
                std.IsActive = true;
                entity.SaveChanges();
                FillGrid();
                Utils.ShowAlert(this, "User activated successfully.");
            }
            else
            {
                ScentaurusEntities2 entity = MyDBContext;
                UserDetail std = entity.UserDetails.Where(x => x.ID == userID).FirstOrDefault();
                std.IsActive = false;
                entity.SaveChanges();
                FillGrid();
                Utils.ShowAlert(this, "User inactivated successfully.");
            }

        }
        catch (Exception ex)
        {
            Utils.ShowAlert(this, "Error", ex.Message, false);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //  ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'> $('#ContentPlaceHolder1_PageUpdateProgress').parent('.modalPopup').addClass('progress');$('input[type=radio]').addClass('css-checkbox');$('input[type=radio] + label').addClass('css-label');$('input[type=checkbox]').addClass('css-checkbox'); $('input[type=checkbox] + label').addClass('css-label-chk'); $('.abc th .css-checkbox').click(function () { var checked = 'checked'; var checkedstatus = this.checked;var checkbox = $(this).parents('.widget-content').find('.abc tr td .css-checkbox');checkbox.each(function () {this.checked = checkedstatus;}); });</script>", false);


            txtPassword.Attributes["type"] = "password";

            if (!IsPostBack)
            {
                ViewState["SortedColumnIndex"] = null;
                ViewState["IsAscending"] = null;
                PageLoadSetting();

                if (Request.QueryString["i"] != null && Request.QueryString["i"] == "1")
                {
                    tblDetail.Visible = true;
                    tblListing.Visible = false;
                    FillPages(string.Empty);
                    RefreshControls(true);
                    lbl_MainHeading.InnerText = "Add User";
                    hdnMode.Value = "Add";
                    btnSave.Text = "Add";

                    LoadRepeater();
                }
                else if (Request.QueryString["i"] != null && Request.QueryString["i"] == "2")
                {
                    tblDetail.Visible = false;
                    tblListing.Visible = true;
                    lbl_MainHeading.InnerText = "View Users";

                    FillGrid();
                }
                else
                {
                    Response.Redirect("Home.aspx");
                }
            }
        }
        catch (Exception)
        {
            DisplayMessage(@"Error while getting information.");

        }
    }



    protected void btn_SaveAlert_Click(object sender, EventArgs e)
    {
        DeleteRecord(int.Parse(hdnIsDelete.Value), hdnUserName.Value);
    }

    protected void btn_CancelAlert_Click(object sender, EventArgs e)
    {
        mpe_Alert.Hide();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        RefreshControls(false);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            Page.Validate();

            if (Page.IsValid == false)
            {
                return;
            }

            ScentaurusEntities2 entity = MyDBContext;
            string userName = txtUserName.Text;
            string password = txtPassword.Text.Trim();
            string firstName = txtFirstName.Text;
            string lastName = txtLastName.Text;
            int userType = int.Parse(ddl_Type.SelectedValue);
            //int camp = int.Parse((Master.FindControl("ddlCampus") as DropDownList).SelectedValue);
            // int camp = Utils.GetCampusIDForAddition();
            DataTable dtUsers = new DataTable();
            DataTable dtFilter = new DataTable();

            List<UserDetail> userData = entity.UserDetails.Where(x => x.Username.ToLower().Trim() == userName.ToLower().Trim()).ToList();

            if (hdnMode.Value == "Add")
            {
                if (userData.Count > 0)
                {
                    UserDetail obj = new UserDetail
                    {
                        Username = txtUserName.Text.Trim(),
                        Password = txtPassword.Text,
                        //   obj.Role = ddl_Type.SelectedItem.Text;
                        FirstName = txtFirstName.Text,
                        LastName = txtLastName.Text,
                        IsActive = true,
                        // obj.CampusID = camp;
                        CreatedBy = Utils.GetUserName(),
                        CreatedOn = DateTime.Now
                    };

                    entity.UserDetails.Add(obj);

                    if (entity.SaveChanges() > 0)
                    {
                        int userID = entity.UserDetails.OrderByDescending(x => x.ID).FirstOrDefault().ID;

                        foreach (RepeaterItem ri in rpPages.Items)
                        {
                            // chkParentPage hdnID cblPages
                            HiddenField hdnID = ri.FindControl("hdnID") as HiddenField;
                            CheckBox chkParentPage = ri.FindControl("chkParentPage") as CheckBox;
                            CheckBoxList cblPages = ri.FindControl("cblPages") as CheckBoxList;

                            if (chkParentPage.Checked)
                            {
                                UserPage up = new UserPage
                                {
                                    PageID = int.Parse(hdnID.Value),
                                    UserID = userID,
                                    IsAllowed = true
                                };
                                entity.UserPages.Add(up);
                                entity.SaveChanges();

                                foreach (ListItem item in cblPages.Items)
                                {
                                    if (item.Selected)
                                    {
                                        UserPage upc = new UserPage
                                        {
                                            PageID = int.Parse(item.Value),
                                            UserID = userID,
                                            IsAllowed = true
                                        };
                                        entity.UserPages.Add(upc);
                                        entity.SaveChanges();
                                    }
                                }
                            }
                        }


                        Utils.ShowAlert(this, "User Added successfully.");
                        btnCancel_Click(null, null);
                    }
                }
                else
                {
                    Utils.ShowAlert(this, "Error", "Username already exist.", false);
                }
            }

            else
            {
                UserDetail obj = entity.UserDetails.Where(x => x.Username.ToLower().Trim() == hdnUserName.Value.ToLower().Trim()).FirstOrDefault();

                //obj.Username = txtUserName.Text.Trim();
                obj.Password = txtPassword.Text;
                //obj.Role = ddl_Type.SelectedItem.Text;
                obj.FirstName = txtFirstName.Text;
                obj.LastName = txtLastName.Text;
                // obj.IsActive = true;
                //obj.CampusID = camp;
                //obj.CreatedBy = Utils.GetUserName();
                //obj.CreatedTimeStamp = DateTime.Now.ToString();

                ///entity.Users.Add(obj);
                entity.SaveChanges();
                //if (entity.SaveChanges() > 0)
                {
                    List<UserPage> upl = entity.UserPages.Where(x => x.UserID == obj.ID).ToList();
                    int userID = obj.ID;

                    foreach (UserPage i in upl)
                    {
                        entity.UserPages.Remove(i);
                    }


                    foreach (RepeaterItem ri in rpPages.Items)
                    {
                        // chkParentPage hdnID cblPages
                        HiddenField hdnID = ri.FindControl("hdnID") as HiddenField;
                        CheckBox chkParentPage = ri.FindControl("chkParentPage") as CheckBox;
                        CheckBoxList cblPages = ri.FindControl("cblPages") as CheckBoxList;

                        if (chkParentPage.Checked)
                        {
                            UserPage up = new UserPage
                            {
                                PageID = int.Parse(hdnID.Value),
                                UserID = userID,
                                IsAllowed = true
                            };
                            entity.UserPages.Add(up);
                            entity.SaveChanges();

                            foreach (ListItem item in cblPages.Items)
                            {
                                if (item.Selected)
                                {
                                    UserPage upc = new UserPage
                                    {
                                        PageID = int.Parse(item.Value),
                                        UserID = userID,
                                        IsAllowed = true
                                    };
                                    entity.UserPages.Add(upc);
                                    entity.SaveChanges();
                                }
                            }
                        }
                    }

                    Utils.ShowAlert(this, "User updated successfully.");
                    btnCancel_Click(null, null);
                    FillGrid();

                }
            }

        }
        catch (Exception)
        {
            DisplayMessage(@"Error while saving information.");
        }
    }

    protected void chkShowPassword_CheckedChanged(object sender, EventArgs e)
    {
        //lblPassword.Visible = chkShowPassword.Checked;
    }

    public void LoadDataFromSession()
    {
        List<UserDetail> data = (List<UserDetail>)Session["UsersPageData"];

        if (data.Count > 0)
        {
            gvw_Search.DataSource = data;
            gvw_Search.DataBind();
        }
        else
        {
            gvw_Search.DataSource = null;
            gvw_Search.DataBind();
        }
    }

    protected void gvw_Search_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvw_Search.PageIndex = e.NewPageIndex;
        LoadDataFromSession();


    }

    protected void gvw_Search_Sorting(object sender, GridViewSortEventArgs e)
    {
        //try
        //{
        //    DataTable dtUsers = new DataTable();
        //    DataView sortedView = null;
        //    StringBuilder builder = new StringBuilder();

        //    string imagePath = string.Empty;
        //    RemoveSortingImage();
        //    int columnIndex = 0; // First column of grid view.

        //    // Retrieving contacts data from View state
        //    dtUsers = (DataTable)Session[Labels.Users.GridData]; ;
        //    {
        //        string sortingDirection = string.Empty;
        //        sortedView = new DataView(dtUsers);

        //        if (SortDirection == SortDirection.Ascending)
        //        {
        //            ViewState["IsAscending"] = "false";
        //            SortDirection = SortDirection.Descending;
        //            sortingDirection = Descending;

        //            imagePath = ConfigurationManager.AppSettings["SortAscending"].ToString();
        //            sortImage.ImageUrl = imagePath;
        //        }
        //        else
        //        {
        //            isAscending = true;
        //            ViewState["IsAscending"] = "true";
        //            SortDirection = SortDirection.Ascending;
        //            sortingDirection = Ascending;

        //            imagePath = ConfigurationManager.AppSettings["SortDescending"].ToString();
        //            sortImage.ImageUrl = imagePath;
        //        }

        //        builder.Append(e.SortExpression);
        //        builder.Append(StringLiterals.Values.WhiteSpace);
        //        builder.Append(sortingDirection);
        //        sortedView.Sort = builder.ToString();

        //        gvw_Search.DataSource = sortedView;
        //        gvw_Search.DataBind();
        //        Session[Labels.Users.GridData] = sortedView.ToTable();

        //        foreach (DataControlFieldHeaderCell headerCell in gvw_Search.HeaderRow.Cells)
        //        {
        //            if (headerCell.ContainingField.SortExpression == e.SortExpression)
        //            {
        //                columnIndex = gvw_Search.HeaderRow.Cells.GetCellIndex(headerCell);
        //            }
        //        }
        //        gvw_Search.HeaderRow.Cells[columnIndex].Controls.Add(sortImage);
        //        sortColumnIndex = columnIndex;
        //        ViewState["SortedColumnIndex"] = columnIndex;
        //    }
        //    dtUsers.Dispose();
        //}
        //catch (Exception ex)
        //{
        //    DisplayMessage(@"Error while sorting the records.");
        //    Utilities.AddLogEntry(ex.Message, Utilities.GetValue(Session[SessionKeys.PageName]), "gvw_Search_Sorting");
        //}
    }

    protected void tvPages_TreeNodePopulate(object sender, TreeNodeEventArgs e)
    {
        try
        {
            // PopulateSubNodes(int.Parse(e.Node.Value), e.Node);
            // SelectParents(e.Node,e.Node.Checked);
        }
        catch (Exception)
        {
            DisplayMessage(@"Error while getting information.");
        }
    }

    protected void tvTemplates_TreeNodePopulate(object sender, TreeNodeEventArgs e)
    {
        try
        {
            PopulateSubNodes(int.Parse(e.Node.Value), e.Node);
        }
        catch (Exception)
        {
            DisplayMessage(@"Error while getting information.");

        }
    }

    protected void gvw_Search_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //try
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        Label Status = e.Row.FindControl("gv_lblIsLocked") as Label;
        //        Label lblUserType = e.Row.FindControl("gv_lblUserType") as Label;

        //        if (lblUserType.Text == StringLiterals.Values.OneValue)
        //        {
        //            lblUserType.Text = "Administrator";
        //        }
        //        else if (lblUserType.Text == StringLiterals.Values.TwoValue)
        //        {
        //            lblUserType.Text = "General";
        //        }
        //        else if (lblUserType.Text == StringLiterals.Values.ThreeValue)
        //        {
        //            lblUserType.Text = "View Only";
        //        }

        //        LinkButton btnAllowAccess = e.Row.FindControl("lnkAllowAccess") as LinkButton;
        //        if (bool.Parse(Status.Text))
        //        {
        //            btnAllowAccess.Visible = true;
        //        }
        //        else
        //        {
        //            btnAllowAccess.Visible = false;
        //        }
        //    }

        //}
        //catch (Exception ex)
        //{
        //    DisplayMessage(@"Error while getting information.");
        //    Utilities.AddLogEntry(ex.Message, Utilities.GetValue(Session[SessionKeys.PageName]), "grd_RowDataBound");
        //}
    }

    protected void ddl_Type_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        //if (hdnMode.Value == Labels.Users.InsertMode)
        //{
        //    if (ddl_Type.SelectedValue == "1")
        //    {
        //        DataTable dtParents = new DataTable();
        //        tvPages.Nodes.Clear();

        //        UsersOps.GetAllParentPagesByUserName("admin", out dtParents);

        //        if (dtParents.Select("ParentID = -1").Length > 0)
        //        {
        //            dtParents = dtParents.Select("ParentID = -1 AND PageTitle <> 'Dashboard/User'", "OrderNo").CopyToDataTable(); // AND PageTitle <> 'Dashboard/User'
        //        }

        //        dtParents.DefaultView.RowFilter = Labels.Users.PageTitleFilter;
        //        PopulateAdminNodes(dtParents.DefaultView, tvPages.Nodes);
        //        tvPages.CollapseAll();
        //        dtParents.DefaultView.RowFilter = Labels.Users.DocumentFilter;
        //        dtParents.Dispose();
        //    }
        //    else
        //    {
        //        FillPages(string.Empty);
        //    }
        //}
        //else
        //{
        //    if (ddl_Type.SelectedValue == "1")
        //    {
        //        DataTable dtParents = new DataTable();
        //        tvPages.Nodes.Clear();

        //        if (!string.IsNullOrEmpty(txtUserName.Text.Trim()))
        //        {
        //            UsersOps.GetAllParentPagesByUserName(txtUserName.Text.Trim(), out dtParents);

        //            if (dtParents.Select("ParentID = -1").Length > 0)
        //            {
        //                dtParents = dtParents.Select("ParentID = -1 AND PageTitle <> 'Dashboard/User'", "OrderNo").CopyToDataTable(); // AND PageTitle <> 'Dashboard/User'
        //            }

        //            dtParents.DefaultView.RowFilter = Labels.Users.PageTitleFilter;
        //            PopulateAdminNodes(dtParents.DefaultView, tvPages.Nodes);
        //            tvPages.CollapseAll();
        //            dtParents.DefaultView.RowFilter = Labels.Users.DocumentFilter;
        //            dtParents.Dispose();
        //        }
        //    }
        //    else
        //    {
        //        DataTable dtParents = new DataTable();
        //        tvPages.Nodes.Clear();

        //        if (!string.IsNullOrEmpty(txtUserName.Text.Trim()))
        //        {
        //            UsersOps.GetAllParentPagesByUserName(txtUserName.Text.Trim(), out dtParents);

        //            if (dtParents.Select("ParentID = -1").Length > 0)
        //            {
        //                dtParents = dtParents.Select("ParentID = -1 AND PageTitle <> 'Dashboard/User'", "OrderNo").CopyToDataTable(); // AND PageTitle <> 'Dashboard/User'
        //            }

        //            dtParents.DefaultView.RowFilter = Labels.Users.PageTitleFilter;
        //            PopulateNodes(dtParents.DefaultView, tvPages.Nodes);
        //            tvPages.CollapseAll();
        //            dtParents.DefaultView.RowFilter = Labels.Users.DocumentFilter;
        //            dtParents.Dispose();
        //        }
        //    }
        //}
    }

    #endregion Events

    #region Methods

    /// <summary>
    /// This method is used to fill the grid view.
    /// </summary>
    private void FillGrid()
    {
        try
        {
            ScentaurusEntities2 entity = MyDBContext;
            //x.IsActive == true
            List<UserDetail> data = entity.UserDetails.Where(x => x.Username != "Admin").ToList();
            Session["UsersPageData"] = data;
            LoadDataFromSession();

            tblListing.Visible = true;
            tblDetail.Visible = false;

        }
        catch (Exception)
        {
            DisplayMessage(@"Error while getting information.");

        }
    }

    public void DisplayAlert(string message)
    {
        lbl_AlertMsg.Text = message;
        mpe_Alert.Show();
    }

    // Method to add default image on sorted column
    protected void AddSortingImage()
    {
        string imagePath = string.Empty;
        imagePath = ConfigurationManager.AppSettings["SortDescending"].ToString();
        Image img = new Image
        {
            ImageUrl = imagePath,
            ID = "imgSorting"
        };

        // cell 5 is Utility here
        // adding default sorting image on utility column
        GridViewRow row = gvw_Search.HeaderRow;
        row.Cells[2].Controls.Add(img);
    }

    // Method to remove default image on sorted column
    protected void RemoveSortingImage()
    {
        GridViewRow row = gvw_Search.HeaderRow;
        //Image img = (Image)row.Cells[5].FindControl("imgSorting");

        foreach (DataControlFieldHeaderCell headerCell in gvw_Search.HeaderRow.Cells)
        {
            int cellIndex = gvw_Search.HeaderRow.Cells.GetCellIndex(headerCell);
            Image img = (Image)row.Cells[cellIndex].FindControl("imgSorting");
            row.Cells[cellIndex].Controls.Remove(img);
        }

        // Removing default image for sorting from utility column

    }

    /// <summary>
    /// This method is used to display the message.
    /// </summary>
    public void DisplayMessage(string message)
    {
        lblErrorMsg.Text = message;
        mpeErrorMsg.Show();
    }

    /// <summary>
    /// This method is used to populate sub nodes.
    /// </summary>
    private void PopulateSubNodes(int parentID, TreeNode parentnode)
    {
        try
        {
            ScentaurusEntities2 entity = MyDBContext;
            List<UserPage> data = entity.UserPages.Where(x => x.UserDetail.Username.ToLower() == txtUserName.Text.ToLower() && x.Page.ParentID == parentID).ToList();

            PopulateNodes(data, parentnode.ChildNodes);
        }
        catch (Exception)
        {
            DisplayMessage(@"Error while getting child pages.");
        }
    }

    /// <summary>
    /// This method is used to PopulateDashboardUserNodes.
    /// </summary>
    private void PopulateDashboardUserNodes(DataView dview, TreeNodeCollection nodes)
    {
        try
        {
            //TreeNode tnode = null;

            //foreach (DataRowView row in dview)
            //{
            //    DataTable dtChilds = new DataTable();

            //    tnode = new TreeNode();

            //    tnode.Checked = true;
            //    tnode.Text = row[ColumnNames.PageTitle].ToString();
            //    tnode.Value = row[ColumnNames.PageID].ToString();
            //    tnode.SelectAction = TreeNodeSelectAction.None;
            //    nodes.Add(tnode);

            //    UsersOps.GetAllChildPagesByUserNameAndParentID(tnode.Text, long.Parse(tnode.Value), out dtChilds);
            //    tnode.PopulateOnDemand = true;
            //    if (!Utilities.IsDataTableNullOrEmpty(dtChilds))
            //    {

            //        PopulateDashboardUserNodes(dtChilds.DefaultView, nodes);
            //    }

            //    dtChilds.Dispose();
            //}
        }
        catch (Exception)
        {
            DisplayMessage(@"Error while getting child pages.");
        }
    }

    private void SelectParents(TreeNode node, bool isChecked)
    {
        TreeNode parent = node.Parent;

        if (parent == null)
        {
            return;
        }

        if (isChecked)
        {
            parent.Checked = true; // we should always check parent
            SelectParents(parent, true);
        }
        else
        {
            if (parent.ChildNodes.Cast<TreeNode>().Any(n => n.Checked))
            {
                return; // do not uncheck parent if there other checked nodes
            }

            SelectParents(parent, false); // otherwise uncheck parent
        }
    }

    private void PopulateAdminNodes(DataView dview, TreeNodeCollection nodes)
    {
        try
        {

            //foreach (DataRowView row in dview)
            //{
            //    DataTable dtChilds = new DataTable();

            //    tnode = new TreeNode();


            //    tnode.Checked = true;
            //    tnode.Text = row[ColumnNames.PageTitle].ToString();
            //    tnode.Value = row[ColumnNames.PageID].ToString();
            //    tnode.SelectAction = TreeNodeSelectAction.None;

            //    nodes.Add(tnode);

            //    UsersOps.GetAllChildPagesByUserNameAndParentID(txtUserName.Text.Trim(), long.Parse(row[ColumnNames.PageID].ToString()), out dtChilds);

            //    if (!Utilities.IsDataTableNullOrEmpty(dtChilds))
            //    {
            //        // tnode.PopulateOnDemand = true;
            //        PopulateAdminNodes(dtChilds.DefaultView, tnode.ChildNodes);
            //    }

            //    dtChilds.Dispose();
            //}
        }
        catch (Exception)
        {
            DisplayMessage(@"Error while getting child pages.");
        }
    }

    /// <summary>
    /// This method is used to populate nodes.
    /// </summary>
    private void PopulateNodes(List<UserPage> data, TreeNodeCollection nodes)
    {
        try
        {
            TreeNode tnode = null;

            foreach (UserPage row in data)
            {
                DataTable dtChilds = new DataTable();
                tnode = new TreeNode
                {
                    Checked = row.IsAllowed.HasValue ? row.IsAllowed.Value : false,
                    Text = row.Page.Title,
                    Value = row.PageID.ToString(),
                    SelectAction = TreeNodeSelectAction.None
                };

                if (tnode.Text == "Plans")
                {
                    // Set a class so disabled nodes can be formatted thru CSS
                    // and be identifiable as disabled in Javascript.
                    tnode.Text = "<span class=disabledTreeviewNode>" + tnode.Text + "</span>";
                }

                if (tnode.Text.ToUpper() == "USERS" && ddl_Type.SelectedValue == "2")
                {
                }
                else
                {
                    if (tnode.Text.ToUpper() == "ADD USER" && ddl_Type.SelectedValue == "3")
                    {
                    }
                    else
                    {
                        nodes.Add(tnode);

                        ScentaurusEntities2 entity = MyDBContext;
                        List<UserPage> cdata = entity.UserPages.Where(x => x.UserDetail.Username.ToLower() == txtUserName.Text.ToLower() && x.Page.ParentID == row.PageID).ToList();

                        if (cdata.Count > 0)
                        {
                            // tnode.PopulateOnDemand = true;
                            PopulateNodes(cdata, tnode.ChildNodes);
                        }
                    }
                }

                dtChilds.Dispose();
            }
        }
        catch (Exception)
        {
            DisplayMessage(@"Error while getting child pages.");
        }
    }



    //private void FillNodes(List<DB.Page> data)
    //{
    //    try
    //    {
    //        TreeNode tnode = null;
    //        TreeView tv = new TreeView();

    //        foreach (var row in data)
    //        {
    //            DataTable dtChilds = new DataTable();
    //            tnode = new TreeNode();
    //            //tnode.Checked = row.IsAllowed;
    //            tnode.Text = row.Title;
    //            tnode.Value = row.ID.ToString();
    //            tnode.SelectAction = TreeNodeSelectAction.None;
    //            nodes.Add(tnode);

    //            var childPages = new DB.EduMSEntities().Pages.Where(x => x.ParentID == row.ID).ToList();

    //            if (childPages.Count > 0)
    //            {
    //                FillNodes(childPages);
    //            }

    //            //if (tnode.Text == "Plans")
    //            //{
    //            //    // Set a class so disabled nodes can be formatted thru CSS
    //            //    // and be identifiable as disabled in Javascript.
    //            //    tnode.Text = "<span class=disabledTreeviewNode>" + tnode.Text + "</span>";
    //            //}

    //            //if (tnode.Text.ToUpper() == "USERS" && ddl_Type.SelectedValue == "2")
    //            //{
    //            //}
    //            //else
    //            //{
    //            //    if (tnode.Text.ToUpper() == "ADD USER" && ddl_Type.SelectedValue == "3")
    //            //    {
    //            //    }
    //            //    else
    //            //    {


    //            var entity = new DB.EduMSEntities();
    //            var cdata = entity.UserPages.Where(x => x.User.Username.ToLower() == txtUserName.Text.ToLower() && x.Page.ParentID == row.PageID).ToList();

    //            if (data.Count > 0)
    //            {
    //                // tnode.PopulateOnDemand = true;
    //                PopulateNodes(cdata, tnode.ChildNodes);
    //            }
    //            // }
    //            //}
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        DisplayMessage(@"Error while getting child pages.");
    //    }
    //}

    /// <summary>
    /// This method is used to fill pages in tree view.
    /// </summary>
    private void FillPages(string userName)
    {
        try
        {
            ScentaurusEntities2 entity = MyDBContext;
            List<UserPage> data = entity.UserPages.Where(x => x.UserDetail.Username.ToLower() == userName.ToLower() && x.Page.ParentID == null).ToList();

            //UsersOps.GetAllParentPagesByUserName(userName, out dtParents);

            //if (dtParents.Select("ParentID = -1").Length > 0)
            //{
            //    dtDashboardFilter = dtParents.Select("ParentID = -1 AND PageTitle ='Dashboard/User'", "OrderNo").CopyToDataTable(); // AND PageTitle <> 'Dashboard/User'
            //}

            //dtDashboardFilter.DefaultView.RowFilter = Labels.Users.PageTitleFilter;
            //PopulateDashboardUserNodes(dtDashboardFilter.DefaultView, tvDashboardUser.Nodes);
            //tvDashboardUser.CollapseAll();

            //if (dtParents.Select("ParentID = -1").Length > 0)
            //{
            //    dtParents = dtParents.Select("ParentID = -1 AND PageTitle <> 'Dashboard/User'", "OrderNo").CopyToDataTable(); // AND PageTitle <> 'Dashboard/User'
            //}

            //dtParents.DefaultView.RowFilter = Labels.Users.PageTitleFilter;



            //dtParents.DefaultView.RowFilter = Labels.Users.DocumentFilter;
            //PopulateNodes(dtParents.DefaultView, tvTemplates.Nodes);

        }
        catch (Exception)
        {
            DisplayMessage(@"Error while getting parent pages information.");
        }
    }

    /// <summary>
    /// This method is used to save user pages.
    /// </summary>
    private void SavingUserPages(TreeNodeCollection nodes)
    {
        try
        {
            foreach (TreeNode tnode in nodes)
            {
                if (tnode.ChildNodes.Count > 0)
                {
                    if (tnode.Checked == true && tnode.ChildNodes.Cast<TreeNode>().All(n => !n.Checked))
                    {
                    }
                    else
                    {
                        //User objUser = new User(txtUserName.Text.Trim(), long.Parse(tnode.Value), tnode.Checked);
                        //objUser.UserPagesAddEntry();
                        //SavingUserPages(tnode.ChildNodes);
                    }
                }
                else
                {
                    //User objUser = new User(txtUserName.Text.Trim(), long.Parse(tnode.Value), tnode.Checked);
                    //objUser.UserPagesAddEntry();
                }
            }
        }
        catch (Exception)
        {
            DisplayMessage(@"Error while saving pages information.");
        }
    }

    /// <summary>
    /// This method is used to save admin pages rights.
    /// </summary>
    protected void SaveAdminPages(string userName, bool allowed)
    {
        try
        {
            DataTable dtParents = new DataTable();
            DataTable dtFilter = new DataTable();
            DataTable dtChilds = new DataTable();


            //UsersOps.GetAllParentPagesByUserName(userName, out dtParents);

            //if (dtParents.Select(Labels.Users.UsersFilter).Length > 0)
            //{
            //    dtFilter = dtParents.Select(Labels.Users.UsersFilter).CopyToDataTable();

            //    User objUser = new User(userName, long.Parse(dtFilter.Rows[0][ColumnNames.PageID].ToString()), allowed);
            //    objUser.UserPagesAddEntry();

            //    UsersOps.GetAllChildPagesByUserNameAndParentID(userName,
            //                                                   long.Parse(dtFilter.Rows[0][ColumnNames.PageID].ToString()),
            //                                                   out dtChilds);

            //    foreach (DataRow drChilds in dtChilds.Rows)
            //    {
            //        objUser = new User(userName, long.Parse(dtChilds.Rows[0][ColumnNames.PageID].ToString()), allowed);
            //        objUser.UserPagesAddEntry();
            //    }
            //}

            dtChilds.Dispose();
            dtFilter.Dispose();
            dtParents.Dispose();
        }
        catch (Exception)
        {
            DisplayMessage(@"Error while saving pages information.");
        }
    }

    /// <summary>
    /// This method is used to refresh the controls.
    /// </summary>
    protected void RefreshControls(bool addMode)
    {
        try
        {
            btnSave.Text = "Add";
            hdnMode.Value = "Add";
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            //txtPassword.Text = string.Empty;
            txtUserName.Text = string.Empty;
            ddl_Type.SelectedIndex = 0;

            //lblPassword.Visible = false;
            //chkShowPassword.Visible = false;
            //valPassword.Enabled = true;
            //div_Permissions.Visible = false;

            if (addMode)
            {
                //hdnMode.Value = Labels.Users.InsertMode;
                tblListing.Visible = false;
                tblDetail.Visible = true;
            }
            else
            {
                //hdnMode.Value = Labels.Users.EditMode;
                tblListing.Visible = true;
                tblDetail.Visible = false;
            }
        }
        catch (Exception)
        {
            DisplayMessage(@"Error while reset controls.");
        }
    }

    /// <summary>
    /// This method is used to populate settings on page load event.
    /// </summary>
    protected void PageLoadSetting()
    {
        try
        {
            FillGrid();
            tblListing.Visible = true;
            tblDetail.Visible = false;
            btnCancel_Click(null, null);

            //// tvPages.Attributes.Add("onclick", "DisableCheckBoxes(event)");
            //tvPages.Attributes.Add("onclick", "OnTreeClick(event)");
            ////tvTemplates.Attributes.Add("onclick", "OnTreeClick(event)");
            ////lbl_Heading.Text = Labels.Users.UsersPageName;
            //lbl_MainHeading.InnerText = Labels.Users.UsersPageName;
            ////lbl_DetailHeading.Text = Labels.Users.UsersPageName;
            //Session[SessionKeys.PageName] = Labels.Users.UsersPageName;
        }
        catch (Exception)
        {
            DisplayMessage(@"Error while getting information.");
        }
    }

    /// <summary>
    /// This method is used to edit the record.
    /// </summary>
    protected void AllowAccess(int id)
    {
        try
        {
            //btnSave.Text = "Modify";
            //DataTable dtUsers = new DataTable();

            ////div_Permissions.Visible = false;
            //hdnMode.Value = Labels.Users.EditMode;
            //UsersOps.GetUsersByID(id, out dtUsers);

            //if (!Utilities.IsDataTableNullOrEmpty(dtUsers))
            //{
            //    DataRow drUsers = dtUsers.Rows[0];
            //    App_Code.DataClasses.User objUser = new App_Code.DataClasses.User(drUsers[ColumnNames.UserName].ToString(), true);
            //    objUser.UpdateLockedStatus();

            //    ActivityLog objActivityLog = new ActivityLog(string.Concat("User name: ", drUsers[ColumnNames.UserName].ToString(), Labels.Users.HasBeenEditied),
            //                                                     Session[SessionKeys.UserName].ToString(), Labels.Users.UsersPageName);
            //    objActivityLog.ActivityLogAddEntry();
            //    FillPages(txtUserName.Text.Trim());
            //    Response.Redirect("Users.aspx?i=2");
            //}

            //dtUsers.Dispose();
        }
        catch (Exception)
        {
            DisplayMessage(@"Error while getting information.");
        }
    }

    /// <summary>
    /// This method is used to edit the record.
    /// </summary>
    protected void EditRecord(int id)
    {
        try
        {
            //btnSave.Text = "Modify";
            //DataTable dtUsers = new DataTable();
            //lbl_MainHeading.InnerText = "Modify User";
            ////div_Permissions.Visible = false;
            //hdnMode.Value = Labels.Users.EditMode;
            //UsersOps.GetUsersByID(id, out dtUsers);


            //if (!Utilities.IsDataTableNullOrEmpty(dtUsers))
            //{
            //    DataRow drUsers = dtUsers.Rows[0];
            //    cbk_Locked.Visible = false;
            //    lbl_IsLocked.Visible = false;
            //    cbk_Locked.Checked = bool.Parse(drUsers[ColumnNames.IsLocked].ToString());

            //    if (cbk_Locked.Checked)
            //    {
            //        rowIsLocked.Visible = true;
            //    }
            //    else
            //    {
            //        rowIsLocked.Visible = false;
            //    }

            //    txtFirstName.Text = drUsers[ColumnNames.FirstName].ToString();
            //    txtLastName.Text = drUsers[ColumnNames.LastName].ToString();
            //    txtUserName.Text = drUsers[ColumnNames.UserName].ToString();
            //    //txtPassword.Text = drUsers[ColumnNames.Password].ToString();
            //    hdnPassword.Value = drUsers[ColumnNames.Password].ToString();
            //    //lblPassword.Text = drUsers[ColumnNames.Password].ToString();
            //    ddl_Type.SelectedValue = drUsers[ColumnNames.UserType].ToString();
            //    hdnID.Value = id.ToString();
            //    //valPassword.Enabled = false;
            //    //chkShowPassword.Visible = true;
            //    //chkShowPassword.Checked = false;
            //    //lblPassword.Visible = false;
            //    tblListing.Visible = false;
            //    tblDetail.Visible = true;
            //    FillPages(txtUserName.Text.Trim());
            //    div_Permissions.Visible = true;

            //    if (Session[SessionKeys.UserType].ToString() == StringLiterals.Values.OneValue)
            //    {
            //        valPassword.Enabled = false;
            //    }
            //    else
            //    {
            //        valPassword.Enabled = true;
            //    }
            //}

            //dtUsers.Dispose();
        }
        catch (Exception)
        {
            DisplayMessage(@"Error while getting information.");
        }
    }

    /// <summary>
    /// This method is used to delete the record.
    /// </summary>
    protected void DeleteRecord(int id, string userName)
    {
        try
        {
            //if (UsersOps.UsersDeleteEntry(id))
            //{
            //    bool isResult = false;

            //    FillGrid();
            //    User objUser = new User(userName);
            //    isResult = objUser.UserPagesDeleteEntry();

            //    if (isResult)
            //    {
            //        FillGrid();
            //        DisplayMessage(@"User deleted successfully.");
            //        ActivityLog objActivityLog = new ActivityLog(string.Concat("User name: ", userName, Labels.Users.HasBeenDeleted),
            //                                                     Session[SessionKeys.UserName].ToString(), Labels.Users.UsersPageName);
            //        objActivityLog.ActivityLogAddEntry();
            //        btnCancel_Click(null, null);
            //    }
            //}
            //else
            //{
            //    FillGrid();
            //    DisplayMessage(@"Error in deleting user.");
            //}
        }
        catch (Exception)
        {
            DisplayMessage(@"Error while deleting user.");
        }
    }

    //private void IsUserAllowedToViewPage()
    //{
    //    DataTable dtPages = new DataTable();

    //    string BaseUrl = WebConfigurationManager.AppSettings["BaseURL"];
    //    string Pagename = Request.Url.ToString().Replace(BaseUrl, "").Trim();

    //    UsersOps.GetIsPageAllowedByUser(Session[SessionKeys.UserName].ToString(), Pagename, out dtPages);

    //    if (Utilities.IsDataTableNullOrEmpty(dtPages))
    //    {
    //        Response.Redirect("NotAuthorized.aspx");
    //    }
    //}

    private void HideButtonForViewOnlyTypeUser()
    {
        //Hide grid column
        gvw_Search.Columns
         .Cast<DataControlField>().Where(fld => fld.HeaderText == "Modify").SingleOrDefault().Visible = false;

        gvw_Search.Columns
          .Cast<DataControlField>().Where(fld => fld.HeaderText == "Delete").SingleOrDefault().Visible = false;

        gvw_Search.Columns
          .Cast<DataControlField>().Where(fld => fld.HeaderText == "Lock Out").SingleOrDefault().Visible = false;

        // hide permissions
        div_Permissions.Visible = false;
    }

    #endregion Methods    

    protected void chkParentPage_CheckedChanged(object sender, EventArgs e)
    {

    }

    protected void cblPages_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void LoadRepeater()
    {
        ScentaurusEntities2 entity = MyDBContext;

        List<Context.Page> data = entity.Pages.Where(x => x.ParentID == null).OrderBy(x => x.ID).ToList();
        rpPages.DataSource = data;
        rpPages.DataBind();
    }

    private void LoadGrid()
    {
        ScentaurusEntities2 entity = MyDBContext;

        List<UserDetail> data = entity.UserDetails.OrderByDescending(x => x.ID).ToList();
        gvw_Search.DataSource = data;
        gvw_Search.DataBind();
    }

    protected void rpPages_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            // if (e.Item.ItemType == ListItemType.Item)
            {
                ScentaurusEntities2 entity = MyDBContext;
                CheckBoxList cblPages = e.Item.FindControl("cblPages") as CheckBoxList;
                HiddenField hdnID = e.Item.FindControl("hdnID") as HiddenField;

                int id = int.Parse(hdnID.Value);

                List<Context.Page> child = entity.Pages.Where(x => x.ParentID == id).ToList();
                cblPages.DataSource = child;
                cblPages.DataTextField = "Title";
                cblPages.DataValueField = "ID";
                cblPages.DataBind();

            }
        }
        catch (Exception ex)
        {
            Utils.ShowAlert(this, "Error", ex.Message, false);
        }
    }

    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        try
        {
            GridViewRow gvr = ((LinkButton)sender).NamingContainer as GridViewRow;
            HiddenField hdnID = gvr.FindControl("hdnID") as HiddenField;

            int userID = int.Parse(hdnID.Value);
            UserDetail user = MyDBContext.UserDetails.Where(x => x.ID == userID).FirstOrDefault();
            List<UserPage> userPages = MyDBContext.UserPages.Where(x => x.UserID == userID).ToList();

            txtUserName.Text = user.Username;
            txtUserName.Enabled = false;

            hdnUserName.Value = user.Username;
            txtPassword.Text = user.Password;
            txtFirstName.Text = user.FirstName;
            txtLastName.Text = user.LastName;

            //if (ddl_Type.Items.FindByText(user.Role) != null)
            //{
            //    ddl_Type.SelectedValue = ddl_Type.Items.FindByText(user.Role).Value;
            //}

            LoadRepeater();

            foreach (RepeaterItem ri in rpPages.Items)
            {
                // chkParentPage hdnID cblPages
                HiddenField pageID = ri.FindControl("hdnID") as HiddenField;
                CheckBox chkParentPage = ri.FindControl("chkParentPage") as CheckBox;
                CheckBoxList cblPages = ri.FindControl("cblPages") as CheckBoxList;
                int pID = int.Parse(pageID.Value);

                if (userPages.Where(x => x.PageID == pID).ToList().Count > 0)
                {
                    chkParentPage.Checked = true;
                }

                foreach (ListItem item in cblPages.Items)
                {
                    int cpID = int.Parse(item.Value);

                    if (userPages.Where(x => x.PageID == cpID).ToList().Count > 0)
                    {
                        item.Selected = true;
                    }
                }
            }

            hdnMode.Value = "Update";
            tblDetail.Visible = true;
            tblListing.Visible = false;
            lbl_MainHeading.InnerText = "Update User";
            btnSave.Text = "Update";

        }
        catch (Exception ex)
        {
            Utils.ShowAlert(this, "Error", ex.Message, false);
        }
    }


}