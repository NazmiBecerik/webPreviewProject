<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="kumasSecim.HomePage" %>

<!DOCTYPE html>
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>Kumaş Seçim Ekranı</title>
    <script src="https://ajax.aspnetcdn.com/ajax/jquery/jquery-1.8.0.js"></script>
    <script type="text/javascript">
        var jQuery_1_8_0 = $.noConflict(false);
    </script>
    <script src="https://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.22/jquery-ui.js"></script>
    <link rel="Stylesheet" href="https://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.10/themes/redmond/jquery-ui.css" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.3.1/dist/css/bootstrap.min.css" integrity="sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T" crossorigin="anonymous" />
    <script src="https://code.jquery.com/jquery-3.3.1.slim.min.js" integrity="sha384-q8i/X+965DzO0rT7abK41JStQIAqVgRVzpbzo5smXKp4YfRvH+8abtTE1Pi6jizo" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/popper.js@1.14.7/dist/umd/popper.min.js" integrity="sha384-UO2eT0CpHqdSJQ6hJty5KVphtPhzWj9WO1clHTMGa3JDZwrnQq4sF86dIHNDz0W1" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.3.1/dist/js/bootstrap.min.js" integrity="sha384-JjSmVgyd0p3pXB1rRibZUAYoIIy6OrQ6VrjIEaFf/nJGzIxFDsf4x0xIM+B07jRM" crossorigin="anonymous"></script>
</head>
<body>
    <nav class="navbar navbar-light bg-success">
        <a class="navbar-brand" href="#">

            <img src="Icon/logo.png" width="150" height="50" class="d-inline-block align-top" alt="">
        </a>
    </nav>
    <div class="container" style="height: 50px"></div>

    <div class="container">
        <div class="column align-content-center">
            <div class="col-sm-12">
                <form runat="server">

                    <asp:Label ID="lblBantAktifMi" Font-Size="20px" Width="40%" runat="server" Enabled="false">Aktiflik Durumu</asp:Label>
                    <asp:TextBox ID="tbxBantAktifMi" Font-Size="Large" Height="50" Width="60%" runat="server" Enabled="false"></asp:TextBox>
                    <div class="container" style="height: 20px"></div>

                    <asp:Label Font-Size="20px" Width="40%" runat="server">Parti Numarası</asp:Label>
                    <asp:TextBox ID="tbxPartiNo" Font-Size="Large" Height="50" Width="60%" runat="server"></asp:TextBox>

                    <div class="container" style="height: 20px"></div>

                    <asp:Label Font-Size="20px" Width="40%" runat="server">Article Kodu</asp:Label>
                    <asp:TextBox ID="tbxArticleKod" Font-Size="Large" Height="50" Width="60%" runat="server"></asp:TextBox>

                    <div class="container" style="height: 20px"></div>

                    <asp:Label Font-Size="20px" Width="40%" runat="server">Article Tanımı</asp:Label>
                    <asp:TextBox ID="tbxArticleTanim" Font-Size="Large" Height="50" Width="60%" runat="server"></asp:TextBox>

                    <div class="container" style="height: 20px"></div>

                    <asp:Label Font-Size="20px" Width="40%" runat="server">Base Kodu</asp:Label>
                    <asp:TextBox ID="tbxBaseKod" Font-Size="Large" Height="50" Width="60%" runat="server"></asp:TextBox>

                    <div class="container" style="height: 20px"></div>

                    <asp:Label Font-Size="20px" Width="40%" runat="server">Gramaj</asp:Label>
                    <asp:TextBox ID="tbxGramaj" Font-Size="Large" Height="50" Width="60%" runat="server"></asp:TextBox>

                    <div class="container" style="height: 20px"></div>

                    <asp:Label Font-Size="20px" Width="40%" runat="server">En</asp:Label>
                    <asp:TextBox ID="tbxEn" Font-Size="Large" Height="50" Width="60%" runat="server"></asp:TextBox>

                    <div class="container" style="height: 20px"></div>

                    <asp:Label Font-Size="20px" Width="40%" runat="server">Cari Ünvan</asp:Label>
                    <asp:TextBox ID="tbxCariUnvan" Font-Size="Large" Height="50" Width="60%" runat="server"></asp:TextBox>
                </form>
            </div>
            <br />
        </div>
    </div>
    <footer>
        <div class="row">
            <button type="button" id="btnBaslat" runat="server" onclick="Baslat()" style="height: 100px" class="btn btn-success btn-lg btn-block">Başlat</button>
            <button type="button" id="btnDurdur" runat="server" onclick="Durdur()" style="height: 100px" class="btn btn-danger btn-lg btn-block">Durdur</button>
            <button type="button" id="btnDuraklat" runat="server" onclick="Duraklat()" style="height: 100px" class="btn btn-primary btn-lg btn-block">Duraklat</button>
        </div>
    </footer>
    <script>
        var btnBaslat = document.getElementById("btnBaslat");
        var btnDurdur = document.getElementById("btnDurdur");
        var btnDuraklat = document.getElementById("btnDuraklat");
        var tbxBantAktifMi = document.getElementById("tbxBantAktifMi");
        var lblBantAktifMi = document.getElementById("lblBantAktifMi");
        var tbxPartiNo = document.getElementById("tbxPartiNo");
        var tbxArticleKod = document.getElementById("tbxArticleKod");
        var tbxArticleTanim = document.getElementById("tbxArticleTanim");
        var tbxBaseKod = document.getElementById("tbxBaseKod");
        var tbxGramaj = document.getElementById("tbxGramaj");
        var tbxEn = document.getElementById("tbxEn");
        var tbxCariUnvan = document.getElementById("tbxCariUnvan");
    </script>


    <script>      
        function Baslat() {
            StartAction();
            DisabledTextBox();
            btnDuraklat.removeAttribute('disabled');
            btnDurdur.removeAttribute('disabled');
            tbxBantAktifMi.value = tbxPartiNo.value + " Numaralı Parti Devam Ediyor";
            tbxBantAktifMi.style.backgroundColor = 'green';
        }
    </script>

    <script>
        function Durdur() {
            StopAction();
            ActivateTextBox();
            btnDuraklat.setAttribute('disabled', 'disabled');
            btnDurdur.setAttribute('disabled', 'disabled');
            btnDuraklat.innerText = "Duraklat";
            clearFields();
        };
    </script>

    ,   
    <style>
        .ui-autocomplete {
            max-height: 200px;
            overflow-y: auto;
            overflow-x: hidden;
        }
    </style>

    <script>

        function Duraklat() {
            if (btnDuraklat.innerText == "Duraklat") {
                DisabledTextBox();
                btnDuraklat.innerText = "Devam Ettir";
                PauseAction();
                tbxBantAktifMi.value = tbxPartiNo.value + " Numaralı Parti Duraklatıldı";
                tbxBantAktifMi.style.backgroundColor = 'red';
            }
            else {
                tbxBantAktifMi.value = tbxPartiNo.value + " Numaralı Parti Devam Ediyor";
                tbxBantAktifMi.style.backgroundColor = 'green';
                DisabledTextBox();
                btnDuraklat.innerText = "Duraklat";
                ResumeAction();
            }
        }

    </script>
    <script>
        function DisabledTextBox() {
            btnBaslat.setAttribute('disabled', 'disabled');
            tbxPartiNo.setAttribute('disabled', 'disabled');
            tbxArticleKod.setAttribute('disabled', 'disabled');
            tbxArticleTanim.setAttribute('disabled', 'disabled');
            tbxBaseKod.setAttribute('disabled', 'disabled');
            tbxGramaj.setAttribute('disabled', 'disabled');
            tbxEn.setAttribute('disabled', 'disabled');
            tbxCariUnvan.setAttribute('disabled', 'disabled');
        }

        function ActivateTextBox() {
            btnBaslat.removeAttribute('disabled');
            tbxPartiNo.removeAttribute('disabled');
            tbxArticleKod.removeAttribute('disabled');
            tbxArticleTanim.removeAttribute('disabled');
            tbxBaseKod.removeAttribute('disabled');
            tbxGramaj.removeAttribute('disabled');
            tbxEn.removeAttribute('disabled');
            tbxCariUnvan.removeAttribute('disabled');
        }
    </script>
    <script>

        function StopAction() {
            tbxBantAktifMi.value = "";
            tbxBantAktifMi.style.backgroundColor = '';

           

            var param = { partiNo: $('#tbxPartiNo').val() };
            jQuery_1_8_0.ajax({
                url: "HomePage.aspx/CallStopInCL",
                data: JSON.stringify(param),
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },
                success: function (data) {
                    console.log(data);
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    var err = eval("(" + XMLHttpRequest.responseText + ")");
                    alert(err.Message)
                    // console.log("Ajax Error!");    
                }
            });
        }

        function PauseAction() {
            var param = { partiNo: $('#tbxPartiNo').val() };
            jQuery_1_8_0.ajax({
                url: "HomePage.aspx/CallPauseInCL",
                data: JSON.stringify(param),
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },
                success: function (data) {
                    console.log(data);
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    var err = eval("(" + XMLHttpRequest.responseText + ")");
                    alert(err.Message)
                    // console.log("Ajax Error!");    
                }
            });
        }

        function StartAction() {
            var param = {
                partiNo: $('#tbxPartiNo').val(),
                articleKod: $('#tbxArticleKod').val(),
                articleTanim: $('#tbxArticleTanim').val(),
                baseKod: $('#tbxBaseKod').val(),
                gramaj: $('#tbxGramaj').val(),
                en: $('#tbxEn').val(),
                cariUnvan: $('#tbxCariUnvan').val(),
            };
            jQuery_1_8_0.ajax({
                url: "HomePage.aspx/CallStartInCL",
                data: JSON.stringify(param),
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },
                success: function (data) {
                    console.log(data);
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    var err = eval("(" + XMLHttpRequest.responseText + ")");
                    alert(err.Message)
                    // console.log("Ajax Error!");    
                }
            });
        }

        function ResumeAction() {
            var param = { partiNo: $('#tbxPartiNo').val() };
            jQuery_1_8_0.ajax({
                url: "HomePage.aspx/CallResumeInCL",
                data: JSON.stringify(param),
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },
                success: function (data) {
                    console.log(data);
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    var err = eval("(" + XMLHttpRequest.responseText + ")");
                    alert(err.Message)
                    // console.log("Ajax Error!");    
                }
            });
        }
    </script>
    <script>  
        jQuery_1_8_0(document).ready(function () {
            jQuery_1_8_0("#tbxPartiNo").autocomplete({
                source: function (request, response) {
                    var param = { partiNo: $('#tbxPartiNo').val() };
                    jQuery_1_8_0.ajax({
                        url: "HomePage.aspx/GetFabric",
                        data: JSON.stringify(param),
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataFilter: function (data) { return data; },
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    value: item.PartiNo,
                                    label: item.PartiNo,
                                    items: item
                                }
                            }))
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            var err = eval("(" + XMLHttpRequest.responseText + ")");
                            alert(err.Message)
                            // console.log("Ajax Error!");    
                        }
                    });
                },
                select: function (event, ui) {
                    $("#tbxPartiNo").val(ui.item.items.PartiNo);
                    $("#tbxArticleKod").val(ui.item.items.ArticleKod);
                    $("#tbxArticleTanim").val(ui.item.items.ArticleTanim);
                    $("#tbxBaseKod").val(ui.item.items.BaseKod);
                    $("#tbxGramaj").val(ui.item.items.Gramaj);
                    $("#tbxEn").val(ui.item.items.En);
                    $("#tbxCariUnvan").val(ui.item.items.CariUnvan);
                    return false;
                },
                minLength: 1 //This is the Char length of inputTextBox    
            });
        });


        function clearFields() {
            $('#tbxPartiNo').val("");
            $('#tbxArticleKod').val("");
            $('#tbxArticleTanim').val("");
            $('#tbxBaseKod').val("");
            $('#tbxGramaj').val("");
            $('#tbxEn').val("");
            $('#tbxCariUnvan').val("");


        }
    </script>
</body>
</html>
