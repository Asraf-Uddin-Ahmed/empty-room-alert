import { Component } from '@angular/core';

import { NavController, NavParams, AlertController } from 'ionic-angular';

import { RemoteServiceProvider } from '../../providers/remote-service/remote-service';

import { RoomDetailsPage } from '../room-details/room-details';

@Component({
  selector: 'page-rooms',
  templateUrl: 'rooms.html'
})
export class RoomsPage {

  selectedArea: any;
  icons: string[];
  items: Array<{ title: string, note: string, icon: string }>;
  timer = null;


  constructor(public navCtrl: NavController,
    public navParams: NavParams,
    private remoteService: RemoteServiceProvider,
    private alertCtrl: AlertController
  ) {
    this.selectedArea = navParams.get('area');
    this.icons = ['star', 'person', 'book', 'car'];

    this.getRooms();
    this.timer = setInterval(() => {
      this.getRooms();
    }, 30000);
  }
  ionViewWillLeave() {
    clearInterval(this.timer);
  }


  getRooms() {
    this.remoteService.get("areas/" + this.selectedArea.id + "/rooms").subscribe(rooms => {
      this.items = rooms;
      console.log(rooms);
      for (let room of rooms) {
        let endPoint = "room-states?searchItem.roomID=" + room['id']
          + "&searchItem.LogTimeFrom=" + new Date().toLocaleString()
          + "&sortBy.fieldName=LogTime&sortBy.isAscending=true&pagination.displayStart=0&pagination.displaySize=3";

        this.remoteService.get(endPoint).subscribe(data => {
          if (data.items.length) {
            room['roomState'] = data.items[0];
            room['color'] = room['roomState'].isEmpty ? "secondary" : "danger";
          }
        }, err => {
          console.log("Failed to getRoomDetail -> ", err);
        });
      }
    }, err => {
      console.log("Failed to getRooms -> ", err);
      let alert = this.alertCtrl.create({
        title: 'Failed to getRooms',
        subTitle: err,
        buttons: ['OK']
      });
      alert.present();
    });
  }
  itemTapped(event, room) {
    this.navCtrl.push(RoomDetailsPage, {
      room: room
    });
  }
}
