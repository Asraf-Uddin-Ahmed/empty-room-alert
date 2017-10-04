import { Injectable } from '@angular/core';
import { Network } from '@ionic-native/network';
import { ToastController } from 'ionic-angular';
import 'rxjs/add/operator/map';

import { GoogleMapServiceProvider } from '../../providers/google-map-service/google-map-service';

/*
  Generated class for the NetworkServiceProvider provider.

  See https://angular.io/guide/dependency-injection for more info on providers
  and Angular DI.
*/
@Injectable()
export class NetworkServiceProvider {

  constructor(private network: Network,
    private toastCtrl: ToastController,
    private googleMapServiceProvider: GoogleMapServiceProvider
  ) {
    // console.log('Hello NetworkServiceProvider Provider');
  }

  isOnline(): boolean {
    return navigator.onLine;
  }
  
  initializeConnectivityChecker() {
    // watch network for a disconnect
    let disconnectSubscription = this.network.onDisconnect().subscribe(() => {
      console.log('network was disconnected :(');
      this.toastCtrl.create({
        message: 'Disconnected! Please check your network connection.',
        duration: 5000
      }).present();
    });

    // watch network for a connection
    let connectSubscription = this.network.onConnect().subscribe(() => {
      console.log('network connected :)');
      // We just got a connection but we need to wait briefly
      // before we determine the connection type. Might need to wait.
      // prior to doing any api requests as well.
      console.log(this.network);

      this.toastCtrl.create({
        message: 'Connected! Network found.',
        duration: 5000
      }).present();

      if(this.googleMapServiceProvider.isMapLoaded()) {
        return;
      } else {
        location.reload();
      }
    });
  }
}
