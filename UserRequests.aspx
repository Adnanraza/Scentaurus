<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Admin.master" AutoEventWireup="true" CodeFile="UserRequests.aspx.cs" Inherits="UserRequests" %>

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
                    <span class="heading">User Requests</span>


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
                                        <span class="btn btn-xs" style='margin-bottom: 20px; display:<%#Eval("RequestType").ToString().ToLower() != "web request" ? "none" : String.Empty %>'>Web Request</span>
                                        <asp:Button ID="btnApprove" OnClientClick="return confirm('Are you sure you want to approve this request?');" Enabled='<%#Eval("Status") != null ?( (Eval("Status").ToString().ToLower() == "pending" && Eval("RequestType").ToString().ToLower() == "web request" ? true : false )  ) : false%>' runat="server" OnClick="btnApprove_Click" Text="Approve" CssClass="btn btn-success btn-xs" />
                                        <asp:HiddenField ID="hdnID" runat="server" Value='<%#Eval("ID")%>' />
                                        <asp:HiddenField ID="hdnUserID" runat="server" Value='<%#Eval("RequestByUserID")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        
                                        <asp:Button ID="btnReject" OnClientClick="return confirm('Are you sure you want to reject this request?');" Enabled='<%#Eval("Status") != null ?( (Eval("Status").ToString().ToLower() == "pending" && Eval("RequestType").ToString().ToLower() == "web request" ? true : false )  ) : false%>' runat="server"
                                            OnClick="btnReject_Click" Text="Reject" CssClass="btn btn-primary btn-xs" />

                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Time stamp">
                                    <ItemTemplate>

                                        <%#Eval("CreatedOn")%>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Request By">
                                    <ItemTemplate>

                                        <%#Eval("UserDetail.FirstName")%> <%#Eval("UserDetail.LastName")%> (<%#Eval("UserDetail.Username")%>)
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Request Title">
                                    <ItemTemplate>
                                        <%#Eval("RequestTitle") %>
                                    </ItemTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Transaction ID">
                                    <ItemTemplate>

                                        <%#Eval("TransactionID") %>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>

                                        <%#Eval("Status") %>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Description">
                                    <ItemTemplate>

                                        <%#Eval("RequestDescription") %>
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


