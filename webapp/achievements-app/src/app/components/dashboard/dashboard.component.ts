import {Component, OnInit} from '@angular/core';
import {AuthenticationService} from '../../services/authentication.service';

export interface Tile {
    color: string;
    cols: number;
    rows: number;
    name: string;
    description: string;
}

@Component({
    selector: 'app-dashboard',
    templateUrl: './dashboard.component.html',
    styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

    constructor(private authenticationService: AuthenticationService) {}

    ngOnInit(): void {}

    tiles: Tile[] = [
        {cols: 2, rows: 3, color: '#F46036', name: "Achievements", description: "Lorem ipsumn"},
        {cols: 2, rows: 3, color: '#1B998B', name: "Teams", description: "Lorem ipsumn"},
        {cols: 2, rows: 3, color: '#E71D36', name: "Events", description: "Lorem ipsumn"},
        {cols: 2, rows: 3, color: '#2E294E', name: "Members", description: "Lorem ipsumn"},
    ];

    logout() {
        this.authenticationService.logout();
    }

}
