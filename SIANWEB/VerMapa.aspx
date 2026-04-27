<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VerMapa.aspx.cs" Inherits="SIANWEB.VerMapa" %>

 <!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>GeoUbicación</title>
    <link rel="icon" type="image/png" href="Images/Iconos/icon_ServicesDesk.ico" />
    <link rel="shortcut icon" type="image/x-icon" href="Images/Iconos/icon_ServicesDesk.ico" />
    <script type="text/javascript" src="https://api.tiles.mapbox.com/mapbox-gl-js/v0.52.0/mapbox-gl.js"></script>
    <link href="https://api.tiles.mapbox.com/mapbox-gl-js/v0.52.0/mapbox-gl.css" rel="stylesheet" />

    
    <style type="text/css">
        html, body, #map {
            width: 100%;
            height: 100%;
            margin: 0;
        }
        .marker {
            /* 
              <script type="text/javascript" src="https://unpkg.com/leaflet@1.3.1/dist/leaflet.js"></script>  
              <link rel="stylesheet" href="https://unpkg.com/leaflet@1.3.1/dist/leaflet.css" />
             *  The following icon is owned by Stadia Maps. While you maintain an account with us, we grant you royalty-free use of
             * this image when displayed on our maps.
             */
            background-image: url('data:image/png;base64,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');
            background-size: cover;
            width: 27px;
            height: 42px;
            cursor: pointer;
        }
    </style>
</head>
<body>

            <div id="map"></div>
        <script type="text/javascript">
            // Initialize a map centered at (Lat, Lng) at zoom level 15
            //  var map = L.map('map').setView([25.713047 , -100.305328], 15);
            var map = new mapboxgl.Map({
                container: 'map',
                // Theme URL; see our themes documentation for more options
                style: 'https://tiles.stadiamaps.com/styles/alidade_smooth.json',
                center: [<%=strInicio%>],  // Initial focus coordinate (long, lat)
                zoom: 16
            });
            /*    // Theme URL format in XYZ PNG format; see our themes documentation for more options
            L.tileLayer('https://tiles.stadiamaps.com/tiles/alidade_smooth/{z}/{x}/{y}{r}.png', {
                maxZoom: 20,
                attribution: '&copy; <a href="https://stadiamaps.com/">Stadia Maps</a>, &copy; <a href="https://openmaptiles.org/">OpenMapTiles</a> &copy; <a href="http://openstreetmap.org">OpenStreetMap</a> contributors',
            }).addTo(map);
            */
            // Add zoom and rotation controls to the map.
            map.addControl(new mapboxgl.NavigationControl());
            // First, we define our marker locations. You can use whatever format you want when
            // working with custom markers, but we have chosen to use GeoJSON for this example, as
            // a lot of geospatial data comes in this form. If you have a lot of data, you may want to
            // put it in another file that is loaded separately.
            // NOTE: in GeoJSON notation, LONGITUDE comes first. GeoJSON
            // uses x, y coordinate notation, just like you used to describe
            // graph coordinates in high school. What you may not realize is
            // that latitude, often said first in English, is actually the y axis. 
            var markerCollection = {
                "type": "FeatureCollection",
                "features": [
                    <%=strUbicaciones%>
                    /*
                    {
                        'type': 'Feature',
                        'geometry': { 'type': 'Point', 'coordinates': [-100.305328, 25.713147 ] },
                        'properties': { 'title': 'Ejemplo Marker 1'}
                    },
                    {
                        'type': 'Feature',
                        'geometry': { 'type': 'Point', 'coordinates': [-100.305428, 25.713347] },
                        'properties': { 'title': 'Ejemplo Marker 2' }
                    }
                    */
                ]
            }; 
            // Next, we can add markers to the map
            markerCollection.features.forEach(function (point) {
                // Since these are HTML markers, we create a DOM element first, which we will later
                // pass to the Marker constructor.
                var elem = document.createElement('div');
                elem.className = 'marker';
                // Now, we construct a marker and set it's coordinates from the GeoJSON. Note the coordinate order.
                var marker = new mapboxgl.Marker(elem); 
                marker.setLngLat(point.geometry.coordinates);
                // You can also create a popup that gets shown when you click on a marker. You can style this using
                // CSS as well if you so desire. A minimal example is shown. The offset will depend on the height of your image.
                var popup = new mapboxgl.Popup({ offset: 24, closeButton: false });
                popup.setHTML('<div>' + point.properties.title + '</div>');
                // Set the marker's popup.
                marker.setPopup(popup);
                // Finally, we add the marker to the map.
                marker.addTo(map);
            });
        </script>
</body>
</html>