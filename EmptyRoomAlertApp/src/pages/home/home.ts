import { Component, ViewChild, ElementRef } from '@angular/core';
import { NavController, AlertController } from 'ionic-angular';
import { Geolocation } from '@ionic-native/geolocation';
import { Network } from '@ionic-native/network';

import { RoomsPage } from '../rooms/rooms';


declare var google;


@Component({
  selector: 'page-home',
  templateUrl: 'home.html'
})
export class HomePage {

  @ViewChild('map') mapElement: ElementRef;
  map: any;
  isMapLoaded: boolean = false;
  apiKey: any = "AIzaSyCRVmQqJ3CaG8Grz2l1XDSmAaJ0lv_xzl4";

  constructor(private navCtrl: NavController,
    private geolocation: Geolocation,
    private network: Network,
    private alertCtrl: AlertController) {
    // watch network for a disconnect
    let disconnectSubscription = this.network.onDisconnect().subscribe(() => {
      console.log('network was disconnected :(');

      const alert = this.alertCtrl.create({
        title: 'Disconnected',
        subTitle: 'Network not found',
        message: 'Please check your network connection.',
        enableBackdropDismiss: false,
        buttons: ['OK']
      });
      alert.present();
    });

    // watch network for a connection
    let connectSubscription = this.network.onConnect().subscribe(() => {
      console.log('network connected :)');
      // We just got a connection but we need to wait briefly
      // before we determine the connection type. Might need to wait.
      // prior to doing any api requests as well.
      console.log(this.network);

      const alert = this.alertCtrl.create({
        title: 'Connected',
        subTitle: 'Network found',
        message: 'You can use app now.',
        enableBackdropDismiss: false,
        buttons: [{
          text: 'OK',
          role: 'cancel',
          handler: data => {
            location.reload();
          }
        }]
      });
      alert.present();

    });
  }

  enterRoom(event) {
    this.navCtrl.push(RoomsPage);
  }

  ionViewDidLoad() {
    this.loadMap();
  }

  loadMap() {
    if (this.isMapLoaded == true) {
      return;
    }
    console.log("loadMap");

    // this.loadGoogleMapScript();

    this.geolocation.getCurrentPosition().then((position) => {

      let latLng = new google.maps.LatLng(position.coords.latitude, position.coords.longitude);

      let mapOptions = {
        center: latLng,
        zoom: 15,
        mapTypeId: google.maps.MapTypeId.ROADMAP
      }

      this.map = new google.maps.Map(this.mapElement.nativeElement, mapOptions);
      this.addMarker();

      this.isMapLoaded = true;

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
