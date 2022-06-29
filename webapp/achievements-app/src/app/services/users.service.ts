import {Injectable} from '@angular/core';
import {Router} from '@angular/router';
import { Observable } from 'rxjs/internal/Observable';
import { User } from '../interfaces/User/User';
import {ApiService} from './api.service';

@Injectable({
    providedIn: 'root'
})
export class UsersService {

    private readonly GET_ALL: string = '/User/list';

    constructor(private apiService: ApiService, private router: Router) {
    }


    getAll(): Observable<User[]> {
        return this.apiService.get(this.GET_ALL);
    }
}
