<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" Async="true" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AzureBlog._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Just-in-time manufacturing</h1>
        <p class="lead">
            Just-in-time (JIT) manufacturing, also known as just-in-time production or the Toyota production system (TPS), is a methodology aimed primarily at reducing flow times within production as well as response times from suppliers and to customers. Following its origin and development in Japan, largely in the 1960s and 1970s and particularly at Toyota. 

            Alternative terms for JIT manufacturing have been used. Motorola's choice was short-cycle manufacturing (SCM). IBM's was continuous-flow manufacturing (CFM), and demand-flow manufacturing (DFM), a term handed down from consultant John Constanza at his Institute of Technology in Colorado. Still another alternative was mentioned by Goddard, who said that "Toyota Production System is often mistakenly referred to as the 'Kanban System,'" and pointed out that kanban is but one element of TPS, as well as JIT production. 

            But the wide use of the term JIT manufacturing throughout the 1980s faded fast in the 1990s, as the new term lean manufacturing became established as "a more recent name for JIT." As just one testament to the commonality of the two terms, Toyota production system (TPS) has been and is widely used as a synonym for both JIT and lean manufacturing. 
        </p>
    </div>
    
    <div>
        
    </div>

    <asp:Label ID="Label1" runat="server" Text="Comments"></asp:Label> <br/><br/>
    <asp:Repeater ID="commentsRepeater" runat="server">
        <ItemTemplate>
            <asp:Label id="lblcomment" runat="server" text='<%# Eval("Text") %>' ></asp:Label><br/>
            <asp:Label id="lbluser" runat="server" text= '<%# Eval("UserInfo.UserId") %>'></asp:Label><br/>
            <asp:Label runat="server" Text="Reply:"></asp:Label><br/>
            <textarea id="txtreply"></textarea><br/>
            <button id="btnSubmit">Submit</button><br/><br/>
        </ItemTemplate>
    </asp:Repeater>
    <br/><br/>
    <asp:Label runat="server">Add Comments</asp:Label><br/>
    <asp:TextBox ID="txtComment" runat="server" TextMode ="MultiLine" Height="129px" Width="362px"></asp:TextBox> <br/>
    <asp:Button ID="btnCreate" runat="server" Text="Submit" OnClick="btnCreate_Click" />
</asp:Content>


