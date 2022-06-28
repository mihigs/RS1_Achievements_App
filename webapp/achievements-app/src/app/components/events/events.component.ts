// import { Component, OnInit } from '@angular/core';

// @Component({
//   selector: 'app-events',
//   templateUrl: './events.component.html',
//   styleUrls: ['./events.component.css']
// })
// export class EventsComponent implements OnInit {

//   constructor() { }

//   ngOnInit(): void {
//   }

// }

import { Component, OnInit } from '@angular/core';
import {Router} from '@angular/router';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { OrganizedEvent } from 'src/app/interfaces/Events/OrganizedEvent';
import { ApiService } from 'src/app/services/api.service';
import { ActivatedRoute } from '@angular/router';
import { CreateEventModalComponent } from './create-event-modal/create-event-modal.component';

@Component({
  selector: 'app-events',
  templateUrl: './events.component.html',
  styleUrls: ['./events.component.css']
})
export class EventsComponent implements OnInit {

  constructor(private router: Router, private route: ActivatedRoute, public dialog: MatDialog, private apiService: ApiService) { }

  ngOnInit(): void {
    this.getEvents();
  }

  events: OrganizedEvent[];
  selectedEventId: string;

  goBack(): void {
    this.router.navigate([".."]);
  }

  goEventDetails(eventId: string): void {
    this.router.navigate(["events", eventId]);
  }

  openCreateEventModal(): void {
    const dialogRef = this.dialog.open(CreateEventModalComponent, {
      // width: '250px',
      data: {name: "Create event"},
    });

    dialogRef.afterClosed().subscribe(result => {
    });
  }

  getEvents(): void{
    this.apiService.get("/Event/list").subscribe((resp: any) => {
      this.events = resp.result;
    })
  }

}
