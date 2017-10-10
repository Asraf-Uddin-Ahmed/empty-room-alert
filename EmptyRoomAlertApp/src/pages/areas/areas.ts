import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams, AlertController } from 'ionic-angular';

import { RemoteServiceProvider } from '../../providers/remote-service/remote-service';

import { RoomsPage } from '../rooms/rooms';

/**
 * Generated class for the AreasPage page.
 *
 * See https://ionicframework.com/docs/components/#navigation for more info on
 * Ionic pages and navigation.
 */

@Component({
  selector: 'page-areas',
  templateUrl: 'areas.html',
})
export class AreasPage {

  items: Array<any>;
  
  constructor(
    public navCtrl: NavController, 
    private remoteService: RemoteServiceProvider,
    private alertCtrl: AlertController,
    public navParams: NavParams
  ) {
    this.getAreas();
  }

  ionViewDidLoad() {
    console.log('ionViewDidLoad AreasPage');
  }

  getAreas() {
    this.remoteService.get("areas").subscribe(areas => {
      this.items = areas;
      console.log(areas);
    }, err => {
      console.log("Failed to getRooms -> ", err);
      let alert = this.alertCtrl.create({
        title: 'Failed to getAreas',
        subTitle: err,
        buttons: ['OK']
      });
      alert.present();
    });
  }
  itemTapped(event, area) {
    this.navCtrl.push(RoomsPage, {
      area: area
    });
  }
}
