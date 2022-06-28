import { Component, OnInit, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { CreateAchievementRequest } from 'src/app/interfaces/Achievements/CreateAchievementRequest';
import { OrganizedEvent } from 'src/app/interfaces/Events/OrganizedEvent';
import { Team } from 'src/app/interfaces/Teams/Team';
import { AchievementsService } from 'src/app/services/achievements.service';
import { ApiService } from 'src/app/services/api.service';
import { EventsService } from 'src/app/services/events.service';
import { TeamsService } from 'src/app/services/teams.service';
import { AchievementTiers } from 'src/app/shared/enums/achievementTiers';

export interface DialogData {
  name: string;
}

@Component({
  selector: 'app-create-achievement-modal',
  templateUrl: './create-achievement-modal.component.html',
  styleUrls: ['./create-achievement-modal.component.css']
})
export class CreateAchievementModalComponent implements OnInit {
  createAchievementRequest: CreateAchievementRequest;
  createAchievementForm: FormGroup;
  availableTeams: Team[];
  availableEvents: OrganizedEvent[];
  achievementTiers: ['1', '2', '3'];

  constructor(private achievementsService: AchievementsService, private eventsService: EventsService, private teamsService: TeamsService, private formBuilder: FormBuilder, public dialogRef: MatDialogRef<CreateAchievementModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData,) { 
      this.createNewAchievementForm();
    }

  ngOnInit(): void {
    this.getTeams();
  }

  createNewAchievementForm() {
    this.createAchievementForm = this.formBuilder.group({
        name: ['', Validators.required],
        description: ['', Validators.required],
        tier: ['', Validators.required],
        event: [null],
        team: [null],
        iconUrl: ['']
    });
}


  sendCreateAchievementRequest() {
    if(this.createAchievementForm.invalid){
      return;
    }
    this.createAchievementRequest = Object.assign({}, this.createAchievementForm.value);
    this.achievementsService.create(this.createAchievementRequest).subscribe((resp: any) => {
      this.dialogRef.close();
    });
    // this.apiService.post("/Achievement/create-achievement", this.createAchievementRequest as CreateAchievementRequest).subscribe();
  }

  getTeams() {
    this.teamsService.getAll().subscribe((resp: any) => {
      this.availableTeams = resp.result;  
    })
  }

  getEvents() {
    this.eventsService.getAll().subscribe((resp: any) => {
      this.availableEvents = resp.result;  
    })
  }
}
