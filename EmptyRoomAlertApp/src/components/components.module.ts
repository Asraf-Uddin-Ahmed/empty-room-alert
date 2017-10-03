import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { NetworkStatusComponent } from './network-status/network-status';
@NgModule({
	declarations: [NetworkStatusComponent],
	imports: [BrowserModule],
	exports: [NetworkStatusComponent]
})
export class ComponentsModule {}
