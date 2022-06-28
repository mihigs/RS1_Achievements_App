import {Injectable} from '@angular/core';
import {Router} from '@angular/router';
import { Observable } from 'rxjs/internal/Observable';
import { Achievement } from '../interfaces/Teams/Achievement';
import {ApiService} from './api.service';

@Injectable({
    providedIn: 'root'
})
export class AchievementsService {

    private readonly GET_ALL: string = '/Achievement/list';
    private readonly GET_BY_ID: string = '/Achievement';
    private readonly CREATE_ACHIEVEMENT: string = '/Achievement/create-achievement';

    constructor(private apiService: ApiService, private router: Router) {
    }


    getAll(): Observable<Achievement[]> {
        return this.apiService.get(this.GET_ALL);
    }

}
