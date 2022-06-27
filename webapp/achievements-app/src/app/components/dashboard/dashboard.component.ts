import {Component, OnInit} from '@angular/core';
import {AuthenticationService} from '../../services/authentication.service';
import {Router} from '@angular/router';

export interface Tile {
    color: string;
    cols: number;
    rows: number;
    name: string;
    description: string;
    path: string;
}

@Component({
    selector: 'app-dashboard',
    templateUrl: './dashboard.component.html',
    styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

    constructor(private authenticationService: AuthenticationService, private router: Router) {}

    ngOnInit(): void {}

    tiles: Tile[] = [
        {cols: 2, rows: 3, color: '#F46036', name: "Achievements", description: "Lorem ipsumn", path: "achievements"},
        {cols: 2, rows: 3, color: '#1B998B', name: "Teams", description: "Lorem ipsumn", path: "teams"},
        {cols: 2, rows: 3, color: '#E71D36', name: "Events", description: "Lorem ipsumn", path: "events"},
        {cols: 2, rows: 3, color: '#2E294E', name: "Members", description: "Lorem ipsumn", path: "members"},
    ];

    logout() {
        this.authenticationService.logout();
    }

    navigateTo(path: string) {
        this.router.navigate([path]);
    }
}
