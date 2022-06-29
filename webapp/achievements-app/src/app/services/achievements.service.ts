import {Injectable} from '@angular/core';
import {Router} from '@angular/router';
import { Observable } from 'rxjs/internal/Observable';
import { Achievement } from '../interfaces/Achievements/Achievement';
import { AssignAchievementRequest } from '../interfaces/Achievements/AssignAchievementRequest';
import { CreateAchievementRequest } from '../interfaces/Achievements/CreateAchievementRequest';
import {ApiService} from './api.service';

@Injectable({
    providedIn: 'root'
})
export class AchievementsService {

    private readonly GET_ALL: string = '/Achievement/list';
    private readonly GET_BY_ID: string = '/Achievement';
    private readonly CREATE_ACHIEVEMENT: string = '/Achievement/create-achievement';
    private readonly REMOVE_ACHIEVEMENT: string = '/Achievement/';
    private readonly ASSIGN_ACHIEVEMENT: string = '/Achievement/assign-achievement';

    constructor(private apiService: ApiService, private router: Router) {
    }


    getAll(): Observable<Achievement[]> {
        return this.apiService.get(this.GET_ALL);
    }

    create(body: CreateAchievementRequest): Observable<any> {
        return this.apiService.post(this.CREATE_ACHIEVEMENT, body);
    }

    remove(achievementId: any): Observable<any> {
        return this.apiService.delete(this.REMOVE_ACHIEVEMENT + achievementId);
    }

    assign(body: AssignAchievementRequest): Observable<any> {
        return this.apiService.post(this.ASSIGN_ACHIEVEMENT, body);
    }

}
