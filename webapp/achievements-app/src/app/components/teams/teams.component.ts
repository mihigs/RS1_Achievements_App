import { Component, OnInit } from '@angular/core';
import {Router} from '@angular/router';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { CreateTeamModalComponent } from './create-team-modal/create-team-modal.component';

@Component({
  selector: 'app-teams',
  templateUrl: './teams.component.html',
  styleUrls: ['./teams.component.css']
})
export class TeamsComponent implements OnInit {

  constructor(private router: Router, public dialog: MatDialog) { }

  ngOnInit(): void {
  }

  goBack(): void {
    this.router.navigate([".."]);
  }

  openCreateTeamModal(): void {
    const dialogRef = this.dialog.open(CreateTeamModalComponent, {
      // width: '250px',
      data: {name: "Create team"},
    });

    dialogRef.afterClosed().subscribe(result => {
    });
  }

}
