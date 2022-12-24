import { Injectable } from '@angular/core';
import * as signalR from "@microsoft/signalr"
import { environment } from 'src/environments/environment';
import { NotifierService } from 'angular-notifier';

@Injectable({
  providedIn: 'root'
})
export class SignalrService {
  private readonly notifier: NotifierService;

  constructor(notifierService: NotifierService) {
    this.notifier = notifierService;
  }

  private hubConnection: signalR.HubConnection
    public startConnection = () => {
      this.hubConnection = new signalR.HubConnectionBuilder()
                              .withUrl(environment.webSocketUrl)
                              .build();
      this.hubConnection
        .start()
        .then(() => console.log('Connection started'))
        .catch(err => console.log('Error while starting connection: ' + err))
    }
    
    public addNotifierMessageListener = () => {
      this.hubConnection.on('ReceiveMessage', (data) => {
        console.log(data);
        this.notifier.notify('info', data);
      });
    }
}