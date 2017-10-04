import { Injectable, ElementRef } from '@angular/core';
import { Geolocation } from '@ionic-native/geolocation';
import 'rxjs/add/operator/map';

import { LocationTrackerProvider } from '../../providers/location-tracker/location-tracker';


declare var google;

/*
  Generated class for the GoogleMapServiceProvider provider.

  See https://angular.io/guide/dependency-injection for more info on providers
  and Angular DI.
*/
@Injectable()
export class GoogleMapServiceProvider {

  private directionsService = null;
  private directionsDisplay = null;
  private end = "0,-0";
  
  constructor(
    private locationTracker: LocationTrackerProvider,
    private geolocation: Geolocation
  ) {
    // console.log('Hello GoogleMapServiceProvider Provider');
    if(this.isMapScriptLoaded()) {
      this.directionsService = new google.maps.DirectionsService;
      this.directionsDisplay = new google.maps.DirectionsRenderer;
    }
  }


  isMapScriptLoaded(): boolean {
    return typeof google == 'object';
  }
  loadMap(mapElement: ElementRef) {
    if (!this.isMapScriptLoaded()) {
      console.log("loadMap() -> Map script not loaded");
      return;
    }
    console.log("loadMap() -> Map script loaded");

    this.geolocation.getCurrentPosition().then((position) => {
      let currentLatLng = new google.maps.LatLng(position.coords.latitude, position.coords.longitude);
      let map = this.initMap(currentLatLng, mapElement);
      this.addMarker(map);
    }, (err) => {
      console.log(err);
    });
  }
  loadDirectionalMap(mapElement: ElementRef, destinationLatLng: string){
    if (!this.isMapScriptLoaded()) {
      console.log("loadDirectionalMap() -> Map script not loaded");
      return;
    }
    console.log("loadDirectionalMap() -> Map script loaded");

    this.end = destinationLatLng;
    let currentLatLng = new google.maps.LatLng(this.locationTracker.lat, this.locationTracker.lng);
    let map = this.initMap(currentLatLng, mapElement);

    this.directionsDisplay.setMap(map);
    this.calculateAndDisplayRoute(currentLatLng);
  }
  calculateAndDisplayRoute(originLatLng) {
    this.directionsService.route({
      origin: originLatLng,
      destination: this.end,
      travelMode: 'DRIVING'
    }, (response, status) => {
      if (status === 'OK') {
        this.directionsDisplay.setDirections(response);
      } else {
        window.alert('Directions request failed due to ' + status);
      }
    });
  }


  private initMap(centerLatLng: any, mapElement: ElementRef): any {
    let mapOptions = {
      center: centerLatLng,
      zoom: 15,
      gestureHandling: "cooperative",
      mapTypeId: google.maps.MapTypeId.ROADMAP
    }
    return new google.maps.Map(mapElement.nativeElement, mapOptions);
  }
  private addMarker(map) {
    let marker = new google.maps.Marker({
      map: map,
      animation: google.maps.Animation.DROP,
      // icon: "http://maps.google.com/mapfiles/kml/shapes/man.png",
      position: map.getCenter()
    });

    let content = "<p>You are here</p>";
    this.addInfoWindow(map, marker, content);
  }
  private addInfoWindow(map, marker, content) {
    let infoWindow = new google.maps.InfoWindow({
      content: content
    });

    google.maps.event.addListener(marker, 'click', () => {
      infoWindow.open(map, marker);
    });
  }

}
