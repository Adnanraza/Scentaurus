<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Admin.master" AutoEventWireup="true" CodeFile="Customers.aspx.cs" Inherits="Customers" %>

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
                    <span class="heading">Customers List</span>
                    <asp:Button ID="btnDele" runat="server" Text="Delete Test Data" OnClick="btnDele_Click" />

                </div>
            </div>

            <div class="row">
                <div class="col-md-12  col-xs-12">


                    <asp:TextBox ID="txtSearch" placeholder="Enter Search By Name, Username" AutoPostBack="true" OnTextChanged="txtSearch_TextChanged" runat="server"></asp:TextBox>
                </div>
            </div>

            <div class="row">
                <div class="col-md-12  col-xs-12">

                    <div class="row table-responsive">

                        <asp:GridView ID="gvwList" runat="server" Width="1000px" AutoGenerateColumns="false" AllowPaging="true" PageSize="20"
                            OnPageIndexChanging="gvwList_PageIndexChanging" OnPageIndexChanged="gvwList_PageIndexChanged">
                            <PagerStyle HorizontalAlign="right" CssClass="paging" />

                            <Columns>

                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdnID" runat="server" Value='<%#Eval("ID")%>' />
                                        <a href='EditMember.aspx?i=<%#Eval("ID")%>' target="_blank" ><img width="25" src="assets/img/edit.png" /></a>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <a href='CustomerLedger.aspx?i=<%#Eval("ID")%>' target="_blank" class="btn btn-xs btn-warning">Show Ledger</a>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                  <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <a href='AddAdjustment.aspx?i=<%#Eval("ID")%>' target="_blank" class="btn btn-xs btn-primary">Add Adjustment</a>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                
                                  <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:Button  ID="btnApproveRegistration" CssClass="btn btn-xs btn-success" Enabled='<%#Eval("IsActive").ToString().ToLower() == "false" %>'  OnClientClick="return confirm('Are you sure you want to approve this request?');"  runat="server" OnClick="btnApproveRegistration_Click" Text="Approve"/>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Time stamp">
                                    <ItemTemplate>

                                        <%#Eval("CreatedOn")%>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Name">
                                    <ItemTemplate>

                                        <%#Eval("FirstName")%> <%#Eval("LastName")%>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Username">
                                    <ItemTemplate>
                                        <%#Eval("Username") %>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Email">
                                    <ItemTemplate>
                                        <%#Eval("Email") %>
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


