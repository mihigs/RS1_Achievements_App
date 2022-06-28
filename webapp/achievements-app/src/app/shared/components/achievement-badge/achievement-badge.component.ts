import { Component, Input, OnInit } from '@angular/core';
import { Achievement } from 'src/app/interfaces/Achievements/Achievement';

@Component({
  selector: 'app-achievement-badge',
  templateUrl: './achievement-badge.component.html',
  styleUrls: ['./achievement-badge.component.css']
})
export class AchievementBadgeComponent implements OnInit {
  @Input() achievement: Achievement;

  constructor() { }

  ngOnInit(): void {
  }

}
