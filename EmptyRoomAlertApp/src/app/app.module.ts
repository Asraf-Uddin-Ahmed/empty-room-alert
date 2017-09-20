import { BrowserModule } from '@angular/platform-browser';
import { NgModule, ErrorHandler } from '@angular/core';
import { HttpModule } from '@angular/http';

import { IonicApp, IonicModule, IonicErrorHandler } from 'ionic-angular';
import { MyApp } from './app.component';

import { HomePage } from '../pages/home/home';
import { RoomDetailsPage } from '../pages/room-details/room-details';
import { RoomsPage } from '../pages/rooms/rooms';

import { StatusBar } from '@ionic-native/status-bar';
import { SplashScreen } from '@ionic-native/splash-screen';
import { BackgroundMode } from '@ionic-native/background-mode';
import { LocalNotifications } from '@ionic-native/local-notifications';
import { AppMinimize } from '@ionic-native/app-minimize';

import { RemoteServiceProvider } from '../providers/remote-service/remote-service';

@NgModule({
  declarations: [
    MyApp,
    HomePage,
    RoomDetailsPage,
    RoomsPage
  ],
  imports: [
    BrowserModule,
    IonicModule.forRoot(MyApp),
    HttpModule
  ],
  bootstrap: [IonicApp],
  entryComponents: [
    MyApp,
    HomePage,
    RoomDetailsPage,
    RoomsPage
  ],
  providers: [
    StatusBar,
    SplashScreen,
    {provide: ErrorHandler, useClass: IonicErrorHandler},
    RemoteServiceProvider,
    BackgroundMode,
    LocalNotifications,
    AppMinimize
  ]
})
export class AppModule {}
