import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { ActivitiesTableComponent } from "./components/activities-table/activities-table.component";
import { ScheduleComponent } from "./components/schedule/schedule.component";
import { ProfileComponent } from "./components/profile/profile.component";
import { SettingsComponent } from "./components/settings/settings.component";

const routes: Routes = [
  { path: "activities", component: ActivitiesTableComponent },
  { path: "schedule", component: ScheduleComponent },
  { path: "profile", component: ProfileComponent },
  { path: "settings", component: SettingsComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
