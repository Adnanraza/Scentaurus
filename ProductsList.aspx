<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Admin.master" AutoEventWireup="true" CodeFile="ProductsList.aspx.cs" Inherits="ProductsList" %>

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


                </div>
            </div>

            <div class="row">
                <div class="col-md-12  col-xs-12">


                    <asp:TextBox ID="txtSearch" placeholder="Enter product name, price" AutoPostBack="true" OnTextChanged="txtSearch_TextChanged" runat="server"></asp:TextBox>
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
                                        <a href='Products.aspx?i=<%#Eval("ID")%>' target="_blank">
                                            <img width="25" src="assets/img/edit.png" /></a>
                                    </ItemTemplate>
                                </asp:TemplateField>

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

                                <asp:TemplateField HeaderText="Time stamp">
                                    <ItemTemplate>

                                        <%#Eval("CreatedOn")%>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Name">
                                    <ItemTemplate>

                                        <%#Eval("Name")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Price">
                                    <ItemTemplate>
                                        <%#Eval("Price") %>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Levels">
                                    <ItemTemplate>
                                        <%#Eval("LevelCount") %>
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


