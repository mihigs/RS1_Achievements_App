import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { Achievement } from 'src/app/interfaces/Achievements/Achievement';
import { AchievementsService } from 'src/app/services/achievements.service';
import { ApiService } from 'src/app/services/api.service';
import { AssignAchievementModalComponent } from '../assign-achievement-modal/assign-achievement-modal.component';

@Component({
  selector: 'app-achievement-details',
  templateUrl: './achievement-details.component.html',
  styleUrls: ['./achievement-details.component.css']
})
export class AchievementDetailsComponent implements OnInit {
  achievementId: string;
  achievementDetails: Achievement;
  constructor(private router: Router, private route: ActivatedRoute, private apiService: ApiService, private achievementsService: AchievementsService, public dialog: MatDialog) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe((params: ParamMap) => {
      this.achievementId = params.get('id');
      this.getAchievementDetails();
    })
  }

  goBack(): void {
    this.router.navigate([".."], { relativeTo: this.route });
  }


  getAchievementDetails(): void {
    this.apiService.get(`/Achievement/${+this.achievementId}`).subscribe((resp: any) => {
      this.achievementDetails = resp.result;
    })
  }

  openRemoveAchievementDialog(): void {
    this.achievementsService.remove(this.achievementId).subscribe((resp: any) => {
      this.goBack();
    })
  }

  getTier() {
    return Array(parseInt(this.achievementDetails.tier ? this.achievementDetails.tier : '1')).fill(0).map((x, i)=>i);
  }

  openAssignAchievementDialog(){
    const dialogRef = this.dialog.open(AssignAchievementModalComponent, {
      // width: '250px',
      data: {name: "Assign achievement"},
    });

    dialogRef.afterClosed().subscribe(result => {
      // this.getAchievementDetails();
    });
  }

}
