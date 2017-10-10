import { Component, ViewChild, ElementRef } from '@angular/core';
import { NavController } from 'ionic-angular';

import { LocationTrackerProvider } from '../../providers/location-tracker/location-tracker';
import { GoogleMapServiceProvider } from '../../providers/google-map-service/google-map-service';
import { RoomServiceProvider } from '../../providers/room-service/room-service';

import { AreasPage } from '../areas/areas';


@Component({
  selector: 'page-home',
  templateUrl: 'home.html'
})
export class HomePage {

  @ViewChild('maphome') mapElement: ElementRef;

  constructor(private navCtrl: NavController,
    private locationTracker: LocationTrackerProvider,
    private roomServiceProvider: RoomServiceProvider,
    private googleMapServiceProvider: GoogleMapServiceProvider
  ) {

  }
  ionViewDidLoad() {
    this.googleMapServiceProvider.loadMap(this.mapElement);
  }

  enterRoom(event) {
    this.navCtrl.push(AreasPage);
  }

  // lat: number = 23.87297;
  // lng: number = 90.3828587;
  // changeMarker(){
  //   this.googleMapServiceProvider.addOrUpdateDeviceMarker(this.lat * 1, this.lng * 1);
  //   console.log("changeMarker() => ", this.lat, this.lng);
  // }

  // start(){
  //   this.locationTracker.startForegroundTracking();
  // }

  // stop(){
  //   this.locationTracker.stopForegroundTracking();
  // }


}
