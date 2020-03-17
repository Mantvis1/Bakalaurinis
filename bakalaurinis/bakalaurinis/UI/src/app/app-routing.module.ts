import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { ActivitiesTableComponent } from "./components/activities-table/activities-table.component";
import { ScheduleComponent } from "./components/schedule/schedule.component";
import { ProfileComponent } from "./components/profile/profile.component";
import { AuthGuard } from './helpers/auth-guard';
import { LoginComponent } from './components/login/login.component';
import { RegistrationComponent } from './components/registration/registration.component';
import { MessagesComponent } from './components/messages/messages.component';
import { RecieveInvitationsComponent } from './components/recieve-invitations/recieve-invitations.component';

const routes: Routes = [
  { path: "activities", component: ActivitiesTableComponent, canActivate: [AuthGuard] },
  { path: "schedule", component: ScheduleComponent, canActivate: [AuthGuard] },
  { path: "profile", component: ProfileComponent, canActivate: [AuthGuard] },
  { path: "messages", component: MessagesComponent, canActivate: [AuthGuard] },
  { path: "invitations", component: RecieveInvitationsComponent, canActivate: [AuthGuard] },
  { path: "login", component: LoginComponent },
  { path: "registration", component: RegistrationComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
