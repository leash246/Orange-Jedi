<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="PitchScoresheet.ascx.vb" Inherits="leightoneash.PitchScoresheet" %>
    
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div>
                <span class="RobColRight"><asp:Label ID="lblBlankTop" runat="server" Text="" /></span>   
                <span class="RobColRight"><asp:Label ID="Label1" runat="server" Text=""/></span>  
                <span class="RobColRight"><asp:Label ID="Label2" runat="server" Text=""/></span> 
                <span class="RobColRight"><asp:TextBox ID="txtTeam1" runat="server" CssClass="RobTeam" /></span>
                <span class="RobColCenter"><asp:Label ID="Label3" runat="server" Text="Versus"/></span> 
                <span class="RobCol"><asp:TextBox ID="txtTeam2" runat="server" CssClass="RobTeam" /></span>
                <span class="RobCol"><asp:Label ID="Label4" runat="server" Text=""/></span>  
            </div>
            <div>
                <span class="RobColRight"><asp:Label ID="Label5" runat="server" Text="" /></span>   
                <span class="RobColRight"><asp:Label ID="lblHandTop" runat="server" Text="Hand"/></span>  
                <span class="RobColRight"><asp:Label ID="Label6" runat="server" Text="Took" /></span>
                <span class="RobColRight"><asp:Label ID="lblBid1Top" runat="server" Text="Bid"/></span> 
                <span class="RobColCenter"><asp:Label ID="lblSuitTop" runat="server" Text="Suit"/></span> 
                <span class="RobCol"><asp:Label ID="lblBid2Top" runat="server" Text="Bid"/></span>  
                <span class="RobCol"><asp:Label ID="Label7" runat="server" Text="Took" /></span>
            </div>
            <div>
                <span class="RobColRight" ><asp:Label ID="lblBlank" runat="server"/></span> 
                <span class="RobColRight"><asp:Label ID="lblHand" runat="server" Text="" /></span>   
                <span class="RobColRight"><asp:TextBox ID="txtScore1" runat="server" CssClass="PitchPoints" /></span>
                <span class="RobColRight"><asp:TextBox ID="txtBid1" runat="server" CssClass="PitchPoints" /></span> 
                <span class="RobColCenter"><asp:DropDownList ID="ddlSuit" runat="server" CssClass="RobSuit" >
                    <asp:ListItem Text="" Value="" />
                    <asp:ListItem Text="Hearts" Value="Hearts" />
                    <asp:ListItem Text="Clubs" Value="Clubs" />
                    <asp:ListItem Text="Diamonds" Value="Diamonds" />
                    <asp:ListItem Text="Spades" Value="Spades" />
                </asp:DropDownList></span> 
                <span class="RobCol"  ><asp:TextBox ID="txtBid2" runat="server" CssClass="PitchPoints" /></span>  
                <span class="RobCol" ><asp:TextBox ID="txtScore2" runat="server" CssClass="PitchPoints" /></span>
                <asp:Button runat="server" ID="btnAddScore" Text="Add Score" PostBackUrl="~/Cards.aspx#Scoresheet" />
            </div>

            <div>
                <asp:Repeater runat="server" ID="repHands">
                    <HeaderTemplate>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div>
                            <span class="RobColRight"><asp:Label ID="lblBlankItem" runat="server" Text="" /></span>
                            <span class="RobColRight"><asp:Label ID="lblHand" runat="server" /></span>
                            <span class="RobColRight"><asp:Label ID="lblScore1" runat="server" /></span>
                            <span class="RobColRight"><asp:Label ID="lblBid1" runat="server" /></span>
                            <span class="RobColCenter"><asp:Image ID="imgSuit" runat="server" GenerateEmptyAlternateText="true" /><asp:Label ID="lblSuit" runat="server" Visible="false" /></span>
                    
                            <span class="RobCol"><asp:Label ID="lblBid2" runat="server" /></span>
                            <span class="RobCol"><asp:Label ID="lblScore2" runat="server" /></span>
                        </div>
                    </ItemTemplate>
                    <FooterTemplate>
                    <br /><br />
                        <span class="RobColRight"><asp:Label ID="lblBlankFooter" runat="server" Text="Total:" /></span>
                        <span class="RobColRight"><asp:Label ID="lblHandTotal" runat="server" Visible="false" /></span>
                        <span class="RobColRight"><b><asp:Label ID="lblTeam1Total" runat="server" /></b></span>
                        <span class="RobColRight"><asp:Label ID="Label8" runat="server" /></span>
                        <span class="RobColCenter"><asp:Label ID="Label9" runat="server" /></span>
                        <span class="RobCol"><asp:Label ID="Label10" runat="server" /></span>
                        <span class="RobCol"><b><asp:Label ID="lblTeam2Total" runat="server" /></b></span>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
            <div>
                <asp:Button runat="server" ID="btnClear" Text="Clear Game" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>