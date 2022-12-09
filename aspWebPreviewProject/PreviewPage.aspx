<%@ Page Language="C#" Title="Önizleme Sayfası" AutoEventWireup="true" CodeBehind="PreviewPage.aspx.cs" Inherits="aspWebPreviewProject.PreviewPage" %>

<%@ Register Src="~/ImageTable.ascx" TagPrefix="uc1" TagName="ImageTable" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.0.0/dist/css/bootstrap.min.css"
        integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm"
        crossorigin="anonymous">
    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN"
        crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/popper.js@1.12.9/dist/umd/popper.min.js"
        integrity="sha384-ApNbgh9B+Y1QKtv3Rn7W3mgPxhU9K/ScQsAP7hUibX39j7fakFPskvXusvfa0b4Q"
        crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.0.0/dist/js/bootstrap.min.js"
        integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl"
        crossorigin="anonymous"></script>
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>

    <title>Önizleme Sayfası</title>
    <style>
        table {
            border-collapse: collapse;
            width: 100%;
        }

        th, td {
            text-align: left;
            padding: 8px;
        }

        tr:nth-child(even) {
            background-color: #f2f2f2;
        }
    </style>
</head>
<body>
    <nav class="navbar navbar-light">
        <style>
            .navbar-light {
                background-color: #C3E6CB;
            }
        </style>
        <a class="navbar-brand" href="#">
            <img src="Icon/logo.png" width="150" height="50" class="d-inline-block align-top" alt="">
        </a>
    </nav>
    <br />
    <form id="form1" runat="server">

        <div class="display:flex">
            <row>
                <div class="input-group">
                    <div class="input-group-prepend">
                        <span class="input-group-text" id="basic-addon1">Parti Numarası Giriniz : </span>
                    </div>
                    <asp:TextBox CssClass="col-sm-4" ID="TextBox1" runat="server" placeholder="Örn: P200" aria-describedby="basic-addon1"></asp:TextBox>

                    <asp:Button CssClass="col-sm-auto" ID="Button1" runat="server" Text="Hata Ara" OnClick="Button1_Click" Style="margin-left: 15px" />

                    <asp:Button CssClass="col-sm-auto" ID="Button2" runat="server" Text="Takvimi Aç" OnClick="Button2_Click" />
                </div>
            </row>
        </div>
        <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#C3E6CB" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#C3E6CB" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#E9E7E2" />
            <SortedAscendingHeaderStyle BackColor="#506C8C" />
            <SortedDescendingCellStyle BackColor="#FFFDF8" />
            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
        </asp:GridView>
        <br />
        <div class="display:flex">
            <div class="row" style="margin-left: 0px; margin-right: 0px">
                <div class="col-sm-6" runat="server" id="date">
                    <ul class="list-group">
                        <li class="list-group-item  list-group-item-success">Tarihi</li>
                        <asp:Calendar ID="Calendar1" runat="server" OnSelectionChanged="Calendar1_SelectionChanged"></asp:Calendar>
                    </ul>
                </div>
                <div class="col-sm-6" runat="server" id="PartiNumber">
                    <ul class="list-group">
                        <li id="fabricType" class="list-group-item list-group-item-success">Parti Numarası</li>
                        <asp:ListBox ID="ListBox2" Height="340px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ListBox2Click"></asp:ListBox>
                    </ul>
                </div>
            </div>
            <br />
            <div class="container">
                <div class="row">
                    <div class="col text-center">
                        <asp:Button ID="btnGeri" CssClass="btn btn-danger col-sm-auto" runat="server" Text="Geri" Visible="false" OnClick="btnGeri_Click" />
                        <asp:Button ID="btnIleri" CssClass="btn btn-primary col-sm-auto" runat="server" Text="İleri" Visible="false" OnClick="btnIleri_Click" />

                    </div>
                </div>
            </div>

            <div class="table-responsive">
                <asp:Table runat="server" CssClass="table table-bordered table-sm" ID="table1">
                </asp:Table>
            </div>

            <br />
            <asp:PlaceHolder ID="placeHolderForUserControl" runat="server" />



            <asp:ListBox ID="ListBox3" runat="server" Visible="false" AutoPostBack="True" OnSelectedIndexChanged="ListBox3Click"></asp:ListBox>
            <div class="footer">
            </div>
    </form>

    <script>

        //function OpenWindow(base64) {
        //    let pdfWindow = window.open("")
        //    pdfWindow.document.write(
        //        "<iframe allowFullScreen='true' title='Hata Görüntüleme Sayfası' width='1920' height='1100' src='data:image/jpeg;base64, " +
        //        encodeURI(base64) + "'></iframe>"
        //    )
        //}

        function OpenWindow(fileName) {
            <%--var fileName = <%= fileName%>;--%>
            var screenWidth = window.innerWidth - 20;
            var screenHeight = window.innerHeight - 20;
            window.open("https://localhost:44372/ViewPage.aspx?FileName=" + fileName)
            /*pdfWindow.document.write(
                "<title>" + fileName + "</title><div class='wrap'> <img width='" + screenWidth + "' height = '" + screenHeight + "' status=no style='- webkit - transform: scale(0.5); -moz - transform - scale(0.5);' src = 'data:image/jpeg;base64, " +
                encodeURI(base64) + "'></img > </div>"

            )*/
        }
    </script>

</body>
</html>
