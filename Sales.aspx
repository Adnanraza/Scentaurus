<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Admin.master" AutoEventWireup="true" CodeFile="Sales.aspx.cs" Inherits="Sales" %>

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
                    <span class="heading">Sales</span>
                    <h5>
                        This screen shows the list of sales of customers on which you are getting comission.
                    </h5>

                </div>
            </div>

            <div class="row">
                <div class="col-md-12  col-xs-12">


                    <asp:TextBox ID="txtSearch" placeholder="Enter Username, First name, Last name, Sales no." AutoPostBack="true" OnTextChanged="txtSearch_TextChanged" runat="server"></asp:TextBox>
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

                                        <%#Eval("AddedOn")%>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Sales No">
                                    <ItemTemplate>

                                        <%#Eval("SalesNo")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Beneficier">
                                    <ItemTemplate>
                                        <%#Eval("UserDetail1.FirstName") %> <%#Eval("UserDetail1.LastName") %>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Product Name">
                                    <ItemTemplate>

                                        <%#Eval("Product.ProductName") %>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Product Rate">
                                    <ItemTemplate>

                                      RS: <%#Eval("Product.FinalSalePrice") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Quantity">
                                    <ItemTemplate>

                                        <%#Eval("Quantity") %>
                                        
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Total Amount">
                                    <ItemTemplate>

                                       RS: <%#Eval("TotalPrice") %>
                                        
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Percentage">
                                    <ItemTemplate>

                                        <%#Eval("ComissionPercentage") %>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Comission Amount">
                                    <ItemTemplate>

                                        RS: <%#Eval("ComissionAmount") %>
                                        
                                    </ItemTemplate>
                                </asp:TemplateField>


                               <%-- <asp:TemplateField HeaderText="Check Out">
                                    <ItemTemplate>

                                        <%#Eval("CheckOut").ToString().ToLower() == "true" ? "Yes":"No" %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sale To">
                                    <ItemTemplate>
                                        <%#Eval("SaleTo") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Referrer">
                                    <ItemTemplate>
                                        <%#Eval("Referrer") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Product Name">
                                    <ItemTemplate>
                                        <%#Eval("Product") %>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Quantity">
                                    <ItemTemplate>
                                        <%#Eval("Quantity") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Product Price">
                                    <ItemTemplate>
                                        <%#Eval("ProductPrice") %>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Sale's Total Amout">
                                    <ItemTemplate>
                                        <%#Eval("Total") %>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>


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


