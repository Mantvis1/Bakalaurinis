import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { ActivitiesTableComponent } from "./components/activities-table/activities-table.component";
import { ScheduleComponent } from "./components/schedule/schedule.component";
import { ProfileComponent } from "./components/profile/profile.component";
import { SettingsComponent } from "./components/settings/settings.component";
import { AuthGuard } from './helpers/auth-guard';

const routes: Routes = [
  { path: "activities", component: ActivitiesTableComponent, canActivate: [AuthGuard] },
  { path: "schedule", component: ScheduleComponent, canActivate: [AuthGuard] },
  { path: "profile", component: ProfileComponent, canActivate: [AuthGuard] },
  { path: "settings", component: SettingsComponent, canActivate: [AuthGuard] }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
