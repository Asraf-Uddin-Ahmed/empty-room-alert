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
  private _normalMap = null;


  constructor(
    private geolocation: Geolocation
  ) {
    // console.log('Hello GoogleMapServiceProvider Provider');
    if (this.isMapScriptLoaded()) {
      this._directionsService = new google.maps.DirectionsService;
      this._directionsDisplay = new google.maps.DirectionsRenderer;
    }
  }



  isMapScriptLoaded(): boolean {
    return typeof google == 'object';
  }

  setCurrentLatLng(currentLat, currentLng) {
    this._currentLatLng = new google.maps.LatLng(currentLat, currentLng);
  }

  getCurrentLatLng() {
    return this._currentLatLng;
  }

  loadMap(mapElement: ElementRef) {
    if (!this.isMapScriptLoaded()) {
      console.log("loadMap() -> Map script not loaded");
      return;
    }
    console.log("loadMap() -> Map script loaded");

    if(this._currentLatLng != null) {
      this._normalMap = this.initMap(this._currentLatLng, mapElement);
      this.addMarker();
      return;
    }

    this.geolocation.getCurrentPosition().then((position) => {
      this._currentLatLng = new google.maps.LatLng(position.coords.latitude, position.coords.longitude);
      this._normalMap = this.initMap(this._currentLatLng, mapElement);
      this.addMarker();
    }, (err) => {
      console.log(err);
    });
  }

  loadDirectionalMap(mapElement: ElementRef, destinationLatLng: string) {
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
    if (!this._isDirectionalMapLoaded) {
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

  getDistanceFromLatLonInMeter(lat1, lon1, lat2, lon2) {
    const RADIUS_OF_EARTH_IN_KM = 6371;
    let distanceLat = this.deg2rad(lat2 - lat1);
    let distanceLon = this.deg2rad(lon2 - lon1);
    let squareOfHalfChordLengthBetweenPoints =
      Math.sin(distanceLat / 2) * Math.sin(distanceLat / 2) +
      Math.cos(this.deg2rad(lat1)) * Math.cos(this.deg2rad(lat2)) *
      Math.sin(distanceLon / 2) * Math.sin(distanceLon / 2);
    var angularDistanceInRadian = 2 * Math.atan2(Math.sqrt(squareOfHalfChordLengthBetweenPoints), Math.sqrt(1 - squareOfHalfChordLengthBetweenPoints));
    var distanceInKm = RADIUS_OF_EARTH_IN_KM * angularDistanceInRadian;
    return distanceInKm * 1000;
  }



  private deg2rad(deg) {
    return deg * (Math.PI / 180)
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

  private addMarker() {
    let marker = new google.maps.Marker({
      map: this._normalMap,
      animation: google.maps.Animation.DROP,
      icon: new google.maps.MarkerImage('//maps.gstatic.com/mapfiles/mobile/mobileimgs2.png',
        new google.maps.Size(22, 22),
        new google.maps.Point(0, 18),
        new google.maps.Point(11, 11)),
      position: this._normalMap.getCenter()
    });

    let content = "<p>You are here</p>";
    this.addInfoWindow(this._normalMap, marker, content);
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
