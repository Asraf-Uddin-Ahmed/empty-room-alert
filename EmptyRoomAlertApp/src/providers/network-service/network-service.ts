import { Injectable } from '@angular/core';
import { Network } from '@ionic-native/network';
import { AlertController } from 'ionic-angular';
import 'rxjs/add/operator/map';

/*
  Generated class for the NetworkServiceProvider provider.

  See https://angular.io/guide/dependency-injection for more info on providers
  and Angular DI.
*/
@Injectable()
export class NetworkServiceProvider {

  constructor(private network: Network,
    private alertCtrl: AlertController,
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

      const alert = this.alertCtrl.create({
        title: 'Disconnected',
        subTitle: 'Network not found',
        message: 'Please check your network connection.',
        enableBackdropDismiss: false,
        buttons: ['OK']
      });
      alert.present();
    });

    // watch network for a connection
    let connectSubscription = this.network.onConnect().subscribe(() => {
      console.log('network connected :)');
      // We just got a connection but we need to wait briefly
      // before we determine the connection type. Might need to wait.
      // prior to doing any api requests as well.
      console.log(this.network);

      const alert = this.alertCtrl.create({
        title: 'Connected',
        subTitle: 'Network found',
        message: 'You can use app now.',
        enableBackdropDismiss: false,
        buttons: [{
          text: 'OK',
          role: 'cancel',
          handler: data => {
            location.reload();
          }
        }]
      });
      alert.present();
    });
  }
}
