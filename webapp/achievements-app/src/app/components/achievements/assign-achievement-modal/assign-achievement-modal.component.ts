import { Component, OnInit, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { AssignAchievementRequest } from 'src/app/interfaces/Achievements/AssignAchievementRequest';
import { User } from 'src/app/interfaces/User/User';
import { AchievementsService } from 'src/app/services/achievements.service';
import { UsersService } from 'src/app/services/users.service';

export interface DialogData {
  name: string;
}

@Component({
  selector: 'app-assign-achievement-modal',
  templateUrl: './assign-achievement-modal.component.html',
  styleUrls: ['./assign-achievement-modal.component.css']
})
export class AssignAchievementModalComponent implements OnInit {
  assignAchievementRequest: AssignAchievementRequest;
  assignAchievementForm: FormGroup;
  availableUsers: User[];
  selectedUser: string;
  selectedAchievement: number;

  constructor(private router: Router, private usersService: UsersService, private achievementsService: AchievementsService, private formBuilder: FormBuilder, public dialogRef: MatDialogRef<AssignAchievementModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData,) { 
      this.assignNewAchievementForm();
    }

  ngOnInit(): void {
    this.getUsers();
    this.selectedAchievement = +this.router.url.split('/').pop();
  }

  assignNewAchievementForm() {
    this.assignAchievementForm = this.formBuilder.group({
        name: ['', Validators.required],
        description: ['', Validators.required],
        tier: ['', Validators.required],
        eventId: [null],
        teamId: [''],
        iconUrl: ['']
    });
}


  sendAssignAchievementRequest() {
    if(this.selectedUser == null || this.selectedUser == ""){
      return;
    }
    this.assignAchievementRequest = Object.assign({}, {
      userId: this.selectedUser,
      achievementId: this.selectedAchievement
    });
    this.achievementsService.assign(this.assignAchievementRequest).subscribe((resp: any) => {
      this.dialogRef.close();
    });
    // this.apiService.post("/Achievement/assign-achievement", this.assignAchievementRequest as AssignAchievementRequest).subscribe();
  }

  getUsers(){
    this.usersService.getAll().subscribe((resp: any) => {
      this.availableUsers = resp.result;
    })
  }

  selectUser(userId: string){
    this.selectedUser = userId;
  }
}
