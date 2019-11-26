<%@ Page Title="Admin Portal - DashBoard Settings" Language="C#" MasterPageFile="~/Masters/Admin.master" AutoEventWireup="true"
    CodeFile="DashboardSettings.aspx.cs" Inherits="DashboardSettings" ValidateRequest="false"
    EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/bootstrap-fancyfile.css" rel="stylesheet">
    <style>
        .col2 {
            margin-top: -7px;
        }

        .plan_filter {
            height: 48px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="dynamic_title">
        <span class="page_btn">
            <asp:Label ID="lbl_MainHeading" runat="server" CssClass="heading" Text="Dashboard Settings"></asp:Label>
        </span>
    </div>


    <asp:Button ID="btnHiddenErrorMsg" runat="server" Style="display: none;" />
    <asp:ModalPopupExtender runat="server" ID="mpeErrorMsg" BehaviorID="mpeBehaviorIDErrorMsg"
        TargetControlID="btnHiddenErrorMsg" PopupControlID="programmaticPopupErrorMsg" BackgroundCssClass="overlay2"
        DropShadow="False" PopupDragHandleControlID="mpePopupDragHandleControlIDErrorMsg" RepositionMode="RepositionOnWindowScroll">
    </asp:ModalPopupExtender>
    <asp:Panel runat="server" ID="programmaticPopupErrorMsg" CssClass="overlay2 mainPopup" Style="display: none;">
        <div class="blue-popup">
            <div align="right" class="close">
                <asp:ImageButton ID="ibClose_pnlPopUP" runat="server" ImageUrl="~/Images/x.png" />
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

    <div class="draftplans container-fluid">
        <div runat="server" id="tblDetail" class="row">
            <div class="col-12">
                <div class="widget-box">
                    <div class="widget-content nopadding">
                        <div class="col-sm-12 plan_filter area_filter">
                            <div class="col-sm-8 col-md-9">
                                <div class="col-md-4 col-sm-5 col2 nopadding1" style="padding-top: 18px;">
                                    <span class="filter_label uploadimg">
                                        <asp:FileUpload ID="FileUploadImage" runat="server" CssClass="span10 demo2" />
                                    </span>
                                    <span class="filter_label" style="padding-top: 10px;">
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.pdf|.jpg|.jpeg|.png|.PNG|.gif|.GIF )$"
                                            ControlToValidate="FileUploadImage" ValidationGroup="save" runat="server" CssClass="required"
                                            ErrorMessage="Invalid file format." Display="Dynamic" />

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="save"
                                            ControlToValidate="FileUploadImage" ErrorMessage="Please select Image." CssClass="required pselectreq"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                    </span>
                                </div>

                                <div class="col-md-8 col-sm-7">


                                    <asp:Label ID="lblMsg" runat="server" Text="Image size must be 90 x 90 and cannot be more than 1 MB."></asp:Label>

                                </div>
                            </div>
                            <div class="col-sm-4 col-md-3">

                                <div style="margin-top: 20px">

                                    <asp:Button ID="btnSave" ValidationGroup="save" runat="server" Text="Save" OnClick="btnSave_Click"
                                        CssClass="btn btn-primary btn-xs" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                        CssClass="btn btn-error btn-xs" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="row" style="margin-top: 25px">
            <div class="col-xs-2">
                <strong>Change Password :

                </strong>
            </div>
            <div class="col-xs-4">
                <asp:TextBox ID="txtPassword" placeholder="Password" runat="server" TextMode="Password"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="rfv_txtPassword" CssClass="required" runat="server"
                                                                ControlToValidate="txtPassword"
                                                                ErrorMessage="*" ValidationGroup="ChangePassword" ForeColor=""></asp:RequiredFieldValidator>

            </div>

            <div class="col-xs-3">
                <asp:TextBox ID="txtConfirmPassword" placeholder="Confirm Password"  TextMode="Password" runat="server"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="rfv_txtConfirmPassword" CssClass="required" runat="server"
                                                                ControlToValidate="txtConfirmPassword"
                                                                ErrorMessage="*" ValidationGroup="ChangePassword" ForeColor=""></asp:RequiredFieldValidator>

            </div>
            <div class="col-xs-3">
                <asp:Button ID="btnChangePassword" runat="server" CssClass="btn btn-xs btn-primary"  OnClientClick="return confirm('Are you sure you want to update your password?');"  ValidationGroup="ChangePassword" Text="Change Password" OnClick="btnChangePassword_Click" />
            </div>
        </div>


        <div class="row" style="margin-top: 25px">
            <div class="col-xs-2">
                <strong>Parent Account :

                </strong>
            </div>
            <div class="col-xs-4">
                <asp:TextBox ID="txtParentAccount" runat="server"></asp:TextBox>

            </div>

            <div class="col-xs-3">

                <asp:Button ID="btnUpdateSettings" runat="server" CssClass="btn btn-xs btn-primary" Text="Update" OnClick="btnUpdateSettings_Click" />
            </div>
            <div class="col-xs-3">
                <asp:Button ID="btnMoveAmount" runat="server" OnClientClick="return confirm('Are you sure you want to move your balance amount to parent account?');" CssClass="btn btn-xs btn-warning" Text="Move Amount to Parent Account" OnClick="btnMoveAmount_Click" />
            </div>
        </div>
    </div>
</asp:Content>
