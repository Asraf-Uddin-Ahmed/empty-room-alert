import { Component, ViewChild, ElementRef } from '@angular/core';
import { NavController } from 'ionic-angular';

import { LocationTrackerProvider } from '../../providers/location-tracker/location-tracker';
import { GoogleMapServiceProvider } from '../../providers/google-map-service/google-map-service';
import { RoomServiceProvider } from '../../providers/room-service/room-service';

import { RoomsPage } from '../rooms/rooms';


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
    this.navCtrl.push(RoomsPage);
  }

  // start(){
  //   this.locationTracker.startForegroundTracking();
  // }

  // stop(){
  //   this.locationTracker.stopForegroundTracking();
  // }


}
