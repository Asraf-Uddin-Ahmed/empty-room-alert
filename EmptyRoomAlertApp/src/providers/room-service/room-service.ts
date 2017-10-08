import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';
import { LocalNotifications } from '@ionic-native/local-notifications';

import { GoogleMapServiceProvider } from '../../providers/google-map-service/google-map-service';
import { RemoteServiceProvider } from '../../providers/remote-service/remote-service';

/*
  Generated class for the RoomServiceProvider provider.

  See https://angular.io/guide/dependency-injection for more info on providers
  and Angular DI.
*/
@Injectable()
export class RoomServiceProvider {

  radiusInMeter = 3000;
  isAnyRoomInRadius = false;
  minRoomDistance = null;

  constructor(
    private remoteService: RemoteServiceProvider,
    private localNotifications: LocalNotifications,
    private googleMapServiceProvider: GoogleMapServiceProvider
  ) {
    // console.log('Hello RoomServiceProvider Provider');
  }

  findAnyRoomIsInRadius(lat, lng) {
    this.minRoomDistance = Infinity;
    this.isAnyRoomInRadius = false;
    this.remoteService.get("rooms").subscribe(rooms => {
      for (let room of rooms) {
        let roomLatLng = room.address.split(",");
        let distance = this.googleMapServiceProvider.getDistanceFromLatLonInMeter(lat, lng, roomLatLng[0], roomLatLng[1]);
        // console.log(room.name, distance);
        this.minRoomDistance = this.minRoomDistance > distance ? distance : this.minRoomDistance;
        if(distance <= this.radiusInMeter){
          this.isAnyRoomInRadius = true;
          break;
        }
      }
    }, err => {
      console.log("Failed to getRooms -> ", err);
    });
  }
  
  checkRoomState(intervalInMiliSecond: number) {
    let start = new Date();
    let end = new Date();
    end.setMilliseconds(end.getMilliseconds() + intervalInMiliSecond);

    let endPoint = "room-states?searchItem.LogTimeFrom=" + start.toLocaleString()
      + "&searchItem.LogTimeTo=" + end.toLocaleString()
      + "&sortBy.fieldName=LogTime&sortBy.isAscending=true&pagination.displayStart=0&pagination.displaySize=100";

    this.remoteService.get(endPoint).subscribe(data => {
      let arrRoomStateWithRoom = data.items;
      console.log(arrRoomStateWithRoom);
      this.notifyLocally(arrRoomStateWithRoom)      
    }, err => {
      console.log("Oops!");
    });
  }

  private notifyLocally(arrRoomStateWithRoom){
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
    this.localNotifications.schedule(arrNotification);
  }
}
