<%@ Page Title="Edu-Ms - Adjustment" Language="C#" MasterPageFile="~/Masters/Admin.master" AutoEventWireup="true" CodeFile="AddAdjustment.aspx.cs" Inherits="AddAdjustment" %>

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
                    <span class="heading">Adjustment</span>

                    <asp:HiddenField ID="hdnUserID" runat="server" />
                    <div id="divAddPayment" runat="server">

                        <div class="row">
                            <div class="col-md-6">
                                <div class="col-md-6 col-xs-12">
                                    <b>
                                        <span>Username:
                                        </span>
                                    </b>
                                </div>
                                <div class="col-md-6 col-xs-12">
                                    <asp:Label ID="lblUserName" runat="server"></asp:Label>


                                </div>
                            </div>

                            <div class="col-md-6">
                                <div class="col-md-6 col-xs-12">
                                    <b>
                                        <span>Name:
                                        </span></b>
                                </div>
                                <div class="col-md-6 col-xs-12">

                                    <asp:Label ID="lblName" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-6">
                                <div class="col-md-6 col-xs-12">
                                    <b>
                                        <span>Balance:
                                        </span>
                                    </b>
                                </div>
                                <div class="col-md-6 col-xs-12">
                                    <asp:Label ID="lblBalance" runat="server"></asp:Label>


                                </div>
                            </div>

                            <div class="col-md-6">
                                <div class="col-md-6 col-xs-12">
                                    <b>
                                        <span>Amount:
                                        </span>
                                    </b>
                                </div>
                                <div class="col-md-6 col-xs-12">

                                    <asp:TextBox ID="txtAmount" onkeyup="NumMinusAndEnterOnly(event , this);" MaxLength="5" runat="server"></asp:TextBox>

                                    <asp:RequiredFieldValidator ID="rfv_txtAmount" CssClass="required" runat="server"
                                        ControlToValidate="txtAmount"
                                        ErrorMessage="*" ValidationGroup="AddPayment" ForeColor=""></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>



                        <div class="row">
                            <div class="col-md-6">
                                <div class="col-md-6 col-xs-12">
                                    <b>
                                        <span>Adjustment type
                                        </span></b>
                                </div>
                                <div class="col-md-6 col-xs-12">
                                    <asp:DropDownList ID="ddlAdjustmentType" runat="server">
                                    </asp:DropDownList>

                                </div>
                            </div>

                            <div class="col-md-6">
                                <div class="col-md-6 col-xs-12">
                                    <b>
                                        <span>Description:
                                        </span>
                                    </b>
                                </div>
                                <div class="col-md-6 col-xs-12">
                                    <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine"></asp:TextBox>

                                </div>
                            </div>


                        </div>

                        <div class="row">
                            <div class="col-md-4 col-sm-12"></div>

                            <div class="col-md-4 col-sm-12">

                                <asp:HiddenField ID="hdnCategoryID" runat="server" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" Width="100px" CssClass="btn-danger btn-xs" />
                                <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" ValidationGroup="AddPayment" Width="100px" CssClass="btn-success btn-xs" />

                            </div>

                            <div class="col-md-4 col-sm-12"></div>


                        </div>

                    </div>



                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>


