<%@ Page Title="S - Member Registration" Language="C#" MasterPageFile="~/Masters/Admin.master" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        div.tree ul {
            padding: 0em;
        }

            div.tree ul li, ul li ul li {
                position: relative;
                top: 0;
                bottom: 0;
                padding-bottom: 7px;
            }

                div.tree ul li ul {
                    margin-left: 4em;
                }

        div.tree li {
            list-style-type: none;
        }

            div.tree li a {
                padding: 0 0 0 10px;
                position: relative;
                top: 1em;
            }

                div.tree li a:hover {
                    text-decoration: none;
                }

        div.tree a.addBorderBefore:before {
            content: "";
            display: inline-block;
            width: 2px;
            height: 28px;
            position: absolute;
            left: -47px;
            top: -16px;
            border-left: 1px solid gray;
        }

        div.tree li:before {
            content: "";
            display: inline-block;
            width: 25px;
            height: 0;
            position: relative;
            left: 0em;
            top: 1em;
            border-top: 1px solid gray;
        }

        div.tree ul li ul li:last-child:after, ul li:last-child:after {
            content: '';
            display: block;
            width: 1em;
            height: 1em;
            position: relative;
            background: #fff;
            top: 9px;
            left: -1px;
        }
    </style>
    <script>

        function LoadUISettings() {
            // Select the main list and add the class "hasSubmenu" in each LI that contains an UL
            $('div.tree ul').each(function () {
                $this = $(this);
                //$this.find("li").has("ul").addClass("hasSubmenu");
                $this.find("li").each(function () {

                    if (!$(this).hasClass("hasSubmenu")) {
                        $(this).addClass("hasSubmenu");
                    }
                })


            });
            // Find the last li in each level
            $('div.tree li:last-child').each(function () {
                $this = $(this);
                // Check if LI has children
                if ($this.children('ul').length === 0) {
                    // Add border-left in every UL where the last LI has not children
                    $this.closest('ul').css("border-left", "1px solid gray");
                } else {
                    // Add border in child LI, except in the last one
                    $this.closest('ul').children("li").not(":last").css("border-left", "1px solid gray");
                    // Add the class "addBorderBefore" to create the pseudo-element :defore in the last li
                    $this.closest('ul').children("li").last().children("a").addClass("addBorderBefore");
                    // Add margin in the first level of the list
                    $this.closest('ul').css("margin-top", "20px");
                    // Add margin in other levels of the list
                    $this.closest('ul').find("li").children("ul").css("margin-top", "20px");
                };
            });
            // Add bold in li and levels above
            $('div.tree ul li').each(function () {
                $this = $(this);
                $this.mouseenter(function () {
                    $(this).children("a").css({ "font-weight": "bold", "color": "#336b9b" });
                });
                $this.mouseleave(function () {
                    $(this).children("a").css({ "font-weight": "normal", "color": "#428bca" });
                });
            });
            // Add button to expand and condense - Using FontAwesome
            $('div.tree ul li.hasSubmenu').each(function () {
                $this = $(this);

                if ($(this).children("a").children("i.fa").length == 0) {
                    $this.prepend("<a href='#'><i  style='display:none;'  class='fa fa-minus-circle'></i><i class='fa fa-plus-circle'></i></a>");
                    $this.children("a").not(":last").removeClass().addClass("toogle");
                }
            });


            // Actions to expand and consense

        }

        function AnchorClick(obj) {
            var _id = $(obj).parent("li").data("id");

            if ($(obj).children("i.fa-plus-circle").is(":visible")) {



                $.ajax({
                    type: "POST",
                    url: "Dashboard.aspx/getChildren",
                    data: '{Id:"' + _id + '"}',
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        $(obj).parent("li").append(data.d);

                        LoadUISettings();

                        $('div.tree ul li.hasSubmenu a.toogle').each(function (index, e) {
                            if (jQuery._data(e, 'events') == undefined || jQuery._data(e, 'events')["click"].length == 0) {
                                $(e).on("click", function () { AnchorClick(e) });
                            }
                        })
                    }
                });

                $this = $(obj);
                $this.closest("li").children("ul").toggle("slow");
                $this.children("i").toggle();
                return false;
            } else {
                $("li[data-id='" + _id + "']").closest("li").children("ul").toggle("slow");
                $("li[data-id='" + _id + "']").children("i").toggle();
                $(obj).children("li.fa-minus-circle").hide();
                $(obj).children("li.fa-plus-circle").hide();


                return false;
            }
        }

        $(document).ready(function () {
            LoadUISettings();


            $('div.tree ul li.hasSubmenu a.toogle').click(function (obj) {
                AnchorClick(obj.currentTarget);
            });


        })




    </script>

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
                <div class="col-md-12  col-xs-12" style="float: left">
                    <span class="heading">Dashboard</span>
                    <div class="container tree" style="text-align: left">
                        <asp:Literal ID="ltrTree" runat="server"></asp:Literal>
                    </div>

                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>

