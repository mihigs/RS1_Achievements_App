import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {LoginComponent} from './components/login/login.component';
import {DashboardComponent} from './components/dashboard/dashboard.component';
import {AchievementsComponent} from './components/achievements/achievements.component';
import {TeamsComponent} from './components/teams/teams.component';
import {EventsComponent} from './components/events/events.component';
import {MembersComponent} from './components/members/members.component';
import {NotFoundComponent} from './components/not-found/not-found.component';
import {AuthGuard} from './auth.guard';
import {RegistrationComponent} from './components/registration/registration.component';

const routes: Routes = [
    {path: '', component: LoginComponent},
    {path: 'register', component: RegistrationComponent},
    {path: 'login', component: LoginComponent},
    {path: 'dashboard', component: DashboardComponent, canActivate: [AuthGuard]},
    {path: 'achievements', component: AchievementsComponent, canActivate: [AuthGuard]},
    {path: 'teams', component: TeamsComponent, canActivate: [AuthGuard]},
    {path: 'events', component: EventsComponent, canActivate: [AuthGuard]},
    {path: 'members', component: MembersComponent, canActivate: [AuthGuard]},
    {path: '**', component: NotFoundComponent}
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule {
}
