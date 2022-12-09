<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ImageTable.ascx.cs" Inherits="aspWebPreviewProject.ImageTable" %>

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


<div id="ImageContainer">
    <ul class="list-group mt-5">
        <li class="list-group-item d-flex justify-content-between align-content-center">
            <asp:Image ID="IconImage" Width="75" Height="75" runat="server"></asp:Image>
            <div id="outerDiv" class="col-sm-6">
                <asp:Label runat="server" ID="LabelFileName"></asp:Label>
                <div id="about">
                    <asp:Label runat="server" ID="LabelTypeOfError"></asp:Label>
                    <br>
                    <asp:Label runat="server" ID="LabelMeter"></asp:Label>
                    <br />
                    <div class="btn-group">
                        <asp:Button id="BtnShow" text="İncele" runat="server" OnClick="BtnShow_Click" style="width: 120px; margin-right: 20px" type="button" class="btn btn-success" />
                        <button id="BtnDraw" runat="server" style="width: 120px" type="button" class="btn btn-success">Hataları Gör </button>
                    </div>

                </div>
            </div>
            <div class="col-sm-2">
            </div>

        </li>
    </ul>
</div>

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

