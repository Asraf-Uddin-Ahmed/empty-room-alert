import { BrowserModule } from '@angular/platform-browser';
import { NgModule, ErrorHandler } from '@angular/core';
import { HttpModule } from '@angular/http';

import { IonicApp, IonicModule, IonicErrorHandler } from 'ionic-angular';
import { MyApp } from './app.component';

import { HomePage } from '../pages/home/home';
import { RoomDetailsPage } from '../pages/room-details/room-details';
import { RoomsPage } from '../pages/rooms/rooms';
import { SettingsPage } from '../pages/settings/settings';

import { StatusBar } from '@ionic-native/status-bar';
import { SplashScreen } from '@ionic-native/splash-screen';
import { BackgroundMode } from '@ionic-native/background-mode';
import { LocalNotifications } from '@ionic-native/local-notifications';
import { AppMinimize } from '@ionic-native/app-minimize';
import { Geolocation } from '@ionic-native/geolocation';
import { Network } from '@ionic-native/network';
import { BackgroundGeolocation } from '@ionic-native/background-geolocation';

import { ComponentsModule } from '../components/components.module';

import { RemoteServiceProvider } from '../providers/remote-service/remote-service';
import { LocationTrackerProvider } from '../providers/location-tracker/location-tracker';
import { GoogleMapServiceProvider } from '../providers/google-map-service/google-map-service';
import { NetworkServiceProvider } from '../providers/network-service/network-service';
import { RoomServiceProvider } from '../providers/room-service/room-service';

@NgModule({
  declarations: [
    MyApp,
    HomePage,
    RoomDetailsPage,
    RoomsPage,
    SettingsPage
  ],
  imports: [
    BrowserModule,
    IonicModule.forRoot(MyApp),
    HttpModule,
    ComponentsModule
  ],
  bootstrap: [IonicApp],
  entryComponents: [
    MyApp,
    HomePage,
    RoomDetailsPage,
    RoomsPage,
    SettingsPage
  ],
  providers: [
    StatusBar,
    SplashScreen,
    {provide: ErrorHandler, useClass: IonicErrorHandler},
    BackgroundMode,
    LocalNotifications,
    Geolocation,
    BackgroundGeolocation,
    AppMinimize,
    Network,
    RemoteServiceProvider,
    LocationTrackerProvider,
    GoogleMapServiceProvider,
    NetworkServiceProvider,
    RoomServiceProvider
  ]
})
export class AppModule {}
