import { Injectable, ElementRef } from '@angular/core';
import { Geolocation } from '@ionic-native/geolocation';
import 'rxjs/add/operator/map';


declare var google;

/*
  Generated class for the GoogleMapServiceProvider provider.

  See https://angular.io/guide/dependency-injection for more info on providers
  and Angular DI.
*/
@Injectable()
export class GoogleMapServiceProvider {

  map: any;

  constructor(private geolocation: Geolocation) {
    // console.log('Hello GoogleMapServiceProvider Provider');
  }


  loadMap(mapElement: ElementRef) {
    if (typeof google != 'object') {
      console.log("Map not loaded");
      return;
    }

    console.log("Map loaded");
    this.geolocation.getCurrentPosition().then((position) => {

      let latLng = new google.maps.LatLng(position.coords.latitude, position.coords.longitude);

      let mapOptions = {
        center: latLng,
        zoom: 15,
        gestureHandling: "cooperative",
        mapTypeId: google.maps.MapTypeId.ROADMAP
      }

      this.map = new google.maps.Map(mapElement.nativeElement, mapOptions);
      this.addMarker();

    }, (err) => {
      console.log(err);
    });
  }

  private addMarker() {

    let marker = new google.maps.Marker({
      map: this.map,
      animation: google.maps.Animation.DROP,
      // icon: "http://maps.google.com/mapfiles/kml/shapes/man.png",
      position: this.map.getCenter()
    });

    let content = "<h3>You are here</h3>";

    this.addInfoWindow(marker, content);
  }
  private addInfoWindow(marker, content) {

    let infoWindow = new google.maps.InfoWindow({
      content: content
    });

    google.maps.event.addListener(marker, 'click', () => {
      infoWindow.open(this.map, marker);
    });
  }

}
