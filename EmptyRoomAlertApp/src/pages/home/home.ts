import { Component, ViewChild, ElementRef } from '@angular/core';
import { NavController } from 'ionic-angular';

import { LocationTrackerProvider } from '../../providers/location-tracker/location-tracker';
import { GoogleMapServiceProvider } from '../../providers/google-map-service/google-map-service';

import { RoomsPage } from '../rooms/rooms';


@Component({
  selector: 'page-home',
  templateUrl: 'home.html'
})
export class HomePage {

  @ViewChild('map') mapElement: ElementRef;
 
  constructor(private navCtrl: NavController, 
    private locationTracker: LocationTrackerProvider,
    private googleMapServiceProvider: GoogleMapServiceProvider
  ) {
    
  }
  ionViewDidLoad() {
    this.googleMapServiceProvider.loadDirectionalMap(this.mapElement, "23.8529789,90.3828734");
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
