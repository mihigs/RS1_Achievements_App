import {Component} from '@angular/core';
import {TranslateService} from '@ngx-translate/core';
import { SignalrService } from './services/signalr.service';
import { HttpClient } from '@angular/common/http';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent {

    constructor(private translateService: TranslateService, public signalRService: SignalrService, private http: HttpClient) {
        translateService.addLangs(['ba', 'en']);
        const browserLanguage = translateService.getBrowserLang();
        translateService.use(browserLanguage.match(/en|ba/) ? browserLanguage : 'ba');
    }

    ngOnInit() {
        this.signalRService.startConnection();
        this.signalRService.addTransferChartDataListener();   
        // this.startHttpRequest();
      }
      
    //   private startHttpRequest = () => {
    //     this.http.get('https://localhost:5001/notifier')
    //       .subscribe(res => {
    //         console.log(res);
    //       })
    // }
}
