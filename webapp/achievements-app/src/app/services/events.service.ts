import {Injectable} from '@angular/core';
import {Router} from '@angular/router';
import { Observable } from 'rxjs/internal/Observable';
import { OrganizedEvent } from '../interfaces/Events/OrganizedEvent';
import { CreateEventRequest } from '../interfaces/Events/CreateEventRequest';
import {ApiService} from './api.service';

@Injectable({
    providedIn: 'root'
})
export class EventsService {

    private readonly GET_ALL: string = '/Event/list';
    private readonly GET_BY_ID: string = '/Event';
    private readonly CREATE_EVENT: string = '/Event/create-event';

    constructor(private apiService: ApiService, private router: Router) {
    }


    getAll(): Observable<OrganizedEvent[]> {
        return this.apiService.get(this.GET_ALL);
    }

    create(body: CreateEventRequest): Observable<any> {
        return this.apiService.post(this.CREATE_EVENT, body);
    }

}
