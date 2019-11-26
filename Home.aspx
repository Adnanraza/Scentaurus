<%@ Page Title="Edu-Ms - Home" Language="C#" MasterPageFile="~/Masters/Admin.master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script src="https://code.highcharts.com/highcharts.js"></script>
    <script src="https://code.highcharts.com/highcharts-3d.js"></script>
    <script src="https://code.highcharts.com/modules/data.js"></script>
    <script src="https://code.highcharts.com/modules/exporting.js"></script>
    <asp:HiddenField ID="hdnCat" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdnData" runat="server" ClientIDMode="Static" />


    <%--<table id='datatable'>
    <thead>
        <tr>
            <th></th>
            <th>Jane</th>
            <th>John</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <th>Apples</th>
            <td>3</td>
            <td>4</td>
        </tr>
        <tr>
            <th>Pears</th>
            <td>2</td>
            <td>0</td>
        </tr>
        <tr>
            <th>Plums</th>
            <td>5</td>
            <td>11</td>
        </tr>
        <tr>
            <th>Bananas</th>
            <td>1</td>
            <td>1</td>
        </tr>
        <tr>
            <th>Oranges</th>
            <td>2</td>
            <td>4</td>
        </tr>
    </tbody>
</table>--%>

    <asp:HiddenField ID="hdnCredit" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdnDebit" runat="server" ClientIDMode="Static" />


    <asp:HiddenField ID="hdnCredit5Months" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdnDebit5Months" runat="server" ClientIDMode="Static" />


    <asp:Literal ID="l5Month" runat="server"></asp:Literal>
    <asp:Literal ID="lpTopPerformer" runat="server"></asp:Literal>
    <script>

        function LoadTopPerformer() {


            var cat = $("#hdnCat").val().split(',');
            var _data = $("#hdnData").val().split(',');

            var _da = [];

            for (var i = 0; i < _data.length; i++) {

                _da.push(parseFloat(_data[i]));


                // _da.push(parseFloat(_data[i]));
            }

            Highcharts.chart('topPerformer', {
                chart: {
                    type: 'column',
                    options3d: {
                        enabled: true,
                        alpha: 10,
                        beta: 25,
                        depth: 70
                    }
                },
                title: {
                    text: 'Top Performers'
                },
                subtitle: {
                    text: ''//'Notice the difference between a 0 value and a null point'
                },
                plotOptions: {
                    column: {
                        depth: 25
                    }
                },
                xAxis: {
                    categories: cat,//Highcharts.getOptions().lang.shortMonths,
                    labels: {
                        skew3d: true,
                        style: {
                            fontSize: '16px'
                        }
                    }
                },
                yAxis: {
                    title: {
                        text: null
                    }
                },
                series: [{
                    name: 'Percentage',
                    data: _da//[2, 3, null, 4, 0, 5, 1, 4, 6, 3]
                }]
            });
        }




        function Load5Months() {

            Highcharts.chart('c5Month', {
                data: {
                    table: 'datatable'
                },
                chart: {
                    type: 'column'
                },
                title: {
                    text: 'Last 5 Month Ledger'
                },
                yAxis: {
                    allowDecimals: false,
                    title: {
                        text: 'Rupees'
                    }
                },
                tooltip: {
                    formatter: function () {
                        return '<b>' + this.series.name + '</b><br/>' +
                            this.point.y + ' ' + this.point.name.toLowerCase();
                    }
                }
            });

        }

        window.onload = function () {
            setTimeout(function () {
                Highcharts.chart('container', {
                    chart: {
                        plotBackgroundColor: null,
                        plotBorderWidth: 0,
                        plotShadow: false
                    },
                    title: {
                        text: 'Last Month Ledger',
                        align: 'center',
                        verticalAlign: 'middle',
                        y: 40
                    },
                    tooltip: {
                        pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
                    },
                    plotOptions: {
                        pie: {
                            dataLabels: {
                                enabled: true,
                                distance: -50,
                                style: {
                                    fontWeight: 'bold',
                                    color: 'white'
                                }
                            },
                            startAngle: -90,
                            endAngle: 90,
                            center: ['50%', '75%']
                        }
                    },
                    series: [{
                        type: 'pie',
                        name: '',
                        innerSize: '50%',
                        data: [
                            ['Credit - ' + $("#hdnCredit").val(), parseFloat($("#hdnCredit").val())],
                            ['Debit - ' + $("#hdnDebit").val(), parseFloat($("#hdnDebit").val())],
                            {
                                name: 'Ledger',
                                y: 0.2,
                                dataLabels: {
                                    enabled: false
                                }
                            }
                        ]
                    }]
                });
            }, 1000)

            setTimeout(function () {
                Load5Months();
                LoadTopPerformer();
            }, 1000)
        }



    </script>

    <div class="row">
        <div class="col-md-6">
            <div id="topPerformer" style="min-width: 310px; height: 400px; margin: 0 auto"></div>
        </div>
        <div class="col-md-6">
        </div>
    </div>


    <div class="row">
        <div class="col-md-6">
            <div id="container" style="min-width: 310px; height: 350px; max-width: 400px;"></div>
        </div>
        <div class="col-md-6">
            <div id="c5Month" style="min-width: 310px; height: 400px; "></div>
        </div>
    </div>









</asp:Content>

