import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";

import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { LoginComponent } from "./components/login/login.component";
import { RegistrationComponent } from "./components/registration/registration.component";
import { ToolbarComponent } from "./components/toolbars/toolbar/toolbar.component";
import { ToolbarAfterLogInComponent } from "./components/toolbars/toolbar-after-log-in/toolbar-after-log-in.component";

import { MatMenuModule } from "@angular/material/menu";
import { MatCardModule } from "@angular/material/card";
import { MatFormFieldModule } from "@angular/material/form-field";
import {
  MatDialogModule,
  MatInputModule,
  MatButtonModule,
  MatToolbarModule,
  MatTableModule,
  MatDatepickerModule,
  MatNativeDateModule,
  MAT_DATE_LOCALE,
  MatOptionModule,
  MatSelectModule,
  MatSnackBarModule,
  MatIconModule,
  MatPaginatorModule,
  MatCheckboxModule,
  MatGridListModule
} from "@angular/material";
import { ReactiveFormsModule, FormsModule } from "@angular/forms";
import { ActivitiesTableComponent } from "./components/activities-table/activities-table.component";
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { ActivityFormComponent } from './components/activity-form/activity-form.component';
import { ScheduleComponent } from './components/schedule/schedule.component';
import { ProfileComponent } from './components/profile/profile.component';

import { JwtModule } from '@auth0/angular-jwt';
import { JwtInterceptor } from './helpers/jwt-iterceptor';
import { ErrorInterceptor } from './helpers/error-interceptor';
import { DeleteAccountComponent } from './components/delete-account/delete-account.component';
import { ScheduleSettingsComponent } from './components/schedule-settings/schedule-settings.component';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { MessagesComponent } from './components/messages/messages.component';
import { RecieveInvitationsComponent } from './components/recieve-invitations/recieve-invitations.component';
import { InviteUserComponent } from './components/invite-user/invite-user.component';
import { PageSizeSettingsComponent } from './components/page-size-settings/page-size-settings.component';
import { ActivityReviewComponent } from './components/activity-review/activity-review.component';

import { FullCalendarModule } from 'primeng/fullcalendar';
import { RefreshActivitiesComponent } from './components/refresh-activities/refresh-activities.component';
import { DatePipe } from '@angular/common';

export function tokenGetter() {
  return localStorage.getItem('token');
}

@NgModule({
  declarations: [
    AppComponent,
    ToolbarComponent,
    ToolbarAfterLogInComponent,
    LoginComponent,
    RegistrationComponent,
    ActivitiesTableComponent,
    ActivityFormComponent,
    ScheduleComponent,
    ProfileComponent,
    DeleteAccountComponent,
    ScheduleSettingsComponent,
    MessagesComponent,
    RecieveInvitationsComponent,
    InviteUserComponent,
    PageSizeSettingsComponent,
    ActivityReviewComponent,
    RefreshActivitiesComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatMenuModule,
    MatCardModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    FormsModule,
    MatDialogModule,
    MatInputModule,
    MatButtonModule,
    MatCardModule,
    MatToolbarModule,
    AppRoutingModule,
    MatTableModule,
    HttpClientModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatOptionModule,
    MatSelectModule,
    MatSnackBarModule,
    MatIconModule,
    DragDropModule,
    MatMenuModule,
    MatPaginatorModule,
    FullCalendarModule,
    MatCheckboxModule,
    MatGridListModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        whitelistedDomains: ['http://localhost:4200'],
        blacklistedRoutes: ['example.com/examplebadroute/']
      }
    })
  ],
  entryComponents: [
    LoginComponent,
    RegistrationComponent,
    ActivityFormComponent,
    InviteUserComponent,
    ActivityReviewComponent
  ],
  providers: [
    { provide: MAT_DATE_LOCALE, useValue: 'en-gb' },
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    [DatePipe]
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
