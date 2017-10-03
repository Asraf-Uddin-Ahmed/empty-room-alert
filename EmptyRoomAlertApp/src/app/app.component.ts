import { Component, ViewChild } from '@angular/core';

import { Platform, MenuController, Nav } from 'ionic-angular';

import { HomePage } from '../pages/home/home';
import { RoomsPage } from '../pages/rooms/rooms';

import { StatusBar } from '@ionic-native/status-bar';
import { SplashScreen } from '@ionic-native/splash-screen';
import { BackgroundMode } from '@ionic-native/background-mode';
import { LocalNotifications } from '@ionic-native/local-notifications';
import { AppMinimize } from '@ionic-native/app-minimize';

import { RemoteServiceProvider } from '../providers/remote-service/remote-service';
import { LocationTrackerProvider } from '../providers/location-tracker/location-tracker';
import { NetworkServiceProvider } from '../providers/network-service/network-service';


@Component({
  templateUrl: 'app.html'
})
export class MyApp {
  @ViewChild(Nav) nav: Nav;

  // make HelloIonicPage the root (or first) page
  rootPage = HomePage;
  pages: Array<{title: string, component: any}>;

  constructor(
    public platform: Platform,
    public menu: MenuController,
    public statusBar: StatusBar,
    public splashScreen: SplashScreen,
    private remoteService: RemoteServiceProvider,
    private backgroundMode: BackgroundMode,
    private localNotifications: LocalNotifications,
    private appMinimize: AppMinimize,
    private locationTracker: LocationTrackerProvider,
    private networkServiceProvider: NetworkServiceProvider
  ) {
    this.initializeApp();

    // set our app's pages
    this.pages = [
      { title: 'Home', component: HomePage },
      { title: 'Spots', component: RoomsPage }
    ];
  }

  initializeApp() {
    this.platform.ready().then(() => {
      // Okay, so the platform is ready and our plugins are available.
      // Here you can do any higher level native things you might need.
      this.platform.registerBackButtonAction(() => {
        console.log("inside registerBackButtonAction");
        this.appMinimize.minimize();
      });
      this.pullNotificationData();
      
      this.networkServiceProvider.initializeConnectivityChecker();
      this.locationTracker.startForegroundTracking();
      
      this.statusBar.styleDefault();
      this.splashScreen.hide();
    });
  }

  
  private pullNotificationData() {
    let intervalInMiliSecond = 30000;
    let rmtService = this.remoteService;
    let lclNotifications = this.localNotifications;
    let fnNotify = this.notifyRoomStateChange;

    // this.backgroundMode.enable();
    // this.backgroundMode.overrideBackButton();
    // this.backgroundMode.on("activate").subscribe(() => {
    //   console.log("activating background process");
    //   setInterval(function () {
    //     console.log("background process");
    //     // fnNotify(intervalInMiliSecond, rmtService, lclNotifications);
    //   }, intervalInMiliSecond);
    // });

    setInterval(function () {
      console.log("foreground process");
      fnNotify(intervalInMiliSecond, rmtService, lclNotifications);
    }, intervalInMiliSecond);
    
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

  openPage(page) {
    // close the menu when clicking a link from the menu
    this.menu.close();
    // navigate to the new page if it is not the current page
    this.nav.setRoot(page.component);
  }
}
