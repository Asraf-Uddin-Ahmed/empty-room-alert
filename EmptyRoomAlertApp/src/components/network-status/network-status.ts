import { Component } from '@angular/core';
import { NetworkServiceProvider } from '../../providers/network-service/network-service';

/**
 * Generated class for the NetworkStatusComponent component.
 *
 * See https://angular.io/api/core/Component for more info on Angular
 * Components.
 */
@Component({
  selector: 'network-status',
  templateUrl: 'network-status.html'
})
export class NetworkStatusComponent {

  constructor(private networkServiceProvider: NetworkServiceProvider) {
    // console.log('Hello NetworkStatusComponent Component');
  }

}
