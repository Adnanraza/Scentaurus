<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Admin.master" AutoEventWireup="true" CodeFile="CheckOutRequests.aspx.cs" Inherits="CheckOutRequests" %>

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
                    <span class="heading">Check out requests</span>


                </div>
            </div>

            <div class="row">
                <div class="col-md-12  col-xs-12">


                    <asp:TextBox ID="txtFilter" placeholder="Search by First Name, Last Name, Username" AutoPostBack="true" OnTextChanged="txtFilter_TextChanged" runat="server"></asp:TextBox>
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
                                        <asp:Button ID="btnApprove"  OnClientClick="return confirm('Are you sure you want to approve this request?');"  Enabled='<%#Eval("Status").ToString().ToLower() == "pending"%>' runat="server" OnClick="btnApprove_Click" Text="Approve" CssClass="btn btn-success btn-xs" />
                                        <asp:HiddenField ID="hdnID" runat="server" Value='<%#Eval("ID")%>' />
                                        <asp:HiddenField ID="hdnUserID" runat="server" Value='<%#Eval("UserID")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:Button ID="btnReject"  OnClientClick="return confirm('Are you sure you want to reject this request?');"  Enabled='<%#Eval("Status").ToString().ToLower() == "pending"%>' runat="server" 
                                            OnClick="btnReject_Click" Text="Reject" CssClass="btn btn-primary btn-xs" />
                                        
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Time stamp">
                                    <ItemTemplate>

                                        <%#Eval("TimeStamp")%>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Name">
                                    <ItemTemplate>

                                        <%#Eval("UserDetail.FirstName")%> <%#Eval("UserDetail.LastName")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        RS: <%#Eval("Amount") %>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>

                                        <%#Eval("Status") %>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Description">
                                    <ItemTemplate>

                                        <%#Eval("Description") %>
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


