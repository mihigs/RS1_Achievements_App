import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Achievement } from 'src/app/interfaces/Teams/Achievement';
import { AchievementsService } from 'src/app/services/achievements.service';

@Component({
  selector: 'app-achievements',
  templateUrl: './achievements.component.html',
  styleUrls: ['./achievements.component.css']
})
export class AchievementsComponent implements OnInit {
  achievementsData: Achievement[];
  
  constructor(private router: Router, private achievementsService: AchievementsService) { }

  ngOnInit(): void {
    this.achievementsService.getAll().subscribe((resp: any) => {
      this.achievementsData = resp.result;
    })
  }

  goBack(): void {
    this.router.navigate([".."]);
  }

}
