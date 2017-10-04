import { Component, ViewChild, ElementRef } from '@angular/core';

import { NavController, NavParams, AlertController } from 'ionic-angular';

import { RemoteServiceProvider } from '../../providers/remote-service/remote-service';
import { GoogleMapServiceProvider } from '../../providers/google-map-service/google-map-service';
import { LocationTrackerProvider } from '../../providers/location-tracker/location-tracker';

@Component({
  selector: 'page-room-details',
  templateUrl: 'room-details.html'
})
export class RoomDetailsPage {

  @ViewChild('mapdetail') mapElement: ElementRef;

  selectedRoom: any;
  roomStateWithRoom: any;
  isRequestCompleted = false;

  constructor(public navCtrl: NavController,
    public navParams: NavParams,
    private remoteService: RemoteServiceProvider,
    private alertCtrl: AlertController,
    private locationTracker: LocationTrackerProvider,
    private googleMapServiceProvider: GoogleMapServiceProvider
  ) {
    // If we navigated to this page, we will have an item available as a nav param
    this.selectedRoom = navParams.get('room');
    this.getRoomDetail();
  }
  ionViewDidLoad() {
    this.googleMapServiceProvider.loadDirectionalMap(this.mapElement, this.selectedRoom.address);
  }


  getRoomDetail() {
    let endPoint = "room-states?searchItem.roomID=" + this.selectedRoom.id
      + "&searchItem.LogTimeFrom=" + new Date().toLocaleString()
      + "&sortBy.fieldName=LogTime&sortBy.isAscending=true&pagination.displayStart=0&pagination.displaySize=3";

    this.remoteService.get(endPoint).subscribe(data => {
      this.roomStateWithRoom = data.items.length ? data.items[0] : null;
      console.log(data, this.roomStateWithRoom);
      this.isRequestCompleted = true;
    }, err => {
      this.isRequestCompleted = true;
      console.log("Failed to getRoomDetail", err);

      let alert = this.alertCtrl.create({
        title: 'Failed to getRoomDetail',
        subTitle: err,
        buttons: ['OK']
      });
      alert.present();
    });
  }
}
