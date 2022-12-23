import {Component, OnInit} from '@angular/core';
import {AuthenticationService} from '../../services/authentication.service';
import {Router} from '@angular/router';
import {TranslateService} from '@ngx-translate/core';

export interface Tile {
    color: string;
    cols: number;
    rows: number;
    name: string;
    path: string;
}

@Component({
    selector: 'app-dashboard',
    templateUrl: './dashboard.component.html',
    styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
    language_selected: String = this.translateService.currentLang;
    constructor(private authenticationService: AuthenticationService, private router: Router, private translateService: TranslateService) {}

    ngOnInit(): void {}

    tiles: Tile[] = [
        {cols: 2, rows: 3, color: '#F46036', name: "Achievements", path: "achievements"},
        {cols: 2, rows: 3, color: '#1B998B', name: "Teams", path: "teams"},
        {cols: 2, rows: 3, color: '#E71D36', name: "Events", path: "events"},
        {cols: 2, rows: 3, color: '#2E294E', name: "Members", path: "members"},
    ];

    logout() {
        this.authenticationService.logout();
    }

    navigateTo(path: string) {
        this.router.navigate([path]);
    }

    switchLanguage(lang: string){
        this.translateService.use(lang);
    }
}
