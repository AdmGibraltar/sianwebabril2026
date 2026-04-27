<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebMap01.aspx.cs" Inherits="SIANWEB.WebMap01" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
 <style>
      /* Always set the map height explicitly to define the size of the div
       * element that contains the map. */
      #map {
        height: 90%;
      }
      /* Optional: Makes the sample page fill the window. */
      html, body {
        height: 100%;
        margin: 0;
        padding: 0;
        font: Arial ;
        font-size: small;
        font-style:normal;
      }
      #directions-panel {
        margin-top: 10px;
        background-color: #FFFFFF;
        padding: 10px;
        overflow: scroll;
        font: Verdana;
        font-size: small;
        color:Navy;
      }
      
    </style>

<link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.4.1/css/all.css" integrity="sha384-5sAR7xN1Nv6T6+dT2mhtzEpVJvfS3NScPQTrOxhwjIuvcA67KV2R5Jz6kr4abQsz" crossorigin="anonymous">
 </head>
<body><!-- 25.712888, -100.305081 -->

    <script  type="text/javascript">
        // In the following example, markers appear when the user clicks on the map.
        // Each marker is labeled with a single alphabetical character.
        var labels = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890';
        var labelIndex = 0;
        var marker;
        var waypts = [];

        var KeyMty = <%= Origen %>; //  { lat: 25.712888, lng: -100.305081 };

        /** @constructor */
        function CoordMapType(tileSize) {
            this.tileSize = tileSize;
        }

        CoordMapType.prototype.getTile = function (coord, zoom, ownerDocument) {
            var div = ownerDocument.createElement('div');
            div.innerHTML = coord;
            div.style.width = this.tileSize.width + 'px';
            div.style.height = this.tileSize.height + 'px';
            div.style.fontSize = '10';
            div.style.borderStyle = 'solid';
            div.style.borderWidth = '1px';
            div.style.borderColor = '#AAAAAA';
            return div;
        };

        function initMap() 
        {
            
            var directionsService = new google.maps.DirectionsService;
            var directionsDisplay = new google.maps.DirectionsRenderer;
            var map = new google.maps.Map(document.getElementById('map'), {
                //  center: { lat: 25.712888, lng: -100.305081 },
                center: KeyMty,
                zoom: 12
                
            });     //  de maps
                    //  }       //  del initMap pero se quita para el AddMarker
            directionsDisplay.setMap(map);
            //  directionsDisplay.setPanel(document.getElementById('right-panel'));

            // This event listener calls addMarker() when the map is clicked.
            google.maps.event.addListener(map, 'click', function(event) { addMarker2(event.latLng, map);
          }
          );
            

          // Add a marker at the center of the map.
          //    addMarker(KeyMty, map, 'Key Mty');
          <%= MarcadorOrigen %>
          

          var geocoder = new google.maps.Geocoder();
          
          //    document.getElementById('submit').addEventListener('click', function () { cmbClientesSelected});
          /*
          try {
          var address0 = 'Paseo De Los Triunfadores 3200, , 64610, Monterrey';
          //alert(address0);
          geocoder.geocode({ 'address': address0 }, function (results, status)
              {
                var coor = results[0].geometry.location;
               // alert(coor);
                addMarker( coor , map, 'HD Cumbres');
              }); 
                }
          catch(err) { alert(err.message); }
          */
          <%= Direcciones %>
          
          // Cuadricula  
          //    map.overlayMapTypes.insertAt(0, new CoordMapType(new google.maps.Size(400, 400)));

          //    Click a la imagen para el routeo completo
          document.getElementById('submitroute').addEventListener('click', function() {
              calculateAndDisplayRoute(directionsService, directionsDisplay);
            });
         
          document.getElementById('RecoverRoute').addEventListener('click', function() {
              DrawRoute(directionsService, directionsDisplay);
            });
            // dibujado de ruta prueba
            

        }       // del initMap

      // Adds a marker to the map.
      function addMarker2(location, map) 
      {
        // Add the marker at the clicked location, and add the next-available label
        // from the array of alphabetical characters.
        /*
        var marker2 = new google.maps.Marker({
            position: location,
            draggable: true,
            //  animation: google.maps.Animation.BOUNCE,
            label: labels[labelIndex++ % labels.length],
            map: map
        });
        */
       }

      function addMarker(location, map, lab) 
      {
        // Add the marker at the clicked location, and add the next-available label
        // from the array of alphabetical characters.
        var marker = new google.maps.Marker({
            position: location,
            draggable: true,
            //  animation: google.maps.Animation.BOUNCE,
            label: lab, //   labels[labelIndex++ % labels.length],
            map: map
        });

        marker.addListener('click', function() {
        try{
        var mLat = marker.getPosition().lat();
        var mLat0 = KeyMty.lat;
        var mLng = marker.getPosition().lng();
        var mLng0 = KeyMty.lng;
        var cua = 0;

        if (mLat < mLat0)
            {
            if (mLng > mLng0)
                {
                 cua = 4; 
                }
            else
                {
                 cua = 3;
                }
            }
        else
            {
            if (mLng > mLng0) 
                {
                 cua= 2; 
                }
            else
                {
                  cua = 1; 
                }
            }
            var txtCuad= document.getElementById('cuadrantes');
            var txtGuar= document.getElementById('txtNombreRuta');
            var txtRutas = document.getElementById('txtNumRuta');
            if (txtCuad.value.length == 0) {
                txtCuad.value = cua; 
                ru = txtRutas.value;
                seleccionar(lab, mLat, mLng); 
                txtGuar.value = 'Cuadrante ' + cua + ' Ruta ' + ru;}
            else {
                  if (txtCuad.value == cua) {
                      seleccionar(lab, mLat, mLng); }
                  else {
                       alert("La ruta no puede cruzar cuadrantes!!!");
                     /*  return false; */ }
                }
        }
        catch(err)
        {
        alert(err.Message);
        }
        }) 
      }

      function geocodeAddress(geocoder, resultsMap) {
          var address = document.getElementById('address').value;
          geocoder.geocode({ 'address': address }, function (results, status)
              {
              if (status === 'OK') {
                  //    alert(results[0].geometry.location);
                  //    resultsMap.setCenter(results[0].geometry.location);
                  var marker = new google.maps.Marker({
                      map: resultsMap,
                      position: results[0].geometry.location
                  });
              } else {
                  alert('Geocode was not successful for the following reason: ' + status);
              }
          });
      }

      function seleccionar(elemento, Lati, Long){
      try {

           var combo =document.getElementById('cmbDomicilios');
           var cantidad = combo.length;
           for (i = 0; i < cantidad; i++) {
              if (combo[i].text == elemento) {
                 combo[i].selected = true;
                 // combo[i].value = " { lat: " + Lati + ", lng: " + Long + " } ";
                }   
            }
          }
          catch(err)
            {
            alert(err.Message);
            }
        }

      // Removes the markers from the map, but keeps them in the array.
      function clearMarkers() {
        setMapOnAll(null);
        txtGuar.value = '';
      }

      // Deletes all markers in the array by removing references to them.
      function deleteMarkers() {
      try {
        clearMarkers();
        waypts = [];

        }
          catch(err)
            {
            alert(err.Message);
            }
      }

      // test for recovery route
      function DrawRoute(directionsService, directionsDisplay) {
      try {
            var final;
            /*
            waypts.push( { location: { lat: 25.7128738, lng: -100.305081 } , stopover: true } );
            waypts.push( { location: { lat: 25.6877181, lng: -100.2619691 } , stopover: true } );
            waypts.push( { location: { lat: 25.6557382, lng: -100.2800876 } , stopover: true } );
            waypts.push( { location: { lat: 25.6303663, lng: -100.2992788 } , stopover: true } );
            waypts.push( { location: { lat: 25.6604534, lng: -100.2325794 } , stopover: true } );
            */
            <%= DetalleRuta %>
            <%= FinalDeRuta %>
            
           directionsService.route({
                      origin: KeyMty,
                      destination: final,
                      waypoints: waypts,
                      optimizeWaypoints: true,
                      provideRouteAlternatives: true,
                      travelMode: 'DRIVING'
                    }, function(response, status) {
                    
                     if (status === 'OK') {
                        directionsDisplay.setDirections(response);
                    /*
                        var route = response.routes[0];
                        var msg="";
                        var summaryPanel = document.getElementById('directions-panel');
                        var summarytext = document.getElementById('address');
                        summaryPanel.innerHTML = '';
                        summarytext.value = '';

                        // For each route, display summary information.
                        
                        for (var i = 0; i < route.legs.length; i++) {
                            var routeSegment = i + 1;
                            //  alert(routeSegment);
                            //  msg = "Segment: " + routeSegment + ":: " + route.legs[i].start_address + " --> " + route.legs[i].end_address + "   Distancia: " + route.legs[i].distance.text + "kms";
                            //  alert(msg);
                            //  alert(route.legs[i].start_latLng);
                          summaryPanel.innerHTML += '<b>Segmento: ' + routeSegment +
                              '::</b><br>';
                          //    summaryPanel.innerHTML += route.legs[i].start_address + ' to ';
                          //    summaryPanel.innerHTML += route.legs[i].end_address + '<br>';
                          summaryPanel.innerHTML += '{ ' + route.legs[i].start_location + ' <b>--></b> ' + route.legs[i].end_location + ' } <br>'; 
                          summaryPanel.innerHTML += route.legs[i].distance.text + '<br><br>';

                          summarytext.value += routeSegment + ';' + route.legs[i].start_location + ';' + route.legs[i].end_location + ';' + route.legs[i].distance.value + '|';
                        }
                        */
                     }
                 
                    });
     
            }
          catch(err)
            {
            alert(err.Message);
            }
      }





      function calculateAndDisplayRoute(directionsService, directionsDisplay) {
        try {
                
                var final;
                waypts = [];
                var checkboxArray = document.getElementById('cmbDomicilios');
                
                for (var i = 0; i < checkboxArray.length; i++) {
                  if (checkboxArray.options[i].selected) {
                    waypts.push(    /// location: checkboxArray[i].value);
                    
                    {
                      location: checkboxArray[i].value,
                      stopover: true
                    } );
                    
                    final = checkboxArray[i].value;
                  }
                }
                directionsService.route({
                  origin: KeyMty,
                  destination: final,
                  waypoints: waypts,
                  optimizeWaypoints: true,
                  provideRouteAlternatives: true,
                  travelMode: 'DRIVING'
                }, function(response, status) {
                 if (status === 'OK') {
                    directionsDisplay.setDirections(response);
                    
                    var route = response.routes[0];
                    var msg="";
                    var summaryPanel = document.getElementById('directions-panel');
                    var summarytext = document.getElementById('address');
                    summaryPanel.innerHTML = '';
                    summarytext.value = '';

                    // For each route, display summary information.
                     for (var i = 0; i < route.legs.length; i++) {
                        var routeSegment = i + 1;
                        //  alert(routeSegment);
                        //  msg = "Segment: " + routeSegment + ":: " + route.legs[i].start_address + " --> " + route.legs[i].end_address + "   Distancia: " + route.legs[i].distance.text + "kms";
                        //  alert(msg);
                        //  alert(route.legs[i].start_latLng);
                          summaryPanel.innerHTML += '<b>Segmento: ' + routeSegment + '::</b><br>';
                          summaryPanel.innerHTML += route.legs[i].start_address + ' A ';
                          summaryPanel.innerHTML += route.legs[i].end_address + '<br>';
                          summaryPanel.innerHTML += '{ ' + route.legs[i].start_location + ' <b>--></b> ' + route.legs[i].end_location + ' } <br>'; 
                          summaryPanel.innerHTML += 'Distancia: ' + route.legs[i].distance.text + '<br><br>';

                         summarytext.value += routeSegment + ';' + route.legs[i].start_location + ';' + route.legs[i].end_location + ';' + route.legs[i].distance.value + '|';
                      }
                 }
                 
                });
            }
          catch(err)
            {
            alert(err.Message);
            }
      }

      function GuardaRutas()
      {
        var summaryPanel = document.getElementById('divNomRuta');
        summaryPanel.style.visibility = "block";
      }

      function SaveRoutes()
      {
      try{
        var boton = document.getElementById('<%= btnGuardaRuta.ClientID %>');
        var textBox = document.getElementById('<%= txtNombreRuta.ClientID %>');
        if (textBox.value == "")
        {
            alert('Debe Capturar un nombre para la ruta');
            return false;
        }
        else
        {
         boton.click();
        }
        }
        catch(err)
        {
            alert(err.Message);
        }
      }

      function Cuadrante(cua)
      {
        var txtCuad= document.getElementById('<%= cuadrantes.ClientID %>');
      }
</script>
<table style=" width:100%; height:100%" >
    <tr>
        <td style=" width:20%; vertical-align:top;">
        <form id="form1" runat="server">
        <table>
            <tr>
                <td>
                    <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server" width="99%">
                    <tr>
                        <td><asp:Label ID="lblMensaje" runat="server"></asp:Label></td>
                        <td style="text-align: right" width="150px"><telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
                            <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server"></telerik:RadCodeBlock>
                            <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
                            </telerik:RadAjaxLoadingPanel>
                            <telerik:RadAjaxManager ID="RAM1" runat="server" ></telerik:RadAjaxManager>
                        </td>
                        <td width="150px" style="font-weight: bold">&nbsp;<asp:HiddenField ID="HF_Cve" runat="server" /></td>
                    </tr>
                </table>
                </td>
            </tr>
            <tr>
                <td valign="top">Cliente:&nbsp;</td>
            </tr>
            <tr>
                <td valign="top">&nbsp;
                    <asp:DropDownList ID="cmbClientes" runat="server" AutoPostBack="true" 
                        OnSelectedIndexChanged="cmbClientesSelected"  Width="200px">
                        <asp:ListItem Text="Seleccione" Value="0" />
                        <asp:ListItem Text="Home Depot" Value="1" />
                        <asp:ListItem Text="Johnson Controls" Value="2" />
                        <asp:ListItem Text="Cerveceria Cuauhtemoc" Value="3" />
                        <asp:ListItem Text="MITSUBA" Value="4" />
                    </asp:DropDownList>
                    <br />
                    <asp:TextBox ID="address" runat="server" Width="200px" Wrap="true" style=" visibility:hidden;"></asp:TextBox>
                    <asp:TextBox ID="cuadrantes" runat="server" Width="3px" Wrap="true" style=" visibility:hidden;"></asp:TextBox>
                    <asp:TextBox ID="txtNumRuta" runat="server" Width="3px" Wrap="true" style=" visibility:hidden;"></asp:TextBox>
                </td>
            </tr>
            <tr><td>Domicilios:&nbsp;</td></tr>
            <tr>
                <td>&nbsp;<!-- AutoPostBack="true" OnSelectedIndexChanged="cmbDomicilioSelected" -->
                    <asp:ListBox ID="cmbDomicilios" runat="server" Height="250px" Width="200px" SelectionMode="Multiple" >
                    </asp:ListBox>           
                </td>
            </tr>
            <tr><td>
                    <div id="divNomRuta" style=" visibility:visible">
                    <table>
                        <tr>
                            <td colspan="3">Nombre de la Ruta:&nbsp;</td>
                        </tr>
                        <tr>
                            <td> <asp:TextBox ID="txtNombreRuta" runat="server" Width="170px" ></asp:TextBox></td>
                            <td>&nbsp;</td>
                            <td align="center" valign="top" style="width:20px; height:20px;color:#746855;   cursor: pointer;" onmouseover="this.style.color='#0000cc';"  onmouseout=" this.style.color='#746855';"
                            id="Td2" class="fa-1x" onclick="SaveRoutes();">&nbsp;
                                <div>
                                    <span class="fa-layers fa-fw" >
                                        <i class="fas fa-save" ></i>
                                    </span>
                                </div>                
                            </td>
                        </tr>
                    </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td align="center" valign="top" style="width:20px; height:20px;color:#746855;   cursor: pointer;" onmouseover="this.style.color='#0000cc';"  onmouseout=" this.style.color='#746855';"
                            id="submitroute" class="fa-2x" >&nbsp;
                                <div>
                                    <span class="fa-layers fa-fw" >
                                        <i class="fas fa-route" ></i>
                                    </span>
                                </div>
                            </td>
                            <td>&nbsp;</td>
                            <td align="center" valign="top" style="width:20px; height:20px;color:#746855;   cursor: pointer;" onmouseover="this.style.color='#0000cc';"  onmouseout=" this.style.color='#746855';"
                            id="RecoverRoute" class="fa-2x" >&nbsp;
                                <div>
                                    <span class="fa-layers fa-fw" >
                                        <i class="fas fa-bezier-curve"></i>
                                    </span>
                                </div>
                            </td>
                            <td>&nbsp;</td>
                            <td align="center" valign="top" style="width:20px; height:20px;color:#746855;   cursor: pointer;" onmouseover="this.style.color='#0000cc';"  onmouseout=" this.style.color='#746855';"
                            id="Td1" class="fa-2x" onclick="location.reload();">&nbsp;
                                <div>
                                    <span class="fa-layers fa-fw" >
                                        <i class="fas fa-sync-alt" ></i>
                                    </span>
                                </div>
                            </td>
                            <td>
                                <div style=" visibility:hidden">
                                    <asp:Button ID="btnGuardaRuta" runat="server" OnClick="btnGuardar_Click" />
                                    <asp:HiddenField ID="hfRuta" runat="server" />
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr><td>&nbsp;</td></tr>
            <td valign="top">Rutas Grabadas:&nbsp;</td>
            <tr>
                <td>
                    <asp:DropDownList ID="cmbRutasCliente" runat="server" AutoPostBack="true" 
                        OnSelectedIndexChanged="cmbRutasClienteSelected"  Width="200px">
                    </asp:DropDownList>
                
                    <div  style="width: 200px; height: 200px; overflow-y: scroll;">
                        <div id="directions-panel" ></div>
                    </div>
                
                </td>
            </tr>
            <tr>
                <td colspan="4">
            
                        <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyCbt9boQiPzeWjZJfM7By24EB5-ZKLv3dA&callback=initMap"
                        async defer></script>
                </td>
            </tr>
            </table>
            </form>
        </td>
        <td><div id="map"></div></td>
    </tr>
    </table>

</body>
</html>
<!-- /*
                styles: [
                { elementType: 'geometry', stylers: [{ color: '#242f3e'}] },
                { elementType: 'labels.text.stroke', stylers: [{ color: '#242f3e'}] },
                { elementType: 'labels.text.fill', stylers: [{ color: '#746855'}] },
                {
                featureType: 'administrative.locality',
                elementType: 'labels.text.fill',
                stylers: [{ color: '#d59563'}]
                },
                {
                featureType: 'poi',
                elementType: 'labels.text.fill',
                stylers: [{ color: '#d59563'}]
                },
                {
                featureType: 'poi.park',
                elementType: 'geometry',
                stylers: [{ color: '#263c3f'}]
                },
                {
                featureType: 'poi.park',
                elementType: 'labels.text.fill',
                stylers: [{ color: '#6b9a76'}]
                },
                {
                featureType: 'road',
                elementType: 'geometry',
                stylers: [{ color: '#38414e'}]
                },
                {
                featureType: 'road',
                elementType: 'geometry.stroke',
                stylers: [{ color: '#212a37'}]
                },
                {
                featureType: 'road',
                elementType: 'labels.text.fill',
                stylers: [{ color: '#9ca5b3'}]
                },
                {
                featureType: 'road.highway',
                elementType: 'geometry',
                stylers: [{ color: '#746855'}]
                },
                {
                featureType: 'road.highway',
                elementType: 'geometry.stroke',
                stylers: [{ color: '#1f2835'}]
                },
                {
                featureType: 'road.highway',
                elementType: 'labels.text.fill',
                stylers: [{ color: '#f3d19c'}]
                },
                {
                featureType: 'transit',
                elementType: 'geometry',
                stylers: [{ color: '#2f3948'}]
                },
                {
                featureType: 'transit.station',
                elementType: 'labels.text.fill',
                stylers: [{ color: '#d59563'}]
                },
                {
                featureType: 'water',
                elementType: 'geometry',
                stylers: [{ color: '#17263c'}]
                },
                {
                featureType: 'water',
                elementType: 'labels.text.fill',
                stylers: [{ color: '#515c6d'}]
                },
                {
                featureType: 'water',
                elementType: 'labels.text.stroke',
                stylers: [{ color: '#17263c'}]
                }
                ]*/-->