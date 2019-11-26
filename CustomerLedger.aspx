<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Admin.master" AutoEventWireup="true" CodeFile="CustomerLedger.aspx.cs" Inherits="CustomerLedger" %>

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
                    <span class="heading">Customer Ledger</span>
                    <asp:HiddenField ID="hdnUserID" runat="server" />

                </div>
            </div>

            <div class="row">
                <div class="col-md-3  col-xs-6">
                   <b> Username</b>
                </div>
                <div class="col-md-3  col-xs-6">
                    <asp:Label ID="lblUserName" runat="server"></asp:Label>
                </div>

                <div class="col-md-3 col-xs-6">
                    <b>Name</b>
                </div>
                <div class="col-md-3 col-xs-6">
                    <asp:Label ID="lblName" runat="server"></asp:Label>
                </div>
            </div>

            <div class="row">
                <div class="col-md-3  col-xs-6">
                 <b>   Balance</b>
                </div>
                <div class="col-md-3  col-xs-6">
                    <asp:Label ID="lblBalance" runat="server"></asp:Label>
                </div>

                <div class="col-md-3 col-xs-6">
                </div>
                <div class="col-md-3 col-xs-6">
                </div>
            </div>

            <div class="row">
                <div class="col-md-12  col-xs-12">

                    <div class="row table-responsive">

                        <asp:GridView ID="gvwList" runat="server" Width="1000px" AutoGenerateColumns="false" AllowPaging="true" PageSize="20"
                            OnPageIndexChanging="gvwList_PageIndexChanging" OnPageIndexChanged="gvwList_PageIndexChanged">
                            <PagerStyle HorizontalAlign="right" CssClass="paging" />

                            <Columns>
                                <asp:TemplateField HeaderText="Time stamp">
                                    <ItemTemplate>
                                        <%#Eval("CreatedTimeStamp")%>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Adjustment Type">
                                    <ItemTemplate>

                                        <%#Eval("AdjustmentType")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Type">
                                    <ItemTemplate>
                                        <%#Eval("Type") %>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>

                                        RS: <%#Eval("Amount") %>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Balance">
                                    <ItemTemplate>

                                        <%#Eval("Balance") %>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                
                                <asp:TemplateField HeaderText="Created By">
                                    <ItemTemplate>

                                        <%#Eval("CreatedBy") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>

                        </asp:GridView>

                        <div class="col-md-6">
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>


