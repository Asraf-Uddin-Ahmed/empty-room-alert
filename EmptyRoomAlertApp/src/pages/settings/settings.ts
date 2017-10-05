import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams } from 'ionic-angular';

import { RoomServiceProvider } from '../../providers/room-service/room-service';
import { GoogleMapServiceProvider } from '../../providers/google-map-service/google-map-service';

/**
 * Generated class for the SettingsPage page.
 *
 * See https://ionicframework.com/docs/components/#navigation for more info on
 * Ionic pages and navigation.
 */

@Component({
  selector: 'page-settings',
  templateUrl: 'settings.html',
})
export class SettingsPage {

  constructor(
    public navCtrl: NavController, 
    public roomServiceProvider: RoomServiceProvider,
    private googleMapServiceProvider: GoogleMapServiceProvider
  ) {
    
  }

  ionViewDidLoad() {
    // console.log('ionViewDidLoad SettingsPage');
  }


  checkRadius() {
    let currentLatLng = this.googleMapServiceProvider.getCurrentLatLng();
    this.roomServiceProvider.findAnyRoomIsInRadius(currentLatLng.lat(), currentLatLng.lng());
  }
}
