import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';

import { GoogleMapServiceProvider } from '../../providers/google-map-service/google-map-service';
import { RemoteServiceProvider } from '../../providers/remote-service/remote-service';

/*
  Generated class for the RoomServiceProvider provider.

  See https://angular.io/guide/dependency-injection for more info on providers
  and Angular DI.
*/
@Injectable()
export class RoomServiceProvider {

  radiusInMeter = 100;
  isAnyRoomInRadius = false;
  minRoomDistance = null;

  constructor(
    private remoteService: RemoteServiceProvider,
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
        console.log(room.name, distance);
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
  
}
