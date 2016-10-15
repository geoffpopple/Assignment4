<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PlayersApp.aspx.cs" Inherits="Assignment4.PlayersApp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
</head>
<body>

    <form id="form1" runat="server">
          <asp:ScriptManager runat="server" EnablePageMethods="true"></asp:ScriptManager>  
<div>
    <div>
        <h2>All Players</h2>
        <ul id="players"></ul>
    </div>
</div>
    <div>
        <h2>Search or Delete</h2>
        <p>
            <asp:DropDownList name="fieldChoice" ID="fc" runat="server">
                <asp:ListItem value="Name">Name</asp:ListItem>
                <asp:ListItem value="Registration_ID">Registration_ID</asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox ID="PlayerID" size="5" runat="server" style="margin-bottom: 0px"></asp:TextBox>
            <asp:Button value="Search"  OnClientClick="find()" runat="server" Text="Search" />
            <asp:Button value="Delete"  OnClientClick="del()" runat="server" Text="Delete" />
    <ul id="player"></ul>
    </div>
<div>
    <asp:Label ID="Label4" runat="server" Text="Regitration ID"></asp:Label>
    <asp:TextBox ID="TxtRegistrationID" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="lblFirstName" runat="server" Text="Player First Name"></asp:Label>
    <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
    Player Last Name<asp:TextBox ID="TxtLastName" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="Label2" runat="server" Text="Team Name"></asp:Label>
    <asp:TextBox ID="txtTeamName" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="Label3" runat="server" Text="Date Of Birth"></asp:Label>
    <asp:TextBox ID="txtDOB" runat="server"></asp:TextBox>
    <br />
    <br />
    <asp:Button ID="btnAdd" runat="server" Text="Add Player"  OnClientClick="myupdate()"/>
    <br />
    </div>
    </form>
        </body>
        <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-2.0.3.min.js"></script>
    <script>
    var uri = 'api/Player';

    $(document).ready(function () {
      // Send an AJAX request
      $.getJSON(uri)
          .done(function (data) {
            // On success, 'data' contains a list of products.
            $.each(data, function (key, item) {
              // Add a list item for the product.
              $('<li>', { text: formatItem(item) }).appendTo($('#players'));
            });
          });
    });

    function formatItem(item) {
        var my_date = item.Date_of_Birth.substr(0,10);
        return item.Registration_ID + ':' + item.Player_name + ':' + item.Team_name + ':' + my_date;
    }

    function validateForm() {
        var x = $('#txtFirstname').val();
        if (x == null || x === "") {

            return false;
        }

        x = $('#txtLastname').val();
        if (x == null || x == "") {

            return false;
        }
        x = $('#txtTeamname').val();
        if (x == null || x == "") {

            return false;
        }
        x = $('#txtDOB').val();
        if (x == null || x == "") {

            return false;
        }
        x = $('#txtregID').val();
        if (x == null || x == "") {

            return false;
        }


        return true;
    }

    function find() {
        var id = $('#PlayerID').val();
        var selfield = $('#fc').val();
        $.getJSON(uri + '/' + selfield + '/' + id)
            .done(function (data) {
                $('#player').html("");
                $.each(data, function (key, item) {
                    // Add a list item for the product.
                    $('<li>', { text: formatItem(item) }).appendTo($('#player'));
                });
            })
        .fail(function (jqXhr, textStatus, err) {
          $('#player').text('Error: ' + err);
        });
    }

    function del() {
            var id = $('#playerId').val();
            var selfield = $('#fc').val();
            $.ajax({
                type: 'DELETE',
                url: uri + '/' + selfield + '/' + id,
                success: function(data) {
                    // On success, 'data' contains a list of products.
                    $('#players').html("");
                    $.each(data,
                        function(key, item) {
                            // Add a list item for the product.
                            $('<li>', { text: formatItem(item) }).appendTo($('#players'));
                        }
                    )
                },
                error: function(jqXhr, textStatus, err) {
                    $('#player').text('Error: ' + err);
                }
            });
    }

    function myupdate() {
        if (validateForm() === true) {
            var aplayer = {
                Registration_ID: $("#txtregID").val(),
                Player_name: $("#txtFirstname").val() + " " + $("#txtLastname").val(),
                Team_name: $("#txtTeamname").val(),
                Date_of_Birth: $("#txtDOB").val()
            };

            $.ajax({
                type: 'POST',
                url: uri,
                data: JSON.stringify(aplayer),
                contentType: "application/json",
                success: function(data) {
                    // On success, 'data' contains a list of products.
                    $('#players').html("");
                    $.each(data,
                        function(key, item) {
                            // Add a list item for the product.
                            $('<li>', { text: formatItem(item) }).appendTo($('#players'));
                        }
                    )
                },
                error: function(jqXhr, textStatus, err) {
                    $('#player').text('Error: ' + err);
                }
            });
        }
    }
    </script>
    </html>
