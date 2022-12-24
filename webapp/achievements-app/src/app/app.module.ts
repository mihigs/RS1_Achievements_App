import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';

import {AppComponent} from './app.component';
import {AppRoutingModule} from './app-routing.module';
import {LoginComponent} from './components/login/login.component';
import {NotFoundComponent} from './components/not-found/not-found.component';
import {DashboardComponent} from './components/dashboard/dashboard.component';
import {HTTP_INTERCEPTORS, HttpClient, HttpClientModule} from '@angular/common/http';
import {TranslateHttpLoader} from '@ngx-translate/http-loader';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {ApiService} from './services/api.service';
import {AuthGuard} from './auth.guard';
import {CustomHttpInterceptorService} from './services/custom-http-interceptor.service';
import {TranslateLoader, TranslateModule} from '@ngx-translate/core';
import {RegistrationComponent} from './components/registration/registration.component';

import {MatGridListModule} from '@angular/material/grid-list';
import {MatButtonModule} from '@angular/material/button';
import {MatIconModule} from '@angular/material/icon';
import { AchievementsComponent } from './components/achievements/achievements.component';
import { TeamsComponent } from './components/teams/teams.component';
import { EventsComponent } from './components/events/events.component';
import { MembersComponent } from './components/members/members.component';
import { CreateTeamModalComponent } from './components/teams/create-team-modal/create-team-modal.component';
import { MatDialogModule } from '@angular/material/dialog';
import {MatFormFieldModule } from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import {MatCardModule} from '@angular/material/card';
import { TeamDetailsComponent } from './components/teams/team-details/team-details.component';
import { AchievementDetailsComponent } from './components/achievements/achievement-details/achievement-details.component';
import { AchievementBadgeComponent } from './shared/components/achievement-badge/achievement-badge.component';
import { CreateAchievementModalComponent } from './components/achievements/create-achievement-modal/create-achievement-modal.component';
import {MatSelectModule} from '@angular/material/select';
import { CreateEventModalComponent } from './components/events/create-event-modal/create-event-modal.component';
import { AssignAchievementModalComponent } from './components/achievements/assign-achievement-modal/assign-achievement-modal.component';
import { NotifierModule } from 'angular-notifier';

export const createTranslateLoader = (http: HttpClient) => {
    return new TranslateHttpLoader(http, './assets/i18/', '.json');
};

@NgModule({
    declarations: [
        AppComponent,
        RegistrationComponent,
        LoginComponent,
        NotFoundComponent,
        DashboardComponent,
        AchievementsComponent,
        TeamsComponent,
        EventsComponent,
        MembersComponent,
        CreateTeamModalComponent,
        TeamDetailsComponent,
        AchievementBadgeComponent,
        CreateAchievementModalComponent,
        CreateEventModalComponent,
        AchievementDetailsComponent,
        AssignAchievementModalComponent,
    ],
    imports: [
        BrowserModule,
        BrowserAnimationsModule,
        AppRoutingModule,
        HttpClientModule,
        ReactiveFormsModule,
        FormsModule,
        TranslateModule.forRoot({
            loader: {
                provide: TranslateLoader,
                useFactory: createTranslateLoader,
                deps: [HttpClient]
            }
        }),
        MatGridListModule,
        MatButtonModule,
        MatIconModule,
        MatDialogModule,
        MatFormFieldModule,
        MatInputModule,
        MatCardModule,
        MatSelectModule,
        NotifierModule.withConfig({
            position: {
                horizontal: {
                    position: 'middle'
                }
            },
            behaviour: {
                autoHide: 5000
            }
        }),
    ],
    exports: [
        MatFormFieldModule, 
        MatInputModule,
        MatSelectModule,
    ],
    providers: [ApiService, AuthGuard, {
        provide: HTTP_INTERCEPTORS,
        useClass: CustomHttpInterceptorService,
        multi: true
    }],
    bootstrap: [AppComponent]
})
export class AppModule {
}
