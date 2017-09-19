import { Component } from '@angular/core';

import { NavController, NavParams } from 'ionic-angular';

import { RemoteServiceProvider } from '../../providers/remote-service/remote-service';

@Component({
  selector: 'page-room-details',
  templateUrl: 'room-details.html'
})
export class RoomDetailsPage {
  selectedRoom: any;
  roomStateWithRoom: any;
  isRequestCompleted = false;

  constructor(public navCtrl: NavController, public navParams: NavParams, private remoteService: RemoteServiceProvider) {
    // If we navigated to this page, we will have an item available as a nav param
    this.selectedRoom = navParams.get('room');
    this.getRoomDetail();
  }

  getRoomDetail() {
    let endPoint = "room-states?searchItem.roomID=" + this.selectedRoom.id
      + "&searchItem.LogTimeFrom=" + new Date().toLocaleString()
      + "&sortBy.fieldName=LogTime&sortBy.isAscending=true&pagination.displayStart=0&pagination.displaySize=3";
    this.remoteService.get(endPoint).subscribe(data => {
      this.roomStateWithRoom = data.items[0];
      console.log(data, this.roomStateWithRoom);
      this.isRequestCompleted = true;
    }, err => {
      console.log("Oops!");
      this.isRequestCompleted = true;
    });
  }
}
