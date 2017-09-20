import { Component } from '@angular/core';
import { NavController } from 'ionic-angular';

import { BackgroundMode } from '@ionic-native/background-mode';
import { LocalNotifications } from '@ionic-native/local-notifications';

import { RemoteServiceProvider } from '../../providers/remote-service/remote-service';
import { RoomsPage } from '../rooms/rooms';

@Component({
  selector: 'page-home',
  templateUrl: 'home.html'
})
export class HomePage {
  constructor(private navCtrl: NavController,
    private remoteService: RemoteServiceProvider,
    private backgroundMode: BackgroundMode,
    private localNotifications: LocalNotifications) {

    this.runInBackground();

  }

  private runInBackground() {
    let intervalInMiliSecond = 30000;
    let rmtService = this.remoteService;
    let lclNotifications = this.localNotifications;
    let fnNotify = this.notifyRoomStateChange;

    this.backgroundMode.enable();
    this.backgroundMode.on("activate").subscribe(() => {
      console.log("activating background process");
      setInterval(function () {
        console.log("background process");
        fnNotify(intervalInMiliSecond, rmtService, lclNotifications);
      }, intervalInMiliSecond);
    });

    console.log("foreground process");
    fnNotify(intervalInMiliSecond, rmtService, lclNotifications);
  }

  private notifyRoomStateChange(intervalInMiliSecond: number, rmtService: RemoteServiceProvider, lclNotifications: LocalNotifications) {
    let start = new Date();
    let end = new Date();
    end.setMilliseconds(end.getMilliseconds() + intervalInMiliSecond);

    let endPoint = "room-states?searchItem.LogTimeFrom=" + start.toLocaleString()
      + "&searchItem.LogTimeTo=" + end.toLocaleString()
      + "&sortBy.fieldName=LogTime&sortBy.isAscending=true&pagination.displayStart=0&pagination.displaySize=100";

    rmtService.get(endPoint).subscribe(data => {
      let arrRoomStateWithRoom = data.items;
      console.log(arrRoomStateWithRoom);

      let arrNotification = [];
      for (let I = 0; I < arrRoomStateWithRoom.length; I++) {
        arrNotification.push({
          id: I,
          title: arrRoomStateWithRoom[I].room.name + ' has been ' + (arrRoomStateWithRoom[I].isEmpty ? 'empty' : 'booked') + ' now',
          text: arrRoomStateWithRoom[I].room.address,
          data: arrRoomStateWithRoom[I].room.address
        });
      }
      console.log(arrNotification);
      lclNotifications.schedule(arrNotification);
    }, err => {
      console.log("Oops!");
    });
  }

  enterRoom(event) {
    this.navCtrl.push(RoomsPage);
  }
}
