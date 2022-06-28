import { Component, OnInit, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { ApiService } from 'src/app/services/api.service';
import { CreateTeamRequest } from '../../../interfaces/Teams/CreateTeamRequest';

export interface DialogData {
  name: string;
}

@Component({
  selector: 'app-create-team-modal',
  templateUrl: './create-team-modal.component.html',
  styleUrls: ['./create-team-modal.component.css']
})
export class CreateTeamModalComponent implements OnInit {
  createTeamRequest: CreateTeamRequest;
  createTeamForm: FormGroup;

  constructor(private apiService: ApiService, private formBuilder: FormBuilder, public dialogRef: MatDialogRef<CreateTeamModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData,) { 
      this.createNewTeamForm();
    }

  ngOnInit(): void {
  }

  createNewTeamForm() {
    this.createTeamForm = this.formBuilder.group({
        name: ['', Validators.required],
        description: ['', Validators.required]
    });
}


  sendCreateTeamRequest() {
    if(this.createTeamForm.invalid){
      return;
    }

    this.createTeamRequest = Object.assign({}, this.createTeamForm.value);
    this.apiService.post("/Team/create-team", this.createTeamRequest).subscribe();
    this.dialogRef.close();
  }
}
