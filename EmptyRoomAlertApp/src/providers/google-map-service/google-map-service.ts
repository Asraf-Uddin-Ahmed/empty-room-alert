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

  private _directionsService = null;
  private _directionsDisplay = null;
  private _end = "0,-0";
  private _isDirectionalMapLoaded = false;
  private _currentLatLng = null;

  constructor(
    private geolocation: Geolocation
  ) {
    // console.log('Hello GoogleMapServiceProvider Provider');
    if(this.isMapScriptLoaded()) {
      this._directionsService = new google.maps.DirectionsService;
      this._directionsDisplay = new google.maps.DirectionsRenderer;
    }
  }



  isMapScriptLoaded(): boolean {
    return typeof google == 'object';
  }

  setCurrentLatLng(currentLat, currentLng){
    this._currentLatLng = new google.maps.LatLng(currentLat, currentLng);
  }

  loadMap(mapElement: ElementRef) {
    if (!this.isMapScriptLoaded()) {
      console.log("loadMap() -> Map script not loaded");
      return;
    }
    console.log("loadMap() -> Map script loaded");

    this.geolocation.getCurrentPosition().then((position) => {
      this._currentLatLng = new google.maps.LatLng(position.coords.latitude, position.coords.longitude);
      let map = this.initMap(this._currentLatLng, mapElement);
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

    this._end = destinationLatLng;
    let map = this.initDirectionalMap(mapElement);
    this._directionsDisplay.setMap(map);
    this._isDirectionalMapLoaded = true;
    this.calculateAndDisplayRoute(this._currentLatLng.lat(), this._currentLatLng.lng());
  }

  calculateAndDisplayRoute(originLat, originLng) {
    if(!this._isDirectionalMapLoaded) {
      return;
    }
    let originLatLng = new google.maps.LatLng(originLat, originLng);
    
    this._directionsService.route({
      origin: originLatLng,
      destination: this._end,
      travelMode: 'DRIVING'
    }, (response, status) => {
      if (status === 'OK') {
        this._directionsDisplay.setDirections(response);
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
  private initDirectionalMap(mapElement: ElementRef): any {
    let mapOptions = {
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
