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

  private readonly RADIUS_IN_METER = 100;
  isAnyRoomInRadius = false;


  constructor(
    private remoteService: RemoteServiceProvider,
    private googleMapServiceProvider: GoogleMapServiceProvider
  ) {
    // console.log('Hello RoomServiceProvider Provider');
  }

  findAnyRoomIsInRadius(lat, lng) {
    this.isAnyRoomInRadius = false;
    this.remoteService.get("rooms").subscribe(rooms => {
      for (let room of rooms) {
        let roomLatLng = room.address.split(",");
        let distance = this.googleMapServiceProvider.getDistanceFromLatLonInMeter(lat, lng, roomLatLng[0], roomLatLng[1]);
        console.log(room.name, distance);
        if(distance <= this.RADIUS_IN_METER){
          this.isAnyRoomInRadius = true;
          break;
        }
      }
    }, err => {
      console.log("Failed to getRooms -> ", err);
    });
  }
  
}
