<%@ Page Title="Ver Remisiones" Language="C#" MasterPageFile="~/MasterPage/MasterPage02.master"
    AutoEventWireup="true" CodeBehind="VentanaRemisionesOC.aspx.cs" Inherits="SIANWEB.VentanaRemisionesOC" %>


<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">

<h3> Ver Remisiones</h3>

<br />

              <asp:Panel ID="Panel1" runat="server" ScrollBars="Horizontal" Width="900px">
                            <telerik:RadGrid ID="rgRemisiones" runat="server" AutoGenerateColumns="False" GridLines="None"
                                EnableLinqExpressions="False" PageSize="15"
                                AllowPaging="True" MasterTableView-NoMasterRecordsText="No se encontraron registros."
                                Width="1100px">
                                <MasterTableView ClientDataKeyNames="Id_Rem" CommandItemDisplay="Top">

                                    <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="Id_Emp" HeaderText="Id_Emp" UniqueName="Id_Emp"
                                            Display="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Id_Cd" HeaderText="Id_Cd" UniqueName="Id_Cd"
                                            Display="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Id_Ped" HeaderText="Id_Ped" UniqueName="Id_Ped"
                                            Display="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Id_Ter" HeaderText="Id_Ter" UniqueName="Id_Ter"
                                            Display="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Id_Tm" HeaderText="Id_Tm" UniqueName="Id_Tm"
                                            Display="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Id_Rem" HeaderText="Id_Rem" UniqueName="Id_Rem"
                                            Display="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Id_U" HeaderText="Usuario" UniqueName="Id_U"
                                            Display="false">
                                            <HeaderStyle Width="50px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>


                                        <telerik:GridBoundColumn DataField="Rem_Estatus" HeaderText="Rem_Estatus" UniqueName="Rem_Estatus"
                                            Display="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Id_Rem" HeaderText="Número" UniqueName="Id_Rem">
                                            <HeaderStyle Width="60px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Rem_Fecha" HeaderText="Fecha" UniqueName="Rem_Fecha"
                                            DataFormatString="{0:dd/MM/yy}">
                                            <HeaderStyle Width="80px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Id_Cte" HeaderText="Núm. cte." UniqueName="Id_Cte">
                                            <HeaderStyle Width="80px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Rem_Subtotal" HeaderText="Subtotal" UniqueName="Rem_Subtotal"
                                            DataFormatString="{0:N2}">
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle Width="90px" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Rem_Iva" HeaderText="I.V.A." UniqueName="Rem_Iva"
                                            DataFormatString="{0:N2}">
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle Width="90px" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Rem_Total" HeaderText="Total" UniqueName="Rem_Total"
                                            DataFormatString="{0:N2}">
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle Width="90px" />
                                        </telerik:GridBoundColumn>
       
                                    </Columns>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </MasterTableView>
                                <ClientSettings>
                                    <Selecting AllowRowSelect="true" />
                                </ClientSettings>
                                <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                    FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                                    PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                                    ShowPagerText="True" PageButtonCount="3" />
                            </telerik:RadGrid>
                    </asp:Panel>


</asp:Content>