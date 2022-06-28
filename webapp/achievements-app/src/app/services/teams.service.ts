import {Injectable} from '@angular/core';
import {Router} from '@angular/router';
import { Observable } from 'rxjs/internal/Observable';
import { Team } from '../interfaces/Teams/Team';
import { CreateTeamRequest } from '../interfaces/Teams/CreateTeamRequest';
import {ApiService} from './api.service';

@Injectable({
    providedIn: 'root'
})
export class TeamsService {

    private readonly GET_ALL: string = '/Team/list';
    private readonly GET_BY_ID: string = '/Team';
    private readonly CREATE_TEAM: string = '/Team/create-team';

    constructor(private apiService: ApiService, private router: Router) {
    }


    getAll(): Observable<Team[]> {
        return this.apiService.get(this.GET_ALL);
    }

    create(body: CreateTeamRequest): Observable<any> {
        return this.apiService.post(this.CREATE_TEAM, body);
    }

}
