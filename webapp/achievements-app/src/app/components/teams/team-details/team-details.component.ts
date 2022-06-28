import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { Team } from 'src/app/interfaces/Teams/Team';
import { ApiService } from 'src/app/services/api.service';

@Component({
  selector: 'app-team-details',
  templateUrl: './team-details.component.html',
  styleUrls: ['./team-details.component.css']
})
export class TeamDetailsComponent implements OnInit {
  teamId: string;
  teamDetails: Team;
  constructor(private router: Router, private route: ActivatedRoute, private apiService: ApiService) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe((params: ParamMap) => {
      this.teamId = params.get('id');
      this.getTeamDetails();
    })
  }

  goBack(): void {
    this.router.navigate([".."], { relativeTo: this.route });
  }


  getTeamDetails(): void {
    this.apiService.get(`/Team/${+this.teamId}`).subscribe((resp: any) => {
      this.teamDetails = resp.result;
    })
  }

}
