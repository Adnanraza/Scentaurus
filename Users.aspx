<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Admin.master" AutoEventWireup="true"
    CodeFile="Users.aspx.cs" Inherits="Users" ValidateRequest="false" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Import Namespace="System.Web.Configuration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <style type="text/css">
        #custommessage {
            position: static;
            width: 55%;
            color: Red;
            margin-left: -200px;
        }

        .fliter_widget {
            margin-bottom: 0;
            padding: 0 10px 8px;
        }

        /*Azad*/
        table[id$="gvw_Search"] td > .btn.btn-primary.btn-small {
            line-height: 20px;
            margin: -11px 0;
            padding: 1px 10px;
        }

        .form-horizontal .control-label {
            padding-top: 13px;
        }

        .pages {
            border: 1px solid #ccc;
            border-radius: 5px;
            background: #eee;
        }


        .css-label-chk {
            margin-top: 5px;
        }

        .chkdiv ~ .chkdiv {
            display: block;
            position: absolute;
            left: 30px;
        }
    </style>
    <script type="text/javascript">
        //DisableCheckBoxes();

<%--        function DisableCheckBoxes() {

            TREEVIEW_ID = document.getElementById('<%=tvPages.ClientID %>');


            var treeView = document.getElementById(TREEVIEW_ID);

            if (treeView) {
                var childCheckBoxes = treeView.getElementsByTagName("input");
                for (var i = 0; i < childCheckBoxes.length; i++) {
                    var textSpan = GetCheckBoxTextSpan(childCheckBoxes[i]);

                    if (textSpan.firstChild)
                        if (textSpan.firstChild.className == "disabledTreeviewNode")
                            childCheckBoxes[i].disabled = true;
                }
            }
        }

        function GetCheckBoxTextSpan(checkBox) {
            // Set label text to node name
            var parentDiv = checkBox.parentNode;
            var nodeSpan = parentDiv.getElementsByTagName("span");

            return nodeSpan[0];
        }--%>


</script>

    <script type="text/javascript">


        function parentclick(ele) {
            if ($(ele).children("input").is(":checked")) {


                $("#" + $(ele).siblings("input").val() + " input[type=checkbox]").each(function () {
                    $(this).prop("checked", true);
                })

            }
            else {

                $("#" + $(ele).siblings("input").val() + " input[type=checkbox]").each(function () {
                    $(this).prop("checked", false);
                })
            }

        }


        function ChildClick() {
            $(".child input[type=checkbox]").change(function () {
                console.log($(this));
            })
        }

        function OnTreeClick(evt) {
            var src = window.event != window.undefined ? window.event.srcElement : evt.target;
            var isChkBoxClick = (src.tagName.toLowerCase() == "input" && src.type == "checkbox");

            if (isChkBoxClick) {
                var parentTable = GetParentByTagName("table", src);

                var nxtSibling = parentTable.nextSibling;
                if (nxtSibling && nxtSibling.nodeType == 1)//check if nxt sibling is not null & is an element node
                {
                    if (nxtSibling.tagName.toLowerCase() == "div") //if node has children
                    {
                        //check or uncheck children at all levels
                        var result = CheckUncheckChildren(parentTable.nextSibling, src.checked);
                        if (result) {
                            src.checked = false;
                        }
                    }
                }
                //check or uncheck parents at all levels
                CheckUncheckParents(src, src.checked);
            }
        }


        function CheckUncheckChildren(childContainer, check) {

            var childChkBoxes = childContainer.getElementsByTagName("input");
            var childChkBoxCount = childChkBoxes.length;
            for (var i = 0; i < childChkBoxCount; i++) {
                childChkBoxes[i].checked = check;
            }
        }


        function CheckUncheckParents(srcChild, check) {
            var parentDiv = GetParentByTagName("div", srcChild);
            var parentNodeTable = parentDiv.previousSibling;


            if (parentNodeTable) {
                var checkUncheckSwitch;

                if (check) //checkbox checked
                {
                    var isAllSiblingsChecked = AreAllSiblingsChecked(srcChild);
                    if (isAllSiblingsChecked)

                        checkUncheckSwitch = true;
                    else

                        return; //do not need to check parent if any child is not checked
                }
                else //checkbox unchecked
                {
                    var res = forAllUncheckChildren(srcChild);

                    if (res) {
                        checkUncheckSwitch = true;
                    }
                    else {
                        checkUncheckSwitch = true;
                    }
                }

                var inpElemsInParentTable = parentNodeTable.getElementsByTagName("input");
                if (inpElemsInParentTable.length > 0) {
                    var parentNodeChkBox = inpElemsInParentTable[0];
                    parentNodeChkBox.checked = checkUncheckSwitch;
                    //do the same recursively
                    CheckUncheckParents(parentNodeChkBox, checkUncheckSwitch);
                }
            }
        }

        function forAllUncheckChildren(chkBox) {

            var parentDiv = GetParentByTagName("div", chkBox);
            var childCount = parentDiv.childNodes.length;

            var cuount = 0;

            for (var i = 0; i < childCount; i++) {
                if (parentDiv.childNodes[i].nodeType == 1) //check if the child node is an element node
                {
                    if (parentDiv.childNodes[i].tagName.toLowerCase() == "table") {
                        var prevChkBox = parentDiv.childNodes[i].getElementsByTagName("input")[0];
                        //if any of sibling nodes are not checked, return false
                        if (!prevChkBox.checked == false) {
                            cuount++
                        }
                    }
                }
            }

            if (cuount == childCount) {
                return true;
            }
            else {
                return false;
            }
        }
        function AreAllSiblingsChecked(chkBox) {
            var parentDiv = GetParentByTagName("div", chkBox);
            var childCount = parentDiv.childNodes.length;
            var countOne = 0;

            for (var i = 0; i < childCount; i++) {
                if (parentDiv.childNodes[i].nodeType == 1) //check if the child node is an element node
                {
                    if (parentDiv.childNodes[i].tagName.toLowerCase() == "table") {
                        var prevChkBox = parentDiv.childNodes[i].getElementsByTagName("input")[0];
                        //if any of sibling nodes are not checked, return false
                        if (!prevChkBox.checked) {
                            countOne++;
                        }
                    }
                }
            }

            if (countOne > 0) {
                return true;
            }
            else {
                return true;
            }

        }


        //utility function to get the container of an element by tagname
        function GetParentByTagName(parentTagName, childElementObj) {
            var parent = childElementObj.parentNode;
            while (parent.tagName.toLowerCase() != parentTagName.toLowerCase()) {
                parent = parent.parentNode;
            }
            return parent;
        }


    </script>

    <script type="text/javascript">

</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <asp:UpdatePanel ID="upMain" runat="server">
        <ContentTemplate>

            <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                <ProgressTemplate>
                    <div id="Imgt" style="position: fixed; background: rgba(255,255,255,.5); width: 100%; z-index: 999999; height: 100%;"
                        align="center" valign="middle" runat="server"
                        class="blur">
                        <asp:Image ID="imgLoading" runat="server" ImageUrl="~/Images/731.gif" Style="margin: auto; position: fixed; top: 0; right: 0; bottom: 0px; left: 0px;" /><br />
                        <br />
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/276.gif" Style="margin: auto; position: fixed; top: 100px; right: 0; bottom: 0px; left: 0px;" /><br />
                        <br />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <%--  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
            <div class="dynamic_title">
                <span class="page_btn">
                    <span id="lbl_MainHeading" runat="server" class="heading">Add User</span>
                </span>
            </div>
            <div runat="server" id="tblDetail" class="row add_plans_tab draftplans accdetail1">
                <div class="col-12">
                    <div class="widget-box">
                        <div class="widget-content nopadding">
                            <div class="form-horizontal">
                                <div class="row admin_forms_row">
                                    <div class="col-sm-4 col-sm-offset-2">
                                        <div class="form-group">
                                            <div class="profile-user-info profile-user-info-striped">
                                                <div class="profile-info-row">
                                                    <div class="profile-info-name">First Name: </div>

                                                    <div class="profile-info-value">

                                                        <asp:TextBox ID="txtFirstName" runat="server" CssClass="input-small"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFirstName"
                                                            Display="Dynamic" ErrorMessage="Required" CssClass="required" ValidationGroup="save"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>


                                            <div class="controls" style="margin-left: 110px;">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">


                                            <div class="profile-user-info profile-user-info-striped">
                                                <div class="profile-info-row">
                                                    <div class="profile-info-name">Last Name: </div>

                                                    <div class="profile-info-value">

                                                        <asp:TextBox ID="txtLastName" runat="server" CssClass="input-small"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtLastName"
                                                            Display="Dynamic" ErrorMessage=" Required" CssClass="required" ValidationGroup="save"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>



                                        </div>
                                    </div>
                                </div>
                                <div class="row admin_forms_row">
                                    <div class="col-sm-4 col-sm-offset-2">
                                        <div class="form-group">

                                            <div class="profile-user-info profile-user-info-striped">
                                                <div class="profile-info-row">
                                                    <div class="profile-info-name">User Name: </div>

                                                    <div class="profile-info-value">

                                                        <asp:TextBox ID="txtUserName" runat="server" CssClass="input-small"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" CssClass="required"
                                                            ControlToValidate="txtUserName" Display="Dynamic" ErrorMessage="Required" ValidationGroup="save"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>


                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">

                                            <div class="profile-user-info profile-user-info-striped">
                                                <div class="profile-info-row">
                                                    <div class="profile-info-name">Password: </div>

                                                    <div class="profile-info-value">

                                                        <asp:TextBox ID="txtPassword" runat="server" CssClass="input-small"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="valPassword" runat="server" CssClass="required" ControlToValidate="txtPassword"
                                                            Display="Dynamic" ErrorMessage="Required" ValidationGroup="save"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>


                                        </div>
                                    </div>
                                </div>
                                <div class="row admin_forms_row">
                                    <div class="col-sm-4 col-sm-offset-2">
                                        <div class="form-group">



                                            <div class="profile-user-info profile-user-info-striped">
                                                <div class="profile-info-row">
                                                    <div class="profile-info-name">Access: </div>

                                                    <div class="profile-info-value">

                                                        <asp:DropDownList ID="ddl_Type" class="form_custom_selectbox" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddl_Type_OnSelectedIndexChanged">
                                                            <asp:ListItem Value="0">-----Select-----</asp:ListItem>
                                                            <asp:ListItem Value="1">Admin</asp:ListItem>
                                                            <asp:ListItem Value="2">General</asp:ListItem>
                                                        </asp:DropDownList>


                                                        <asp:RequiredFieldValidator ID="rfv_ddl_Type" runat="server" ControlToValidate="ddl_Type"
                                                            Display="Dynamic" ErrorMessage="Required" CssClass="required" InitialValue="0"
                                                            ValidationGroup="save"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>


                                        </div>
                                    </div>
                                    <%--  <div class="col-sm-6" runat="server" id="rowPassword" visible="true">
                                        <div class="form-group">
                                              <label class="control-label lebel-left">
                                        Password:</label>
                                            <div class="controls">
                                                  <asp:CheckBox ID="chkShowPassword" runat="server" AutoPostBack="True" OnCheckedChanged="chkShowPassword_CheckedChanged"
                                            Text="Show Password" Visible="false" />
                                        <asp:Label ID="lblPassword" runat="server" Text="Label" Visible="false"></asp:Label>
                                            </div>
                                        </div>
                                    </div>--%>
                                </div>

                                <div runat="server" id="div_Permissions" class="row admin_forms_row">

                                    <div class="col-sm-12">
                                        <label class="heading">
                                            Areas of Access:</label>
                                        <div class="clear"></div>
                                        <div class="form-group" style="">
                                            <div class="controls" style="margin-left: 110px;">

                                                <asp:Panel ID="pnl_TreeView" runat="server" Width="80%" CssClass="pages">



                                                    <asp:Repeater ID="rpPages" runat="server" OnItemDataBound="rpPages_ItemDataBound">
                                                        <ItemTemplate>

                                                            <div class="row">

                                                                <div class="col-md-6">

                                                                    <div class="profile-user-info profile-user-info-striped">
                                                                        <div class="profile-info-row">
                                                                            <div class="profile-info-name"><%#Eval("Title") %> </div>

                                                                            <div class="profile-info-value">

                                                                                <asp:CheckBox ID="chkParentPage" runat="server" onchange='parentclick(this)' />
                                                                                <asp:HiddenField ID="hdnClass" runat="server" Value='<%#Eval("ID") %>' />
                                                                            </div>
                                                                        </div>
                                                                    </div>


                                                                    <div class="clearfix"></div>
                                                                    <asp:HiddenField ID="hdnID" runat="server" Value='<%#Eval("ID") %>' />
                                                                </div>

                                                                <div class="col-md-6">


                                                                    <div class="profile-user-info profile-user-info-striped">
                                                                        <div class="profile-info-row">
                                                                            <div class="profile-info-name">

                                                                                <div id='<%#Eval("ID") %>'>
                                                                                    <asp:CheckBoxList ID="cblPages" CssClass="child" Width="200px" runat="server"></asp:CheckBoxList>
                                                                                </div>
                                                                            </div>
                                                                            <%--OnSelectedIndexChanged="cblPages_SelectedIndexChanged" AutoPostBack="true"--%>
                                                                        </div>
                                                                    </div>


                                                                </div>

                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <div class="controls">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row admin_forms_row align-center">
                                    <div class="col-sm-12" align="center">
                                        <div class="form-group">
                                            <div class="col-lg-12">
                                                <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" CssClass="btn btn-primary btn-small"
                                                    ValidationGroup="save" />
                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                                    CssClass="btn btn-primary btn-small" CausesValidation="False" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row admin_forms_row">
                                    <div class="col-sm-12">
                                        <div class="form-group">
                                            <div class="controls">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="draftplans container-fluid">
                <div runat="server" id="tblListing" class="row">
                    <div class="col-12">
                        <div class="widget-box">
                            <div class="widget-content nopadding">

                                <%--OnRowCommand="gvw_Search_RowCommand"--%>

                                <asp:GridView ID="gvw_Search" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                    AllowPaging="true" CssClass="table table-bordered table-striped table-hover with-check"
                                    GridLines="None" Width="100%" ShowHeaderWhenEmpty="true"
                                    OnPageIndexChanging="gvw_Search_PageIndexChanging" PageSize="15" EmptyDataText="No record found."
                                    AllowSorting="True" OnSorting="gvw_Search_Sorting" OnRowDataBound="gvw_Search_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>

                                                <div class="checkbox">
                                                    <label>
                                                        <asp:CheckBox ID="chkSelect" OnCheckedChanged="chkSelect_CheckedChanged" AutoPostBack="true" runat="server" Checked='<%#Eval("IsActive").ToString().ToLower().Contains("true") ? true : false %>' />
                                                        <span class="cr"><i class="cr-icon fa fa-check"></i></span>

                                                    </label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>

                                                <div style="margin: 0px !important" class='<%#Eval("IsActive").ToString().ToLower().Contains("true") ? "legend bggreen" : "legend bgred"%>'></div>

                                                <asp:HiddenField ID="hdnID" runat="server" Value='<%# Eval("ID") %>' />

                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="First Name"  HeaderStyle-HorizontalAlign="Left"
                                            HeaderStyle-Width="">
                                            <ItemStyle Width="15%" />
                                            <ItemTemplate>
                                                <asp:Label ID="gv_lblFirstName" runat="server" Text='<%# Eval("FirstName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Last Name" HeaderStyle-HorizontalAlign="Left">
                                            <ItemStyle Width="" />
                                            <ItemTemplate>
                                                <asp:Label ID="gv_lblLastName" runat="server" Text='<%# Eval("LastName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="User Name" HeaderStyle-HorizontalAlign="Left">
                                            <ItemStyle Width="" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="gv_lblUserName" runat="server" Text='<%# Eval("UserName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%-- <asp:TemplateField HeaderText="User Level" SortExpression="UserType" HeaderStyle-HorizontalAlign="Left">
                                            <ItemStyle Width="15%" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="gv_lblUserType" runat="server" Text='<%# Eval("Role") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="">
                                            <ItemStyle Width="" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEdit" Text="Edit" runat="server" CommandName="EditRecord" OnClick="lnkEdit_Click"></asp:LinkButton>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--     <asp:TemplateField HeaderText="Delete">
                                            <ItemStyle Width="" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandName="DeleteRecord" CommandArgument='<%# Eval("ID") %>'
                                                    CssClass="Deletebtn" CausesValidation="false"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <%--<asp:TemplateField HeaderText="Lock Out" HeaderStyle-HorizontalAlign="Left">
                                            <ItemStyle Width="" />
                                            <ItemTemplate>
                                                <asp:Label ID="gv_lblIsLocked" runat="server" Text='<%# Eval("IsLocked") %>' Visible="false"></asp:Label>
                                                <asp:LinkButton ID="lnkAllowAccess" runat="server" CommandName="AllowAccess" CommandArgument='<%# Eval("ID") %>'
                                                    Text="Allow access" CssClass="btn btn-primary btn-small">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>--%>
                                    </Columns>
                                    <PagerStyle HorizontalAlign="Right" CssClass="paging" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <asp:HiddenField ID="hdnID" runat="server" />
            <asp:HiddenField ID="hdnMode" runat="server" />
            <asp:HiddenField ID="hdnIsDelete" runat="server" />
            <asp:HiddenField ID="hdnUserName" runat="server" />
            <asp:HiddenField ID="hdnPassword" runat="server" />
            <asp:Button ID="btnHiddenErrorMsg" runat="server" Style="display: none; background-color: #CCC;" />
            <asp:ModalPopupExtender ID="mpeErrorMsg" runat="server" PopupControlID="programmaticPopupErrorMsg"
                TargetControlID="btnHiddenErrorMsg" BehaviorID="mpeBehaviorIDErrorMsg" DropShadow="false"
                BackgroundCssClass="popup_opacity" PopupDragHandleControlID="mpePopupDragHandleControlIDErrorMsg"
                RepositionMode="RepositionOnWindowScroll" />
            <asp:Panel runat="server" ID="programmaticPopupErrorMsg" CssClass="overlay2 mainPopup" Style="display: none; background: white; border: 1px solid #ccc; padding: 10px; border-radius: 5px;">
                <div class="blue-popup">
                    <div align="right" class="close">
                        <asp:ImageButton ID="ibClose_pnlPopUP" runat="server" ImageUrl="~/Images/close.gif" Width="90px" />
                    </div>
                    <div class="overlay2">
                        <div align="center">
                            <h3>
                                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label></h3>
                        </div>
                        <br />
                        <div align="center">
                            <asp:Button ID="btnClose" runat="server" Text="OK" CssClass="btn btn-primary" Style="height: 32px; width: 100px" />
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <asp:Button ID="btnAlert" runat="server" Style="display: none; background-color: #CCC;" />
            <asp:ModalPopupExtender ID="mpe_Alert" runat="server" PopupControlID="pnl_Alert"
                TargetControlID="btnAlert" BehaviorID="mpeBehaviorIDErrorMsgAlert" DropShadow="false"
                BackgroundCssClass="popup_opacity" PopupDragHandleControlID="mpePopupDragHandleControlIDErrorMsgAlert"
                RepositionMode="RepositionOnWindowScroll">
            </asp:ModalPopupExtender>
            <asp:Panel runat="server" CssClass="overlay2 mainPopup" ID="pnl_Alert" Style="display: none;">
                <div class="blue-popup text-center">
                    <asp:Panel runat="Server" ID="pnl_AlertMsg">
                        <h3>
                            <asp:Label ID="lbl_AlertMsg" runat="server" />
                        </h3>
                        <asp:Button ID="btn_SaveAlert" OnClick="btn_SaveAlert_Click" Text="Yes" CssClass="btn btn-primary btn-small"
                            runat="server" />
                        &nbsp;
                        <asp:Button ID="btn_CancelAlert" OnClick="btn_CancelAlert_Click" Text="No" CssClass="btn btn-primary btn-small"
                            runat="server" />
                    </asp:Panel>
                </div>
            </asp:Panel>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
