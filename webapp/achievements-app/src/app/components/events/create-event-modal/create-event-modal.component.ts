import { Component, OnInit, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { ApiService } from 'src/app/services/api.service';
import { CreateEventRequest } from '../../../interfaces/Events/CreateEventRequest';

export interface DialogData {
  name: string;
}

@Component({
  selector: 'app-create-event-modal',
  templateUrl: './create-event-modal.component.html',
  styleUrls: ['./create-event-modal.component.css']
})
export class CreateEventModalComponent implements OnInit {
  createEventRequest: CreateEventRequest;
  createEventForm: FormGroup;

  constructor(private apiService: ApiService, private formBuilder: FormBuilder, public dialogRef: MatDialogRef<CreateEventModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData,) { 
      this.createNewEventForm();
    }

  ngOnInit(): void {
  }

  createNewEventForm() {
    this.createEventForm = this.formBuilder.group({
        name: ['', Validators.required],
        description: ['', Validators.required]
    });
}


  sendCreateEventRequest() {
    if(this.createEventForm.invalid){
      return;
    }

    this.createEventRequest = Object.assign({}, this.createEventForm.value);
    debugger;
    this.apiService.post("/Event/create-event", this.createEventRequest).subscribe();
    this.dialogRef.close();
  }
}
