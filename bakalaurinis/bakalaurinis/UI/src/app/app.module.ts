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
import { MatFormFieldModule, MatError } from "@angular/material/form-field";
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
  MatIconModule
} from "@angular/material";
import { ReactiveFormsModule, FormsModule } from "@angular/forms";
import { ActivitiesTableComponent } from "./components/activities-table/activities-table.component";
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { ActivityFormComponent } from './components/activity-form/activity-form.component';
import { ScheduleComponent } from './components/schedule/schedule.component';
import { ProfileComponent } from './components/profile/profile.component';
import { SettingsComponent } from './components/settings/settings.component';

import { JwtModule } from '@auth0/angular-jwt';
import { JwtInterceptor } from './helpers/jwt-iterceptor';
import { ErrorInterceptor } from './helpers/error-interceptor';
import { DeleteAccountComponent } from './components/delete-account/delete-account.component';
import { ChangePasswordComponent } from './components/change-password/change-password.component';
import { ScheduleSettingsComponent } from './components/schedule-settings/schedule-settings.component';

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
    SettingsComponent,
    DeleteAccountComponent,
    ChangePasswordComponent,
    ScheduleSettingsComponent
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
    ActivityFormComponent
  ],
  providers: [
    { provide: MAT_DATE_LOCALE, useValue: 'en-gb' },
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
