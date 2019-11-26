<%@ Page Title="S - Member Registration" Language="C#" MasterPageFile="~/Masters/Admin.master" AutoEventWireup="true" CodeFile="Settings.aspx.cs" Inherits="Settings" %>

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
                    <span class="heading">Settings</span>
                    <asp:HiddenField ID="hdnProductID" runat="server" />
                    <asp:HiddenField ID="hdnImagePath" runat="server" />

                    <asp:HiddenField ID="hdnMode" runat="server" Value="Add" />

                    <div id="divAddStudent" runat="server">
                        <div>
                            <div id="user-profile-1" class="user-profile row">


                                <div class="col-xs-12 col-sm-12">


                                    <div class="space-12"></div>

                                    <div class="row">
                                        <div class="col-md-6 col-sm-12 col-xs-12">

                                            <div id="divlevels" runat="server">

                                                <h2>Add Configuration Keys</h2>

                                                <div class="profile-user-info profile-user-info-striped">

                                                    <div class="profile-info-row">
                                                        <div class="profile-info-name">Key Name: </div>

                                                        <div class="profile-info-value">
                                                            <asp:TextBox ID="txtKeyNames" runat="server"></asp:TextBox>

                                                            <asp:RequiredFieldValidator ID="rfv_txtKeyNames" CssClass="required" runat="server"
                                                                ControlToValidate="txtKeyNames"
                                                                ErrorMessage="*" ValidationGroup="AddConfigs" ForeColor=""></asp:RequiredFieldValidator>


                                                        </div>
                                                    </div>

                                                    <div class="profile-info-row">
                                                        <div class="profile-info-name">Key Value: </div>

                                                        <div class="profile-info-value">
                                                            <asp:TextBox ID="txtKeyValues" runat="server"></asp:TextBox>

                                                            <asp:RequiredFieldValidator ID="rfv_txtKeyValues" CssClass="required" runat="server"
                                                                ControlToValidate="txtKeyValues"
                                                                ErrorMessage="*" ValidationGroup="AddConfigs" ForeColor=""></asp:RequiredFieldValidator>



                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div style="margin-top: 25px; text-align: center">

                                                            <asp:Button ID="btnAddKey" runat="server" ValidationGroup="AddConfigs" Text="Add Key" OnClick="btnAddKey_Click" Width="100px" CssClass="btn-success btn-xs" />
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
                                                                <asp:ImageButton ID="ibDeleteLevel" Enabled='<%#(Eval("KeyName").ToString().ToLower() == "checkoutlimit" ? false : true) %>' Width="30px" Style="margin-top: 10px" runat="server" OnClick="ibDeleteLevel_Click" ImageUrl="~/assets/img/delete.png" />

                                                                <asp:ImageButton ID="ibEditLevel" Width="30px" Style="margin-top: 10px" runat="server" OnClick="ibEditLevel_Click" ImageUrl="~/assets/img/edit.png" />
                                                            </div>

                                                            <asp:HiddenField ID="hdnID" runat="server" Value='<%#Eval("ID") %> ' />
                                                            <div class="profile-info-name"><%#Eval("KeyName") %> </div>

                                                            <div class="profile-info-value">


                                                                <asp:TextBox ID="txtKeyValue" runat="server" Enabled="false" Text='<%#Eval("KeyValue") %> '></asp:TextBox>

                                                                <asp:ImageButton ID="ibUpdateLevel" Visible="false" Width="30px" Style="margin-top: 10px" runat="server" OnClick="ibUpdateLevel_Click" ImageUrl="~/assets/img/update.png" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>


                                    </div>

                                </div>
                            </div>
                        </div>

                    </div>

                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>

