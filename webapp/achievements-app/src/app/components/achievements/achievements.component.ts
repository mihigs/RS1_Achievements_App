import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { Achievement } from 'src/app/interfaces/Achievements/Achievement';
import { AchievementsService } from 'src/app/services/achievements.service';
import { CreateAchievementModalComponent } from './create-achievement-modal/create-achievement-modal.component';

@Component({
  selector: 'app-achievements',
  templateUrl: './achievements.component.html',
  styleUrls: ['./achievements.component.css']
})
export class AchievementsComponent implements OnInit {
  achievementsData: Achievement[];
  
  constructor(private router: Router, private achievementsService: AchievementsService, public dialog: MatDialog) { }

  ngOnInit(): void {
    this.getAchievements();
  }

  goBack(): void {
    this.router.navigate([".."]);
  }

  goAchievementDetails(achievementId: string): void {
    this.router.navigate(["achievements", achievementId]);
  }

  openCreateAchievementModal(): void {
    const dialogRef = this.dialog.open(CreateAchievementModalComponent, {
      // width: '250px',
      data: {name: "New achievement"},
    });

    dialogRef.afterClosed().subscribe(result => {
      this.getAchievements();
    });
  }

  getAchievements() {
    this.achievementsService.getAll().subscribe((resp: any) => {
      this.achievementsData = resp.result;
    })
  }

}
