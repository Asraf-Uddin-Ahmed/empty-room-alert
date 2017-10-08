import { Component, ViewChild } from '@angular/core';

import { Platform, MenuController, Nav } from 'ionic-angular';

import { HomePage } from '../pages/home/home';
import { RoomsPage } from '../pages/rooms/rooms';
import { SettingsPage } from '../pages/settings/settings';

import { StatusBar } from '@ionic-native/status-bar';
import { SplashScreen } from '@ionic-native/splash-screen';
import { BackgroundMode } from '@ionic-native/background-mode';
import { LocalNotifications } from '@ionic-native/local-notifications';
import { AppMinimize } from '@ionic-native/app-minimize';

import { RemoteServiceProvider } from '../providers/remote-service/remote-service';
import { LocationTrackerProvider } from '../providers/location-tracker/location-tracker';
import { NetworkServiceProvider } from '../providers/network-service/network-service';
import { RoomServiceProvider } from '../providers/room-service/room-service';


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
    private roomServiceProvider: RoomServiceProvider,
    private networkServiceProvider: NetworkServiceProvider
  ) {
    this.initializeApp();

    // set our app's pages
    this.pages = [
      { title: 'Home', component: HomePage },
      // { title: 'Spots', component: RoomsPage },
      { title: 'Settings', component: SettingsPage }
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

    this.roomServiceProvider.notifyIfRoomStateChange(intervalInMiliSecond);
    setInterval(() => {
      console.log("foreground process");
      this.roomServiceProvider.notifyIfRoomStateChange(intervalInMiliSecond);
    }, intervalInMiliSecond);
    
    // this.backgroundMode.enable();
    // this.backgroundMode.overrideBackButton();
    // this.backgroundMode.on("activate").subscribe(() => {
    //   console.log("activating background process");
    //   setInterval(function () {
    //     console.log("background process");
    //     // fnNotify(intervalInMiliSecond, rmtService, lclNotifications);
    //   }, intervalInMiliSecond);
    // });
  }

  

  openPage(page) {
    // close the menu when clicking a link from the menu
    this.menu.close();
    // navigate to the new page if it is not the current page
    this.nav.setRoot(page.component);
  }
}
