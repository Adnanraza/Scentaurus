﻿<%@ Page Title="S - Member Registration" Language="C#" MasterPageFile="~/Masters/Admin.master" AutoEventWireup="true" CodeFile="AddProduct.aspx.cs" Inherits="AddProduct" %>

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
                    <span class="heading">Add Product to Customer</span>
                    <asp:HiddenField ID="hdnUserID" runat="server" />

                    <div id="divAddStudent" runat="server">
                        <div>
                            <div id="user-profile-1" class="user-profile row">


                                <div class="col-xs-12 col-sm-12">


                                    <div class="space-12"></div>

                                    <div class="row">
                                        <div class="col-md-12 col-sm-12 col-xs-12">

                                            <asp:Panel ID="pnlAddProduct" runat="server">

                                                <div class="profile-user-info profile-user-info-striped">
                                                    <div class="profile-info-row">
                                                        <div class="profile-info-name">Name: </div>

                                                        <div class="profile-info-value">
                                                            <asp:Label ID="lblName" runat="server"></asp:Label>
                                                        </div>
                                                    </div>

                                                    <div class="profile-info-row">
                                                        <div class="profile-info-name">Username: </div>

                                                        <div class="profile-info-value">
                                                            <asp:Label ID="lblUsername" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>


                                                <div class="profile-user-info profile-user-info-striped">
                                                    <div class="profile-info-row">
                                                        <div class="profile-info-name">Product: </div>

                                                        <div class="profile-info-value">
                                                            <asp:DropDownList ID="ddlProduct" runat="server">
                                                            </asp:DropDownList>

                                                            <asp:RequiredFieldValidator ID="rfv_ddlProduct" CssClass="required" runat="server"
                                                                ControlToValidate="ddlProduct" InitialValue="0"
                                                                ErrorMessage="*" ValidationGroup="AddProduct" ForeColor=""></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>

                                                    <div class="profile-info-row">
                                                        <div class="profile-info-name">Quantity: </div>

                                                        <div class="profile-info-value">
                                                            <asp:TextBox ID="txtQuantity" runat="server"></asp:TextBox>

                                                            <asp:RequiredFieldValidator ID="rfv_txtQuantity" onkeyup="NumAndEnterOnly(event , this);" CssClass="required" runat="server"
                                                                ControlToValidate="txtQuantity"
                                                                ErrorMessage="*" ValidationGroup="AddProduct" ForeColor=""></asp:RequiredFieldValidator>

                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div style="margin-top: 25px; text-align: center">

                                                            <asp:Button ID="btnAddProduct" runat="server" ValidationGroup="AddProduct" Text="Add Product" OnClick="btnAddProduct_Click" Width="100px" CssClass="btn-success btn-xs" />
                                                        </div>
                                                    </div>
                                                </div>

                                            </asp:Panel>
                                        </div>

                                    </div>

                                    <div class="row">
                                        <div style="margin-top: 25px; text-align: center">
                                        </div>
                                    </div>



                                    <div class="space-20"></div>

                                </div>
                            </div>
                        </div>

                    </div>

                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>

