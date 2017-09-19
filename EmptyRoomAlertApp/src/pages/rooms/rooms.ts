import { Component } from '@angular/core';

import { NavController, NavParams } from 'ionic-angular';

import { RemoteServiceProvider } from '../../providers/remote-service/remote-service';

import { RoomDetailsPage } from '../room-details/room-details';

@Component({
  selector: 'page-rooms',
  templateUrl: 'rooms.html'
})
export class RoomsPage {
  icons: string[];
  items: Array<{title: string, note: string, icon: string}>;

  constructor(public navCtrl: NavController, public navParams: NavParams, private remoteService : RemoteServiceProvider) {
    this.icons = ['star', 'person', 'book', 'car'];

    this.getRooms();
  }

  getRooms(){
    this.remoteService.get("rooms").subscribe(data => {
      this.items = data;
      console.log(data);
    }, err => {
      console.log("Oops!");
    });
}
  itemTapped(event, item) {
    this.navCtrl.push(RoomDetailsPage, {
      item: item
    });
  }
}
