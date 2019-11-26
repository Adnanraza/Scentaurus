<%@ Page Title="S - Member Registration" Language="C#" MasterPageFile="~/Masters/Admin.master" AutoEventWireup="true" CodeFile="Products.aspx.cs" Inherits="Products" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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

            <div class="row">
                <div class="col-md-12  col-xs-12">
                    <span class="heading">Products</span>
                    <asp:HiddenField ID="hdnProductID" runat="server" />
                    <asp:HiddenField ID="hdnImagePath" runat="server" />

                    <asp:HiddenField ID="hdnMode" runat="server" Value="Add" />

                    <div id="divAddStudent" runat="server">
                        <div>
                            <div id="user-profile-1" class="user-profile row">


                                <div class="col-xs-12 col-sm-12">
                                    <div class="center" id="progressDiv" runat="server" visible="false">
                                        <span class="btn btn-app btn-sm btn-light no-hover">
                                            <span class="line-height-1 bigger-170 blue">1,411 </span>

                                            <br />
                                            <span class="line-height-1 smaller-90">Views </span>
                                        </span>

                                        <span class="btn btn-app btn-sm btn-yellow no-hover">
                                            <span class="line-height-1 bigger-170">32 </span>

                                            <br />
                                            <span class="line-height-1 smaller-90">Followers </span>
                                        </span>

                                        <span class="btn btn-app btn-sm btn-pink no-hover">
                                            <span class="line-height-1 bigger-170">4 </span>

                                            <br />
                                            <span class="line-height-1 smaller-90">Projects </span>
                                        </span>

                                        <span class="btn btn-app btn-sm btn-grey no-hover">
                                            <span class="line-height-1 bigger-170">23 </span>

                                            <br />
                                            <span class="line-height-1 smaller-90">Reviews </span>
                                        </span>

                                        <span class="btn btn-app btn-sm btn-success no-hover">
                                            <span class="line-height-1 bigger-170">7 </span>

                                            <br />
                                            <span class="line-height-1 smaller-90">Albums </span>
                                        </span>

                                        <span class="btn btn-app btn-sm btn-primary no-hover">
                                            <span class="line-height-1 bigger-170">55 </span>

                                            <br />
                                            <span class="line-height-1 smaller-90">Contacts </span>
                                        </span>
                                    </div>

                                    <div class="space-12"></div>

                                    <div class="row">
                                        <div class="col-md-6 col-sm-12 col-xs-12">

                                            <asp:Panel ID="pnlAddProduct" runat="server">

                                             <%--   <span class="profile-picture">
                                                    <img id="avatar" class="editable img-responsive" alt="" src="assets/avatars/profile-pic.jpg" style="width: 130px" />
                                                </span>
--%>

                                                <h5></h5>
                                                <div class="profile-user-info profile-user-info-striped">
                                                    <div class="profile-info-row">
                                                        <div class="profile-info-name">Product Name: </div>

                                                        <div class="profile-info-value">


                                                            <asp:TextBox ID="txtProductName" MaxLength="50" runat="server"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfv_txtProductName" CssClass="required" runat="server"
                                                                ControlToValidate="txtProductName"
                                                                ErrorMessage="*" ValidationGroup="AddProduct" ForeColor=""></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>

                                                    <div class="profile-info-row">
                                                        <div class="profile-info-name">Cost: </div>

                                                        <div class="profile-info-value">
                                                            <asp:TextBox ID="txtCost" MaxLength="5" runat="server"></asp:TextBox>

                                                            <asp:RequiredFieldValidator ID="rfv_txtCost" CssClass="required" runat="server"
                                                                ControlToValidate="txtCost"
                                                                ErrorMessage="*" ValidationGroup="AddProduct" ForeColor=""></asp:RequiredFieldValidator>

                                                        </div>
                                                    </div>

                                                    <div class="profile-info-row">
                                                        <div class="profile-info-name">Active: </div>

                                                        <div class="profile-info-value">
                                                            <asp:CheckBox ID="chkIsActive" runat="server" />
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <div style="margin-top: 25px; text-align: center">

                                                            <asp:Button ID="btnAddProduct" runat="server" ValidationGroup="AddProduct" Text="Add Product" OnClick="btnAddProduct_Click" Width="100px" CssClass="btn-success btn-xs" />

                                                            <asp:Button ID="btnUpdate" runat="server" ValidationGroup="AddProduct" Visible="false" Text="Update Product" OnClick="btnUpdate_Click" Width="100px" CssClass="btn-primary btn-xs" />
                                                        </div>
                                                    </div>
                                                </div>

                                            </asp:Panel>
                                            <div id="divlevels" runat="server" visible="false">

                                                <h2>Add Level</h2>

                                                <div class="profile-user-info profile-user-info-striped">
                                                    <div class="profile-info-row">
                                                        <div class="profile-info-name">Level #: </div>

                                                        <div class="profile-info-value">


                                                            <asp:Label ID="lblLevel" runat="server"></asp:Label>
                                                        </div>
                                                    </div>


                                                    <div class="profile-info-row">
                                                        <div class="profile-info-name">Percentage: </div>

                                                        <div class="profile-info-value">
                                                            <asp:TextBox ID="txtPercentage" runat="server" MaxLength="2"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfv_txtPercentage" CssClass="required" runat="server"
                                                                ControlToValidate="txtPercentage"
                                                                ErrorMessage="*" ValidationGroup="AddLevel" ForeColor=""></asp:RequiredFieldValidator>


                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div style="margin-top: 25px; text-align: center">

                                                            <asp:Button ID="btnAddLevel" runat="server" ValidationGroup="AddLevel" Text="Add Level" OnClick="btnAddLevel_Click" Width="100px" CssClass="btn-success btn-xs" />
                                                        </div>
                                                    </div>




                                                </div>

                                            </div>
                                        </div>



                                        <div class="col-md-6 col-sm-12 col-xs-12">
                                            <asp:Repeater ID="rptLevels" runat="server">
                                                <ItemTemplate>
                                                    <div class="profile-user-info profile-user-info-striped">
                                                        <div class="profile-info-row">
                                                            <div class="">
                                                                <asp:ImageButton ID="ibDeleteLevel" Width="30px" Style="margin-top: 10px" runat="server" OnClick="ibDeleteLevel_Click" ImageUrl="~/assets/img/delete.png" />

                                                                <asp:ImageButton ID="ibEditLevel" Width="30px" Style="margin-top: 10px" runat="server" OnClick="ibEditLevel_Click" ImageUrl="~/assets/img/edit.png" />
                                                            </div>

                                                            <asp:HiddenField ID="hdnLevelID" runat="server" Value='<%#Eval("ID") %> ' />
                                                            <div class="profile-info-name"><%#Eval("LevelNumber") %> </div>

                                                            <div class="profile-info-value">


                                                                <asp:TextBox ID="txtLevelPercentage" runat="server" Enabled="false" Text='<%#Eval("Percentage") %> '></asp:TextBox>

                                                                <asp:ImageButton ID="ibUpdateLevel" Visible="false" Width="30px" Style="margin-top: 10px" runat="server" OnClick="ibUpdateLevel_Click" ImageUrl="~/assets/img/update.png" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>


                                    </div>

                                    <div class="row">
                                        <div style="margin-top: 25px; text-align: center">
                                        </div>
                                    </div>



                                    <div class="space-20"></div>
                                    <div id="activitiesBox" runat="server" visible="false">
                                        <div class="widget-box transparent">
                                            <div class="widget-header widget-header-small">
                                                <h4 class="widget-title blue smaller">
                                                    <i class="ace-icon fa fa-rss orange"></i>
                                                    Recent Activities
                                                </h4>

                                                <div class="widget-toolbar action-buttons">
                                                    <a href="#" data-action="reload">
                                                        <i class="ace-icon fa fa-refresh blue"></i>
                                                    </a>
                                                    &nbsp;
														<a href="#" class="pink">
                                                            <i class="ace-icon fa fa-trash-o"></i>
                                                        </a>
                                                </div>
                                            </div>

                                            <div class="widget-body">
                                                <div class="widget-main padding-8">
                                                    <div id="profile-feed-1" class="profile-feed">
                                                        <div class="profile-activity clearfix">
                                                            <div>
                                                                <img class="pull-left" alt="Alex Doe's avatar" src="assets/avatars/avatar5.png" />
                                                                <a class="user" href="#">Alex Doe </a>
                                                                changed his profile photo.
																	<a href="#">Take a look</a>

                                                                <div class="time">
                                                                    <i class="ace-icon fa fa-clock-o bigger-110"></i>
                                                                    an hour ago
                                                                </div>
                                                            </div>

                                                            <div class="tools action-buttons">
                                                                <a href="#" class="blue">
                                                                    <i class="ace-icon fa fa-pencil bigger-125"></i>
                                                                </a>

                                                                <a href="#" class="red">
                                                                    <i class="ace-icon fa fa-times bigger-125"></i>
                                                                </a>
                                                            </div>
                                                        </div>

                                                        <div class="profile-activity clearfix">
                                                            <div>
                                                                <img class="pull-left" alt="Susan Smith's avatar" src="assets/avatars/avatar1.png" />
                                                                <a class="user" href="#">Susan Smith </a>

                                                                is now friends with Alex Doe.
																	<div class="time">
                                                                        <i class="ace-icon fa fa-clock-o bigger-110"></i>
                                                                        2 hours ago
                                                                    </div>
                                                            </div>

                                                            <div class="tools action-buttons">
                                                                <a href="#" class="blue">
                                                                    <i class="ace-icon fa fa-pencil bigger-125"></i>
                                                                </a>

                                                                <a href="#" class="red">
                                                                    <i class="ace-icon fa fa-times bigger-125"></i>
                                                                </a>
                                                            </div>
                                                        </div>

                                                        <div class="profile-activity clearfix">
                                                            <div>
                                                                <i class="pull-left thumbicon fa fa-check btn-success no-hover"></i>
                                                                <a class="user" href="#">Alex Doe </a>
                                                                joined
																	<a href="#">Country Music</a>

                                                                group.
																	<div class="time">
                                                                        <i class="ace-icon fa fa-clock-o bigger-110"></i>
                                                                        5 hours ago
                                                                    </div>
                                                            </div>

                                                            <div class="tools action-buttons">
                                                                <a href="#" class="blue">
                                                                    <i class="ace-icon fa fa-pencil bigger-125"></i>
                                                                </a>

                                                                <a href="#" class="red">
                                                                    <i class="ace-icon fa fa-times bigger-125"></i>
                                                                </a>
                                                            </div>
                                                        </div>

                                                        <div class="profile-activity clearfix">
                                                            <div>
                                                                <i class="pull-left thumbicon fa fa-picture-o btn-info no-hover"></i>
                                                                <a class="user" href="#">Alex Doe </a>
                                                                uploaded a new photo.
																	<a href="#">Take a look</a>

                                                                <div class="time">
                                                                    <i class="ace-icon fa fa-clock-o bigger-110"></i>
                                                                    5 hours ago
                                                                </div>
                                                            </div>

                                                            <div class="tools action-buttons">
                                                                <a href="#" class="blue">
                                                                    <i class="ace-icon fa fa-pencil bigger-125"></i>
                                                                </a>

                                                                <a href="#" class="red">
                                                                    <i class="ace-icon fa fa-times bigger-125"></i>
                                                                </a>
                                                            </div>
                                                        </div>

                                                        <div class="profile-activity clearfix">
                                                            <div>
                                                                <img class="pull-left" alt="David Palms's avatar" src="assets/avatars/avatar4.png" />
                                                                <a class="user" href="#">David Palms </a>

                                                                left a comment on Alex's wall.
																	<div class="time">
                                                                        <i class="ace-icon fa fa-clock-o bigger-110"></i>
                                                                        8 hours ago
                                                                    </div>
                                                            </div>

                                                            <div class="tools action-buttons">
                                                                <a href="#" class="blue">
                                                                    <i class="ace-icon fa fa-pencil bigger-125"></i>
                                                                </a>

                                                                <a href="#" class="red">
                                                                    <i class="ace-icon fa fa-times bigger-125"></i>
                                                                </a>
                                                            </div>
                                                        </div>

                                                        <div class="profile-activity clearfix">
                                                            <div>
                                                                <i class="pull-left thumbicon fa fa-pencil-square-o btn-pink no-hover"></i>
                                                                <a class="user" href="#">Alex Doe </a>
                                                                published a new blog post.
																	<a href="#">Read now</a>

                                                                <div class="time">
                                                                    <i class="ace-icon fa fa-clock-o bigger-110"></i>
                                                                    11 hours ago
                                                                </div>
                                                            </div>

                                                            <div class="tools action-buttons">
                                                                <a href="#" class="blue">
                                                                    <i class="ace-icon fa fa-pencil bigger-125"></i>
                                                                </a>

                                                                <a href="#" class="red">
                                                                    <i class="ace-icon fa fa-times bigger-125"></i>
                                                                </a>
                                                            </div>
                                                        </div>

                                                        <div class="profile-activity clearfix">
                                                            <div>
                                                                <img class="pull-left" alt="Alex Doe's avatar" src="assets/avatars/avatar5.png" />
                                                                <a class="user" href="#">Alex Doe </a>

                                                                upgraded his skills.
																	<div class="time">
                                                                        <i class="ace-icon fa fa-clock-o bigger-110"></i>
                                                                        12 hours ago
                                                                    </div>
                                                            </div>

                                                            <div class="tools action-buttons">
                                                                <a href="#" class="blue">
                                                                    <i class="ace-icon fa fa-pencil bigger-125"></i>
                                                                </a>

                                                                <a href="#" class="red">
                                                                    <i class="ace-icon fa fa-times bigger-125"></i>
                                                                </a>
                                                            </div>
                                                        </div>

                                                        <div class="profile-activity clearfix">
                                                            <div>
                                                                <i class="pull-left thumbicon fa fa-key btn-info no-hover"></i>
                                                                <a class="user" href="#">Alex Doe </a>

                                                                logged in.
																	<div class="time">
                                                                        <i class="ace-icon fa fa-clock-o bigger-110"></i>
                                                                        12 hours ago
                                                                    </div>
                                                            </div>

                                                            <div class="tools action-buttons">
                                                                <a href="#" class="blue">
                                                                    <i class="ace-icon fa fa-pencil bigger-125"></i>
                                                                </a>

                                                                <a href="#" class="red">
                                                                    <i class="ace-icon fa fa-times bigger-125"></i>
                                                                </a>
                                                            </div>
                                                        </div>

                                                        <div class="profile-activity clearfix">
                                                            <div>
                                                                <i class="pull-left thumbicon fa fa-power-off btn-inverse no-hover"></i>
                                                                <a class="user" href="#">Alex Doe </a>

                                                                logged out.
																	<div class="time">
                                                                        <i class="ace-icon fa fa-clock-o bigger-110"></i>
                                                                        16 hours ago
                                                                    </div>
                                                            </div>

                                                            <div class="tools action-buttons">
                                                                <a href="#" class="blue">
                                                                    <i class="ace-icon fa fa-pencil bigger-125"></i>
                                                                </a>

                                                                <a href="#" class="red">
                                                                    <i class="ace-icon fa fa-times bigger-125"></i>
                                                                </a>
                                                            </div>
                                                        </div>

                                                        <div class="profile-activity clearfix">
                                                            <div>
                                                                <i class="pull-left thumbicon fa fa-key btn-info no-hover"></i>
                                                                <a class="user" href="#">Alex Doe </a>

                                                                logged in.
																	<div class="time">
                                                                        <i class="ace-icon fa fa-clock-o bigger-110"></i>
                                                                        16 hours ago
                                                                    </div>
                                                            </div>

                                                            <div class="tools action-buttons">
                                                                <a href="#" class="blue">
                                                                    <i class="ace-icon fa fa-pencil bigger-125"></i>
                                                                </a>

                                                                <a href="#" class="red">
                                                                    <i class="ace-icon fa fa-times bigger-125"></i>
                                                                </a>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="hr hr2 hr-double"></div>

                                        <div class="space-6"></div>

                                        <div class="center">
                                            <button type="button" class="btn btn-sm btn-primary btn-white btn-round">
                                                <i class="ace-icon fa fa-rss bigger-150 middle orange2"></i>
                                                <span class="bigger-110">View more activities</span>

                                                <i class="icon-on-right ace-icon fa fa-arrow-right"></i>
                                            </button>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>

                    <div id="DivAssignSubjects" runat="server" visible="true">

                        <asp:Repeater ID="rpStudentSubjects" runat="server">

                            <ItemTemplate>
                                <div class="profile-user-info profile-user-info-striped">
                                    <div class="profile-info-row">

                                        <div class="checkbox">
                                            <label>
                                                <asp:CheckBox ID="chkSelect" runat="server" />
                                                <span class="cr"><i class="cr-icon fa fa-check"></i></span>
                                            </label>
                                        </div>


                                        <div class="profile-info-name">
                                            <%#Eval("Name")%>
                                        </div>
                                        <asp:HiddenField ID="hdnSubjectID" runat="server" Value='<%#Eval("ID")%>' />
                                        <div class="profile-info-value">

                                            <asp:TextBox ID="txtFees" runat="server" Text='<%#Eval("Fees")%>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfv_txtFees" CssClass="required" runat="server"
                                                ControlToValidate="txtFees"
                                                ErrorMessage="*" ValidationGroup="AddSubject" ForeColor=""></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>

                            </ItemTemplate>
                        </asp:Repeater>

                        <div class="row">
                            <div style="margin-top: 25px; text-align: center">
                            </div>
                        </div>
                    </div>

                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>

