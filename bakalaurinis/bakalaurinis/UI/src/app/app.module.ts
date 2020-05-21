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
  MatGridListModule,
  MatSortModule,
  MatTabsModule,
  MatButtonToggleModule,
  MatExpansionModule,
  MatProgressBarModule,
  MatTooltipModule,
  MatListModule
} from "@angular/material";
import { ReactiveFormsModule, FormsModule } from "@angular/forms";
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { ScheduleComponent } from './components/schedule/schedule.component';
import { ProfileComponent } from './components/profile/profile.component';
import { JwtModule } from '@auth0/angular-jwt';
import { JwtInterceptor } from './helpers/jwt-iterceptor';
import { ErrorInterceptor } from './helpers/error-interceptor';
import { DeleteAccountComponent } from './components/delete-account/delete-account.component';
import { ScheduleSettingsComponent } from './components/schedule-settings/schedule-settings.component';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { MessagesComponent } from './components/messages/messages.component';
import { InviteUserComponent } from './components/invite-user/invite-user.component';
import { PageSizeSettingsComponent } from './components/page-size-settings/page-size-settings.component';
import { DatePipe } from '@angular/common';
import { ScheduleInfoComponent } from './components/schedule-info/schedule-info.component';
import { WorkFormComponent } from './components/work-form/work-form.component';
import { WorksTableComponent } from './components/works-table/works-table.component';
import { WorkReviewComponent } from './components/work-review/work-review.component';
import { RefreshScheduleComponent } from './components/refresh-schedule/refresh-schedule.component';
import { ReceiveInvitationsComponent } from './components/receive-invitations/receive-invitations.component';

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
    WorksTableComponent,
    WorkFormComponent,
    ScheduleComponent,
    ProfileComponent,
    DeleteAccountComponent,
    ScheduleSettingsComponent,
    MessagesComponent,
    ReceiveInvitationsComponent,
    InviteUserComponent,
    PageSizeSettingsComponent,
    WorkReviewComponent,
    RefreshScheduleComponent,
    ScheduleInfoComponent,
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
    MatSortModule,
    MatPaginatorModule,
    MatCheckboxModule,
    MatGridListModule,
    MatTabsModule,
    MatButtonToggleModule,
    MatExpansionModule,
    MatProgressBarModule,
    MatTooltipModule,
    MatListModule,
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
    WorkFormComponent,
    InviteUserComponent,
    WorkReviewComponent,
    ScheduleInfoComponent
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
