<%@ Page Title="S - Member Registration" Language="C#" MasterPageFile="~/Masters/Admin.master" AutoEventWireup="true" CodeFile="Registration.aspx.cs" Inherits="Registration" %>

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
                    <span class="heading">Member Registration</span>

                    <asp:HiddenField ID="hdnImagePath" runat="server" />

                    <div>
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
                                    <div id="divAddMember" runat="server" visible="true">
                                        <div class="row">
                                            <div class="col-md-6 col-sm-12 col-xs-12">


                                                <h2>Personal Information</h2>
                                                <div class="profile-user-info profile-user-info-striped">
                                                    <div class="profile-info-row">
                                                        <div class="profile-info-name">First Name: </div>

                                                        <div class="profile-info-value">


                                                            <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfv_txtFirstName" CssClass="required" runat="server"
                                                                ControlToValidate="txtFirstName"
                                                                ErrorMessage="*" ValidationGroup="Registration" ForeColor=""></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>

                                                    <div class="profile-info-row">
                                                        <div class="profile-info-name">Last Name: </div>

                                                        <div class="profile-info-value">
                                                            <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>

                                                            <asp:RequiredFieldValidator ID="rfv_txtLastName" CssClass="required" runat="server"
                                                                ControlToValidate="txtLastName"
                                                                ErrorMessage="*" ValidationGroup="Registration" ForeColor=""></asp:RequiredFieldValidator>

                                                        </div>
                                                    </div>

                                                    <div class="profile-info-row">
                                                        <div class="profile-info-name">Father Name: </div>

                                                        <div class="profile-info-value">
                                                            <asp:TextBox ID="txtFatherName" runat="server"></asp:TextBox>

                                                            <asp:RequiredFieldValidator ID="rfv_txtFatherName" CssClass="required" runat="server"
                                                                ControlToValidate="txtFatherName"
                                                                ErrorMessage="*" ValidationGroup="Registration" ForeColor=""></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>


                                                    <div class="profile-info-row">
                                                        <div class="profile-info-name">Email: </div>

                                                        <div class="profile-info-value">
                                                            <asp:TextBox ID="txtEmail" MaxLength="100" runat="server"></asp:TextBox>

                                                            <asp:RequiredFieldValidator ID="rfv_txtEmail" CssClass="required" runat="server"
                                                                ControlToValidate="txtEmail"
                                                                ErrorMessage="*" ValidationGroup="Registration" ForeColor=""></asp:RequiredFieldValidator>

                                                        </div>
                                                    </div>

                                                    <div class="profile-info-row">
                                                        <div class="profile-info-name">Username: </div>

                                                        <div class="profile-info-value">
                                                            <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox>

                                                            <asp:RequiredFieldValidator ID="rfv_txtUsername" CssClass="required" runat="server"
                                                                ControlToValidate="txtUsername"
                                                                ErrorMessage="*" ValidationGroup="Registration" ForeColor=""></asp:RequiredFieldValidator>

                                                        </div>
                                                    </div>

                                                    <div class="profile-info-row">
                                                        <div class="profile-info-name">CNIC: </div>

                                                        <div class="profile-info-value">
                                                            <asp:TextBox ID="txtCNIC"
                                                                runat="server"></asp:TextBox>

                                                            <asp:RequiredFieldValidator ID="rfv_txtCNIC" CssClass="required" runat="server"
                                                                ControlToValidate="txtCNIC"
                                                                ErrorMessage="*" ValidationGroup="Registration" ForeColor=""></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>

                                                    <%--    <div class="profile-info-row">
                                                    <div class="profile-info-name">DOB: </div>

                                                    <div class="profile-info-value">
                                                        <asp:TextBox ID="txtDOB" placeholder="DD/MM/YYYY" runat="server"
                                                            onkeydown="javascript:return dFilter (event.keyCode, this, '##/##/####');"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfv_txtDOB" CssClass="required" runat="server"

                                                            ControlToValidate="txtDOB"
                                                            ErrorMessage="*" ValidationGroup="Registration" ForeColor=""></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>--%>


                                                    <div class="profile-info-row">
                                                        <div class="profile-info-name">Comission Upto Level: </div>

                                                        <div class="profile-info-value">
                                                            <asp:TextBox ID="txtCOmissionUpto" onkeyup="NumAndEnterOnly(event , this);" Text="" runat="server"></asp:TextBox>

                                                            <asp:RequiredFieldValidator ID="rfv_txtCOmissionUpto" CssClass="required" runat="server"
                                                                ControlToValidate="txtCOmissionUpto"
                                                                ErrorMessage="*" ValidationGroup="Registration" ForeColor=""></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>

                                                    <div class="profile-info-row">
                                                        <div class="profile-info-name">Product: </div>

                                                        <div class="profile-info-value">
                                                            <asp:DropDownList ID="ddlProduct" runat="server">
                                                            </asp:DropDownList>

                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="required" runat="server"
                                                                ControlToValidate="ddlProduct" InitialValue="0"
                                                                ErrorMessage="*" ValidationGroup="Registration" ForeColor=""></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>

                                                    <div class="profile-info-row">
                                                        <div class="profile-info-name">Quantity: </div>

                                                        <div class="profile-info-value">
                                                            <asp:TextBox ID="txtQuantity" onkeyup="NumAndEnterOnly(event , this);" Text="" runat="server"></asp:TextBox>

                                                            <asp:RequiredFieldValidator ID="rfv_txtQuantity" CssClass="required" runat="server"
                                                                ControlToValidate="txtQuantity"
                                                                ErrorMessage="*" ValidationGroup="Registration" ForeColor=""></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>

                                                     <div class="profile-info-row">
                                                        <div class="profile-info-name">Transaction ID: </div>

                                                        <div class="profile-info-value">
                                                            <asp:TextBox ID="txtTransactionID"  Text="" runat="server"></asp:TextBox>

                                                            <asp:RequiredFieldValidator ID="rfv_txtTransactionID" CssClass="required" runat="server"
                                                                ControlToValidate="txtTransactionID"
                                                                ErrorMessage="*" ValidationGroup="Registration" ForeColor=""></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>


                                                    <div class="profile-info-row">
                                                        <div class="profile-info-name">User Type: </div>

                                                        <div class="profile-info-value">
                                                            <asp:DropDownList ID="ddlUserType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlUserType_SelectedIndexChanged">
                                                                <asp:ListItem Text="-----Select-----" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="Admin" Value="Admin"></asp:ListItem>
                                                                <asp:ListItem Text="Normal User" Value="Normal"></asp:ListItem>
                                                                <asp:ListItem Text="Invester" Value="Invester"></asp:ListItem>
                                                            </asp:DropDownList>

                                                            <asp:RequiredFieldValidator ID="rfv_ddlUserType" CssClass="required" runat="server"
                                                                ControlToValidate="ddlUserType" InitialValue="0"
                                                                ErrorMessage="*" ValidationGroup="Registration" ForeColor=""></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>


                                                    <div class="profile-info-row" id="divPer" visible="false" runat="server">
                                                        <div class="profile-info-name">Percentage of sale: </div>

                                                        <div class="profile-info-value">


                                                            <asp:TextBox ID="txtPercentageOfSale" onkeyup="NumAndEnterOnly(event , this);" Text="" runat="server"></asp:TextBox>

                                                            <asp:RequiredFieldValidator ID="rfv_txtPercentageOfSale" CssClass="required" runat="server"
                                                                ControlToValidate="txtPercentageOfSale"
                                                                ErrorMessage="*" ValidationGroup="Registration" ForeColor=""></asp:RequiredFieldValidator>

                                                        </div>
                                                    </div>




                                                    <div class="profile-info-row">
                                                        <div class="profile-info-name">Refer By: </div>

                                                        <div class="profile-info-value">
                                                            <asp:TextBox ID="txtReferBY" placeholder="Enter the user name of the referrer" runat="server"></asp:TextBox>

                                                            <asp:RequiredFieldValidator ID="rfv_txtReferBY" CssClass="required" runat="server"
                                                                ControlToValidate="txtReferBY"
                                                                ErrorMessage="*" ValidationGroup="Registration" ForeColor=""></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>


                                                </div>

                                            </div>



                                            <div class="col-md-6 col-sm-12 col-xs-12">
                                                <h2>Contact Information</h2>

                                                <div class="profile-user-info profile-user-info-striped">
                                                    <div class="profile-info-row">
                                                        <div class="profile-info-name">Contact #: </div>

                                                        <div class="profile-info-value">


                                                            <asp:TextBox ID="txtPhone" runat="server" onkeydown="javascript:return dFilter (event.keyCode, this, '####-#######');"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfv_txtPhone" CssClass="required" runat="server"
                                                                ControlToValidate="txtPhone"
                                                                ErrorMessage="*" ValidationGroup="Registration" ForeColor=""></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>


                                                    <div class="profile-info-row">
                                                        <div class="profile-info-name">Altenative Contact #: </div>

                                                        <div class="profile-info-value">
                                                            <asp:TextBox ID="txtPhone2" onkeydown="javascript:return dFilter (event.keyCode, this, '####-#######');" runat="server"></asp:TextBox>

                                                        </div>
                                                    </div>


                                                    <div class="profile-info-row">
                                                        <div class="profile-info-name">Current Address #: </div>

                                                        <div class="profile-info-value">
                                                            <asp:TextBox ID="txtCurrentAddress" runat="server"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfv_txtCurrentAddress" CssClass="required" runat="server"
                                                                ControlToValidate="txtCurrentAddress"
                                                                ErrorMessage="*" ValidationGroup="Registration" ForeColor=""></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>

                                                    <div class="profile-info-row">
                                                        <div class="profile-info-name">Permanent Address#: </div>

                                                        <div class="profile-info-value">
                                                            <asp:TextBox ID="txtPermanentAddress" runat="server"></asp:TextBox>

                                                        </div>
                                                    </div>



                                                </div>
                                            </div>


                                        </div>

                                        <div class="row">
                                            <div style="margin-top: 25px; text-align: center">

                                                <asp:Button ID="btnAddMember" runat="server" ValidationGroup="Registration" Text="Add" OnClick="btnAddMember_Click" Width="100px" CssClass="btn-success btn-xs" />
                                            </div>
                                        </div>
                                    </div>


                                    <div class="row" id="divLevels" runat="server" visible="false">
                                        <div class="col-md-6 col-sm-12 col-xs-12">
                                            <div>
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
                                                            <asp:TextBox ID="txtPercentage" runat="server"></asp:TextBox>
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
                                            <asp:HiddenField ID="hdnUserID" runat="server" />

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

                                </div>
                            </div>
                        </div>

                    </div>

                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>

